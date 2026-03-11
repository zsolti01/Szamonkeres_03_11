using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;


namespace Szamonkeres_03_11
{
    public class LibraryContext : DbContext
    {
        public DbSet<Book> Books { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                "Server=.;Database=konyvtar;Trusted_Connection=True;");
        }
    }
}
