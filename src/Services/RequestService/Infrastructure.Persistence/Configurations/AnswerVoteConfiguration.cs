using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RequestService.Domain.Requests;

namespace RequestService.Infrastructure.Persistence.Configurations
{
    public class AnswerVoteConfiguration : IEntityTypeConfiguration<AnswerVote>
    {
        public void Configure(EntityTypeBuilder<AnswerVote> builder)
        {
            builder.Property(e => e.Id)
                .HasColumnName("AnswerVoteID");

            builder.Property(e => e.AnswerId).IsRequired();
            builder.Property(e => e.UserId).IsRequired();
        }
    }
}