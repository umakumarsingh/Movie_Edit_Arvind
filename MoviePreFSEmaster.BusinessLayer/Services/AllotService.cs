using MoviePreFSEmaster.BusinessLayer.Interface;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MoviePreFSEmaster.DataLayer;
using MoviePreFSEMaster.Entities;
using MoviePreFSEmaster.BusinessLayer.Interfaces;
namespace MoviePreFSEmaster.BusinessLayer.Services
{
    public class AllotService : IAllotMovie
    {

        //creating fiels for injecting dbcontext and Multiplex mmongo collection
        private readonly IMongoDBContext _mongoContext;
        private IMongoCollection<AllotMovie> _moviedbCollection;


        //injecting dbContext and geetting collection
        public AllotService(IMongoDBContext context)
        {
            _mongoContext = context;
            _moviedbCollection = _mongoContext.GetCollection<AllotMovie>(typeof(AllotMovie).Name);


        }

        ////Add new Allot Movie 
        public async Task<AllotMovie> AddAllot(AllotMovie allotMovie)
        {
            try
            {
                if (allotMovie == null)
                {
                    throw new ArgumentNullException(typeof(MultiplexManagement).Name + " object is null");
                }
                _moviedbCollection = _mongoContext.GetCollection<AllotMovie>(typeof(AllotMovie).Name);
                await _moviedbCollection.InsertOneAsync(allotMovie);
                return allotMovie;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        //get all AllotMovie list 
        public async Task<IEnumerable<AllotMovie>> GetAllotMovieAsync()
        {
            try
            {
                var all = await _moviedbCollection.FindAsync(Builders<AllotMovie>.Filter.Empty, null);
                return await all.ToListAsync();
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }


        //get AllotMovie by MultiplexID
        public async Task<AllotMovie> SearchByAllotMovieIdAsync(string MultiplexID)
        {
            var objectId = new ObjectId(MultiplexID);

            FilterDefinition<AllotMovie> filter = Builders<AllotMovie>.Filter.Eq("_id", objectId);

            _moviedbCollection = _mongoContext.GetCollection<AllotMovie>(typeof(AllotMovie).Name);

            return await _moviedbCollection.FindAsync(filter).Result.FirstOrDefaultAsync();

        }

       
    }
}
