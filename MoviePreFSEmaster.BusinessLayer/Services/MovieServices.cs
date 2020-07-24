using MoviePreFSEmaster.BusinessLayer.Interface;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MoviePreFSEmaster.DataLayer;
using MoviePreFSEMaster.Entities;

namespace MoviePreFSEmaster.BusinessLayer.Services
{
    public class MovieServices : IMovieServices
    {
        //creating fiels for injecting dbcontext and registering mmongo collection
        private readonly IMongoDBContext _mongoContext;
        private IMongoCollection<MovieManagement> _moviedbCollection;
       

        //injecting dbContext and geetting collection
        public MovieServices(IMongoDBContext context)
        {
            _mongoContext = context;
            _moviedbCollection = _mongoContext.GetCollection<MovieManagement>(typeof(MovieManagement).Name);
           

        }

        //register new movie
        public async Task<MovieManagement> RegisterAsync(MovieManagement movie)
        {
            try
            {
                if (movie == null)
                {
                    throw new ArgumentNullException(typeof(MovieManagement).Name + " object is null");
                }
                _moviedbCollection = _mongoContext.GetCollection<MovieManagement>(typeof(MovieManagement).Name);
                await _moviedbCollection.InsertOneAsync(movie);
                return movie;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        //get all movie list 
        public async Task<IEnumerable<MovieManagement>> GetAllMoviesAsync()
        {
            try
            {
                var all = await _moviedbCollection.FindAsync(Builders<MovieManagement>.Filter.Empty, null);
                return await all.ToListAsync();
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        //Login Buyer
        public Task<MovieManagement> Login(MovieManagement buyer)
        {
            //write code here
            throw new NotImplementedException();
        }

        //change movie password
        public Task<MovieManagement> ChangeBuyerPassword(string BuyerId, string newpassword)
        {
            //write code here
            throw new NotImplementedException();
        }
        //logout movie
        public Task<bool> LogOut(MovieManagement buyer)
        {
            //write code here
            throw new NotImplementedException();
        }
        //get movie by rId
        public async Task<MovieManagement> GetBuyerByIdAsync(string MovieId)
        {
            var objectId = new ObjectId(MovieId);

            FilterDefinition<MovieManagement> filter = Builders<MovieManagement>.Filter.Eq("_id", objectId);

            _moviedbCollection = _mongoContext.GetCollection<MovieManagement>(typeof(MovieManagement).Name);

            return await _moviedbCollection.FindAsync(filter).Result.FirstOrDefaultAsync();

        }
        public async Task<MovieManagement> AddMovieManagement(MovieManagement movieManagement)
        {
            try
            {
                if (movieManagement == null)
                {
                    throw new ArgumentNullException(typeof(MovieManagement).Name + " object is null");
                }
                _moviedbCollection = _mongoContext.GetCollection<MovieManagement>(typeof(MovieManagement).Name);
                await _moviedbCollection.InsertOneAsync(movieManagement);
                return movieManagement;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public Task<MovieManagement> SearchByMovieByIdAsync(string BuyerId)
        {
            throw new NotImplementedException();
        }
    }
}
