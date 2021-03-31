using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevCars.API.Controllers
{
    [Route("api/customers")]
    public class CarsController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok();
        }

        //GET api/cars/1
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            // SE O CARRO DE IDENTIFICADOR ID NÃO EXISTIR, RETORNA NOTFOUND
            // SENAO, OK
            return Ok();
        }

        //POST api/cars/1
        [HttpPost]
        public IActionResult Post([FromBody] )
        {
            // SE O CADASTRO FUNCIONAR, RETORNA CREATED (201)
            // SE OS DADOS DE ENTRADA ESTIVEREM INCORRETOS, RETORNA BAD REQUEST(400)
            // SE O CADASTRO FUNCIONAR, MAS NÃO TIVER UMA API DE CONSULTA, RETORNA NOCONTENT(204)
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult Put()
        {
            //SE A ATUALIZAÇÃO FUNCIONAR, RETORNA 204 NO CONTENT
            //SE DADOS DE ENTRADA ESTIVEREM INCORRETOS, RETORNA 400 BAD REQUEST
            return Ok();
        }
    }
}
