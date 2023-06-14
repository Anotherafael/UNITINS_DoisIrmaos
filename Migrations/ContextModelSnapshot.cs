﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using UNITINS_DoisIrmaos.DAL;

#nullable disable

namespace UNITINS_DoisIrmaos.Migrations
{
    [DbContext(typeof(Context))]
    partial class ContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("UNITINS_DoisIrmaos.Models.Acessory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("Price")
                        .HasColumnType("real");

                    b.HasKey("Id");

                    b.ToTable("Acessories");
                });

            modelBuilder.Entity("UNITINS_DoisIrmaos.Models.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("UNITINS_DoisIrmaos.Models.CategoryFeature", b =>
                {
                    b.Property<int>("FeatureID")
                        .HasColumnType("int");

                    b.Property<int>("CategoryID")
                        .HasColumnType("int");

                    b.HasKey("FeatureID", "CategoryID");

                    b.HasIndex("CategoryID");

                    b.ToTable("CategoryFeatures");
                });

            modelBuilder.Entity("UNITINS_DoisIrmaos.Models.Client", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Cnh")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ConfirmPassword")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Clients");
                });

            modelBuilder.Entity("UNITINS_DoisIrmaos.Models.Employee", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Cpf")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Personnel");
                });

            modelBuilder.Entity("UNITINS_DoisIrmaos.Models.Feature", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Features");
                });

            modelBuilder.Entity("UNITINS_DoisIrmaos.Models.Protection", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("PricePerDay")
                        .HasColumnType("real");

                    b.HasKey("Id");

                    b.ToTable("Protections");
                });

            modelBuilder.Entity("UNITINS_DoisIrmaos.Models.Rent", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("BuyerID")
                        .HasColumnType("int");

                    b.Property<int>("CategoryID")
                        .HasColumnType("int");

                    b.Property<int>("DriverID")
                        .HasColumnType("int");

                    b.Property<int>("EmployeeID")
                        .HasColumnType("int");

                    b.Property<DateTime>("EndAt")
                        .HasColumnType("datetime2");

                    b.Property<float>("Price")
                        .HasColumnType("real");

                    b.Property<int>("ProtectionID")
                        .HasColumnType("int");

                    b.Property<DateTime>("ReturnedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("StartAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("TakenAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("VehicleID")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BuyerID");

                    b.HasIndex("CategoryID");

                    b.HasIndex("DriverID");

                    b.HasIndex("EmployeeID");

                    b.HasIndex("ProtectionID");

                    b.HasIndex("VehicleID");

                    b.ToTable("Rents");
                });

            modelBuilder.Entity("UNITINS_DoisIrmaos.Models.RentAcessory", b =>
                {
                    b.Property<int>("RentID")
                        .HasColumnType("int");

                    b.Property<int>("AcessoryID")
                        .HasColumnType("int");

                    b.HasKey("RentID", "AcessoryID");

                    b.HasIndex("AcessoryID");

                    b.ToTable("RentAcessories");
                });

            modelBuilder.Entity("UNITINS_DoisIrmaos.Models.RentTax", b =>
                {
                    b.Property<int>("RentID")
                        .HasColumnType("int");

                    b.Property<int>("TaxID")
                        .HasColumnType("int");

                    b.HasKey("RentID", "TaxID");

                    b.HasIndex("TaxID");

                    b.ToTable("RentTaxes");
                });

            modelBuilder.Entity("UNITINS_DoisIrmaos.Models.Tax", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("PricePerDay")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.ToTable("Taxes");
                });

            modelBuilder.Entity("UNITINS_DoisIrmaos.Models.Vehicle", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<bool>("Available")
                        .HasColumnType("bit");

                    b.Property<int>("CategoryID")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CategoryID");

                    b.ToTable("Vehicles");
                });

            modelBuilder.Entity("UNITINS_DoisIrmaos.Models.CategoryFeature", b =>
                {
                    b.HasOne("UNITINS_DoisIrmaos.Models.Category", "Category")
                        .WithMany("Features")
                        .HasForeignKey("CategoryID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("UNITINS_DoisIrmaos.Models.Feature", "Feature")
                        .WithMany("Categories")
                        .HasForeignKey("FeatureID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("Feature");
                });

            modelBuilder.Entity("UNITINS_DoisIrmaos.Models.Rent", b =>
                {
                    b.HasOne("UNITINS_DoisIrmaos.Models.Client", "Buyer")
                        .WithMany()
                        .HasForeignKey("BuyerID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("UNITINS_DoisIrmaos.Models.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("UNITINS_DoisIrmaos.Models.Client", "Driver")
                        .WithMany()
                        .HasForeignKey("DriverID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("UNITINS_DoisIrmaos.Models.Employee", "Employee")
                        .WithMany()
                        .HasForeignKey("EmployeeID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("UNITINS_DoisIrmaos.Models.Protection", "Protection")
                        .WithMany()
                        .HasForeignKey("ProtectionID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("UNITINS_DoisIrmaos.Models.Vehicle", "Vehicle")
                        .WithMany()
                        .HasForeignKey("VehicleID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Buyer");

                    b.Navigation("Category");

                    b.Navigation("Driver");

                    b.Navigation("Employee");

                    b.Navigation("Protection");

                    b.Navigation("Vehicle");
                });

            modelBuilder.Entity("UNITINS_DoisIrmaos.Models.RentAcessory", b =>
                {
                    b.HasOne("UNITINS_DoisIrmaos.Models.Acessory", "Acessory")
                        .WithMany("Rents")
                        .HasForeignKey("AcessoryID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("UNITINS_DoisIrmaos.Models.Rent", "Rent")
                        .WithMany("Acessories")
                        .HasForeignKey("RentID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Acessory");

                    b.Navigation("Rent");
                });

            modelBuilder.Entity("UNITINS_DoisIrmaos.Models.RentTax", b =>
                {
                    b.HasOne("UNITINS_DoisIrmaos.Models.Rent", "Rent")
                        .WithMany("Taxes")
                        .HasForeignKey("RentID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("UNITINS_DoisIrmaos.Models.Tax", "Tax")
                        .WithMany("Rents")
                        .HasForeignKey("TaxID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Rent");

                    b.Navigation("Tax");
                });

            modelBuilder.Entity("UNITINS_DoisIrmaos.Models.Vehicle", b =>
                {
                    b.HasOne("UNITINS_DoisIrmaos.Models.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("UNITINS_DoisIrmaos.Models.Acessory", b =>
                {
                    b.Navigation("Rents");
                });

            modelBuilder.Entity("UNITINS_DoisIrmaos.Models.Category", b =>
                {
                    b.Navigation("Features");
                });

            modelBuilder.Entity("UNITINS_DoisIrmaos.Models.Feature", b =>
                {
                    b.Navigation("Categories");
                });

            modelBuilder.Entity("UNITINS_DoisIrmaos.Models.Rent", b =>
                {
                    b.Navigation("Acessories");

                    b.Navigation("Taxes");
                });

            modelBuilder.Entity("UNITINS_DoisIrmaos.Models.Tax", b =>
                {
                    b.Navigation("Rents");
                });
#pragma warning restore 612, 618
        }
    }
}
