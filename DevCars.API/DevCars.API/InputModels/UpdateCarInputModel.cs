using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevCars.API.InputModels
{
    /* Por regra de negócio, pode ser permitido alterar outras informações, mas não
     * a marca(brand), Modelo, mas pode ser alterado a cor, documento e data de nascimento.*/
    public class UpdateCarInputModel
    {
        public string Color { get; set; }
        public string Document { get; set; }
    }
}
