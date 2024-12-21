using Microsoft.AspNetCore.Mvc;
using WebApiBib.Interfaces;
using WebApiBib.Model;
namespace WebApiBib.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ZhanrController : Controller
    {
        private readonly IZhanrServices _zhanrService;

        public ZhanrController(IZhanrServices zhanrService)
        {
            _zhanrService = zhanrService;

        }

        [HttpGet]
        [Route("getAllZhanr")] //РАБОТАЕТ
        public async Task<IActionResult> GetAllZhanr()
        {
            return await _zhanrService.GetAllZhanr();
        }

        [HttpPost]
        [Route("ADDZhanr")] //РАБОТАЕТ
        public async Task<IActionResult> PostZhanr([FromBody] Zhanr newZhanr)
        {
            return await _zhanrService.PostZhanr(newZhanr);

        }

        [HttpPut]
        [Route("UpdateZhanr")] //РАБОТАЕТ
        public async Task<IActionResult> PutZhanr([FromBody] Zhanr UpdateZhanr)
        {
            return await _zhanrService.PutZhanr(UpdateZhanr);
        }

        [HttpDelete]
        [Route("DeleteZhanr/{Id}")] //РАБОТАЕТ
        public async Task<IActionResult> DeleteZhanr(int Id)
        {
            return await _zhanrService.DeleteZhanr(Id);
        }

    }
}
