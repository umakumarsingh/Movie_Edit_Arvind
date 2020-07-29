using MoviePreFSEmaster.BusinessLayer;
using MoviePreFSEmaster.BusinessLayer.Services;
using MoviePreFSEmaster.DataLayer;

using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Moq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using MoviePreFSEMaster.Entities;

namespace MoviePreFSEmaster.Tests.TestCases
{
    public class FuctionalTests
    {
        ////write code here
        ///mocking Object
        private Mock<IMongoCollection<MovieManagement>> _mockCollection;
        private Mock<IMongoCollection<MultiplexManagement>> _multiplexmockCollection;
        private Mock<IMongoCollection<AllotMovie>> _allotmockCollection;

        private Mock<IMongoDBContext> _mockContext;
        private MovieManagement _movieManagement;
        private MultiplexManagement _multiplexManagement;
        private AllotMovie _allotMovie;


        private readonly IList<MovieManagement> _list;
        private readonly IList<MultiplexManagement> _multiplexlist;
        private readonly IList<AllotMovie> _allotlist;
        private Mock<IOptions<Mongosettings>> _mockOptions;

        //write code here
        //creating test outpt file for saving test result
        static FuctionalTests()
        {
            if (!File.Exists("../../../../output_revised.txt"))
                try
                {
                    File.Create("../../../../output_revised.txt");
                }
                catch (Exception)
                {

                }
            else
            {
                File.Delete("../../../../output_revised.txt");
                File.Create("../../../../output_revised.txt");
            }
        }

        //creating or mocking dummy object
        public FuctionalTests()
        {
            _movieManagement = new MovieManagement
            {
                DirectedBy = "Arvind",
                Producer = "Soni Movies",
                Production = "Abc Corp",
                ReleasedDate = DateTime.Now.AddDays(5),



            };

            _multiplexManagement = new MultiplexManagement
            {
                Name = "PVR",
                City = "Chandigarh",
                State = "UT"
            };
            _allotMovie = new AllotMovie
            {
                MovieName = "Dil Bechara",
                MultiplexName = "PVR",
                City = "Chandigarh",
                State = "UT"
            };

            _mockCollection = new Mock<IMongoCollection<MovieManagement>>();
            _mockCollection.Object.InsertOne(_movieManagement);


            _multiplexmockCollection = new Mock<IMongoCollection<MultiplexManagement>>();
            _multiplexmockCollection.Object.InsertOne(_multiplexManagement);
            _allotmockCollection = new Mock<IMongoCollection<AllotMovie>>();
            _allotmockCollection.Object.InsertOne(_allotMovie);




            _list = new List<MovieManagement>();
            _list.Add(_movieManagement);

            _multiplexlist = new List<MultiplexManagement>();
            _multiplexlist.Add(_multiplexManagement);
            _mockContext = new Mock<IMongoDBContext>();
            //MongoSettings initialization
            _mockOptions = new Mock<IOptions<Mongosettings>>();
            _list = new List<MovieManagement>();
            _list.Add(_movieManagement);
        }
        //connecting to mongo local host databse
        Mongosettings settings = new Mongosettings()
        {
            Connection = "mongodb://user:password@127.0.0.1:27017/guestbook",
            DatabaseName = "guestbook"
        };


       

        [Fact]
        public async Task TestFor_GetAllMoviesAsync()
        {
            //Arrange
            var res = false;
            Mock<IAsyncCursor<MovieManagement>> _userCursor = new Mock<IAsyncCursor<MovieManagement>>();
            _userCursor.Setup(_ => _.Current).Returns(_list);
            _userCursor
                .SetupSequence(_ => _.MoveNext(It.IsAny<CancellationToken>()))
                .Returns(true)
                .Returns(false);

            _mockCollection.Setup(op => op.FindSync(It.IsAny<FilterDefinition<MovieManagement>>(),
            It.IsAny<FindOptions<MovieManagement, MovieManagement>>(),
             It.IsAny<CancellationToken>())).Returns(_userCursor.Object);
            _mockContext.Setup(c => c.GetCollection<MovieManagement>(typeof(MovieManagement).Name)).Returns(_mockCollection.Object);
            _mockOptions.Setup(s => s.Value).Returns(settings);
            var context = new MongoDBContext(_mockOptions.Object);
            var userRepo = new MovieServices(context);
            //Action
            var result = await userRepo.GetAllMoviesAsync();

            //Assert 
            foreach (MovieManagement user in result)
            {
                Assert.NotNull(user);
                break;
            }

            //writing tset boolean output in text file, that is present in project directory
            if (result != null)
            {
                res = true;
            }
            File.AppendAllText("../../../../output_revised.txt", "TestFor_GetAllMoviesAsync=" + res + "\n");
        
        }

        [Fact]
        public async void TestFor_MovieRegisterAsync()
        {
            //mocking
            var res = false;
            _mockCollection.Setup(op => op.InsertOneAsync(_movieManagement, null,
            default(CancellationToken))).Returns(Task.CompletedTask);
            _mockContext.Setup(c => c.GetCollection<MovieManagement>(typeof(MovieManagement).Name)).Returns(_mockCollection.Object);

            _mockOptions.Setup(s => s.Value).Returns(settings);
            var context = new MongoDBContext(_mockOptions.Object);
            var userRepo = new MovieServices(context);

            //Action
            var movie = await userRepo.RegisterAsync(_movieManagement);

            //Assert
            Assert.NotNull(movie);
            Assert.Equal(_movieManagement.DirectedBy, movie.DirectedBy);

            //writing tset boolean output in text file, that is present in project directory
            if (movie != null)
            {
                res = true;
            }
            File.AppendAllText("../../../../output_revised.txt", "TestFor_MovieRegisterAsync=" + res + "\n");
        
        }

        [Fact]
        public async Task TestFor_SearchByAllotMovieIdAsync()
        {
            //Arrange
            //mocking
            //mocking
            var res = false;
            _mockCollection.Setup(op => op.InsertOneAsync(_movieManagement, null,
            default(CancellationToken))).Returns(Task.CompletedTask);
            _mockContext.Setup(c => c.GetCollection<MovieManagement>(typeof(MovieManagement).Name)).Returns(_mockCollection.Object);

            _mockOptions.Setup(s => s.Value).Returns(settings);
            var context = new MongoDBContext(_mockOptions.Object);
            var userRepo = new MovieServices(context);

            //Action
            var movie = await userRepo.RegisterAsync(_movieManagement);

             //Assert
            Assert.NotNull(movie);
            Assert.Equal(_movieManagement.DirectedBy, movie.DirectedBy);

            //writing tset boolean output in text file, that is present in project directory
            if (movie != null)
            {
                res = true;
            }
            File.AppendAllText("../../../../output_revised.txt", "TestFor_SearchByAllotMovieIdAsync=" + res + "\n");
       

        }
       
    }
}
