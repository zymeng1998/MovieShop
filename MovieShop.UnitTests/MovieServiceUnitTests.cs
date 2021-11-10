using ApplicationCore.Entities;
using ApplicationCore.RepositoryInterfaces;
using ApplicationCore.ServiceInterfaces;
using Infrastructure.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MovieShop.UnitTests
{
    [TestClass]
    public class MovieServiceUnitTests
    {
        private MovieServices _movieServices;
        private static List<Movie> _movies;
        private Mock<IMovieRepository> _mockMovieRepository;

        [TestInitialize]
        // onetimesetup
        public void OneTimeSetup()
        {
            _mockMovieRepository = new Mock<IMovieRepository>();
            _mockMovieRepository.Setup(expression:m => m.GetTop30RevenueMovies()).ReturnsAsync(_movies);
            _movieServices = new MovieServices(_mockMovieRepository.Object);
        }
        [ClassInitialize]
        public static void SetUp(TestContext context)
        {
            _movies = new List<Movie>
            {
                new Movie{Id = 1, Title = "Fake1", Budget = 120, PosterUrl = null },
                new Movie{Id = 2, Title = "Fake2", Budget = 120, PosterUrl = null },
                new Movie{Id = 3, Title = "Fake3", Budget = 120, PosterUrl = null },
                new Movie{Id = 4, Title = "Fake4", Budget = 120, PosterUrl = null },
                new Movie{Id = 5, Title = "Fake5", Budget = 120, PosterUrl = null },
                new Movie{Id = 6, Title = "Fake6", Budget = 120, PosterUrl = null },
                new Movie{Id = 7, Title = "Fake7", Budget = 120, PosterUrl = null },
                new Movie{Id = 8, Title = "Fake8", Budget = 120, PosterUrl = null },
                new Movie{Id = 9, Title = "Fake9", Budget = 120, PosterUrl = null },
                new Movie{Id = 10, Title = "Fake10", Budget = 120, PosterUrl = null },
                new Movie{Id = 11, Title = "Fake11", Budget = 120, PosterUrl = null },
                new Movie{Id = 12, Title = "Fake12", Budget = 120, PosterUrl = null },
                new Movie{Id = 13, Title = "Fake13", Budget = 120, PosterUrl = null },
                new Movie{Id = 14, Title = "Fake14", Budget = 120, PosterUrl = null },
                new Movie{Id = 15, Title = "Fake15", Budget = 120, PosterUrl = null },
                new Movie{Id = 16, Title = "Fake16", Budget = 120, PosterUrl = null },
            };

            
        }
        [TestMethod]
        public async Task TestListOfTopRevenueMovieFromFakeData()
        {
            // arrange act assert
            // arrange : mock object and data
            //_movieServices = new MovieServices(new MockMovieRepository());
            var movies = await _movieServices.GetTop30RevenueMovies();
            Assert.IsNotNull(movies);
        }
    }
    public class MockMovieRepository : IMovieRepository
    {
        public Task<Movie> Add(Movie entity)
        {
            throw new NotImplementedException();
        }

        public Task Delete(Movie entity)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Movie>> Get(Expression<Func<Movie, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Movie>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Genre>> GetAllGenres()
        {
            throw new NotImplementedException();
        }

        public Task<Movie> GetById(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<int> GetCount(Expression<Func<Movie, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<Movie> GetMovieById(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Review>> GetMovieReviews(int Id, int pageSize = 30, int page = 1)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Movie>> GetMoviesByCastId(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Movie>> GetMoviesByGenreId(int id, int pageSize, int pageIndex)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Movie>> GetTop30RevenueMovies()
        {
            var movies = new List<Movie>
            {
                new Movie{Id = 1, Title = "Fake1", Budget = 120, PosterUrl = null },
                new Movie{Id = 2, Title = "Fake2", Budget = 120, PosterUrl = null },
                new Movie{Id = 3, Title = "Fake3", Budget = 120, PosterUrl = null },
                new Movie{Id = 4, Title = "Fake4", Budget = 120, PosterUrl = null },
                new Movie{Id = 5, Title = "Fake5", Budget = 120, PosterUrl = null },
                new Movie{Id = 6, Title = "Fake6", Budget = 120, PosterUrl = null },
                new Movie{Id = 7, Title = "Fake7", Budget = 120, PosterUrl = null },
                new Movie{Id = 8, Title = "Fake8", Budget = 120, PosterUrl = null },
                new Movie{Id = 9, Title = "Fake9", Budget = 120, PosterUrl = null },
                new Movie{Id = 10, Title = "Fake10", Budget = 120, PosterUrl = null },
                new Movie{Id = 11, Title = "Fake11", Budget = 120, PosterUrl = null },
                new Movie{Id = 12, Title = "Fake12", Budget = 120, PosterUrl = null },
                new Movie{Id = 13, Title = "Fake13", Budget = 120, PosterUrl = null },
                new Movie{Id = 14, Title = "Fake14", Budget = 120, PosterUrl = null },
                new Movie{Id = 15, Title = "Fake15", Budget = 120, PosterUrl = null },
                new Movie{Id = 16, Title = "Fake16", Budget = 120, PosterUrl = null },
            };
            return movies;
        }

        public Task<IEnumerable<Movie>> GetTopRatedMovies()
        {
            throw new NotImplementedException();
        }

        public Task<Movie> Update(Movie entity)
        {
            throw new NotImplementedException();
        }
    }
}
