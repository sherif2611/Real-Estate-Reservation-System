﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RealEstate.Models;

#nullable disable

namespace RealEstate.Migrations
{
    [DbContext(typeof(RealEstateReservationDbContext))]
    [Migration("20231211161445_init")]
    partial class init
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("RealEstate.Models.Admin", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("PersonId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PersonId")
                        .IsUnique();

                    b.ToTable("Admins");
                });

            modelBuilder.Entity("RealEstate.Models.Imgs", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<byte[]>("Img")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<int>("RealEstateId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RealEstateId");

                    b.ToTable("Imgs");
                });

            modelBuilder.Entity("RealEstate.Models.Person", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("People");
                });

            modelBuilder.Entity("RealEstate.Models.RealEstate", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("AdminId")
                        .HasColumnType("int");

                    b.Property<bool>("Available")
                        .HasColumnType("bit");

                    b.Property<string>("Category")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Confirmed")
                        .HasColumnType("bit");

                    b.Property<byte[]>("Credentail")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("Desc")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("PublishingDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AdminId");

                    b.HasIndex("UserId");

                    b.ToTable("RealEstates");
                });

            modelBuilder.Entity("RealEstate.Models.RealEstateReservations", b =>
                {
                    b.Property<int>("RealEstateId")
                        .HasColumnType("int");

                    b.Property<DateTime>("ReservationDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("RealEstateId");

                    b.HasIndex("UserId");

                    b.ToTable("RealEstateReservations");
                });

            modelBuilder.Entity("RealEstate.Models.Support", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("PersonId")
                        .HasColumnType("int");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("PersonId")
                        .IsUnique();

                    b.ToTable("Supports");
                });

            modelBuilder.Entity("RealEstate.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("PersonId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PersonId")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("RealEstate.Models.Admin", b =>
                {
                    b.HasOne("RealEstate.Models.Person", "Person")
                        .WithOne("Admin")
                        .HasForeignKey("RealEstate.Models.Admin", "PersonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Person");
                });

            modelBuilder.Entity("RealEstate.Models.Imgs", b =>
                {
                    b.HasOne("RealEstate.Models.RealEstate", "RealEstate")
                        .WithMany("Imgs")
                        .HasForeignKey("RealEstateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("RealEstate");
                });

            modelBuilder.Entity("RealEstate.Models.RealEstate", b =>
                {
                    b.HasOne("RealEstate.Models.Admin", "Admin")
                        .WithMany("RealEstates")
                        .HasForeignKey("AdminId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RealEstate.Models.User", "User")
                        .WithMany("RealEstates")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Admin");

                    b.Navigation("User");
                });

            modelBuilder.Entity("RealEstate.Models.RealEstateReservations", b =>
                {
                    b.HasOne("RealEstate.Models.RealEstate", "RealEstate")
                        .WithMany()
                        .HasForeignKey("RealEstateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RealEstate.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("RealEstate");

                    b.Navigation("User");
                });

            modelBuilder.Entity("RealEstate.Models.Support", b =>
                {
                    b.HasOne("RealEstate.Models.Person", "Person")
                        .WithOne("Support")
                        .HasForeignKey("RealEstate.Models.Support", "PersonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Person");
                });

            modelBuilder.Entity("RealEstate.Models.User", b =>
                {
                    b.HasOne("RealEstate.Models.Person", "Person")
                        .WithOne("User")
                        .HasForeignKey("RealEstate.Models.User", "PersonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Person");
                });

            modelBuilder.Entity("RealEstate.Models.Admin", b =>
                {
                    b.Navigation("RealEstates");
                });

            modelBuilder.Entity("RealEstate.Models.Person", b =>
                {
                    b.Navigation("Admin")
                        .IsRequired();

                    b.Navigation("Support")
                        .IsRequired();

                    b.Navigation("User")
                        .IsRequired();
                });

            modelBuilder.Entity("RealEstate.Models.RealEstate", b =>
                {
                    b.Navigation("Imgs");
                });

            modelBuilder.Entity("RealEstate.Models.User", b =>
                {
                    b.Navigation("RealEstates");
                });
#pragma warning restore 612, 618
        }
    }
}
