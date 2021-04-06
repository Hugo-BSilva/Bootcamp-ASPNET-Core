using DevCars.API.Entities;
using DevCars.API.InputModels;
using DevCars.API.Persistence;
using DevCars.API.ViewModels;
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
        //Readonly = só posso fazer uma atribuição no construtor
        private readonly DevCarsDbContext _dbContext;
        public CustomersController(DevCarsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        //POST api/customers - Cadastra cliente
        [HttpPost]
        public IActionResult Post([FromBody] AddCustomerInputModel model)
        {
            var customer = new Customer(4, model.FullName, model.Document, model.BirthDate);

            _dbContext.Customers.Add(customer);
            return NoContent();
        }

        /* POST api/customers/2/orders - To acessando o cliente de id 2 e também o
        recurso de pedido dele. Estou cadastrando um pedido para o cliente de id 2 */
        [HttpPost("{id}/orders")]
        public IActionResult PostOrder (int id, [FromBody] AddOrderInputModel model)
        {
            var extraItems = model.ExtraItems
                .Select(e => new ExtraOrderItem(e.Description, e.Price))
                .ToList(); //Pra cada item e, converta ele para uma lista com os 2 parâmetros
            
            //Busca o carro pelo ID
            var car = _dbContext.Cars.SingleOrDefault(c => c.Id == model.IdCar); 

            //Busca o cliente atrelando o pedido a ele 
            var order = new Order(1, model.IdCar, model.IdCustomer, car.Price, extraItems);

            var customer = _dbContext.Customers.SingleOrDefault(c => c.Id == model.IdCustomer);

            customer.Purchase(order);

            return CreatedAtAction(
                nameof(GetOrder),
                new { id = customer.Id, orderid = order.Id },
                model
                );
        }

        /* GET api/customers/2/orders/3 - Vai pegar o pedido de identificador 3, 
         do cliente de id 2*/
        [HttpGet("{id}/orders/{orderid}")]
        public IActionResult GetOrder(int id, int orderid)
        {
            var customer = _dbContext.Customers.SingleOrDefault(c => c.Id == id);

            if (customer == null)
            {
                return NotFound();
            }


            var order = customer.Orders.SingleOrDefault(o => o.Id == orderid);

            var extraItems = order.ExtraItems
                .Select(e => e.Description)
                .ToList();

            var orderViewModel = new OrderDetailViewModel(order.IdCar, order.IdCustomer, order.TotalCost, extraItems);

            return Ok(orderViewModel);
        }
    }
}
