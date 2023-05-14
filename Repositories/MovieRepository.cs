using Microsoft.EntityFrameworkCore;
using MovieAppCRUD.Models;
using MovieAppCRUD.ViewModels;

namespace MovieAppCRUD.Repositories
{
    public class MovieRepository : IMovieRepository
    {
        private readonly ApplicationDBContext _context;

        public MovieRepository( ApplicationDBContext context)
        {
            _context = context;
        }

        public void AddMovie(Movie movie)
        {
            _context.Movies.Add(movie);
            _context.SaveChanges();

        }
        public void RemoveMovie(Movie movie)
        {
            _context.Movies.Remove(movie);
            _context.SaveChanges();
        }

        public void UpdateMovie ( Movie movie, MovieFormViewModel? viewModel)
        {
            _context.Movies.Update(movie);
            _context.SaveChanges();
        }


        public  async Task<IEnumerable<Movie>> GetAllMoviesAsync()
        {
            return await _context.Movies.OrderByDescending(m=>m.Rate).ToListAsync();
        }

        public async Task <Movie> GetMovieById(int ? id)
        {
            return await  _context.Movies.Where(m=>m.Id==id).FirstOrDefaultAsync();
        }

        public async Task <Movie> GetMovieByTitle(string title)
        {
            return  await _context.Movies.FirstOrDefaultAsync(m => m.Title == title);
        }

        public async Task<Movie> GetMovieByIdWithGenre(int? id)
        {
            return await _context.Movies.Include(m=>m.Genre).FirstOrDefaultAsync(m => m.Id == id);
        }
    }
}
