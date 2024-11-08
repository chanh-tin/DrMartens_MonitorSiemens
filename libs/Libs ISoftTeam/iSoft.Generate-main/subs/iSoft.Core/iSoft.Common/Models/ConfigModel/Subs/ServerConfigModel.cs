using Microsoft.AspNetCore.DataProtection.KeyManagement;

namespace iSoft.Common.Models.ConfigModel.Subs
{
    public class ServerConfigModel
    {
        public string Address { get; set; }
        public int Port { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string APIKey { get; set; }
        public string GetHostName()
        {
            return $"{Address}:{Port}";
        }

        public ServerConfigModel(string address, int port, string username, string password)
        {
            Address = address;
            Port = port;
            Username = username;
            Password = password;
        }

        public ServerConfigModel(string address, int port, string apiKey)
        {
            Address = address;
            Port = port;
            APIKey = apiKey;
        }

        public override bool Equals(object? obj)
        {
            return obj != null &&
                   Address == ((ServerConfigModel)obj).Address &&
                   Port == ((ServerConfigModel)obj).Port &&
                   Username == ((ServerConfigModel)obj).Username &&
                   Password == ((ServerConfigModel)obj).Password &&
                   APIKey == ((ServerConfigModel)obj).APIKey;
        }

        public virtual object GetLogStr()
        {
            return $"Address: {Address}, Port: {Port}, ApiKey: {(APIKey != null && APIKey.Length >= 2 ? APIKey.Substring(0, 2) : "")}*****, Username: {Username}, Password: {(Password != null && Password.Length >= 2 ? Password.Substring(0, 2) : "")}*****";
        }

        public override string ToString()
        {
            return "Call ToString() error !!";
        }
    }
}