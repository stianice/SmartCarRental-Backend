using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarRental.Repository.Entity
{
    internal class BookingConfig : IEntityTypeConfiguration<Booking>
    {
        public void Configure(EntityTypeBuilder<Booking> builder)
        {
            builder
                .ToTable("t_booking", tb => tb.HasComment("订单表"))
                .UseCollation("utf8mb4_general_ci");

            //builder.Property(x => x.Status).HasMaxLength(20).HasColumnName("status");

            builder
                .Property(x => x.Content)
                .HasColumnType("char")
                .HasColumnName("content")
                .HasMaxLength(20);

            builder
                .Property(x => x.BookingReference)
                .IsRequired()
                .HasMaxLength(15)
                .HasColumnName("booking_reference");

            builder.HasOne(x => x.Car).WithMany().IsRequired();

            builder.HasOne(x => x.User).WithMany(x => x.Bookings).IsRequired();

            builder.HasQueryFilter(x => x.IsDeleted == false);
        }
    }
}
