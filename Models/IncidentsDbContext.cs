using IncidentAPI_X.Models;
using Microsoft.EntityFrameworkCore;

public class IncidentsDbContext : DbContext
{
    public IncidentsDbContext(DbContextOptions<IncidentsDbContext> options)
    : base(options)
    {
    }
    public virtual DbSet<Incident> Incidents { get; set; }
}