using System.ComponentModel.DataAnnotations;

namespace WebImage.Model
{
    public class Image
    {
        [Key]
        public int Id_image { get; set; }
        public int Entity_Id { get; set; }
        public string Entity_Name { get; set; }
        public string File_url { get; set; }
        public DateTime Create_date { get; set; }
    }
}
