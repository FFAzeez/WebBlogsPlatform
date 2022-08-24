using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace WebBloggingPlatform.Utility
{
    public class SendHttpRequest : ISendHttpRequest
    {
        private readonly IConfiguration _configuration;
        public SendHttpRequest(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<T> SendAsync<T>()
        {
            try
            {
                var url = _configuration["blogUrl:uri"];

                using var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, url);
                using var client = new HttpClient();
                var response = await client.SendAsync(httpRequestMessage);

                var result = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(result);
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }

        }
    }
}
