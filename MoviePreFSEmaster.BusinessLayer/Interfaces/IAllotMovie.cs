using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MoviePreFSEMaster.Entities;
namespace MoviePreFSEmaster.BusinessLayer.Interfaces
{
   public interface IAllotMovie
    {
        Task<IEnumerable<AllotMovie>> GetAllotMovieAsync();
        Task<AllotMovie> SearchByAllotMovieIdAsync(string MultiplexID);
        Task<AllotMovie> AddAllot(AllotMovie allotMovie);

    }
}
