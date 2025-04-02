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
    public class equipment_typeContext : DbContext
    {
        public DbSet<equipment_type> equipment_type { get; set; }

        public equipment_typeContext()
        {
            Database.EnsureCreated();
            equipment_type.Load();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(Config.connection, Config.version);
        }
    }
}