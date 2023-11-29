﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using RezervasyonUcak.Models;

#nullable disable

namespace RezervasyonUcak.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20231128143608_init")]
    partial class init
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("RezervasyonUcak.Models.Firma", b =>
                {
                    b.Property<int>("FirmaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("FirmaId"));

                    b.Property<string>("FirmaAdi")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("FirmaId");

                    b.ToTable("Firma");
                });

            modelBuilder.Entity("RezervasyonUcak.Models.Koltuk", b =>
                {
                    b.Property<int>("KoltukId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("KoltukId"));

                    b.Property<bool>("DoluMu")
                        .HasColumnType("boolean");

                    b.Property<int>("UcakModelId")
                        .HasColumnType("integer");

                    b.HasKey("KoltukId");

                    b.HasIndex("UcakModelId");

                    b.ToTable("Koltuk");
                });

            modelBuilder.Entity("RezervasyonUcak.Models.Register", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Registers");
                });

            modelBuilder.Entity("RezervasyonUcak.Models.Ucak", b =>
                {
                    b.Property<int>("UcakId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("UcakId"));

                    b.Property<int>("FirmaId")
                        .HasColumnType("integer");

                    b.HasKey("UcakId");

                    b.HasIndex("FirmaId");

                    b.ToTable("Ucak");
                });

            modelBuilder.Entity("RezervasyonUcak.Models.UcakModel", b =>
                {
                    b.Property<int>("UcakModelId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("UcakModelId"));

                    b.Property<int>("KoltukSayisi")
                        .HasColumnType("integer");

                    b.Property<string>("ModelNumara")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("UcakId")
                        .HasColumnType("integer");

                    b.HasKey("UcakModelId");

                    b.HasIndex("UcakId");

                    b.ToTable("UcakModel");
                });

            modelBuilder.Entity("RezervasyonUcak.Models.Koltuk", b =>
                {
                    b.HasOne("RezervasyonUcak.Models.UcakModel", "UcakModel")
                        .WithMany("Koltuklar")
                        .HasForeignKey("UcakModelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UcakModel");
                });

            modelBuilder.Entity("RezervasyonUcak.Models.Ucak", b =>
                {
                    b.HasOne("RezervasyonUcak.Models.Firma", "Firma")
                        .WithMany("Ucaklar")
                        .HasForeignKey("FirmaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Firma");
                });

            modelBuilder.Entity("RezervasyonUcak.Models.UcakModel", b =>
                {
                    b.HasOne("RezervasyonUcak.Models.Ucak", "Ucak")
                        .WithMany("UcakModel")
                        .HasForeignKey("UcakId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Ucak");
                });

            modelBuilder.Entity("RezervasyonUcak.Models.Firma", b =>
                {
                    b.Navigation("Ucaklar");
                });

            modelBuilder.Entity("RezervasyonUcak.Models.Ucak", b =>
                {
                    b.Navigation("UcakModel");
                });

            modelBuilder.Entity("RezervasyonUcak.Models.UcakModel", b =>
                {
                    b.Navigation("Koltuklar");
                });
#pragma warning restore 612, 618
        }
    }
}
