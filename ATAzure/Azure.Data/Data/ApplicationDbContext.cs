using System;
using System.Collections.Generic;
using System.Text;
using Azure.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AzureWeb.MVC.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public ApplicationDbContext()
        {
                
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionBuilder)
        {
            optionBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=AzureWeb-DB;Trusted_Connection=True;MultipleActiveResultSets=true");
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Usuário + Estado
            builder.Entity<Person>()
                .HasOne<State>(e => e.State)
                .WithMany(e => e.Persons)
                .HasForeignKey(e => e.StateId)
                .OnDelete(DeleteBehavior.NoAction);

            // Estado + País
            builder.Entity<State>()
                .HasOne<Country>(s => s.Country)
                .WithMany(s => s.States)
                .HasForeignKey(s => s.CountryId)
                .OnDelete(DeleteBehavior.NoAction);
        }

        public DbSet<Country> Countries { get; set; }
        public DbSet<State> States { get; set; }
        public DbSet<Person> Persons { get; set; }
    }
}
