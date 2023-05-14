using MovieAppCRUD.Models;

namespace MovieAppCRUD.Repositories
{
    public interface IGenreRepository
    {
        Task<IEnumerable<Genre>> GetAllGenreAsync();

        Genre GetGenreById(int Id);


    }
}
