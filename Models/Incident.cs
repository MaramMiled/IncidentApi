using System;
using System.ComponentModel.DataAnnotations;

namespace IncidentAPI_X.Models
{
    public class Incident
    {
        [Key]
        public int Id { get; set; }  // Auto-increment by default in EF

        [Required]
        [MaxLength(30)]
        public string Title { get; set; }

        [Required]
        [MaxLength(200)]
        public string Description { get; set; }

        [Required]
        public string Severity { get; set; } // LOW, MEDIUM, HIGH, CRITICAL

        [Required]
        public string Status { get; set; } // OPEN, IN_PROGRESS, RESOLVED

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
