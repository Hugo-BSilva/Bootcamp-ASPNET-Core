using DevCars.API.Entities;
using DevCars.API.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection;

namespace DevCars.API.Persistence
{
    public class DevCarsDbContext : DbContext
    {
        public DevCarsDbContext(DbContextOptions<DevCarsDbContext> options) : base (options) { }
        public DbSet<Car> Cars{ get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<ExtraOrderItem> ExtraOrderItems { get; set; }

        //Configurando o Banco de dados com EF Core
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //TABLE CAR
            //modelBuilder.ApplyConfiguration(new CarDbConfiguration());

            ////TABLE CUSTOMER
            //modelBuilder.ApplyConfiguration(new CustomerDbConfiguration());

            ////TABLE ORDER
            //modelBuilder.ApplyConfiguration(new OrderDbConfiguration());

            ////TABLE EXTRAORDERITEM
            //modelBuilder.ApplyConfiguration(new ExtraOrderDbConfiguration());

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        }
    }
}
