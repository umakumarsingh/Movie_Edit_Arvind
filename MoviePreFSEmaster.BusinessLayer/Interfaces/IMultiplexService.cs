using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MoviePreFSEMaster.Entities;
namespace MoviePreFSEmaster.BusinessLayer.Interfaces
{
    public interface IMultiplexService
    {

        Task<IEnumerable<MultiplexManagement>> GetAllMultiplexAsync();
        Task<MultiplexManagement> SearchByMultiplexIdAsync(string MultiplexID);
       
        Task<MultiplexManagement> AddMultiplex(MultiplexManagement multiplexManagement);
      
    }
}
