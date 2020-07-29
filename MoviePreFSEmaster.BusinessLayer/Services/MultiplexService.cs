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
    public class MultiplexService : IMultiplexService
    {

        //creating fiels for injecting dbcontext and Multiplex mmongo collection
        private readonly IMongoDBContext _mongoContext;
        private IMongoCollection<MultiplexManagement> _moviedbCollection;


        //injecting dbContext and geetting collection
        public MultiplexService(IMongoDBContext context)
        {
            _mongoContext = context;
            _moviedbCollection = _mongoContext.GetCollection<MultiplexManagement>(typeof(MultiplexManagement).Name);


        }


        ////Add new Multiplex 
        public async Task<MultiplexManagement> AddMultiplex(MultiplexManagement multiplexManagement)
        {
            try
            {
                if (multiplexManagement == null)
                {
                    throw new ArgumentNullException(typeof(MultiplexManagement).Name + " object is null");
                }
                _moviedbCollection = _mongoContext.GetCollection<MultiplexManagement>(typeof(MultiplexManagement).Name);
                await _moviedbCollection.InsertOneAsync(multiplexManagement);
                return multiplexManagement;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        //get all Multiplex list 
        public async Task<IEnumerable<MultiplexManagement>> GetAllMultiplexAsync()
        {
            try
            {
                var all = await _moviedbCollection.FindAsync(Builders<MultiplexManagement>.Filter.Empty, null);
                return await all.ToListAsync();
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }


        //get Multiplex by MultiplexID
        public async Task<MultiplexManagement> SearchByMultiplexIdAsync(string MultiplexID)
        {
            var objectId = new ObjectId(MultiplexID);

            FilterDefinition<MultiplexManagement> filter = Builders<MultiplexManagement>.Filter.Eq("_id", objectId);

            _moviedbCollection = _mongoContext.GetCollection<MultiplexManagement>(typeof(MultiplexManagement).Name);

            return await _moviedbCollection.FindAsync(filter).Result.FirstOrDefaultAsync();

        }

        
    }
}
