using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoviesAPI.Services;

namespace MoviesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IMoviesService _moviesService;
        private readonly IGenresService _genresService;
        private new List<String> _allowedExtenstions = new List<string> { ".jpg", ".png" };
        private long _maxAllowedPosterSize = 1048576; // 1 MB

        public MoviesController(IMoviesService moviesService, IGenresService genresService, IMapper mapper)
        {
            _moviesService = moviesService;
            _genresService = genresService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var movies = await _moviesService.GetAll();
            var data = _mapper.Map<IEnumerable<MovieDetailsDto>>(movies);
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var movie = await _moviesService.GetById(id);
            if (movie == null)
            {
                return NotFound();
            }
            var dto = _mapper.Map<MovieDetailsDto>(movie);
            return Ok(dto);
        }

        [HttpGet("GetByGenreId")]
        public async Task<IActionResult> GetByGenreIdAsync(byte genreId)
        {
            var movies = await _moviesService.GetAll(genreId);
            var data = _mapper.Map<IEnumerable<MovieDetailsDto>>(movies);
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromForm] MovieDto dto)
        {
            if (dto.Poster == null)
            {
                return BadRequest("Poster is Required !");
            }
            if (!_allowedExtenstions.Contains(Path.GetExtension(dto.Poster.FileName).ToLower()))
            {
                return BadRequest("Only .png and .jpg images are allowed!");
            }

            if (dto.Poster.Length > _maxAllowedPosterSize)
            {
                return BadRequest("Max Allowed size for poster is 1MB!");
            }

            var isValidGenre = await _genresService.IsValidGenre(dto.GenreId);
            if (!isValidGenre)
            {
                return BadRequest("Invalid Genre ID!");
            }

            // Convert photo file to byte datatypes
            using var dataStream = new MemoryStream();
            await dto.Poster.CopyToAsync(dataStream);

            var movie = _mapper.Map<Movie>(dto);
            movie.Poster = dataStream.ToArray();
            movie.Genre = await _genresService.GetById(dto.GenreId);

            _moviesService.Add(movie);

            var movieDetailsDto = _mapper.Map<MovieDetailsDto>(movie);
            return Ok(movieDetailsDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, [FromForm] MovieDto dto)
        {
            var movie = await _moviesService.GetById(id);
            if (movie == null)
            {
                return NotFound($"No movie was found with ID {id}");
            }
            var isValidGenre = await _genresService.IsValidGenre(dto.GenreId);
            if (!isValidGenre)
            {
                return BadRequest("Invalid Genre ID!");
            }

            if (dto.Poster != null)
            {
                if (!_allowedExtenstions.Contains(Path.GetExtension(dto.Poster.FileName).ToLower()))
                {
                    return BadRequest("Only .png and .jpg images are allowed!");
                }

                if (dto.Poster.Length > _maxAllowedPosterSize)
                {
                    return BadRequest("Max Allowed size for poster is 1MB!");
                }

                using var dataStream = new MemoryStream();
                await dto.Poster.CopyToAsync(dataStream);

                movie.Poster = dataStream.ToArray();
            }

            movie.Title = dto.Title;
            movie.Storyline = dto.Storyline;
            movie.Rate = dto.Rate;
            movie.Year = dto.Year;
            movie.GenreId = dto.GenreId;
            movie.Genre = await _genresService.GetById(dto.GenreId);

            _moviesService.Update(movie);

            var movieDetailsDto = _mapper.Map<MovieDetailsDto>(movie);
            return Ok(movieDetailsDto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var movie = await _moviesService.GetById(id);
            if (movie == null)
            {
                return NotFound($"No movie was found with ID {id}");
            }
            // _context.Movies.Remove(movie);
            // _context.Remove(movie);
            _moviesService.Delete(movie);

            var dto = _mapper.Map<MovieDetailsDto>(movie);
            return Ok(dto);
        }

    }
}
