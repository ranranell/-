using static System.Reflection.Metadata.BlobBuilder;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebReaders.Model
{
    public class Arend
    {
        [Key]
        public int Id_HA { get; set; }
        public DateTime ArendStart { get; set; }
        public DateTime? ArendEnd { get; set; }


        [ForeignKey("Readers")]
        public required int Id_Reader { get; set; }
        public Readers Readers { get; set; }
        [ForeignKey("Books")]
        public required int Id_Book { get; set; }
    }
}
