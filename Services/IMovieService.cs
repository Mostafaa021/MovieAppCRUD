using Microsoft.EntityFrameworkCore.Update.Internal;
using MovieAppCRUD.Models;
using MovieAppCRUD.Repositories;
using MovieAppCRUD.ViewModels;

namespace MovieAppCRUD.Services
{
    public interface IMovieService : IMovieRepository
    {
        void FromMovieViewModelToMovie(Movie Movie, MovieFormViewModel ? viewModel);
    }
}
