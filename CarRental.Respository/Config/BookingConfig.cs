using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarRental.Respository.Models
{
    internal class BookingConfig : IEntityTypeConfiguration<Booking>
    {
        public void Configure(EntityTypeBuilder<Booking> builder)
        {
            builder.ToTable("t_booking", tb => tb.HasComment("订单表")).
                    HasCharSet("utf8mb4").
                    UseCollation("utf8mb4_general_ci").
                    HasKey(x => x.Id).HasName("PRIMARY");

            builder.Property(x => x.Staus).
                    HasMaxLength(20).
                    HasColumnName("status");

            builder.Property(x => x.Content).
                    HasColumnType("char").
                    HasColumnName("content");

            builder.Property(x => x.BookingReference).
                    IsRequired().
                    HasMaxLength(15).
                    HasColumnName("booking_reference");



        }
    }
}
