using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevCars.API.Controllers
{
    [Route("api/customers")]
    public class CustomersController: ControllerBase
    {
        //POST api/customers
        [HttpPost]
        public IActionResult Post()
        {
            return Ok();
        }

        //POST api/customers/2/orders
        [HttpPost("{id}")]
        public IActionResult Post (int id)
        {
            return Ok();
        }

        //PUT api/cars/1
    }
}
