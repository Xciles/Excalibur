using System.Threading.Tasks;
using Newtonsoft.Json;

// ReSharper disable once CheckNamespace
namespace System.Net.Http
{
    public static class HttpResponseMessageExtensions
    {
        public static JsonSerializerSettings JsonSerializerSettings = new JsonSerializerSettings { PreserveReferencesHandling = PreserveReferencesHandling.Objects };

        public static async Task<T> ConvertFromJsonResponse<T>(this HttpResponseMessage response)
        {
            var resultAsString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            return JsonConvert.DeserializeObject<T>(resultAsString, JsonSerializerSettings);
        }
    }
}