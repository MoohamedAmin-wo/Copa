namespace Cupa.Infrastructure.Persistence.Repositories
{
    internal sealed class PositionRepository : BaseRepository<Position>, IPositionRepository
    {
        public PositionRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
