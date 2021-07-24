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
    public class 勉強会テーマController : ControllerBase
    {
        static Dictionary<int, 勉強会テーマ> データ { get; } = new Dictionary<int, 勉強会テーマ>();

        // GET: <勉強会テーマController>
        [HttpGet]
        public IEnumerable<勉強会テーマ> Get() => データ.Values;

        // GET <勉強会テーマController>/5
        [HttpGet("{id}")]
        public 勉強会テーマ Get(int id) =>
            データ.ContainsKey(id)
            ? データ[id]
            : null;


        // POST <勉強会テーマController>
        [HttpPost]
        public void Post([FromBody] 勉強会テーマ value)
        {
            Put(value);
        }

        // PUT <勉強会テーマController>
        [HttpPut]
        public void Put([FromBody] 勉強会テーマ value)
        {
            データ[value.Id] = value;
        }

        // DELETE <勉強会テーマController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            データ.Remove(id);
        }
    }
}
