using FilmesAPIServer.Models;
using Microsoft.EntityFrameworkCore;

namespace FilmesAPIServer.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<Filmes> filmes { get; set; }
    }
}
