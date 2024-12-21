using Azure.Core;
using Azure.Messaging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using WebImage.DB;
using WebImage.Interfaces;
using WebImage.Model;


namespace WebImage.Services
{
    public class ImageServices : IImageServices
    {
        private readonly IWebHostEnvironment _environment;
        public ImageServices(IWebHostEnvironment environment)
        {
            _environment = environment;
        }
            

            

            public async Task<string> UploadAsync(IFormFile file)
            {
                if (file == null || file.Length == 0)
                  throw new ArgumentException("Файл не выбран");

                var uploadsPath = Path.Combine(_environment.WebRootPath ?? Directory.GetCurrentDirectory(), "uploads");

                if (!Directory.Exists(uploadsPath))
                    Directory.CreateDirectory(uploadsPath);

                var filePath = Path.Combine(uploadsPath, file.FileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                   await file.CopyToAsync(stream);
                }

                return filePath;
            }
            public async Task<string> Download(string fileName)
            {
                var uploadsPath = Path.Combine(_environment.WebRootPath ?? Directory.GetCurrentDirectory(), "uploads");
                var filePath = Path.Combine(uploadsPath, fileName);

                // Проверяем существование файла
                if (!System.IO.File.Exists(filePath))
                    throw new FileNotFoundException("Файл не найден");

                // Формируем URL файла
                var fileUrl = $"localhost:8888/uploads/{fileName}";

                // Читаем файл как массив байт
                var fileBytes = System.IO.File.ReadAllBytes(filePath);

                // Можно вернуть fileUrl, если это необходимо, через другой механизм, например лог или UI

                return fileUrl;
        }
            
            public async Task<bool> FileExists(string fileName)
            {
            var uploadsPath = Path.Combine(_environment.WebRootPath ?? Directory.GetCurrentDirectory(), "uploads");
            var filePath = Path.Combine(uploadsPath, fileName);
            return System.IO.File.Exists(filePath);
            }

        private string GetContentType(string fileExtension)
        {
            return fileExtension.ToLower() switch
            {
                ".jpg" or ".jpeg" => "image/jpeg",
                ".png" => "image/png",
                ".gif" => "image/gif",
                ".bmp" => "image/bmp",
                _ => "application/octet-stream", // Для неизвестных типов
            };
        }
    }
}

