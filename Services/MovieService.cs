using MovieAppCRUD.Models;
using MovieAppCRUD.Repositories;
using MovieAppCRUD.ViewModels;

namespace MovieAppCRUD.Services
{
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository _MovieRepo;
        public MovieService(IMovieRepository MovieRepo)
        {
            _MovieRepo = MovieRepo;
        }

  
        public Task<IEnumerable<Movie>> GetAllMoviesAsync()
        {
            return _MovieRepo.GetAllMoviesAsync();
        }

        public async Task<Movie> GetMovieById(int ? id)
        {
           return await _MovieRepo.GetMovieById(id);
        }
        public async Task<Movie> GetMovieByIdWithGenre(int? id)
        {
            return await _MovieRepo.GetMovieByIdWithGenre(id);
        }
        public async Task<Movie> GetMovieByTitle(string title)
        {
            return await _MovieRepo.GetMovieByTitle(title);
        }
        public void UpdateMovie(Movie movie , MovieFormViewModel ? viewModel)
        {
            FromMovieViewModelToMovie(movie , viewModel);
           _MovieRepo.UpdateMovie(movie,viewModel);
        }
        public  void FromMovieViewModelToMovie( Movie Movie ,  MovieFormViewModel ?  viewModel)
        {
            Movie.Id = viewModel.Id;
            Movie.GenereId = viewModel.GenereId;
            Movie.Title = viewModel.Title;
            Movie.Rate = viewModel.Rate;
            Movie.StoryLine = viewModel.StoryLine;
            Movie.Year = viewModel.Year;

        }
        public void AddMovie(Movie movie)
        {
            _MovieRepo.AddMovie(movie);
        }
        public void RemoveMovie(Movie movie)
        {
            _MovieRepo.RemoveMovie(movie);
        }

        
    }
}
