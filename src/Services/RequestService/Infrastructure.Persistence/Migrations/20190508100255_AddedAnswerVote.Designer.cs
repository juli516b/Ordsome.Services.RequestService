// <auto-generated />

using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Persistence.Migrations
{
    [DbContext(typeof(RequestServiceDbContext))]
    [Migration("20190508100255_AddedAnswerVote")]
    partial class AddedAnswerVote
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("RequestService.Domain.Requests.Answer", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("AnswerID")
                    .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                b.Property<bool>("IsPreferred");

                b.Property<int>("RequestId");

                b.Property<string>("TextTranslated")
                    .IsRequired();

                b.Property<Guid>("UserId");

                b.HasKey("Id");

                b.HasIndex("RequestId");

                b.ToTable("Answers");
            });

            modelBuilder.Entity("RequestService.Domain.Requests.AnswerVote", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd()
                    .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                b.Property<int>("AnswerId");

                b.Property<bool>("Like");

                b.Property<Guid>("UserId");

                b.HasKey("Id");

                b.HasIndex("AnswerId");

                b.ToTable("AnswerVotes");
            });

            modelBuilder.Entity("RequestService.Domain.Requests.Request", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("RequestID")
                    .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                b.Property<bool>("IsClosed");

                b.Property<string>("LanguageOrigin");

                b.Property<string>("LanguageTarget")
                    .IsRequired();

                b.Property<string>("TextToTranslate")
                    .IsRequired();

                b.HasKey("Id");

                b.ToTable("Requests");
            });

            modelBuilder.Entity("RequestService.Domain.Requests.Answer", b =>
            {
                b.HasOne("RequestService.Domain.Requests.Request", "Request")
                    .WithMany("Answers")
                    .HasForeignKey("RequestId")
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity("RequestService.Domain.Requests.AnswerVote", b =>
            {
                b.HasOne("RequestService.Domain.Requests.Answer", "Answer")
                    .WithMany()
                    .HasForeignKey("AnswerId")
                    .OnDelete(DeleteBehavior.Cascade);
            });
#pragma warning restore 612, 618
        }
    }
}