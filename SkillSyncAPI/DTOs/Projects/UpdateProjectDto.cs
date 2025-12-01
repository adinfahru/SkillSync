using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SkillSyncAPI.DTOs.Projects
{
    public class UpdateProjectDto
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Status { get; set; }
    }
}
