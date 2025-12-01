using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SkillSyncAPI.DTOs.Projects
{
    public class CreateProjectDto
    {
        public required string Name { get; set; }
        public required string Description { get; set; }
        public string Status { get; set; } = "Active";
    }
}
