using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeFirstDB
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options):base(options)
        {

        }
        public DbSet<Department> Departments { get; set; }

        public DbSet <Employee> Employees { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured) {

                optionsBuilder.UseSqlServer("data source=DESKTOP-LT97L00\\SQLEXPRESS;initial catalog=EFCoreTest;persist security info=True;user id=sa;password=India123");
            }
            base.OnConfiguring(optionsBuilder);
        }

    }
}
