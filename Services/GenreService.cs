using MovieAppCRUD.Models;
using MovieAppCRUD.Repositories;

namespace MovieAppCRUD.Services
{
    public class GenreService : IGenreService
    {
        private readonly IGenreRepository _GenreRepo;
        public GenreService( IGenreRepository GenreRepo)
        {
            _GenreRepo = GenreRepo;
        }   
        public Task<IEnumerable<Genre>> GetAllGenreAsync()
        {
             return _GenreRepo.GetAllGenreAsync();
        }

        public Genre GetGenreById(int Id)
        {
           return _GenreRepo.GetGenreById(Id);
        }
    }
}
