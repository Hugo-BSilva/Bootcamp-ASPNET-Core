using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevCars.API.Entities
{
    public class Order
    {
        public Order(int id, int idCar, int idCustomer, decimal totalCost)
        {
            Id = id;
            IdCar = idCar;
            IdCustomer = idCustomer;
            TotalCost = totalCost;

            ExtraItems = new List<ExtraOrderItem>();
        }

        public int Id { get; private set; }
        public int IdCar { get; private set; }
        public int IdCustomer { get; private set; }
        public decimal TotalCost { get; private set; }
        public List<ExtraOrderItem> ExtraItems { get; private set; }
    }

    public class ExtraOrderItem
    {

    }
}
