using DevCars.API.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevCars.API.Persistence.Configurations
{
    public class CarDbConfiguration : IEntityTypeConfiguration<Car>
    {
        public void Configure(EntityTypeBuilder<Car> builder)
        {
            //TABLE CAR
            builder
                .HasKey(c => c.Id); //chave primária do carro


            builder
                .Property(c => c.Brand)
                .HasColumnName("Marca")
                .HasColumnType("VARCHAR(100)")
                .HasDefaultValue("PADRÃO")
                .HasMaxLength(100);

            builder
                .Property(c => c.ProductionDate)
                .HasDefaultValueSql("getdate()");
        }
    }
}
