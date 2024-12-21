using Azure.Messaging;
using Microsoft.AspNetCore.Mvc;

namespace GeneralAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ImageController : Controller
    {
        private readonly HttpClient _httpClient;
        public ImageController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
        }
        [HttpGet]
        [Route("GetImage")]
        public async Task<string> Download(string fileName)
        {
            try
            {
                // Формируем URL запроса к микросервису
                var requestUrl = $"http://localhost:8888/api/Image/GetImage";

                // Отправляем запрос в микросервис
                var response = await _httpClient.GetAsync(requestUrl);
                response.EnsureSuccessStatusCode();

                // Получаем содержимое файла
                var fileBytes = await response.Content.ReadAsByteArrayAsync();

                // Формируем локальный путь для сохранения файла
                var currentDirectory = Directory.GetCurrentDirectory();
                var uploadsPath = Path.Combine(currentDirectory, "uploads");
                if (!Directory.Exists(uploadsPath))
                {
                    Directory.CreateDirectory(uploadsPath);
                }

                var localFilePath = Path.Combine(uploadsPath, fileName);

                // Сохраняем файл на локальный диск
                await System.IO.File.WriteAllBytesAsync(localFilePath, fileBytes);

                // Формируем локальный URL для доступа к файлу
                var fileUrl = $"http://localhost:8888/uploads/{fileName}";

                return fileUrl;
            }
            catch (HttpRequestException ex)
            {
                throw new Exception($"Ошибка при запросе к микросервису: {ex.Message}");
            }
            catch (Exception ex)
            {
                throw new Exception($"Неожиданная ошибка: {ex.Message}");
            }

        }

        [HttpDelete]
        [Route("DeleteImage")]
        public async Task<bool> FileExists(string fileName)
        {
            try
            {
                // Отправляем запрос на проверку существования файла
                var response = await _httpClient.GetAsync($"http://localhost:8888/api/Image/FileExists/{fileName}");
                response.EnsureSuccessStatusCode();

                // Ожидаем ответ (true или false)
                var exists = await response.Content.ReadAsStringAsync();
                return bool.Parse(exists);
            }
            catch (HttpRequestException ex)
            {
                throw new Exception($"Ошибка при проверке файла: {ex.Message}");
            }
            catch (Exception ex)
            {
                throw new Exception($"Неожиданная ошибка: {ex.Message}");
            }
        }

        [HttpPost]
        [Route("AddImage")]
        public async Task<string> UploadAsync(IFormFile file)
        {
            try
            {
                if (file == null || file.Length == 0)
                    throw new ArgumentException("Файл не выбран или пуст.");

                using (var content = new MultipartFormDataContent())
                {
                    var fileStreamContent = new StreamContent(file.OpenReadStream())
                    {
                        Headers =
                {
                    ContentLength = file.Length,
                    ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(file.ContentType)
                }
                    };

                    content.Add(fileStreamContent, "file", file.FileName);

                    var response = await _httpClient.PostAsync("http://localhost:8888/api/Image/AddImage", content);

                    // Проверка статуса ответа
                    if (!response.IsSuccessStatusCode)
                    {
                        var errorMessage = await response.Content.ReadAsStringAsync();
                        throw new HttpRequestException($"Ошибка от микросервиса: {errorMessage}");
                    }

                    // Возвращаем путь к файлу
                    return await response.Content.ReadAsStringAsync();
                }
            }
            catch (HttpRequestException ex)
            {
                return $"Ошибка HTTP-запроса: {ex.Message}";
            }
            catch (Exception ex)
            {
                return $"Неожиданная ошибка: {ex.Message}";
            }
        }
    }
}
