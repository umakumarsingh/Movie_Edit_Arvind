using MoviePreFSEMaster.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MoviePreFSEmaster.BusinessLayer.Interface
{
  public  interface IMovieServices
    {
        //methods for completing all Movie  function
        
        Task<MovieManagement> RegisterAsync(MovieManagement movieManagement);
        Task<MovieManagement> Login(MovieManagement movieManagement);
        Task<MovieManagement> ChangeMoviePassword(string MovieId, string newpassword);
        Task<bool> LogOut(MovieManagement movieManagement);

        Task<IEnumerable<MovieManagement>> GetAllMoviesAsync();
        Task<MovieManagement> SearchByMovieByIdAsync(string MovieId);
       Task<MovieManagement> AddMovieManagement(MovieManagement movieManagement);
    
        
    }
}


