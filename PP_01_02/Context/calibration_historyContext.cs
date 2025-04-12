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
    public class calibration_historyContext : DbContext
    {
        public DbSet<calibration_history> calibration_history { get; set; }

        public calibration_historyContext()
        {
            Database.EnsureCreated();
            calibration_history.Load();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(Config.connection, Config.version);
        }
    }
}