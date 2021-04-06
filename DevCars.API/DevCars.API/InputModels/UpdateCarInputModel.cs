using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevCars.API.InputModels
{
    /* Por regra de negócio, pode ser permitido alterar outras informações, mas não
     * a marca(brand), Modelo, mas pode ser alterado a cor e o preço.*/
    public class UpdateCarInputModel
    {
        public string Color { get; set; }
        public decimal Price { get; set; }
    }
}
