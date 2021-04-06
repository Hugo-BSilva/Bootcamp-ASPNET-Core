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
    //Rota de acesso, endpoint da api de carro
    [Route("api/cars")]
    public class CarsController : ControllerBase
    {
        //Injeção de Dependência, dessa forma o dbContext pode ser acessado por todas as APIs
        private readonly DevCarsDbContext _dbContext;
        public CarsController(DevCarsDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        //GET api/cars
        [HttpGet]
        public IActionResult Get()
        {
            //Retorna lista de CarItemViewModel
            var cars = _dbContext.Cars;
            //Projeção de dados. Pra cada obj do tipo carro, crie um novo carItemViewModel
            var carsViewModel = cars
                .Select(c => new CarItemViewModel(c.Id, c.Brand, c.Model, c.Price))
                .ToList();

            return Ok(carsViewModel);
        }

        /* GET api/cars/1 - O número um representa o identificador do carro, sendo assim,
        vai passar um parâmetro id do tipo int que é referente ao identificador*/
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            // SE O CARRO DE IDENTIFICADOR ID NÃO EXISTIR, RETORNA NOTFOUND
            // SENAO, OK
            var car = _dbContext.Cars.SingleOrDefault(c => c.Id == id);
            if (car == null)
            {
                return NotFound();
            }

            var carsDetailsViewModel = new CarDetailViewModel(
                car.Id,
                car.Brand,
                car.Model,
                car.VinCode,
                car.Year,
                car.Price,
                car.Color,
                car.ProductionDate
                );
            return Ok();
        }

        //POST api/cars/1 - Cadastra um novo veículo com o corpo do AddCarInputModel
        [HttpPost]
        public IActionResult Post([FromBody] AddCarInputModel model)
        {
            // SE O CADASTRO FUNCIONAR, RETORNA CREATED (201)
            // SE OS DADOS DE ENTRADA ESTIVEREM INCORRETOS, RETORNA BAD REQUEST(400)
            // SE O CADASTRO FUNCIONAR, MAS NÃO TIVER UMA API DE CONSULTA, RETORNA NOCONTENT(204)

            if (model.Model.Length > 50)
            {
                return BadRequest("Modelo não pode ter mais de 50 caracteres.");
            }
            var car = new Car(model.VinCode, model.Brand, model.Model, model.Year, model.Price, model.Color, model.ProductionDate);

            _dbContext.Cars.Add(car);
            _dbContext.SaveChanges();

            return CreatedAtAction(
                nameof(GetById),
                new {id = car.Id},
                model
                );
        }

        //PUT api/cars/1 - Essa API atualiza um recurso
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] UpdateCarInputModel model)
        {
            //SE A ATUALIZAÇÃO FUNCIONAR, RETORNA 204 NO CONTENT
            //SE DADOS DE ENTRADA ESTIVEREM INCORRETOS, RETORNA 400 BAD REQUEST
            //SE NÃO EXISTIR RETORNA NOT FOUND 404

            var car = _dbContext.Cars.SingleOrDefault(c => c.Id == id);

            if (car == null)
            {
                return NotFound();
            }

            car.Update(model.Color, model.Price);

            _dbContext.SaveChanges();

            return NoContent();
        }

        //DELETE api/cars/2
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            //SE EXISTIR RETORNA 204 NO CONTENT
            //SE NÃO EXSISTIR RETORNA NOT FOND 404
            var car = _dbContext.Cars.SingleOrDefault(c => c.Id == id);

            if (car == null)
            {
                return NotFound();
            }

            car.SetAsSuspended();

            return NoContent();
        }
    }
}
