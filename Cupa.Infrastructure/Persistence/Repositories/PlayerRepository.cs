namespace Cupa.Infrastructure.Persistence.Repositories;
internal sealed class PlayerRepository : BaseRepository<Player>, IPlayerRepository
{
    public PlayerRepository(ApplicationDbContext context) : base(context) { }
}