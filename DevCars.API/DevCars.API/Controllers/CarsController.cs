using Dapper;
using DevCars.API.Entities;
using DevCars.API.InputModels;
using DevCars.API.Persistence;
using DevCars.API.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
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
        private readonly string _connectionString; //string conexão com Dapper
        public CarsController(DevCarsDbContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;

            //SE EU UTILIZAR O DBCONTEXT, E UTILIZAR O INMEMORY VAI DAR ERRO
            //_connectionString = _dbContext.Database.GetDbConnection().ConnectionString;
            _connectionString = configuration.GetConnectionString("DevCarsCs");
        }


        //GET api/cars
        [HttpGet]
        public IActionResult Get()
        {
            //Retorna lista de CarItemViewModel
            //var cars = _dbContext.Cars;
            ////Projeção de dados. Pra cada obj do tipo carro, crie um novo carItemViewModel
            //var carsViewModel = cars
            //    .Select(c => new CarItemViewModel(c.Id, c.Brand, c.Model, c.Price))
            //    .ToList();

            //QUANDO POSTAR O PROJETO, É IMPORTANTE COMENTAR ESSA PARTE, E DESCOMENTAR A DE CIMA 
            using(var sqlConnection = new SqlConnection(_connectionString))
            {
                var query = "SELECT Id, Brand, Model, Price, Price FROM Cars WHERE Status = 0";

                var carsViewModel = sqlConnection.Query<CarItemViewModel>(query);

                return Ok(carsViewModel);
            }
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

        /// <summary>
        /// Cadastrar um carro
        /// </summary>
        /// <remarks>
        /// Requisição de exemplo:
        /// {
        ///     "brand": "Honda",
        ///     "model": "Civic",
        ///     "VinCode": "abc123",
        ///     "year": 2021,
        ///     "color": "Cinza",
        ///     "productionDate": "2021-04-05"
        /// }
        /// </remarks>       
        /// <param name="model"> Dados de um novo Carro</param>
        /// <returns>Objeto recém criado !</returns>
        /// <response code="201">Objeto Criado com Sucesso</response>
        /// <response code="400">Dados invalidos</response>
        //POST api/cars/1 - Cadastra um novo veículo com o corpo do AddCarInputModel
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
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

        /// <summary>
        /// Atualizar dados de um Carro
        /// </summary>
        /// <remarks>
        /// Requisição de Exemplo: 
        /// {
        ///     "color": "Vermelho",
        ///     "price" : 100000
        /// }
        /// </remarks>
        /// <param name="id">Identificador de um Carro</param>
        /// <param name="model">Dados de Alteração</param>
        /// <returns>Não tem retorno. </returns>
        /// <response code="204">Atualização Bem-Sucedida</response>
        /// <response code="404">Carro não encontrado</response>
        //PUT api/cars/1 - Essa API atualiza um recurso
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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

            //_dbContext.SaveChanges();
            //QUANDO POSTAR O PROJETO COMENTAR ESSA PARTE DE BAIXO
            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                var query = "UPDATE Cars SET Color = @color, Price = @price WHERE Id = @id";

                sqlConnection.Execute(query, new { color = car.Color, price = car.Price, car.Id });
            }

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
