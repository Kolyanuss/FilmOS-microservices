using EFCoreCodeFirstSampleWEBAPI.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace EFCoreCodeFirstSampleWEBAPI.DAL
{
    public class MyAppContext : DbContext
    {
        public DbSet<Films> Films { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<FilmsUsers> FilmsUsers { get; set; }
        public DbSet<Genres> Genres { get; set; }
        public DbSet<Description> Description { get; set; }

        public MyAppContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasKey(u => u.Id);
            modelBuilder.Entity<Genres>().HasKey(u => u.Id);
            modelBuilder.Entity<FilmsUsers>().HasKey(u => new { u.IdFilms, u.IdUser });
            modelBuilder.Entity<FilmsGenres>().HasKey(u => new { u.IdFilms, u.IdGenres });

            modelBuilder.Entity<FilmsUsers>()
                .HasOne(p => p.Films)
            .WithMany(t => t.FilmsUsers)
            .HasForeignKey(p => p.IdFilms);

            modelBuilder.Entity<FilmsUsers>()
                .HasOne(p => p.User)
            .WithMany(t => t.FilmsUsers)
            .HasForeignKey(p => p.IdUser);


            modelBuilder.Entity<FilmsGenres>()
                .HasOne(p => p.Films)
            .WithMany(t => t.FilmsGenres)
            .HasForeignKey(p => p.IdFilms);

            modelBuilder.Entity<FilmsGenres>()
                .HasOne(p => p.Genres)
            .WithMany(t => t.FilmsGenres)
            .HasForeignKey(p => p.IdGenres);


            modelBuilder.Entity<Description>().HasData(
            new Description
            {
                Id = 1,
                DescriptionText = "Best film",
                Author = "Husmant"
            },
            new Description
            {
                Id = 2,
                DescriptionText = "Almost best film",
                Author = "Husmant2"
            });

            modelBuilder.Entity<Films>().HasData(
            new Films
            {
                Id = 1,
                NameFilm = "Hellowin",
                ReleaseData = new DateTime(1979, 04, 25),
                Country = "USA",
                FKDescriptionId = 1,

            },
            new Films
            {
                Id = 2,
                NameFilm = "Strangers",
                ReleaseData = new DateTime(2021, 10, 26),
                Country = "Ukraine",
                FKDescriptionId = 2,
            });

            modelBuilder.Entity<User>().HasData(
            new User
            {
                Id = 1,
                UserName = "Nikolay",
                IsAdmin = true,
                Password = "1234",
            },
            new User
            {
                Id = 2,
                UserName = "Vasya",
                IsAdmin = false,
                Password = "111",
            });

            modelBuilder.Entity<FilmsUsers>().HasData(
            new FilmsUsers
            {
                IdFilms = 1,
                IdUser = 2
            },
            new FilmsUsers
            {
                IdFilms = 2,
                IdUser = 2
            });
        }
    }
}
