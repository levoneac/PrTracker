using Microsoft.EntityFrameworkCore;
using PrTracker.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrTracker.Data
{
    public class LiftContext : DbContext
    {
        public LiftContext()
        {

           
            Trace.WriteLine($"ENSURE CREATED: {Database.EnsureCreated()}");
            //Database.Migrate();
        }
        public DbSet<Lifts> Lifts { get; set; } = null!;
        public DbSet<RecordedLifts> RecordedLifts { get; set; } = null!;
        public DbSet<People> People { get; set; } = null!;
        public DbSet<MuscleGroups> MuscleGroups { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(ConfigurationManager.ConnectionStrings["Default"].ConnectionString, options =>
            {
                
            });
        }
    }
}
