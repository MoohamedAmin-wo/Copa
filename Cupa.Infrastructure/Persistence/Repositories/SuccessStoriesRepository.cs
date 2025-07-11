namespace Cupa.Infrastructure.Persistence.Repositories
{
    public class SuccessStoriesRepository : BaseRepository<SuccessStories>, ISuccessStroriesRepository
    {
        public SuccessStoriesRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
