﻿// <auto-generated />
using System;
using MagicVilla_API.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace MagicVillaAPI.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240516211248_AgregarNumeroVillaTabla")]
    partial class AgregarNumeroVillaTabla
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("MagicVilla_API.Models.NumeroVilla", b =>
                {
                    b.Property<int>("villaNo")
                        .HasColumnType("int");

                    b.Property<string>("DetalleEspecial")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("FechaActualizacion")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("FechaCreacion")
                        .HasColumnType("datetime2");

                    b.Property<int>("villaId")
                        .HasColumnType("int");

                    b.HasKey("villaNo");

                    b.HasIndex("villaId");

                    b.ToTable("NumeroVillas");
                });

            modelBuilder.Entity("MagicVilla_API.Models.Villa", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Amenidad")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Detalle")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("FechaActualizacion")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("FechaCreacion")
                        .HasColumnType("datetime2");

                    b.Property<string>("ImagenUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("MetrosCuadrados")
                        .HasColumnType("int");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Ocupantes")
                        .HasColumnType("int");

                    b.Property<double>("Tarifa")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.ToTable("Villas");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Amenidad = "",
                            Detalle = "Detalle de la villa",
                            FechaActualizacion = new DateTime(2024, 5, 16, 16, 12, 47, 931, DateTimeKind.Local).AddTicks(4908),
                            FechaCreacion = new DateTime(2024, 5, 16, 16, 12, 47, 931, DateTimeKind.Local).AddTicks(4895),
                            ImagenUrl = "",
                            MetrosCuadrados = 50,
                            Nombre = "Villa real",
                            Ocupantes = 5,
                            Tarifa = 200.0
                        },
                        new
                        {
                            Id = 2,
                            Amenidad = "",
                            Detalle = "Detalle de la villa maria",
                            FechaActualizacion = new DateTime(2024, 5, 16, 16, 12, 47, 931, DateTimeKind.Local).AddTicks(4912),
                            FechaCreacion = new DateTime(2024, 5, 16, 16, 12, 47, 931, DateTimeKind.Local).AddTicks(4912),
                            ImagenUrl = "",
                            MetrosCuadrados = 120,
                            Nombre = "Villa Maria",
                            Ocupantes = 50,
                            Tarifa = 500.0
                        });
                });

            modelBuilder.Entity("MagicVilla_API.Models.NumeroVilla", b =>
                {
                    b.HasOne("MagicVilla_API.Models.Villa", "villa")
                        .WithMany()
                        .HasForeignKey("villaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("villa");
                });
#pragma warning restore 612, 618
        }
    }
}
