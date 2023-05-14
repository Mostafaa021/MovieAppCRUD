using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieAppCRUD.Models
{
    public class Movie
    {
        public int Id { get; set; }
        [Required , MaxLength(250)]
        public string Title { get; set; }
        public int Year { get; set; }
        [Required]
        public double Rate { get; set; }
        [Required , MaxLength(3000)]
        public string StoryLine { get; set; }
        [Required]
        public byte[] Poster { get; set; }
        [ForeignKey("Genre")]
        public int GenereId { get; set; }

        public Genre Genre { get; set; }
    }
}
