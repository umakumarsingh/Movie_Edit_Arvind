
using MoviePreFSEmaster.BusinessLayer.Services;
using MoviePreFSEmaster.DataLayer;

using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Moq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using MoviePreFSEMaster.Entities;

namespace MoviePreFSEmaster.Tests.TestCases
{
  public class BoundaryTest
    {
        //write code here
        //mocking Object
        private Mock<IMongoCollection<MovieManagement>> _mockCollection;
        private Mock<IMongoDBContext> _mockContext;
        private MovieManagement _movieManagement;
        private readonly IList<MovieManagement> _list;
        private Mock<IOptions<Mongosettings>> _mockOptions;

        //write code here
        //creating test outpt file for saving test result
         static BoundaryTest()
        {
            if (!File.Exists("../../../../output_boundary_revised.txt"))
                try
                {
                    File.Create("../../../../output_boundary_revised.txt");
                }
                catch (Exception)
                {

                }
            else
            {
                File.Delete("../../../../output_boundary_revised.txt");
                File.Create("../../../../output_boundary_revised.txt");
            }
        }

        //creating or mocking dummy object
        public BoundaryTest()
        {

            _movieManagement = new  MovieManagement
            {
                DirectedBy="Arvind",
                Producer="Soni Movies",
                Production="Abc Corp",
                ReleasedDate= DateTime.Now.AddDays(5),

             
             
            };
            _mockCollection = new Mock<IMongoCollection<MovieManagement>>();
            _mockCollection.Object.InsertOne(_movieManagement);
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

        //tset for valid movie name or not
        [Fact]
        public async Task BoundaryTestFor_ValidMovieName()
        {
            //mocking
            _mockCollection.Setup(op => op.InsertOneAsync(_movieManagement, null,
            default(CancellationToken))).Returns(Task.CompletedTask);
            _mockContext.Setup(c => c.GetCollection<MovieManagement>(typeof(MovieManagement).Name)).Returns(_mockCollection.Object);

            //Craetion of new Db
            _mockOptions.Setup(s => s.Value).Returns(settings);
            var context = new MongoDBContext(_mockOptions.Object);
            var userRepo = new MovieServices(context);

            //Act
            await userRepo.RegisterAsync(_movieManagement);
            var result = await userRepo.SearchByMovieByIdAsync(_movieManagement.MovieId);



            bool DirectorName = Regex.IsMatch(result.DirectedBy, @"^[a-zA-Z0-9]{4,10}$", RegexOptions.IgnoreCase);
            bool isUserName = Regex.IsMatch(_movieManagement.DirectedBy, @"^[a-zA-Z0-9]{4,10}$", RegexOptions.IgnoreCase);
            
            //writing tset boolean output in text file, that is present in project directory
            File.AppendAllText("../../../../output_boundary_revised.txt", "BoundaryTestFor_ValidBuyerName=" + isUserName.ToString() + "\n");
            //Assert
            Assert.True(isUserName);
            Assert.True(DirectorName);
        }

       
        

        //tset for valid Director Name Length length or not
        [Fact]
        public async Task BoundaryTestFor_ValidDirectorNameLength()
        {
            //mocking
            _mockCollection.Setup(op => op.InsertOneAsync(_movieManagement, null,
            default(CancellationToken))).Returns(Task.CompletedTask);
            _mockContext.Setup(c => c.GetCollection<MovieManagement>(typeof(MovieManagement).Name)).Returns(_mockCollection.Object);

            //Craetion of new Db
            _mockOptions.Setup(s => s.Value).Returns(settings);
            var context = new MongoDBContext(_mockOptions.Object);
            var userRepo = new MovieServices(context);
            var res = true;
            //Act
            await userRepo.AddMovieManagement(_movieManagement);
            var result = await userRepo.SearchByMovieByIdAsync(_movieManagement.MovieId);
           

            var MinLength = 3;
            var MaxLength = 50;

            //Action
            var actualLength = _movieManagement.DirectedBy.Length;

            //Assert
            Assert.InRange(result.DirectedBy.Length, MinLength, MaxLength);
            Assert.InRange(actualLength, MinLength, MaxLength);

            //writing tset boolean output in text file, that is present in project directory
            if (actualLength == _movieManagement.DirectedBy.Length)
            {
                res = true;
            }
            File.AppendAllText("../../../../output_boundary_revised.txt", "BoundaryTestFor_ValidDirectorNameLength=" + res + "\n");

        }

        //tset for valid Movie Id
        [Fact]
        public async Task BoundaryTestFor_ValidMovieId()
        {
            //mocking
            _mockCollection.Setup(op => op.InsertOneAsync(_movieManagement, null,
            default(CancellationToken))).Returns(Task.CompletedTask);
            _mockContext.Setup(c => c.GetCollection<MovieManagement>(typeof(MovieManagement).Name)).Returns(_mockCollection.Object);

            //Craetion of new Db
            _mockOptions.Setup(s => s.Value).Returns(settings);
            var context = new MongoDBContext(_mockOptions.Object);
            var userRepo = new MovieServices(context);
            var res = false;
            //Act
            await userRepo.RegisterAsync(_movieManagement);
            var result = await userRepo.SearchByMovieByIdAsync(_movieManagement.MovieId);

            Assert.InRange(_movieManagement.MovieId.Length, 20, 30);

            //writing tset boolean output in text file, that is present in project directory
            if (result.MovieId.Length.ToString() == _movieManagement.MovieId.Length.ToString())
            {
                res = true;
            }
            File.AppendAllText("../../../../output_boundary_revised.txt", "BoundaryTestFor_ValidMovieId=" + res + "\n");

        }
    }
}
