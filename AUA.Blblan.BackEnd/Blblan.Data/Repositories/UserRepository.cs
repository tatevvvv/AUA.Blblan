using Blblan.Data.Entities;

namespace Blblan.Data.Repositories
{
    public class UserRepository : Repository<User>
    {
        public UserRepository(BlblanDbContext context) : base(context)
        {
        }
    }
}
