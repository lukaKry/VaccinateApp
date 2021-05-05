﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using VaccinateRegistration.Data;

namespace VaccinateRegistration.Migrations
{
    [DbContext(typeof(VaccinateDbContext))]
    partial class VaccinateDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.2");

            modelBuilder.Entity("VaccinateRegistration.Data.Registration", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("PinCode")
                        .HasColumnType("int");

                    b.Property<long>("SocialSecurityNumber")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.ToTable("Registrations");
                });

            modelBuilder.Entity("VaccinateRegistration.Data.Vaccination", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("RegistrationId")
                        .HasColumnType("int");

                    b.Property<DateTime>("VaccinationDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("RegistrationId")
                        .IsUnique();

                    b.ToTable("Vaccinations");
                });

            modelBuilder.Entity("VaccinateRegistration.Data.Vaccination", b =>
                {
                    b.HasOne("VaccinateRegistration.Data.Registration", "Registration")
                        .WithOne("Vaccination")
                        .HasForeignKey("VaccinateRegistration.Data.Vaccination", "RegistrationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Registration");
                });

            modelBuilder.Entity("VaccinateRegistration.Data.Registration", b =>
                {
                    b.Navigation("Vaccination");
                });
#pragma warning restore 612, 618
        }
    }
}