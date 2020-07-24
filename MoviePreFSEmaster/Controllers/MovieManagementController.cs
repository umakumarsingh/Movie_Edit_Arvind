using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MoviePreFSEmaster.BusinessLayer.Interface;
using MoviePreFSEMaster.Entities;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MoviePreFSEmaster.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieManagementController : ControllerBase
    {

        private readonly IMovieServices _movieservices;
        public MovieManagementController(IMovieServices movieServices)
        {
            _movieservices = movieServices;
        }
        // GET: api/<MoviePreFSEmaster>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MovieManagement>>> GetAllMovieMgmt()
        {
            var result  = await _movieservices.GetAllMoviesAsync();
            return Ok(result);
        }

        // GET api/<MoviePreFSEmaster>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<MoviePreFSEmaster>
        [HttpPost]
        public async Task<IActionResult> AddMovieManagement([FromBody] MovieManagement moviemgmt)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(moviemgmt.DirectedBy))
                    return BadRequest("Please enter Director Name");
                else if (string.IsNullOrWhiteSpace(moviemgmt.Producer))
                    return BadRequest("Please enter Email");
                else if (string.IsNullOrWhiteSpace(moviemgmt.Production))
                    return BadRequest("Please enter Address");

                await _movieservices.RegisterAsync(moviemgmt);

                return Ok("Movie has been added successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        // PUT api/<MoviePreFSEmaster>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<MoviePreFSEmaster>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
