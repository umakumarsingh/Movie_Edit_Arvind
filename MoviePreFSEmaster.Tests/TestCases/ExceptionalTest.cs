using MoviePreFSEmaster.BusinessLayer;
using MoviePreFSEmaster.BusinessLayer.Services;
using MoviePreFSEmaster.DataLayer;

using MoviePreFSEmaster.Tests.Exceptions;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Moq;
using System;
using System.IO;
using System.Threading.Tasks;
using Xunit;
using MoviePreFSEMaster.Entities;

namespace MoviePreFSEmaster.Tests.TestCases
{
    public class ExceptionalTest
    {
        //write code here
        //mocking Object
        private Mock<IMongoCollection<MovieManagement>> _mockCollection;
        private Mock<IMongoCollection<MultiplexManagement>> _multiplexCollection;

        private Mock<IMongoDBContext> _mockContext;

      

        private MovieManagement _movieManagement;
        private MultiplexManagement _multiplex;
        private AllotMovie _allotMovie;
        private Mock<IOptions<Mongosettings>> _mockOptions;

        //write code here
        //creating test outpt file for saving test result
        static ExceptionalTest()
        {
            if (!File.Exists("../../../../output_exception_revised.txt"))
                try
                {
                    File.Create("../../../../output_exception_revised.txt");
                }
                catch (Exception)
                {

                }
            else
            {
                File.Delete("../../../../output_exception_revised.txt");
                File.Create("../../../../output_exception_revised.txt");
            }
        }

        //creating or mocking dummy object
        public ExceptionalTest()
        {
            _movieManagement = new MovieManagement
            {
                DirectedBy = "Arvind",
                Producer = "Soni Movies",
                Production = "Abc Corp",
                ReleasedDate = DateTime.Now.AddDays(5),



            };

            _multiplex = new MultiplexManagement
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
            _multiplexCollection = new Mock<IMongoCollection<MultiplexManagement>>();
            _multiplexCollection.Object.InsertOne(_multiplex);
            _mockContext = new Mock<IMongoDBContext>();
            _mockOptions = new Mock<IOptions<Mongosettings>>();

        }

        //connecting to mongo local host databse
        Mongosettings settings = new Mongosettings()
        {
            Connection = "mongodb://user:password@127.0.0.1:27017/guestbook",
            DatabaseName = "guestbook"
        };

        //test for creating new Movie if Movie object is null test to check Movie Management  is null.
        [Fact]
        public async void CreateNewMovie_Null_Failure()
        {
            // Arrange
            _movieManagement = null;
            var res = true;
            //Act 
            var bookRepo = new MovieServices(_mockContext.Object);

            // Assert
            var create = bookRepo.RegisterAsync(_movieManagement);

            await Assert.ThrowsAsync<ArgumentNullException>(() => create);
            if (create.IsCompletedSuccessfully)
            {
                res = false;
            }
            //writing tset boolean output in text file, that is present in project directory
            File.AppendAllText("../../../../output_exception_revised.txt", "CreateNewBuyer_Null_Failure=" + res + "\n");

        }

        //test for creating new ContactUs if ContactUs object is null test to check _contactUs is null.
        [Fact]
        public async void CreateNewMultiplex_Null_Failure()
        {
            // Arrange
            MultiplexManagement _multiplex = null;
            var res = true;

            //Act 
            var bookRepo = new MultiplexService(_mockContext.Object);
            var multiplex = bookRepo.AddMultiplex(_multiplex);

            // Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => multiplex);
            if (multiplex.IsCompletedSuccessfully)
            {
                res = false;
            }

            //writing tset boolean output in text file, that is present in project directory
            File.AppendAllText("../../../../output_exception_revised.txt", "CreateNewContactUs_Null_Failure=" + res + "\n");

        }


       
    }
}
