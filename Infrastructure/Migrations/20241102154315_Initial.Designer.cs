﻿// <auto-generated />
using System;
using Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Infrastructure.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20241102154315_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Core.Entity.ContatoModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("CTT_ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ContatoDDD")
                        .HasColumnType("int")
                        .HasColumnName("CTT_DDD");

                    b.Property<string>("ContatoEmail")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("CTT_EMAIL");

                    b.Property<string>("ContatoNome")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("CTT_NOME");

                    b.Property<string>("ContatoNumero")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("CTT_NUMERO");

                    b.Property<DateTime>("DataCriacao")
                        .HasColumnType("datetime2")
                        .HasColumnName("CTT_DTCRIACAO");

                    b.HasKey("Id");

                    b.ToTable("CONTATO_CTT");
                });

            modelBuilder.Entity("Core.Entity.MunicipioModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("MNC_ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("Capital")
                        .HasColumnType("bit")
                        .HasColumnName("MNC_CAPITAL");

                    b.Property<int>("DDD")
                        .HasColumnType("int")
                        .HasColumnName("MNC_DDD");

                    b.Property<int>("IdIbge")
                        .HasColumnType("int")
                        .HasColumnName("MNC_IBGE");

                    b.Property<float>("Latidude")
                        .HasColumnType("real")
                        .HasColumnName("MNC_LAT");

                    b.Property<float>("Longitude")
                        .HasColumnType("real")
                        .HasColumnName("MNC_LONG");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("MNC_NOME");

                    b.Property<string>("SiafiId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("MNC_SIAFI");

                    b.Property<int>("UfId")
                        .HasColumnType("int")
                        .HasColumnName("MNC_UF");

                    b.HasKey("Id");

                    b.ToTable("MUNICIPIO_MNC");
                });
#pragma warning restore 612, 618
        }
    }
}