using Microsoft.EntityFrameworkCore;
using PrTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrTracker.Data
{
    class LiftContext : DbContext
    {
        public DbSet<LiftTypes> LiftTypes { get; set; } = null!;
        public DbSet<RecordedLifts> RecordedLifts { get; set; } = null!;
        public DbSet<People> People { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("connectionstring", options =>
            {
                
            });
        }
    }
}
