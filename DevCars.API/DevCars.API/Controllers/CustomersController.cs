using DevCars.API.InputModels;
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
        //POST api/customers - Cadastra cliente
        [HttpPost]
        public IActionResult Post([FromBody] AddCustomerInputModel model)
        {
            return Ok();
        }

        /* POST api/customers/2/orders - To acessando o cliente de id 2 e também o
        recurso de pedido dele. Estou cadastrando um pedido para o cliente de id 2 */
        [HttpPost("{id}")]
        public IActionResult Post (int id, [FromBody] AddOrderInputModel model)
        {
            return Ok();
        }

        /* GET api/customers/2/orders/3 - Vai pegar o pedido de identificador 3, 
         do cliente de id 2*/
        [HttpGet("{id}/orders/{orderid}")]
        public IActionResult GetOrder(int id, int orderid)
        {
            return Ok();
        }
    }
}
