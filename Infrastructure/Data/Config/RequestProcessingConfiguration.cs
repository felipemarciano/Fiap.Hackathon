using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config
{
    public class RequestProcessingConfiguration : IEntityTypeConfiguration<RequestProcessing>
    {
        public void Configure(EntityTypeBuilder<RequestProcessing> builder)
        {
            builder.Property(ci => ci.Id).IsRequired();

            builder.Property(cb => cb.RequestFilePath)
                .IsRequired(true)
                .HasMaxLength(2000);

            builder.Property(cb => cb.FilePath)
                .IsRequired(false)
                .HasMaxLength(2000);

            builder.Property(e => e.Status)
                .IsRequired()
                .HasConversion<int>();
        }
    }
}
