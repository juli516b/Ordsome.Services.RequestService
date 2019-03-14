using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RequestService.WebApi.Domain.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RequestService.WebApi.Infrastructure.Persistence.Configurations
{
    public class RequestConfiguration : IEntityTypeConfiguration<Request>
    {
        public void Configure(EntityTypeBuilder<Request> builder)
        {
            builder.Property(e => e.Id)
                .HasColumnName("RequestID");

            builder.Property(e => e.LanguageTarget).IsRequired();
            builder.Property(e => e.TextToTranslate).IsRequired();
        }
    }
}
