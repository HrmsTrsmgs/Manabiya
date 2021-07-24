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
    public class 講義Controller : ControllerBase
    {
        static Dictionary<int, 講義> データ { get; } = new Dictionary<int, 講義>();

        // GET: <講義Controller>
        [HttpGet]
        public IEnumerable<講義> Get() => データ.Values;

        // GET <講義Controller>/5
        [HttpGet("{id}")]
        public 講義 Get(int id) =>
            データ.ContainsKey(id)
            ? データ[id]
            : null;


        // POST <講義Controller>
        [HttpPost]
        public void Post([FromBody] 講義 value)
        {
            Put(value);
        }

        // PUT <講義Controller>
        [HttpPut]
        public void Put([FromBody] 講義 value)
        {
            データ[value.Id] = value;
        }

        // DELETE <講義Controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            データ.Remove(id);
        }
    }
}
