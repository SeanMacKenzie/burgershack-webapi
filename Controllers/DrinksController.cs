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
    public class DrinksController : Controller
    {
        DrinkRepository db { get; set; }
        public DrinksController()
        {
            db = new DrinkRepository();
        }

        // GET api/values
        [HttpGet]
        public IEnumerable<Drink> Get()
        {
            return db.GetAll();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public Drink Get(int id)
        {
            return db.GetById(id);
        }

        // POST api/values
        [HttpPost]
        public Drink Post([FromBody]Drink drink)
        {
            return db.Add(drink);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public Drink Put(int id, [FromBody]Drink drink)
        {
            if (ModelState.IsValid)
            {
                return db.GetOneByIdAndUpdate(id, drink);
            }
            return null;
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            db.DeleteById(id);
        }
    }
}