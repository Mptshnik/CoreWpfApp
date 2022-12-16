using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CoreWpfApp.Models
{
    class DatabaseContext : DbContext
    {
        public DbSet<Post> Posts { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<SportEvent> SportEvents { get; set; }
        public DbSet<Gun> Guns { get; set; }
        public DbSet<Equipment> Equipment { get; set; }
        public DbSet<Cheque> Cheques { get; set; }
        public DbSet<ShootingArea> ShootingAreas { get; set; }

        public DatabaseContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=shootingrangedb;Trusted_Connection=True;");
        }
    }
}
