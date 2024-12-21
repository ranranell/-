using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using WebApiBib.Model;

namespace WebApiBib.Interfaces
{
    public interface IBook
    {
        Task<IActionResult> GetAllBooks();
        Task<IActionResult> DeleteBook(int Id);
        Task<IActionResult> PutBook([FromBody] Books UpdateBook);
        Task<IActionResult> PostBook(int Id, Books newBook);
        Task<IActionResult> GetBookId(int Id);
        Task<IActionResult> GetBookId_Zhanr(int Id_Zhanr);
        Task<IActionResult> GetBookAuhtor_Name(string Author, string Name);
        Task<IActionResult> GetBooks([FromQuery] string? Name, [FromQuery] string? author, [FromQuery] int? zhanr, [FromQuery] DateTime? year);
        Task<IActionResult> GetBooksPagination(
            [FromQuery] string? Name,
            [FromQuery] string? author,
            [FromQuery] int? Zhanr,
            [FromQuery] DateTime? year,
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 2);
        Task<IActionResult> GetBooksByIds([FromBody] List<int> bookIds);
    }
}
