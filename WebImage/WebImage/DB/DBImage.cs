using Microsoft.EntityFrameworkCore;
using WebImage.Model;

namespace WebImage.DB
{
    public class DBImage : DbContext
    {
        public DBImage(DbContextOptions options) : base(options) 
        {
        
        }

        public DbSet<Image> Image { get; set; }
    }

    
}
