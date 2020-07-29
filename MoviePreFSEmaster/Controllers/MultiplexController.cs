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
    public class MultiplexController : ControllerBase
    {
        private readonly IMultiplexService _multiplexService;
        public MultiplexController(IMultiplexService multiplexService)
        {
            _multiplexService = multiplexService;
        }
        // GET: api/<MoviePreFSEmaster>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MultiplexManagement>>> GetAllMultiplexAsync()
        {
            var result = await _multiplexService.GetAllMultiplexAsync();
            return Ok(result);
        }




        // GET: api/<Multiplex>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<Multiplex>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }
    
        // POST api/<MoviePreFSEmaster>
        [HttpPost]
        public async Task<IActionResult> AddMultiplex([FromBody] MultiplexManagement multiplexManagement)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(multiplexManagement.Name))
                    return BadRequest("Please enter Multiplex Name");
                else if (string.IsNullOrWhiteSpace(multiplexManagement.City))
                    return BadRequest("Please enter City");
                else if (string.IsNullOrWhiteSpace(multiplexManagement.State))
                    return BadRequest("Please enter State");

                await _multiplexService.AddMultiplex(multiplexManagement);

                return Ok("Multiplex has been added successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }
        //Get Multiplex by Id  SearchByMultiplexIdAsync(string MultiplexID)
        [HttpGet("{id}")]
        public async Task<IActionResult> SearchByMultiplexIdAsync(string MultiplexID)
        {
            //Write Code Here
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var Result = await _multiplexService.SearchByMultiplexIdAsync(MultiplexID);
            if (Result == null)
            {
                return NotFound();
            }
            return Ok(Result);
        }

        // PUT api/<Multiplex>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<Multiplex>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
