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
        
        Task<MovieManagement> RegisterAsync(MovieManagement  allotMovie);
        Task<MovieManagement> Login(MovieManagement allotMovie);
        Task<MovieManagement> ChangeBuyerPassword(string BuyerId, string newpassword);
        Task<bool> LogOut(MovieManagement buyer);
        Task<IEnumerable<MovieManagement>> GetAllMoviesAsync();
        Task<MovieManagement> SearchByMovieByIdAsync(string BuyerId);
        Task<MovieManagement> AddMovieManagement(MovieManagement movieManagement);

        //Task<String> AddMultiplex(MultiplexManagement multiplexManagement);
        //Task<String> AddAllotMovie(AllotMovie allotMovie);
        //Task<IEnumerable<MovieManagement>> ViewGetAllMovies();
    }
}


//IAdminService
////Task<IEnumerable<MovieManagement>> GetAllMovies();
////Task<MovieManagement> SearchByMovieName(String name);
////Task<MultiplexManagement> SearchByMultiplexName(String mname);
//IMovieService
//Task<String> AddMovieManagement(MovieManagement movieManagement);
//Task<String> AddMultiplex(MultiplexManagement multiplexManagement);
//Task<String> AddAllotMovie(AllotMovie allotMovie);