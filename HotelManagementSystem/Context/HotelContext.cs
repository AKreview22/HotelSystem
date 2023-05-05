using HotelManagementSystem.Context.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagementSystem.Context
{
    public class HotelContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer(@"Data Source=.;initial Catalog=HotelDB;Integrated Security=true;Encrypt=false");

        public DbSet<Login> Logins { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
    }
}
