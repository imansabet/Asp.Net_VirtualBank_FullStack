using Common.Wrapper;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace BankUI.Extensions
{
    internal static class ResponseExtensions
    {
        internal static async Task<ResponseWrapper<T>> ToResponse<T>(this HttpResponseMessage responseMessage)
        {
            if (!responseMessage.IsSuccessStatusCode)
            {
                var errorContent = await responseMessage.Content.ReadAsStringAsync();
                throw new Exception($"Error: {responseMessage.StatusCode} - {errorContent}");
            }

            var responseAsString = await responseMessage.Content.ReadAsStringAsync();

            try
            {
                var responseObject = JsonSerializer.Deserialize<ResponseWrapper<T>>(responseAsString, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                    ReferenceHandler = ReferenceHandler.Preserve
                });

                return responseObject;
            }
            catch (JsonException ex)
            {
                throw new Exception("Failed to deserialize response", ex);
            }
        }
    }
}
