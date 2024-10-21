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

                return new ResponseWrapper<T>
                {
                    IsSuccessful = false,
                    Messages = new List<string> { $"Error: {responseMessage.StatusCode} - {errorContent}" },
                    Data = default
                };
            }

            var responseAsString = await responseMessage.Content.ReadAsStringAsync();

            try
            {
                var responseObject = JsonSerializer.Deserialize<ResponseWrapper<T>>(responseAsString, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                    ReferenceHandler = ReferenceHandler.Preserve
                });

                if (responseObject != null)
                {
                    return responseObject;
                }
                else
                {
                    return new ResponseWrapper<T>
                    {
                        IsSuccessful = false,
                        Messages = new List<string> { "Deserialization returned null." },
                        Data = default
                    };
                }
            }
            catch (JsonException ex)
            {
                return new ResponseWrapper<T>
                {
                    IsSuccessful = false,
                    Messages = new List<string> { $"Failed to deserialize response: {ex.Message}" },
                    Data = default
                };
            }
        }
    }
}
