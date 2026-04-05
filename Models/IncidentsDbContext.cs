using Microsoft.AspNetCore.Mvc;
using IncidentAPI_X.Models;
using System.Linq;

namespace IncidentAPI_X.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IncidentsDbController : ControllerBase
    {
        private readonly IncidentsDbContext _context;

        private static readonly string[] AllowedSeverities = { "LOW", "MEDIUM", "HIGH", "CRITICAL" };
        private static readonly string[] AllowedStatuses = { "OPEN", "IN_PROGRESS", "RESOLVED" };
        public IncidentsDbController(IncidentsDbContext context)
        {
            _context = context;
        }

        // ✅ FILTER BY STATUS (SYNC + PARTIAL MATCH)
        // GET: api/IncidentsDb/filter/status/open
        [HttpGet("filter/status/{status}")]
        public IActionResult FilterByStatus(string status)
        {
            if (string.IsNullOrWhiteSpace(status))
                return BadRequest("Status is required.");

            var incidents = _context.Incidents
                .Where(i => i.Status.ToLower().Contains(status.ToLower()))
                .ToList();

            if (incidents.Count == 0)
                return NotFound("No incidents found with this status.");

            return Ok(incidents);
        }

        // ✅ FILTER BY SEVERITY (SYNC + PARTIAL MATCH)
        // GET: api/IncidentsDb/filter/severity/high
        [HttpGet("filter/severity/{severity}")]
        public IActionResult FilterBySeverity(string severity)
        {
            if (string.IsNullOrWhiteSpace(severity))
                return BadRequest("Severity is required.");

            var incidents = _context.Incidents
                .Where(i => i.Severity.ToLower().Contains(severity.ToLower()))
                .ToList();

            if (incidents.Count == 0)
                return NotFound("No incidents found with this severity.");

            return Ok(incidents);
        }

        [HttpPost]
        public IActionResult PostIncident(Incident incident)
        {
            incident.Status = "OPEN";
            incident.CreatedAt = DateTime.Now;

            _context.Incidents.Add(incident);
            _context.SaveChanges();

            return Ok(incident);
        }
    }
}