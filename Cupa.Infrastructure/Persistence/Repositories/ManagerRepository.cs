namespace Cupa.Infrastructure.Persistence.Repositories
{
    public class ManagerRepository : BaseRepository<Manager>, IManagerRepository
    {
        public ManagerRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
