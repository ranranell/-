using Microsoft.AspNetCore.Mvc;
using WebImage.Interfaces;

namespace WebImage.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ImageController : Controller
    {
        private readonly IImageServices _imageServices;
        public ImageController(IImageServices imageServices)
        {
            _imageServices = imageServices;
        }

        [HttpGet]
        [Route("GetImage")]
        public async Task<string> Download(string fileName)
        {
            return await _imageServices.Download(fileName);
        }

        [HttpDelete]
        [Route("DeleteImage")]  
        public async Task<bool> FileExists(string fileName)
        {
           return await _imageServices.FileExists(fileName);
        }

        [HttpPost]
        [Route("AddImage")]
        public async Task<string> UploadAsync(IFormFile file)
        {
            return await _imageServices.UploadAsync(file);
        }
    }
}
