using SkillSyncAPI.Data;
using SkillSyncAPI.Models;
using SkillSyncAPI.Repositories.Interfaces;

namespace SkillSyncAPI.Repositories.Data;

public class RoleRepository : Repository<Role>, IRoleRepository
{
    public RoleRepository(SkillSyncDbContext context)
        : base(context) { }
}
