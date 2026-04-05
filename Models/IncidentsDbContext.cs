using Microsoft.EntityFrameworkCore;

namespace IncidentAPI_X.Models   // ✅ stays Models
{
    public class IncidentsDbContext : DbContext
    {
        public IncidentsDbContext(DbContextOptions<IncidentsDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Incident> Incidents { get; set; }
    }
}