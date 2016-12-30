using Dal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Dal
{
    public class BusinessProContext : DbContext
    {
        public BusinessProContext(DbContextOptions<BusinessProContext> options) : base(options) { }

        //TODO: Add Models here
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Person> People { get; set; }
        public DbSet<Role> Roles { get; set; }
        //public DbSet<PersonRole> PersonRoles { get; set; }       

        //ManytoMany for PersonRole class
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PersonRole>()
                .HasKey(t => new { t.PersonId, t.RoleId });

            modelBuilder.Entity<PersonRole>()
                .HasOne(pt => pt.Person)
                .WithMany(p => p.PersonRole)
                .HasForeignKey(pt => pt.PersonId);

            modelBuilder.Entity<PersonRole>()
                .HasOne(pt => pt.Role)
                .WithMany(t => t.PersonRole)
                .HasForeignKey(pt => pt.RoleId);
        }

    }
}
