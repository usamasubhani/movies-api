using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Homitag.Controllers;
using Homitag.Models;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace HomitagTests
{
    public class Tests
    {
        private DbContextOptions<Context> dbContextOptions;
        [SetUp]
        public void Setup()
        {
            dbContextOptions = new DbContextOptionsBuilder<Context>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
        }

        [Test]
        public async Task GetGenres_ReturnsAllGenres()
        {
            // Arrange
            var genresData = new List<Genre>
            {
                new Genre { Id = 1, Name = "Action", Description = "Action movies" },
                new Genre { Id = 2, Name = "Comedy", Description = "Comedy movies" }
            }.AsQueryable();

            using (var dbContext = new Context(dbContextOptions))
            {
                dbContext.Genres.AddRange(genresData);
                dbContext.SaveChanges();
            }

            using (var dbContext = new Context(dbContextOptions))
            {
                var controller = new GenresController(dbContext);

                // Act
                var result = await controller.GetGenres();

                // Assert
                Assert.IsInstanceOf<OkObjectResult>(result.Result);
                var okObjectResult = result.Result as OkObjectResult;
                Assert.IsInstanceOf<IEnumerable<Genre>>(okObjectResult.Value);
                var genres = okObjectResult.Value as IEnumerable<Genre>;
                Assert.AreEqual(2, genres.Count());
            }
        }

        [Test]
        public async Task GetMovies_ReturnsAllMovies()
        {
            // Arrange
            var moviesData = new List<Movie>
            {
                new Movie { Id = 1, Name = "Movie 1", Description = "Description 1", ReleaseDate = DateTime.Now, Duration = 120, Rating = 8.5 },
                new Movie { Id = 2, Name = "Movie 2", Description = "Description 2", ReleaseDate = DateTime.Now, Duration = 130, Rating = 7.9 }
            }.AsQueryable();

            using (var dbContext = new Context(dbContextOptions))
            {
                dbContext.Movies.AddRange(moviesData);
                dbContext.SaveChanges();
            }

            using (var dbContext = new Context(dbContextOptions))
            {
                var controller = new MoviesController(dbContext);

                // Act
                var result = await controller.GetMovies();

                // Assert
                Assert.IsInstanceOf<OkObjectResult>(result.Result);
                var okObjectResult = result.Result as OkObjectResult;
                Assert.IsInstanceOf<IEnumerable<Movie>>(okObjectResult.Value);
                var movies = okObjectResult.Value as IEnumerable<Movie>;
                Assert.AreEqual(2, movies.Count());
            }
        }
    }
}