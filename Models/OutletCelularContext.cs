using Microsoft.EntityFrameworkCore;

namespace OutletCelular.Models
{
    public class OutletCelularContext : DbContext
    {
        public OutletCelularContext(DbContextOptions<OutletCelularContext> options)
            : base(options)
        {
        }

        public DbSet<Accesorio> Accesorios { get; set; }
    }
}
