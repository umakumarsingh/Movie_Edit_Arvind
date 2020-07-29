
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
    public class BusinessLogicTests
    {
        //write code here
        //mocking Object
        private Mock<IMongoCollection<MovieManagement>> _mockCollection;
        private Mock<IMongoCollection<MultiplexManagement>> _multiplexmockCollection;
        private Mock<IMongoCollection<AllotMovie>> _allotmockCollection;
       
        private Mock<IMongoDBContext> _mockContext;
        private MovieManagement _movieManagement;
        private MultiplexManagement _multiplexManagement ;
        private AllotMovie  _allotMovie;
       

        private readonly IList<MovieManagement> _list;
        private readonly IList<MultiplexManagement> _multiplexlist;
        private readonly IList<AllotMovie> _allotlist;
        private Mock<IOptions<Mongosettings>> _mockOptions;

        //write code here
        //creating test outpt file for saving test result
        static BusinessLogicTests()
        {
            if (!File.Exists("../../../../output_business_revised.txt"))
                try
                {
                    File.Create("../../../../output_business_revised.txt");
                }
                catch (Exception)
                {

                }
            else
            {
                File.Delete("../../../../output_business_revised.txt");
                File.Create("../../../../output_business_revised.txt");
            }
        }

        //creating or mocking dummy object
        public BusinessLogicTests()
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
                City="Chandigarh",
                State="UT"
            };
            _allotMovie = new  AllotMovie
            {
                MovieName="Dil Bechara",
                MultiplexName="PVR",
                City="Chandigarh",
                State="UT"
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

       
        }

    }

