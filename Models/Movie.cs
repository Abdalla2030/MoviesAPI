using System.ComponentModel.DataAnnotations.Schema;

namespace MoviesAPI.Models
{
    public class Movie
    {
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        //  default (1,1) because is int
        public int Id { get; set; }

        [MaxLength(250)]
        public string Title { get; set; }

        public int Year { get; set; }
        public double Rate { get; set; }
        [MaxLength(2500)]
        public string Storyline { get; set; }
        public byte[] Poster {  get; set; }

        // Foreignkey by default
        public byte GenreId { get; set; }
        public Genre Genre { get; set; }

    }
}
