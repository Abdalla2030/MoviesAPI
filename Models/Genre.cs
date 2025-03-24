using System.ComponentModel.DataAnnotations.Schema;

namespace MoviesAPI.Models
{
    public class Genre
    {
        // Primary Key
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public byte Id { get; set; } // 0 -> 255 (1 byte)

        //[Required] by default in dot net 6
        [MaxLength(100)]
        public string Name { get; set; }
    }
}
