namespace Cupa.Infrastructure.Persistence.Repositories
{
    public class ClubRepository : BaseRepository<Club>, IClubRepository
    {
        public ClubRepository(ApplicationDbContext context) : base(context) { }
    }
}
