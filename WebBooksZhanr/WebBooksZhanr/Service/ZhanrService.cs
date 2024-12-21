using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiBib.Interfaces;
using WebApiBib.Model;
using WebBooksZhanr.DBContext;
namespace WebApiBib.Service
{
    public class ZhanrService : IZhanrServices
    {
        private readonly BooksZhanrDB _context;

        public ZhanrService(BooksZhanrDB context)
        {
            _context = context;
        }

        public async Task<IActionResult> DeleteZhanr(int Id)
        {
            var zhanr = await _context.Zhanr.FindAsync(Id);
            if (zhanr == null)//404
            {
                return new NotFoundResult();
            }

            _context.Zhanr.Remove(zhanr);
            await _context.SaveChangesAsync();

            return new OkObjectResult(new { MessageContent = "Жанр удален", status = true });
        }

        public async Task<IActionResult> GetAllZhanr()
        {
            var zhanr = await _context.Zhanr.ToListAsync();
            return new OkObjectResult(new
            {
                zhanr = zhanr,
                status = true
            });
        }

        public async Task<IActionResult> PostZhanr([FromBody] Zhanr newZhanr)
        {
            if (newZhanr == null)
            {
                return new BadRequestResult();
            }

            // Проверка на наличие обязательных полей
            if (string.IsNullOrWhiteSpace(newZhanr.Name))
            {
                return new BadRequestResult();
            }



            _context.Zhanr.Add(newZhanr);
            await _context.SaveChangesAsync();

            return new OkObjectResult(new { MessageContent = "Жанр добавлен", status = true });
        }

        public async Task<IActionResult> PutZhanr([FromBody] Zhanr UpdateZhanr)
        {
            var zhanr = await _context.Zhanr.FindAsync(UpdateZhanr.Id_Zhanr);
            if (zhanr == null) //404
            {
                return new NotFoundResult();
            }

            zhanr.Name = UpdateZhanr.Name;


            _context.Zhanr.Update(zhanr);
            await _context.SaveChangesAsync();


            return new OkObjectResult(new { MessageContent = "Жанр обновлен", status = true });
        }
    }
}

