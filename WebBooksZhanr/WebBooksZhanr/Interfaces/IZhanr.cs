using Microsoft.AspNetCore.Mvc;
using WebApiBib.Model;

namespace WebApiBib.Interfaces
{
    public interface IZhanrServices
    {
        Task<IActionResult> GetAllZhanr();
        Task<IActionResult> PostZhanr([FromBody] Zhanr newZhanr);
        Task<IActionResult> PutZhanr([FromBody] Zhanr UpdateZhanr);
        Task<IActionResult> DeleteZhanr(int Id);

    }
}
