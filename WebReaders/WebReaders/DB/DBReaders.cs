using Microsoft.EntityFrameworkCore;
using WebReaders.Model;
namespace WebReaders.DB
{
    public class DBReaders : DbContext
    {
        public DBReaders(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Readers> Readers {get; set;}
        public DbSet<Arend> Arend {get; set;}
    }
}
    