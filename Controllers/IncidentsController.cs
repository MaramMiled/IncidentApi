using System.Collections.Generic;
using System.Linq;
using IncidentAPI_X.Models;
using Microsoft.AspNetCore.Mvc;

namespace IncidentAPI_X.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class IncidentsController : ControllerBase
    {
        private static readonly List<Incident> _incidents = new();
        private static int _nextId = 1;

        private static readonly string[] AllowedSeverities = { "LOW", "MEDIUM", "HIGH", "CRITICAL" };
        private static readonly string[] AllowedStatuses = { "OPEN", "IN_PROGRESS", "RESOLVED" };

        // 1️⃣ CREATE INCIDENT
        [HttpPost("create-incident")]
        public IActionResult CreateIncident([FromBody] Incident incident)
        {
            if (incident == null)
                return BadRequest("Incident data is required.");

            if (!AllowedSeverities.Contains(incident.Severity))
                return BadRequest("Invalid severity value.");

            incident.Id = _nextId++;
            incident.Status = "OPEN";

            _incidents.Add(incident);

            return Ok(incident);
        }

        // 2️⃣ GET ALL INCIDENTS
        [HttpGet("get-all")]
        public IActionResult GetAllIncidents()
        {
            return Ok(_incidents);
        }

        // 3️⃣ GET INCIDENT BY ID
        [HttpGet("getbyid/{id}")]
        public IActionResult GetIncidentById(int id)
        {
            var incident = _incidents.FirstOrDefault(i => i.Id == id);

            if (incident == null)
                return NotFound();

            return Ok(incident);
        }

        // 4️⃣ UPDATE INCIDENT STATUS
        [HttpPut("update-status/{id}")]
        public IActionResult UpdateIncidentStatus(int id, [FromBody] string status)
        {
            var incident = _incidents.FirstOrDefault(i => i.Id == id);

            if (incident == null)
                return NotFound();

            if (!AllowedStatuses.Contains(status))
                return BadRequest("Invalid status value.");

            incident.Status = status;

            return Ok(incident);
        }

        // 5️⃣ DELETE INCIDENT
        [HttpDelete("delete-incident/{id}")]
        public IActionResult DeleteIncident(int id)
        {
            var incident = _incidents.FirstOrDefault(i => i.Id == id);

            if (incident == null)
                return NotFound();

            if (incident.Severity == "CRITICAL" && incident.Status == "OPEN")
                return BadRequest("Cannot delete an OPEN CRITICAL incident.");

            _incidents.Remove(incident);

            return NoContent();
        }

        // 6️⃣ FILTER BY STATUS
        [HttpGet("filter-by-status/{status}")]
        public IActionResult FilterByStatus(string status)
        {
            if (string.IsNullOrWhiteSpace(status))
                return BadRequest("Status value is required.");

            var filtered = _incidents
                .Where(i => i.Status != null &&
                            i.Status.Contains(status, StringComparison.OrdinalIgnoreCase))
                .ToList();

            return Ok(filtered);
        }


        // 7️⃣ FILTER BY SEVERITY
        [HttpGet("filter-by-severity/{severity}")]
        public IActionResult FilterBySeverity(string severity)
        {
            if (string.IsNullOrWhiteSpace(severity))
                return BadRequest("Severity value is required.");

            var filtered = _incidents
                .Where(i => i.Severity != null &&
                            i.Severity.Contains(severity, StringComparison.OrdinalIgnoreCase))
                .ToList();

            return Ok(filtered);
        }

    }
}
