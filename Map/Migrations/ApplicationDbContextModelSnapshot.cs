﻿// <auto-generated />
using System;
using Map.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Map.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.3")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Map.Models.LogisticCenter", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("LogisticCenters");
                });

            modelBuilder.Entity("Map.Models.Region", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.HasKey("Id");

                    b.ToTable("Regions");
                });

            modelBuilder.Entity("Map.Models.Route", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("EndId")
                        .HasColumnType("int");

                    b.Property<int>("Length")
                        .HasColumnType("int");

                    b.Property<int?>("RegionId")
                        .HasColumnType("int");

                    b.Property<int?>("StartId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("EndId");

                    b.HasIndex("RegionId");

                    b.HasIndex("StartId");

                    b.ToTable("Routes");
                });

            modelBuilder.Entity("Map.Models.Town", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("RegionId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RegionId");

                    b.ToTable("Towns");
                });

            modelBuilder.Entity("Map.Models.Route", b =>
                {
                    b.HasOne("Map.Models.Town", "End")
                        .WithMany()
                        .HasForeignKey("EndId");

                    b.HasOne("Map.Models.Region", null)
                        .WithMany("Routes")
                        .HasForeignKey("RegionId");

                    b.HasOne("Map.Models.Town", "Start")
                        .WithMany()
                        .HasForeignKey("StartId");

                    b.Navigation("End");

                    b.Navigation("Start");
                });

            modelBuilder.Entity("Map.Models.Town", b =>
                {
                    b.HasOne("Map.Models.Region", null)
                        .WithMany("Towns")
                        .HasForeignKey("RegionId");
                });

            modelBuilder.Entity("Map.Models.Region", b =>
                {
                    b.Navigation("Routes");

                    b.Navigation("Towns");
                });
#pragma warning restore 612, 618
        }
    }
}
