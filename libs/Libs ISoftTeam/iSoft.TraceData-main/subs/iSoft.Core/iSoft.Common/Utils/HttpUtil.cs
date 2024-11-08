using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System;
using System.Threading.Tasks;
using iSoft.Common.ExtensionMethods;
using System.Net.Http.Json;
using Newtonsoft.Json;
using System.Collections;
using System.Net.Sockets;

namespace iSoft.Common.Utils
{
    public static class HttpUtil
    {
        public static async Task<T> GetData<T>(string urlAPI, string username, string pass, string authMethod = "Basic", int timeoutInSeconds = 3000) where T : class
        {
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
            using (HttpClient client = new HttpClient(clientHandler))
            {
                var byteArray = Encoding.ASCII.GetBytes($"{username}:{pass}");
                clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(authMethod, Convert.ToBase64String(byteArray));
                client.Timeout = TimeSpan.FromSeconds(timeoutInSeconds);
                HttpResponseMessage response = await client.GetAsync(urlAPI);
                response.EnsureSuccessStatusCode();
                var json = await response.Content.ReadAsStringAsync();
                return JsonExtensionUtil.FromJson<T>(json);

            }
        }
        public static async Task<T> GetData<T>(string urlAPI, byte[] authData, string authMethod = "Bearer", int timeoutInSeconds = 3000) where T : class
        {
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
            using (HttpClient client = new HttpClient(clientHandler))
            {
                clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(authMethod, Convert.ToBase64String(authData));
                client.Timeout = TimeSpan.FromSeconds(timeoutInSeconds);
                HttpResponseMessage response = await client.GetAsync(urlAPI);
                response.EnsureSuccessStatusCode();
                var json = await response.Content.ReadAsStringAsync();
                return JsonExtensionUtil.FromJson<T>(json);

            }
        }
        public static async Task<T> PostData<T>(string urlAPI, string username, string pass, string jsonContent, int timeoutInSeconds = 3000) where T : class
        {
            HttpClientHandler handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true; // Bỏ qua chứng chỉ SSL

            using (HttpClient client = new HttpClient(handler))
            {
                var byteArray = Encoding.ASCII.GetBytes($"{username}:{pass}");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
                StringContent content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                client.Timeout = TimeSpan.FromSeconds(timeoutInSeconds);
                HttpResponseMessage response = await client.PostAsync(urlAPI, content);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                return JsonExtensionUtil.FromJson<T>(responseBody);
            }
        }
        public static async Task<T> PostData<T>(string urlAPI, string jsonContent, byte[] authData, string authMethod = "Bearer", int timeoutInSeconds = 3000) where T : class
        {
            HttpClientHandler handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true; // Bỏ qua chứng chỉ SSL

            using (HttpClient client = new HttpClient(handler))
            {
                //var byteArray = Encoding.ASCII.GetBytes($"{username}:{pass}");
                //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(authMethod, Encoding.ASCII.GetString(authData));
                StringContent content = new StringContent(jsonContent, Encoding.ASCII, "application/json");
                client.Timeout = TimeSpan.FromSeconds(timeoutInSeconds);
                HttpResponseMessage response = await client.PostAsync(urlAPI, content);
                var responseBody = await response.Content.ReadAsByteArrayAsync();
                var ret = Encoding.ASCII.GetString(responseBody);
                return JsonExtensionUtil.FromJson<T>(ret);
            }
        }
        public static async Task<T> PutData<T>(string urlAPI, string username, string pass, string jsonContent, int timeoutInSeconds = 3000) where T : class
        {
            using (HttpClient client = new HttpClient())
            {
                var byteArray = Encoding.ASCII.GetBytes($"{username}:{pass}");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));

                StringContent content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                client.Timeout = TimeSpan.FromSeconds(timeoutInSeconds);
                HttpResponseMessage response = await client.PutAsync(urlAPI, content);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                return JsonExtensionUtil.FromJson<T>(responseBody);
            }
        }
        public static async Task<T> DeleteData<T>(string urlAPI, string username, string pass, int timeoutInSeconds = 3000) where T : class
        {
            using (HttpClient client = new HttpClient())
            {
                var byteArray = Encoding.ASCII.GetBytes($"{username}:{pass}");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
                client.Timeout = TimeSpan.FromSeconds(timeoutInSeconds);
                HttpResponseMessage response = await client.DeleteAsync(urlAPI);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                return JsonExtensionUtil.FromJson<T>(responseBody);
            }
        }
        public static bool PingHost(string hostUri, int portNumber)
        {
            try
            {
                using (var client = new TcpClient(hostUri, portNumber))
                    return true;
            }
            catch (SocketException ex)
            {
                return false;
            }
        }
    }
}
