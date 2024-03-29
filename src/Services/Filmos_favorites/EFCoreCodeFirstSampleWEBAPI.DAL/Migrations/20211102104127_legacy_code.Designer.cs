﻿// <auto-generated />
using EFCoreCodeFirstSampleWEBAPI.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace EFCoreCodeFirstSampleWEBAPI.Migrations
{
    [DbContext(typeof(MyAppContext))]
    [Migration("20211102104127_legacy_code")]
    partial class legacy_code
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.11")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("EFCoreCodeFirstSample.Models.Description", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Author")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DescriptionText")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Description");
                });

            modelBuilder.Entity("EFCoreCodeFirstSample.Models.Films", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Country")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("DescriptionId")
                        .HasColumnType("int");

                    b.Property<int?>("FKDescriptionId")
                        .HasColumnType("int");

                    b.Property<string>("NameFilm")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ReleaseData")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("FKDescriptionId");

                    b.ToTable("Films");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Country = "USA",
                            DescriptionId = 0,
                            NameFilm = "Hellowin",
                            ReleaseData = new DateTime(1979, 4, 25, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 2,
                            Country = "Ukraine",
                            DescriptionId = 1,
                            NameFilm = "Strangers",
                            ReleaseData = new DateTime(2021, 10, 26, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        });
                });

            modelBuilder.Entity("EFCoreCodeFirstSample.Models.ListFilms", b =>
                {
                    b.Property<int>("IdFilms")
                        .HasColumnType("int");

                    b.Property<int>("IdUser")
                        .HasColumnType("int");

                    b.HasKey("IdFilms", "IdUser");

                    b.HasIndex("IdUser");

                    b.ToTable("ListFilms");
                });

            modelBuilder.Entity("EFCoreCodeFirstSample.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("IsAdmin")
                        .HasColumnType("bit");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("EFCoreCodeFirstSample.Models.Films", b =>
                {
                    b.HasOne("EFCoreCodeFirstSample.Models.Description", "Description")
                        .WithMany()
                        .HasForeignKey("FKDescriptionId");

                    b.Navigation("Description");
                });

            modelBuilder.Entity("EFCoreCodeFirstSample.Models.ListFilms", b =>
                {
                    b.HasOne("EFCoreCodeFirstSample.Models.Films", "Films")
                        .WithMany("ListFilms")
                        .HasForeignKey("IdFilms")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EFCoreCodeFirstSample.Models.User", "User")
                        .WithMany("ListFilms")
                        .HasForeignKey("IdUser")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Films");

                    b.Navigation("User");
                });

            modelBuilder.Entity("EFCoreCodeFirstSample.Models.Films", b =>
                {
                    b.Navigation("ListFilms");
                });

            modelBuilder.Entity("EFCoreCodeFirstSample.Models.User", b =>
                {
                    b.Navigation("ListFilms");
                });
#pragma warning restore 612, 618
        }
    }
}
