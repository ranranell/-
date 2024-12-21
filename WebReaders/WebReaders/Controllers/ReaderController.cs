using Azure.Messaging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebReaders.DB;
using WebReaders.Interfaces;
using WebReaders.Model;
using static System.Reflection.Metadata.BlobBuilder;
namespace WebReaders.Controllers
{   
    [ApiController]
    [Route("api/[controller]")]
    public class ReaderController : Controller
    {
        private readonly IReaderServices _readerService;

        public ReaderController(IReaderServices readerService)
        {
            _readerService = readerService;
        }


        [HttpGet]
        [Route("getAllReaders")]
        public async Task<IActionResult> GetAllReaders()
        {
            return await _readerService.GetAllReaders();
        }

        [HttpGet]
        [Route("getReader/{Id}")]
        public async Task<IActionResult> GetReaderId(int Id)
        {
            return await _readerService.GetReaderId(Id);
        }

        [HttpPost]
        [Route("ADDReader")]
        public async Task<IActionResult> PostReader([FromBody] Readers newReader)
        {
            return await _readerService.PostReader(newReader);

        }

        [HttpPut]
        [Route("UpdateReader")]
        public async Task<IActionResult> PutReader([FromBody] Readers UpdateReader)
        {
            return await _readerService.PutReader(UpdateReader);
        }

        [HttpDelete]
        [Route("DeleteReader/{Id}")]
        public async Task<IActionResult> DeleteReader(int Id)
        {
            return await _readerService.DeleteReader(Id);
        }

        [HttpGet]
        [Route("Readers/Pagination")]
        public async Task<IActionResult> GetReaderPagination([FromQuery] string? Name, [FromQuery] string? FName, [FromQuery] string? Contact, [FromQuery] DateTime? Birth_Day, [FromQuery] int page = 1, [FromQuery] int pageSize = 2)
        {
            return await _readerService.GetReaderPagination(Name, FName, Contact, Birth_Day);
        }

        [HttpGet]
        [Route("/Readers/Filter")]
        public async Task<IActionResult> GetReadersFilter([FromQuery] DateTime? DateRegist)
        {
            return await _readerService.GetReadersFilter(DateRegist);
        }

        
    }
}
