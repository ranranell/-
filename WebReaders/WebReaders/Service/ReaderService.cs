using Azure.Messaging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebReaders.DB;
using WebReaders.Interfaces;
using WebReaders.Model;

namespace WebReaders.Service
{
    public class ReaderService : IReaderServices
    {
        private readonly DBReaders _context;

        public ReaderService(DBReaders context)
        {
            _context = context;
        }
        public async Task<IActionResult> DeleteReader(int Id)
        {
            var readers = await _context.Readers.FindAsync(Id);
            if (readers == null)//404
            {
                return new NotFoundResult();
            }

            _context.Readers.Remove(readers);
            await _context.SaveChangesAsync();

            return new OkObjectResult(new
            {
                status = true,
                MessageContent = "Читатель удален"
            });
        }

        public async Task<IActionResult> GetAllReaders()
        {
            var readers = await _context.Readers.ToListAsync();
            return new OkObjectResult(new
            {
                readers = readers,
                status = true
            });
        }

        public async Task<IActionResult> GetReaderPagination([FromQuery] string? Name, [FromQuery] string? FName, [FromQuery] string? Contact, [FromQuery] DateTime? Birth_Day, [FromQuery] int page = 1, [FromQuery] int pageSize = 2)
        {
            var query = _context.Readers.AsQueryable();

            if (!string.IsNullOrWhiteSpace(Name))
                query = query.Where(b => b.Name.Contains(Name));

            if (!string.IsNullOrWhiteSpace(FName))
                query = query.Where(b => b.FName.Contains(FName));

            if (!string.IsNullOrWhiteSpace(Contact))
                query = query.Where(b => b.Contact.Contains(Contact));

            if (Birth_Day.HasValue)
                query = query.Where(b => b.Birth_Day == Birth_Day);

            var totalItems = await query.CountAsync();
            var books = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var result = new
            {
                Page = page,
                PageSize = pageSize,
                TotalItems = totalItems,
                Items = books
            };

            return new OkObjectResult(result);
        }

        public async Task<IActionResult> GetReaderId(int Id)
        {
            var reader = await _context.Readers.FirstOrDefaultAsync(b => b.Id_Reader == Id);
            if (reader == null)//404
            {
                return new NotFoundResult();
            }

            return new OkObjectResult(new { reader });
        }

        public async Task<IActionResult> PostReader([FromBody] Readers newReader)
        {
            if (newReader == null)
            {
                return new BadRequestResult();
            }

            // Проверка на наличие обязательных полей
            if (string.IsNullOrWhiteSpace(newReader.Name))
            {
                return new BadRequestResult();
            }



            if (string.IsNullOrWhiteSpace(newReader.FName))
            {
                return new BadRequestResult();
            }

            if (string.IsNullOrWhiteSpace(newReader.Contact))
            {
                return new BadRequestResult();
            }

            newReader.DateRegist = DateTime.Now;

            _context.Readers.Add(newReader);
            await _context.SaveChangesAsync();

            return new OkObjectResult(new { MessageContent = "Читатель добавлен", status = true });
        }

        public async Task<IActionResult> PutReader([FromBody] Readers UpdateReader)
        {
            var readers = await _context.Readers.FindAsync();
            if (readers == null) //404
            {
                return new NotFoundResult();
            }

            readers.Name = UpdateReader.Name;
            readers.FName = UpdateReader.FName;
            readers.Birth_Day = UpdateReader.Birth_Day;
            readers.Contact = UpdateReader.Contact;

            _context.Readers.Update(readers);
            await _context.SaveChangesAsync();


            return new OkObjectResult(new { MessageContent = "Данные обновлены", status = true });
        }

        public async Task<IActionResult> GetReadersFilter([FromQuery] DateTime? DateRegist)
        {
            var query = _context.Readers.AsQueryable();

            if (DateRegist.HasValue)
            {   
                var startDate = DateRegist.Value.Date;
                var endDate = startDate.AddDays(1);
                query = query.Where(r => r.DateRegist >= startDate && r.DateRegist < endDate);
            }

            var result = await query.ToListAsync();
            return new OkObjectResult(result);
        }
        private async Task<List<BookDto>> GetBooksByIds(List<int> bookIds)
        {
            using var client = new HttpClient();
            var response = await client.PostAsJsonAsync("http://localhost:9999/api/Books/GetBooksByIds", bookIds);

            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<List<BookDto>>() ?? new List<BookDto>();
        }

        
    }
}
