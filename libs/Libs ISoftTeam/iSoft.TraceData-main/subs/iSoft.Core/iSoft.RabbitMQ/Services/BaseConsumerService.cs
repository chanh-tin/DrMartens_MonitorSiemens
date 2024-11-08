using iSoft.Common.ConfigsNS;
using iSoft.Common.ExtensionMethods;
using iSoft.Common.Utils;
using iSoft.Common;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Microsoft.Extensions.Logging;
using iSoft.Common.Cached;
using iSoft.Common.Enums;
using iSoft.Redis.Services;
using iSoft.Common.MetricsNS;
using iSoft.Common.Payloads;

namespace iSoft.RabbitMQ.Services
{
    public class BaseConsumerService : BackgroundService
    {
        public ILogger _logger;
        public IModel _model;
        public IConnection _connection;
        public QueueProperties _queueProperties;
        public MemCached cached = new MemCached(5);
        public EnumConnectionStatus RabbitMQConnectionStatus = EnumConnectionStatus.None;
        public RabbitMQService _rabbitMQService;
        private DateTime _lastReceivedTime = DateTime.Now;
        private static readonly object _lastReceivedTimeLockObject = new object();
        private static readonly object _initLockObject = new object();
        public string GetClassName()
        {
            return this.GetType().Name;
        }
        public BaseConsumerService()
        {
        }

        public BaseConsumerService(ILogger<BaseConsumerService> logger,
            RabbitMQService rabbitMQService,
            string queueName)
        {
            _logger = logger;
            RabbitMQConnectionStatus = initRabbitMQ(rabbitMQService, queueName);
        }
        public virtual int GetRestartIntervalInSeconds()
        {
            return 3 * 60;
        }

        public EnumConnectionStatus initRabbitMQ(RabbitMQService rabbitMQService, string queueName)
        {
            try
            {
                try
                {
                    if (_model != null && _model.IsOpen)
                        _model.Close();
                    if (_connection != null && _connection.IsOpen)
                        _connection.Close();
                }
                catch (Exception ex) { }

                _logger.LogInformation($"*** TRY CONNECT RABBITMQ *** {CommonConfig.GetConfig().RabbitMQConfig.GetHostName()}");

                _rabbitMQService = rabbitMQService;
                _queueProperties = MessageQueueConfig.GetQueueProperties(queueName);

                _connection = rabbitMQService.CreateChannel();
                _connection.ConnectionShutdown += ConnectionShutdownHandler;
                _connection.CallbackException += ConnectionCallbackException;
                _model = _connection.CreateModel();

                _model.ExchangeDeclare(exchange: _queueProperties.ExchangeName, type: ExchangeType.Fanout, _queueProperties.Durable, false);
                _model.BasicQos(0, _queueProperties.RabbitPrefetchCount, false);

                rabbitMQService.ProcessOneProp(_model, _queueProperties);
                _logger.LogInformation("*** CONNECT RABBITMQ SUCCESS ***");
                return EnumConnectionStatus.Connected;
            }
            catch (Exception ex)
            {
                _logger.LogMsg(Messages.ErrException, ex);
                return EnumConnectionStatus.Error;
            }
        }

        private void ConnectionCallbackException(object? sender, CallbackExceptionEventArgs e)
        {
            _logger.LogInformation("ConnectionCallbackException()");

            RabbitMQConnectionStatus = EnumConnectionStatus.None;
            Init().Wait();
        }

        public async void ConnectionShutdownHandler(object sender, ShutdownEventArgs e)
        {
            _logger.LogInformation("ConnectionShutdownHandler()");

            if (e.Initiator == ShutdownInitiator.Application)
            {
                _logger.LogWarning("RabbitMQ connection closed by application.");
            }
            else
            {
                _logger.LogError("Connection closed unexpectedly. Initiator: {0}, Reason: {1}", e.Initiator, e.ReplyText);

                RabbitMQConnectionStatus = EnumConnectionStatus.None;
                await Init();
            }
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            RabbitMQConnectionStatus = EnumConnectionStatus.None;
            Task.Run(() => Init());

            Task.Run(() => CheckResetConnection());
        }

        public virtual async Task CheckResetConnection()
        {
            while (true)
            {
                this._logger.LogInformation("CheckResetConnection() - rabbitMQ");

                if (DateTimeUtil.CompareDateTime(_lastReceivedTime, DateTime.Now, 0, false) >= GetRestartIntervalInSeconds())
                {
                    this._logger.LogInformation($"CheckResetConnection start Init(), _lastReceivedTime: {_lastReceivedTime.GetDateTimeStr()}");

                    _lastReceivedTime = DateTime.Now;
                    RabbitMQConnectionStatus = EnumConnectionStatus.None;
                    await Init();
                }
                Thread.Sleep(60000);
            }
        }
        public virtual async Task Init()
        {
            while (true)
            {
                try
                {
                    lock (_initLockObject)
                    {
                        if (RabbitMQConnectionStatus != EnumConnectionStatus.Connected)
                        {
                            RabbitMQConnectionStatus = initRabbitMQ(_rabbitMQService, _queueProperties.QueueName);
                        }

                        if (RabbitMQConnectionStatus == EnumConnectionStatus.Connected)
                        {
                            ReadMessages(handleMessage).Wait();
                            _logger.LogInformation("Start ReadMessages() and stop loop connect rabbitMQ.");
                            break;
                        }
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogMsg(Messages.ErrException, ex);
                }
                Thread.Sleep(10000);
            }
        }
        public virtual async Task ReadMessages(Func<DeliveryObj, Task> handleMessageFunction)
        {
            var consumer = new AsyncEventingBasicConsumer(_model);
            consumer.Received += async (ch, e) =>
            {
                try
                {
                    await Task.CompletedTask;

                    DeliveryObj deliveryObj = new DeliveryObj()
                    {
                        DeliveryTag = e.DeliveryTag,
                        QueueName = _queueProperties.QueueName,
                        Exchange = _queueProperties.ExchangeName,
                        RoutingKey = _queueProperties.RoutingKey,
                        Data = e.Body.ToArray(),
                    };
                    deliveryObj.model = _model;

                    await handleMessageFunction(deliveryObj);

                    lock (_lastReceivedTimeLockObject)
                    {
                        _lastReceivedTime = DateTime.Now;
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogMsg(Messages.ErrException, ex);
                }
            };
            _model.BasicConsume(_queueProperties.QueueName, false, consumer);
            await Task.CompletedTask;
        }

        public virtual async Task handleMessage(DeliveryObj deliveryMessage)
        {
            try
            {
                this._logger.LogInformation("handleMessage Func Start");

                this.RemoveSuccessMessage(deliveryMessage);

                this._logger.LogInformation("Success, " + deliveryMessage.ToJson());
            }
            catch (Exception ex)
            {
                this._logger.LogMsg(Messages.ErrException.SetParameters(ex));
            }

        }

        public virtual void RetryMessage(DeliveryObj deliveryMessage)
        {
            try
            {
                string errMessage = "";
                string dataJson = "";
                var message = deliveryMessage.GetData<DevicePayloadMessage>(ref dataJson, ref errMessage);
                if (message == null)
                {
                    return;
                }
                _logger.LogWarning($"RetryMessage, {message?.MessageId}");

                string key = $"{this.GetClassName()}_{message.MessageId}";

                int retryCount = CachedSupportFunc.IsCanRetry(key, _queueProperties.TimeRetryInSeconds, _queueProperties.MaxRetryCount);
                if (retryCount >= 0)
                {
                    long expiredTimeInSeconds = _queueProperties.TimeRetryInSeconds;
                    if (retryCount >= 1)
                    {
                        expiredTimeInSeconds = (long)(expiredTimeInSeconds * Math.Pow(2, retryCount - 1));
                    }
                    _rabbitMQService.PushMessage(message, true, _queueProperties.GetRetryExchangeName(), _queueProperties.GetRetryName(), true, expiredTimeInSeconds);
                    GaugeMetrics.TrackMetricsReceiveMessage(deliveryMessage.QueueName, false, true);
                    _model.BasicAck(deliveryMessage.DeliveryTag, false);
                }
                else
                {
                    long expiredTimeInSeconds = (long)(1 * 24 * 3600);
                    _rabbitMQService.PushMessage(message, true, _queueProperties.GetRetryExchangeName(), _queueProperties.GetRetryName(), true, expiredTimeInSeconds);
                    GaugeMetrics.TrackMetricsReceiveMessage(deliveryMessage.QueueName, false, true, true);
                    _model.BasicAck(deliveryMessage.DeliveryTag, false);
                }
            }
            catch (Exception ex)
            {
                _logger.LogMsg(Messages.ErrException, ex);
            }
        }
        public virtual void RemoveSuccessMessage(DeliveryObj deliveryMessage)
        {
            try
            {
                _model.BasicAck(deliveryMessage.DeliveryTag, false);
                GaugeMetrics.TrackMetricsReceiveMessage(deliveryMessage.QueueName, true);

                string errMessage = "";
                string dataJson = "";
                var message = deliveryMessage.GetData<DevicePayloadMessage>(ref dataJson, ref errMessage);
                if (message != null)
                {
                    string key = $"{this.GetClassName()}_{message.MessageId}";
                    CachedSupportFunc.ClearRetryCached(key);
                }
            }
            catch (Exception ex)
            {
                _logger.LogMsg(Messages.ErrException, ex);
            }
        }
        public virtual void RemoveErrorMessage(DeliveryObj deliveryMessage)
        {
            try
            {
                _model.BasicAck(deliveryMessage.DeliveryTag, false);
                GaugeMetrics.TrackMetricsReceiveMessage(deliveryMessage.QueueName, false, false);

                string errMessage = "";
                string dataJson = "";
                var message = deliveryMessage.GetData<DevicePayloadMessage>(ref dataJson, ref errMessage);
                if (message != null)
                {
                    string key = $"{this.GetClassName()}_{message.MessageId}";
                    CachedSupportFunc.ClearRetryCached(key);
                }
            }
            catch (Exception ex)
            {
                _logger.LogMsg(Messages.ErrException, ex);
            }
        }
        public override void Dispose()
        {
            try
            {
                if (_model != null && _model.IsOpen)
                    _model.Close();
                if (_connection != null && _connection.IsOpen)
                    _connection.Close();

                cached.Dispose();

                base.Dispose();
            }
            catch (Exception ex)
            {
                _logger.LogMsg(Messages.ErrException, ex);
            }
        }
    }
}
