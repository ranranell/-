using WebApiBib.Model;
using Microsoft.EntityFrameworkCore;

namespace WebBooksZhanr.DBContext
{
    public class BooksZhanrDB : DbContext
    {
        public BooksZhanrDB(DbContextOptions options) : base(options)
        {   
        }
        public DbSet<Books> Books { get; set; }
        public DbSet<Zhanr> Zhanr { get; set; }
    }
    

}
