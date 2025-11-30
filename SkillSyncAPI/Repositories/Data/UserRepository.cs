using Microsoft.EntityFrameworkCore;
using SkillSyncAPI.Data;
using SkillSyncAPI.Models;
using SkillSyncAPI.Repositories.Interfaces;

namespace SkillSyncAPI.Repositories.Data;

public class UserRepository : Repository<User>, IUserRepository
{
    public UserRepository(SkillSyncDbContext context)
        : base(context) { }

    public async Task<User?> GetByIdWithRoleAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _context
            .User.Include(u => u.Role)
            .FirstOrDefaultAsync(u => u.Id == id, cancellationToken);
    }

    public async Task<IEnumerable<User>> GetAllWithRoleAsync(CancellationToken cancellationToken)
    {
        return await _context.User.Include(u => u.Role).ToListAsync(cancellationToken);
    }
}
