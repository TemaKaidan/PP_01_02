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
    public class employeesContext : DbContext
    {
        public DbSet<employees> employees { get; set; }

        public employeesContext()
        {
            Database.EnsureCreated();
            employees.Load();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(Config.connection, Config.version);
        }
    }
}