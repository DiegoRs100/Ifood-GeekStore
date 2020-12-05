using GeekStore.Core.Extentions.Exceptions;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace GeekStore.Core.Extentions
{
    public static class HttpExtentions
    {
        public static StringContent ToStringContent(this object obj)
        {
            return new StringContent(JsonSerializer.Serialize(obj), Encoding.UTF8, "application/json");
        }

        public static bool IsValid(this HttpResponseMessage response)
        {
            switch ((HttpStatusCode)response.StatusCode.ToInt())
            {
                case HttpStatusCode.Unauthorized:
                case HttpStatusCode.Forbidden:
                case HttpStatusCode.NotFound:
                case HttpStatusCode.InternalServerError:
                    throw new CustomHttpRequestException(response.StatusCode);

                case HttpStatusCode.BadRequest:
                    return false;
            }

            response.EnsureSuccessStatusCode();
            return true;
        }

        public static async Task<T> ReadAsync<T>(this HttpResponseMessage responseMessage)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            return JsonSerializer.Deserialize<T>(await responseMessage.Content.ReadAsStringAsync(), options);
        }
    }
}