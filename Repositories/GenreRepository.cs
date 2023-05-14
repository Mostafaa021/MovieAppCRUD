using Microsoft.EntityFrameworkCore;
using MovieAppCRUD.Models;

namespace MovieAppCRUD.Repositories
{
    public class GenreRepository : IGenreRepository
    {
        private readonly ApplicationDBContext _context;

        public GenreRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Genre>> GetAllGenreAsync()
        {
            return await _context.Genres.OrderBy(g=>g.Name).ToListAsync();
        }

        public Genre GetGenreById(int Id)
        {
            return _context.Genres.FirstOrDefault(g => g.Id == Id);
        }
    }
}
