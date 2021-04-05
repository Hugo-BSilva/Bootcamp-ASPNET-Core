using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevCars.API.ViewModels
{
    /* Modelo de saída, quando requisitado, vai mostrar as informações contidas nessa ViewModel*/
    public class CarItemViewModel
    {
        public int Id { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public decimal Price { get; set; }

    }
}
