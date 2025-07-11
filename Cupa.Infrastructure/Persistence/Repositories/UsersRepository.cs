namespace Cupa.Infrastructure.Persistence.Repositories
{
    public class UsersRepository : BaseRepository<ApplicationUser>, IUsersRepository
    {
        public UsersRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
