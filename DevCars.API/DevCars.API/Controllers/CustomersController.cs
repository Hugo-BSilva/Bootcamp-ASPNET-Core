using DevCars.API.Entities;
using DevCars.API.InputModels;
using DevCars.API.Persistence;
using DevCars.API.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
            var customer = new Customer(model.FullName, model.Document, model.BirthDate);

            _dbContext.Customers.Add(customer);
            _dbContext.SaveChanges();

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
            var order = new Order(model.IdCar, model.IdCustomer, car.Price, extraItems);

            //var customer = _dbContext.Customers.SingleOrDefault(c => c.Id == model.IdCustomer);
            //customer.Purchase(order);
            _dbContext.Orders.Add(order);
            _dbContext.SaveChanges();

            return CreatedAtAction(
                nameof(GetOrder),
                new { id = order.IdCustomer, orderid = order.Id },
                model
                );
        }

        /* GET api/customers/2/orders/3 - Vai pegar o pedido de identificador 3, 
         do cliente de id 2*/
        [HttpGet("{id}/orders/{orderid}")]
        public IActionResult GetOrder(int id, int orderid)
        {
            var order = _dbContext.Orders
                .Include(o => o.ExtraItems)
                .SingleOrDefault(o => o.Id == orderid);

            if (order == null)
            {
                return NotFound();
            }
            var extraItems = order
                .ExtraItems
                .Select(e => e.Description)
                .ToList();

            var orderViewModel = new OrderDetailViewModel(order.IdCar, order.IdCustomer, order.TotalCost, extraItems);

            return Ok(orderViewModel);
        }
    }
}
