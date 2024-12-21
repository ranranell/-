using Azure.Messaging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebBooksZhanr.DBContext;
using WebApiBib.Interfaces;
using WebApiBib.Model;
using WebApiBib.Service;
using static System.Reflection.Metadata.BlobBuilder;
namespace WebApiBib.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : Controller
    {
        private readonly IBook _bookServices;


        public BooksController(IBook bookServices)
        {
            _bookServices = bookServices;
        }
        [HttpGet]
        [Route("getAllBooks")]
        public async Task<IActionResult> GetAllBooks()
        {
            return await _bookServices.GetAllBooks();
        }

        [HttpDelete]
        [Route("DeleteBook/{Id}")]
        public async Task<IActionResult> DeleteBook(int Id)
        {
            return await _bookServices.DeleteBook(Id);
        }

        [HttpPut]
        [Route("UpdateBook")]
        public async Task<IActionResult> PutBook([FromBody] Books UpdateBook)
        {
            return await _bookServices.PutBook(UpdateBook);
        }

        [HttpPost]
        [Route("ADDBook")]
        public async Task<IActionResult> PostBook(int Id, Books newBook)
        {
            return await _bookServices.PostBook(Id, newBook);
        }

        [HttpGet]
        [Route("getBook/{Id}")]
        public async Task<IActionResult> GetBookId(int Id)
        {
            return await _bookServices.GetBookId(Id);
        }

        [HttpGet]
        [Route("getBookZhanr/{Id_Zhanr}")]
        public async Task<IActionResult> GetBookId_Zhanr(int Id_Zhanr)
        {
            return await _bookServices.GetBookId_Zhanr(Id_Zhanr);
        }

        [HttpGet]
        [Route("getBookAuthor_Name/{Author}/{Name}")]
        public async Task<IActionResult> GetBookAuhtor_Name(string Author, string Name)
        {
            return await _bookServices.GetBookAuhtor_Name(Author, Name);
        }

        [HttpGet]
        [Route("api/books")]
        public async Task<IActionResult> GetBooks([FromQuery] string? Name, [FromQuery] string? author, [FromQuery] int? zhanr, [FromQuery] DateTime? year)
        {
            return await _bookServices.GetBooks(Name, author, zhanr, year);
        }

        [HttpGet]
        [Route("api/books/pagination")]
        public async Task<IActionResult> GetBooksPagination(
            [FromQuery] string? Name,
            [FromQuery] string? author,
            [FromQuery] int? Zhanr,
            [FromQuery] DateTime? year,
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 2)
        {
            return await _bookServices.GetBooksPagination(Name, author, Zhanr, year);
        }

        [HttpPost]
        [Route("GetBooksByIds")]
        public async Task<IActionResult> GetBooksByIds([FromBody] List<int> bookIds)
        {
            return await _bookServices.GetBooksByIds(bookIds);
        }


    }
}
