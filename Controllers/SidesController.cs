using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using burgershack_c.Models;
using burgershack_c.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace burgershack_c.Controllers
{
    [Route("api/[controller]")]
    public class SidesController : Controller
    {
        SideRepository db { get; set; }
        public SidesController()
        {
            db = new SideRepository();
        }

        // GET api/values
        [HttpGet]
        public IEnumerable<Side> Get()
        {
            return db.GetAll();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public Side Get(int id)
        {
            return db.GetById(id);
        }

        // POST api/values
        [HttpPost]
        public Side Post([FromBody]Side side)
        {
            return db.Add(side);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public Side Put(int id, [FromBody]Side side)
        {
            return db.GetOneByIdAndUpdate(id, side);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}