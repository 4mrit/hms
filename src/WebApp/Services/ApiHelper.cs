using System.Text.Json;


namespace WebApp.Services
{
    public class ApiHelper
    {
        private readonly HttpClient _httpClient;

        public ApiHelper(IHttpClientFactory httpClient, string clientName)
        {

            _httpClient = httpClient.CreateClient(clientName);

        }

        public async Task<T> GetRequest<T>(string apiPath)
        {
            T response;
            response = await _httpClient.GetFromJsonAsync<T>(apiPath) ;
            return response;

        }

        //public async Task<T> PostRequest<T>(string apiPath, T data)
        //{
        //    T obj;

        //    var response = await _httpClient.PostAsJsonAsync(apiPath, data);
        //    Console.WriteLine(response);
        //    if (response.StatusCode == System.Net.HttpStatusCode.OK)
        //    {
        //        var options = new JsonSerializerOptions
        //        {
        //            PropertyNameCaseInsensitive = true
        //        };
        //        string apiResponse = await response.Content.ReadAsStringAsync();
        //        obj = JsonSerializer.Deserialize<T>(apiResponse, options);
        //        return obj;
        //    }
        //    else
        //    {
        //        obj = Activator.CreateInstance<T>();
        //        return obj;
        //    }
        //}
        public async Task<HttpResponseMessage> PostRequest <T>(string apiPath, T data)
        {
            //T obj;

            var response = await _httpClient.PostAsJsonAsync(apiPath, data);
            Console.WriteLine(response);
            return response;
        }


        public async Task<T> PutRequest<T>(string apiPath, T data)
        {
            T obj;

            var response = await _httpClient.PutAsJsonAsync(apiPath, data);
            Console.WriteLine(response);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                string apiResponse = await response.Content.ReadAsStringAsync();
                obj = JsonSerializer.Deserialize<T>(apiResponse, options);
                return obj;
            }
            else
            {
                obj = Activator.CreateInstance<T>();
                return obj;
            }

        }

        public async Task<T> DeleteRequest<T>(string apiPath)
        {
            T obj;
            var response = await _httpClient.DeleteAsync(apiPath);
            Console.WriteLine(response);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                string apiResponse = await response.Content.ReadAsStringAsync();
                obj = JsonSerializer.Deserialize<T>(apiResponse, options);
                return obj;
            }
            else
            {
                obj = Activator.CreateInstance<T>();
                return obj;
            }
        }
    }
}
