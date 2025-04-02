using Microsoft.EntityFrameworkCore;
using PP_01_02.Context.Database;
using PP_01_02.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PP_01_02.Context
{
    public class calibrationContext : DbContext
    {
        public DbSet<calibration> calibration { get; set; }

        public calibrationContext()
        {
            Database.EnsureCreated();
            calibration.Load();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(Config.connection, Config.version);
        }
    }
}