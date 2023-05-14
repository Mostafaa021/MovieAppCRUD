using Microsoft.AspNetCore.Mvc;
using System.IO;
using MovieAppCRUD.Services;
using MovieAppCRUD.ViewModels;
using System.Diagnostics;
using System.IO.Compression;
using MovieAppCRUD.Models;
using System.Runtime.CompilerServices;
using NToastNotify;

namespace MovieAppCRUD.Controllers
{
    public class MovieController : Controller
    {
        private readonly IMovieService _MovieService;
        private readonly IGenreService _GenreService;
        private readonly IToastNotification _ToastNotification;
        private const long _MaximumSizeInBytes = 1048576;
        private  readonly IEnumerable<string> _allowedExtensions = new List<string> { ".jpg", ".png" };

        private async void CheckImageSize(MovieFormViewModel viewModel)
        { 
            viewModel.Genres = await _GenreService.GetAllGenreAsync();
            ModelState.AddModelError("Poster", "Max File Size 1 MB");
        }
        private async void CheckImageType(MovieFormViewModel viewModel)
        {
            viewModel.Genres = await _GenreService.GetAllGenreAsync();
            ModelState.AddModelError("Poster", " Only .jpg and .png are allowed Extensions");
        }
        public MovieController(IMovieService MovieService , IGenreService GenreService , IToastNotification ToastNotification)
        {
            _MovieService = MovieService;
            _GenreService = GenreService;
            _ToastNotification = ToastNotification;
        }
        public async Task <IActionResult> Index()
        {
           
            return View( await _MovieService.GetAllMoviesAsync());
        }
        public async Task<IActionResult> Create()
        {
            var ViewModel = new MovieFormViewModel()
            {
                Genres =  await _GenreService.GetAllGenreAsync()
            };
            return View("MovieForm", ViewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MovieFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Genres = await _GenreService.GetAllGenreAsync();
                return View("MovieForm", viewModel);
            }
            IFormFileCollection files = Request.Form.Files;
            if (!files.Any())
            {
                viewModel.Genres = await _GenreService.GetAllGenreAsync();
                ModelState.AddModelError("Poster", "Please Select a Movie Poster");
                return View("MovieForm", viewModel);
            }
            var poster = files.FirstOrDefault();
            //Path to get extension of file that user send .
            if (!_allowedExtensions.Contains(Path.GetExtension(poster.FileName).ToLower()))
            {
                 CheckImageType(viewModel);
                return View("MovieForm", viewModel);
            }
                
            if(poster.Length > _MaximumSizeInBytes)
            {
                CheckImageSize(viewModel);
                return View("MovieForm", viewModel);
            }
              
            // when dealing with files in memory we use Memorystream (backStore) 
            // to save in database
            using var dataStream = new MemoryStream();
            await poster.CopyToAsync(dataStream); 

            var AddedMovie = new Movie()
            {
                Poster = dataStream.ToArray()
            };
            _MovieService.FromMovieViewModelToMovie(AddedMovie, viewModel);
            _MovieService.AddMovie(AddedMovie);
            _ToastNotification.AddSuccessToastMessage("Adding New Movie succeeded");
            return RedirectToAction(nameof(Index));
            
        }
        
        public async Task<IActionResult> Edit(int ? Id)
        {
            if (Id == null)
               return BadRequest();

            var movie = await _MovieService.GetMovieById(Id);
            if(movie== null)
                return NotFound();

            var viewModel = new MovieFormViewModel
            {
                Id = movie.Id,
                Title = movie.Title,
                GenereId = movie.GenereId,
                Year = movie.Year,
                Rate = movie.Rate,
                StoryLine = movie.StoryLine,
                Poster = movie.Poster,
                Genres = await _GenreService.GetAllGenreAsync()
            };
            
            return View("MovieForm",viewModel);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(MovieFormViewModel viewModel )
        {
            if (!ModelState.IsValid)
            {
                viewModel.Genres = await _GenreService.GetAllGenreAsync();
                return View("MovieForm", viewModel);
            }
            var Movie = await _MovieService.GetMovieById(viewModel.Id);
            if (Movie == null)
                return NotFound();

            var files = Request.Form.Files;
            var poster =files.FirstOrDefault();
            if (files.Any())
            {
                using var dataStream = new MemoryStream();
                await poster.CopyToAsync(dataStream);
                viewModel.Poster = dataStream.ToArray();
                if (!_allowedExtensions.Contains(Path.GetExtension(poster.FileName).ToLower()))
                {
                    CheckImageType(viewModel);
                    return View("MovieForm", viewModel);
                }
                if (poster.Length > _MaximumSizeInBytes)
                {
                    CheckImageSize(viewModel);
                    return View("MovieForm", viewModel);
                }
                     
                Movie.Poster = viewModel.Poster;
            }
            _MovieService.UpdateMovie(Movie, viewModel);

            _ToastNotification.AddSuccessToastMessage("Updating Movie succeeded");
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return BadRequest();

            var movie = await _MovieService.GetMovieByIdWithGenre(id);
            if (movie == null)
                return NotFound();

            return View(movie);
        }


        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return BadRequest();

            var movie = await _MovieService.GetMovieByIdWithGenre(id);
            if (movie == null)
                return NotFound();

            _MovieService.RemoveMovie(movie);
            return RedirectToAction(nameof(Index));
        }

    }
}
