using DevCars.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevCars.API.Persistence
{
    public class DevCarsDbContext
    {
        public DevCarsDbContext()
        {
            Cars = new List<Car>
            {
                new Car(1, "123ABC", "HONDA", "CIVIC", 2021,100000,"CINZA", new DateTime(2021,1,1)),
                new Car(2, "321BAC", "TOYOTA", "COROLA", 2020,50000,"AZUL", new DateTime(2021,1,1)),
                new Car(3, "789HGW", "CHEVROLET", "ONIX", 2021,120000,"PRETO", new DateTime(2021,1,1))
            };

            Customers = new List<Customer>
            {
                 new Customer(1, "HUGO", "1234567", new DateTime(1999, 1,1)),
                 new Customer(2, "JÚLIO", "123498", new DateTime(1990, 1,4)),
                 new Customer(3, "JOÃO", "1234560", new DateTime(1993, 2,1))
            };
        }
        public List<Car> Cars{ get; set; }
        public List<Customer> Customers { get; set; }
    }
}
