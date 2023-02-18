using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using MySqlConnector;

namespace TestApplication.Controllers
{


    [ApiController]
    [Route("[controller]")]
    public class MovieController : ControllerBase
    {

        private readonly ILogger<MovieController> _logger;

        public MovieController(ILogger<MovieController> logger)
        {
            _logger = logger;
        }
        //GET: api/Movie
        [HttpGet]
        public IEnumerable<Movie> Get()
        {
            using var connection = new MySqlConnection("Default");
            connection.Open();
            using var command = new MySqlCommand("Select * From movie", connection);
            using var reader = command.ExecuteReader();
            List<Movie> movies = new();
            while (reader.Read())
            {
                movies.Add(new Movie()
                {
                    id = reader.GetInt32("id"),
                    title = reader.GetString("title"),
                    description = reader.GetString("description"),
                    rating = reader.GetFloat("rating"),
                    image = reader.GetString("image"),
                    created_at = reader.GetDateTime("created_at"),
                    updated_at = reader.GetDateTime("updated_at")
                });
            }
            movies.ToArray();
            return movies;
        }

        //GET: api/Movie/1
        [HttpGet("{id}",Name ="Get")]
        public IEnumerable<Movie> Get(int id)
        {
            using var connection = new MySqlConnection("Default");
            connection.Open();
            using var command = new MySqlCommand("Select * From movie where id = @id", connection);
            command.Parameters.AddWithValue("@id", id);
            using var reader = command.ExecuteReader();
            List<Movie> movies = new();
            while (reader.Read())
            {
                movies.Add(new Movie()
                {
                    id = reader.GetInt32("id"),
                    title = reader.GetString("title"),
                    description = reader.GetString("description"),
                    rating = reader.GetFloat("rating"),
                    image = reader.GetString("image"),
                    created_at = reader.GetDateTime("created_at"),
                    updated_at = reader.GetDateTime("updated_at")
                });
            }
            movies.ToArray();
            return movies;
        }

        //POST : api/Movie
        [HttpPost]
        public IEnumerable<Movie> Post([FromBody] Movie movie)
        {
            List<Movie> movies = new();
            movies.Add(movie);
            return movies;

        }

        [HttpPatch("{id}")]
        public IEnumerable<Movie> Patch(int id, [FromBody] JsonPatchDocument<Movie> movieFix)
        {

            List<Movie> movies = new();
            Movie movieToUpdate = movies.Find(f => f.id == id);
            int index = movies.IndexOf(movieToUpdate);
            movieFix.ApplyTo(movieToUpdate);
            return movies;
            
        }

        [HttpDelete("{id}")]
        public IEnumerable<Movie> Delete(int id)
        {
            List<Movie> movies = new();
            Movie movieToUpdate = movies.Find(f => f.id == id);
            movies.Remove(movieToUpdate);
            return movies;

        }

    }
}
