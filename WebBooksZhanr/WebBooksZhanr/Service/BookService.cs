using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiBib.Interfaces;
using WebApiBib.Model;
using WebBooksZhanr.DBContext;

namespace WebApiBib.Service
{
    public class BookService : IBook
    {
        private readonly BooksZhanrDB _context;

        public BookService(BooksZhanrDB context)
        {
            _context = context;
        }

        public async Task<IActionResult> DeleteBook(int Id)
        {
            var books = await _context.Books.FindAsync(Id);
            if (books == null)//404
            {
                return new NotFoundResult();
            }

            _context.Books.Remove(books);
            await _context.SaveChangesAsync();

            return new OkObjectResult(new
            {
                status = true,
                MessageContent = "Книга удалена"
            });
        }

        public async Task<IActionResult> GetAllBooks()
        {
            var books = await _context.Books.ToListAsync();
            return new OkObjectResult(new
            {
                books = books,
                status = true
            });
        }

        public async Task<IActionResult> GetBookAuhtor_Name(string Author, string Name)
        {
            var books = await _context.Books
                   .Where(b => b.Author == Author && b.Name == Name)
                   .FirstOrDefaultAsync();
            if (books == null)//404
            {
                return new NotFoundResult();
            }

            return new OkObjectResult(new { books });
        }

        public async Task<IActionResult> GetBookId(int Id)
        {
            var books = await _context.Books.FirstOrDefaultAsync(b => b.Id_Book == Id);
            if (books == null)//404
            {
                return new NotFoundResult();
            }

            return new OkObjectResult(new { books });
        }

        public async Task<IActionResult> GetBookId_Zhanr(int Id_Zhanr)
        {
            var books = await _context.Books.FirstOrDefaultAsync(b => b.Id_Zhanr == Id_Zhanr);
            if (books == null)//404
            {
                return new NotFoundResult();
            }

            return new OkObjectResult(new { books });
        }

        public async Task<IActionResult> PostBook(int Id, Books newBook)
        {
            var existingZhanr = await _context.Zhanr.FindAsync(newBook.Id_Zhanr);
            if (existingZhanr == null)
            {
                return new BadRequestResult();
            }

            var book = new Books()
            {
                Name = newBook.Name,
                Author = newBook.Author,
                Description = newBook.Description,
                Year_Izd = newBook.Year_Izd,
                Id_Zhanr = existingZhanr.Id_Zhanr
            };

            _context.Books.Add(book);
            await _context.SaveChangesAsync();

            return new OkObjectResult(new { book });
        }



        public async Task<IActionResult> PutBook([FromBody] Books UpdateBook)
        {
            var books = await _context.Books.FindAsync(UpdateBook.Id_Book);
            if (books == null) //404
            {
                return new NotFoundResult();
            }
            books.Name = UpdateBook.Name;
            books.Author = UpdateBook.Author;
            books.Description = UpdateBook.Description;
            books.Year_Izd = UpdateBook.Year_Izd;
            books.Id_Zhanr = UpdateBook.Id_Zhanr;

            _context.Books.Update(books);
            await _context.SaveChangesAsync();


            return new OkResult();
        }

        public async Task<IActionResult> GetBooks([FromQuery] string? Name, [FromQuery] string? Author, [FromQuery] int? zhanr, [FromQuery] DateTime? year)
        {
            var query = _context.Books.AsQueryable();

            if (!string.IsNullOrWhiteSpace(Name))
                query = query.Where(b => b.Name.Contains(Name));

            if (!string.IsNullOrWhiteSpace(Author))
                query = query.Where(b => b.Author.Contains(Author));

            if (zhanr.HasValue)
                query = query.Where(b => b.Id_Zhanr == Convert.ToInt32(zhanr.Value));

            if (year.HasValue)
                query = query.Where(b => b.Year_Izd == year);

            var result = await query.ToListAsync();

            return new OkObjectResult(new { result });
        }

        public async Task<IActionResult> GetBooksPagination(
            [FromQuery] string? Name,
            [FromQuery] string? author,
            [FromQuery] int? Zhanr,
            [FromQuery] DateTime? year,
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 2)
        {
            var query = _context.Books.AsQueryable();

            if (!string.IsNullOrWhiteSpace(Name))
                query = query.Where(b => b.Name.Contains(Name));

            if (!string.IsNullOrWhiteSpace(author))
                query = query.Where(b => b.Author.Contains(author));

            if (Zhanr.HasValue)
                query = query.Where(b => b.Id_Zhanr == Convert.ToInt32(Zhanr.Value));

            if (year.HasValue)
                query = query.Where(b => b.Year_Izd == year);

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

        public async Task<IActionResult> GetBooksByIds([FromBody] List<int> bookIds)
        {
            var books = await _context.Books
                .Where(book => bookIds.Contains(book.Id_Book))
                .ToListAsync();

            return new OkObjectResult(books);
        }
    }
}
