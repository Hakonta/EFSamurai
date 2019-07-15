﻿// <auto-generated />
using System;
using EFSamurai;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace EFSamurai.Data.Migrations
{
    [DbContext(typeof(SamuraiContext))]
    partial class SamuraiContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("EFSamurai.Battle", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description");

                    b.Property<DateTime>("EndDate");

                    b.Property<bool>("IsBrutal");

                    b.Property<string>("Name");

                    b.Property<DateTime>("StartDate");

                    b.HasKey("Id");

                    b.ToTable("Battle");
                });

            modelBuilder.Entity("EFSamurai.BattleEvent", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("BattleLogId");

                    b.Property<string>("Description");

                    b.Property<int>("Order");

                    b.Property<string>("Summary");

                    b.HasKey("Id");

                    b.HasIndex("BattleLogId");

                    b.ToTable("BattleEvents");
                });

            modelBuilder.Entity("EFSamurai.BattleLog", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("BattleId");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.HasIndex("BattleId")
                        .IsUnique();

                    b.ToTable("BattleLogs");
                });

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

            modelBuilder.Entity("EFSamurai.SamuraiBattle", b =>
                {
                    b.Property<int>("SamuraiId");

                    b.Property<int>("BattleID");

                    b.HasKey("SamuraiId", "BattleID");

                    b.HasIndex("BattleID");

                    b.ToTable("SamuraiBattles");
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

            modelBuilder.Entity("EFSamurai.BattleEvent", b =>
                {
                    b.HasOne("EFSamurai.BattleLog", "BattleLog")
                        .WithMany("BattleEvents")
                        .HasForeignKey("BattleLogId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("EFSamurai.BattleLog", b =>
                {
                    b.HasOne("EFSamurai.Battle", "Battle")
                        .WithOne("BattleLog")
                        .HasForeignKey("EFSamurai.BattleLog", "BattleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("EFSamurai.Quote", b =>
                {
                    b.HasOne("EFSamurai.Samurai", "Samurai")
                        .WithMany()
                        .HasForeignKey("SamuraiID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("EFSamurai.SamuraiBattle", b =>
                {
                    b.HasOne("EFSamurai.Battle", "battle")
                        .WithMany()
                        .HasForeignKey("BattleID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("EFSamurai.Samurai", "Samurai")
                        .WithMany()
                        .HasForeignKey("SamuraiId")
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
