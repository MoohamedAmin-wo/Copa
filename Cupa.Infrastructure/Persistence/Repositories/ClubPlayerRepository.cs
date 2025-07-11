namespace Cupa.Infrastructure.Persistence.Repositories;
internal sealed class ClubPlayerRepository : BaseRepository<ClubPlayer>, IClubPlayerRepository
{
    public ClubPlayerRepository(ApplicationDbContext context) : base(context) { }
}