using DevCars.API.InputModels;
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
        //GET api/cars
        [HttpGet]
        public IActionResult Get()
        {
            //Retorna lista de CarItemViewModel
            return Ok();
        }

        /* GET api/cars/1 - O número um representa o identificador do carro, sendo assim,
        vai passar um parâmetro id do tipo int que é referente ao identificador*/
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            // SE O CARRO DE IDENTIFICADOR ID NÃO EXISTIR, RETORNA NOTFOUND
            // SENAO, OK
            return Ok();
        }

        //POST api/cars/1 - Cadastra um novo veículo com o corpo do AddCarInputModel
        [HttpPost]
        public IActionResult Post([FromBody] AddCarInputModel model)
        {
            // SE O CADASTRO FUNCIONAR, RETORNA CREATED (201)
            // SE OS DADOS DE ENTRADA ESTIVEREM INCORRETOS, RETORNA BAD REQUEST(400)
            // SE O CADASTRO FUNCIONAR, MAS NÃO TIVER UMA API DE CONSULTA, RETORNA NOCONTENT(204)
            return Ok();
        }

        //PUT api/cars - Essa API atualiza um recurso
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] UpdateCarInputModel model)
        {
            //SE A ATUALIZAÇÃO FUNCIONAR, RETORNA 204 NO CONTENT
            //SE DADOS DE ENTRADA ESTIVEREM INCORRETOS, RETORNA 400 BAD REQUEST
            //SE NÃO EXISTIR RETORNA NOT FOUND 404
            return Ok();
        }

        //DELETE api/cars/2
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            //SE EXISTIR RETORNA 204 NO CONTENT
            //SE NÃO EXSISTIR RETORNA NOT FOND 404
            return Ok();
        }
    }
}
