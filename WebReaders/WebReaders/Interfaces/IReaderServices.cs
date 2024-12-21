using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebReaders.Model;


namespace WebReaders.Interfaces
{
    public interface IReaderServices
    {
        Task<IActionResult> GetAllReaders();
        Task<IActionResult> GetReaderId(int Id);
        Task<IActionResult> PostReader([FromBody] Readers newReader);
        Task<IActionResult> PutReader([FromBody] Readers UpdateReader);
        Task<IActionResult> DeleteReader(int Id);
        Task<IActionResult> GetReaderPagination(
            [FromQuery] string? Name,
            [FromQuery] string? FName,
            [FromQuery] string? Contact,
            [FromQuery] DateTime? Birth_Day,
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 2);
        [HttpGet("api/readers")]
        Task<IActionResult> GetReadersFilter([FromQuery] DateTime? DateRegist);
        

    }
}
