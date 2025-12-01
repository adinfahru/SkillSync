using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SkillSyncAPI.DTOs.Projects
{
    public class ProjectResponseDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
