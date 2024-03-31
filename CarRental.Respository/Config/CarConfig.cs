using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarRental.Respository.Models
{
    internal class CarConfig : IEntityTypeConfiguration<Car>
    {
        public void Configure(EntityTypeBuilder<Car> builder)
        {

            builder.ToTable("b_car", tb => tb.HasComment("车辆表")).
                    HasKey(x => x.Id).
                    HasName("PRIMARY");


            builder.Property(x => x.Price).
                    HasColumnName("price");

            builder.Property(x => x.Image).
                    HasColumnType("blob").
                    HasColumnName("image");


            builder.Property(x=>x.Color).
                    HasMaxLength(10).
                    HasColumnName("color");

            builder.Property(x=>x.Description).
                    HasMaxLength(100).
                    HasColumnName("description");

            builder.Property(x => x.Registration).
                    HasMaxLength(50).
                    HasColumnName("registration");

            builder.HasMany(x => x.Bookings).WithOne(x => x.Car).IsRequired();
      
        }
    }
}
