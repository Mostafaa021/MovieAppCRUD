using MovieAppCRUD.Models;
using MovieAppCRUD.ViewModels;
using System.Collections;

namespace MovieAppCRUD.Repositories
{
    public interface IMovieRepository
    {
       Task<IEnumerable<Movie>> GetAllMoviesAsync();
       Task<Movie> GetMovieById(int ? id);
       Task<Movie> GetMovieByIdWithGenre(int? id);
       Task<Movie> GetMovieByTitle(string title);
       void AddMovie(Movie movie);
       void RemoveMovie(Movie movie);
       void UpdateMovie( Movie Movie , MovieFormViewModel ? viewModel);
      
    }
}
