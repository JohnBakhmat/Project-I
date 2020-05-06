using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using CourseWork.Models;

namespace CourseWork.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<CourseWork.Models.Criminal> Criminal { get; set; }
        public DbSet<CourseWork.Models.Organisation> Organisation { get; set; }
        public DbSet<CourseWork.Models.Crime> Crime { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<CriminalOrganisation>()
                .HasKey(t => new { t.CriminalId, t.OrganisationId });

            modelBuilder.Entity<CriminalOrganisation>()
                .HasOne(sc => sc.Criminal)
                .WithMany(s => s.Organisations)
                .HasForeignKey(sc => sc.CriminalId);

            modelBuilder.Entity<CriminalOrganisation>()
                .HasOne(sc => sc.Organisation)
                .WithMany(c => c.Members)
                .HasForeignKey(sc => sc.OrganisationId);


            modelBuilder.Entity<CrimeCriminal>()
                .HasKey(t => new { t.CriminalId, t.CrimeId });

            modelBuilder.Entity<CrimeCriminal>()
                .HasOne(sc => sc.Criminal)
                .WithMany(s => s.Crimes)
                .HasForeignKey(sc => sc.CriminalId);

            modelBuilder.Entity<CrimeCriminal>()
                .HasOne(sc => sc.Crime)
                .WithMany(c => c.Members)
                .HasForeignKey(sc => sc.CrimeId);
        }
    }
}