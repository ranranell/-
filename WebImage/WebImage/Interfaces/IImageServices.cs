using Microsoft.AspNetCore.Mvc;
using WebImage.Model;

namespace WebImage.Interfaces
{
    public interface IImageServices
    {
        public Task<string> UploadAsync(IFormFile file);
        public Task<string> Download(string fileName);
        public Task<bool> FileExists(string fileName);

    }
}
