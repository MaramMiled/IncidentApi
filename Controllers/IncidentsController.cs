using System.Collections.Generic;
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

        [HttpPost("create-incident")]
        public IActionResult CreateIncident([FromBody] Incident incident)
        {
            if (incident == null)
            {
                return BadRequest("Incident data is required.");
            }

            incident.Status = "OPEN";
            incident.Id = _nextId;
            _nextId++;
            _incidents.Add(incident);

            return Ok(incident);
        }
    }
}
