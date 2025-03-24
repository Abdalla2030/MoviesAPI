using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoviesAPI.Models;
using MoviesAPI.Services;



namespace MoviesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenresController : ControllerBase
    {
        // private readonly ApplicationDbContext _context;
        private readonly IGenresService _genresService;

        public GenresController(IGenresService genresService)
        {
            //_context = context;
            _genresService = genresService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var genres = await _genresService.GetAll();
            return Ok(genres);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(GenreDto dto)
        { 

            // Genre genre = new() {}
            var genre = new Genre {Name = dto.Name};

            // await _context.AddAsync(genre); // without .Genres is right
            /*
            await _context.Genres.AddAsync(genre);

            _context.SaveChanges();
            */
            await _genresService.Add(genre);

            return Ok(genre);

        }
        //api/Genres/1
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(byte id, [FromBody] GenreDto dto)
        {
            var genre = await _genresService.GetById(id);
            if(genre == null)
            {
                return NotFound($"No genre was found with ID: {id}");
            }
            genre.Name = dto.Name;

            _genresService.Update(genre);

            return Ok(genre);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(byte id)
        {
            var genre = await _genresService.GetById(id);
            if (genre == null)
            {
                return NotFound($"No genre was found with ID: {id}");
            }

            _genresService.Delete(genre);
            return Ok(genre);
        }


    }
}
