﻿// <auto-generated />
using EFSamurai;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace EFSamurai.Data.Migrations
{
    [DbContext(typeof(SamuraiContext))]
    [Migration("20190712135346_CreateEFSamurai5")]
    partial class CreateEFSamurai5
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("EFSamurai.Quote", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("Quality");

                    b.Property<int>("SamuraiID");

                    b.Property<string>("Text");

                    b.HasKey("Id");

                    b.HasIndex("SamuraiID");

                    b.ToTable("Quotes");
                });

            modelBuilder.Entity("EFSamurai.Samurai", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("Hair");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Samurais");
                });

            modelBuilder.Entity("EFSamurai.SecretIdentity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("RealName");

                    b.Property<int>("SamuraiID");

                    b.HasKey("Id");

                    b.HasIndex("SamuraiID")
                        .IsUnique();

                    b.ToTable("SecretIdentities");
                });

            modelBuilder.Entity("EFSamurai.Quote", b =>
                {
                    b.HasOne("EFSamurai.Samurai", "Samurai")
                        .WithMany()
                        .HasForeignKey("SamuraiID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("EFSamurai.SecretIdentity", b =>
                {
                    b.HasOne("EFSamurai.Samurai", "Samurai")
                        .WithOne("SecretIdentity")
                        .HasForeignKey("EFSamurai.SecretIdentity", "SamuraiID")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
