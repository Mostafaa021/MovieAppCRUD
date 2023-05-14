using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using MovieAppCRUD.Models;

namespace MovieAppCRUD.ViewModels
{
    public class MovieFormViewModel
    {

        public int Id { get; set; }
        [Required(ErrorMessage = "Required"), StringLength(250)]
        public string Title { get; set; }
        [Required(ErrorMessage = "Required")]
        public int Year { get; set; }
        [Required(ErrorMessage = "Required"), Range(1,10)]
        public double Rate { get; set; }
        [Required(ErrorMessage = "Required"), StringLength(3000)]
        public string StoryLine { get; set; }
        [Display(Name = "Select Poster..." )]
        public byte[] ? Poster { get; set; }
        [Display(Name = "Genre" ) , Required(ErrorMessage ="Required")]
        public int GenereId { get; set; }
        public IEnumerable<Genre> ? Genres { get; set; }
    }
}
