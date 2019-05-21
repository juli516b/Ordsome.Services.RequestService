using Domain.Requests;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class AnswerConfiguration : IEntityTypeConfiguration<Answer>
    {
        public void Configure(EntityTypeBuilder<Answer> builder)
        {
            builder.Property(e => e.Id)
                .HasColumnName("AnswerID");

            builder.Property(e => e.TextTranslated).IsRequired();
            builder.Property(e => e.UserId).IsRequired();
            builder.Property(e => e.RequestId).IsRequired();
        }
    }
}