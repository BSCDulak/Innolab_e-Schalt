using System.Net.Http.Headers;
using System.Text.Json;
using eSchalt.Backend.Models.AI;
using static System.Net.WebRequestMethods;

namespace eSchalt.Backend.HelperClasses

{
    public class AiClient
    {
        private readonly HttpClient _http;

        public AiClient(HttpClient http)
        {
            _http = http;
        }

        public async Task<AiPredictionResponseDto> PredictFromFileAsync(string absoluteImagePath)
        {
            if (!System.IO.File.Exists(absoluteImagePath))
                throw new FileNotFoundException("AI image file not found", absoluteImagePath);

            using var form = new MultipartFormDataContent();

            var bytes = await System.IO.File.ReadAllBytesAsync(absoluteImagePath);
            var fileContent = new ByteArrayContent(bytes);
            fileContent.Headers.ContentType =
                new MediaTypeHeaderValue("image/png");

            form.Add(fileContent, "file", Path.GetFileName(absoluteImagePath));

            var response = await _http.PostAsync("/predict", form);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();

            var result = JsonSerializer.Deserialize<AiPredictionResponseDto>(
                json,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
            );

            return result ?? new AiPredictionResponseDto();
        }

    }
}
