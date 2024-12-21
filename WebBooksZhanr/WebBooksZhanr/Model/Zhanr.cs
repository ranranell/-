using System.ComponentModel.DataAnnotations;

namespace WebApiBib.Model
{
    public class Zhanr
    {
        [Key]
        public int Id_Zhanr { get; set; }

        [Required(ErrorMessage = "Название жанра обязательно")]
        public required string Name { get; set; }
    }
}
