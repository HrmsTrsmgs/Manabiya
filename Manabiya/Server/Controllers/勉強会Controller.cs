using Marimo.Manabiya.Shared;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Marimo.Manabiya.Server.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class 勉強会Controller : ControllerBase
    {
        static Dictionary<int, 勉強会> データ { get; } = new Dictionary<int, 勉強会>();

        // GET: <勉強会Controller>
        [HttpGet]
        public IEnumerable<勉強会> Get() => データ.Values;

        // GET <勉強会Controller>/5
        [HttpGet("{id}")]
        public 勉強会 Get(int id) =>
            データ.ContainsKey(id)
            ? データ[id]
            : null;


        // POST <勉強会Controller>
        [HttpPost]
        public void Post([FromBody] 勉強会 value)
        {
            Put(value);
        }

        // PUT <勉強会Controller>
        [HttpPut]
        public void Put([FromBody] 勉強会 value)
        {
            データ[value.Id] = value;
        }

        // DELETE <勉強会Controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            データ.Remove(id);
        }
    }
}
