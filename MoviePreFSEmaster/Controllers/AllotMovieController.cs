using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using MoviePreFSEmaster.BusinessLayer.Interfaces;
using MoviePreFSEMaster.Entities;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MoviePreFSEmaster.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AllotMovieController : ControllerBase
    {

        private readonly IAllotMovie  _allotMovie;
        public AllotMovieController(IAllotMovie  allotMovie)
        {
            _allotMovie = allotMovie;
        }



        // GET: api/<MoviePreFSEmaster>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AllotMovie>>> GetAllotMovieAsync()
        {
            var result = await _allotMovie.GetAllotMovieAsync();
            return Ok(result);
        }

        // GET api/<AllotMovie>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<MoviePreFSEmaster>
        [HttpPost]
        public async Task<IActionResult> AddAllot([FromBody] AllotMovie allotMovie)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(allotMovie.MovieName))
                    return BadRequest("Please enter Movie Name");
                else if (string.IsNullOrWhiteSpace(allotMovie.MultiplexName))
                    return BadRequest("Please enter Multiplex Name");
                else if (string.IsNullOrWhiteSpace(allotMovie.City))
                    return BadRequest("Please enter City");
                else if (string.IsNullOrWhiteSpace(allotMovie.State))
                    return BadRequest("Please enter State");
                await _allotMovie.AddAllot(allotMovie);

                return Ok("Alloted Movie has been added successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }


        //Get buyer by Id
        //[HttpGet("{id}")]
        //public async Task<IActionResult> SearchByAllotMovieIdAsync(string MultiplexID);
        //{
        //    //Write Code Here
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }
        //    var buyerResult = await _allotMovie.SearchByAllotMovieIdAsync(MultiplexID);
        //    if (buyerResult == null)
        //    {
        //        return NotFound();
        //    }
        //    return Ok(buyerResult);
        //}

        // PUT api/<AllotMovie>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<AllotMovie>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
