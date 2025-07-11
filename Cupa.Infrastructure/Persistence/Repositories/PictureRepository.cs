
namespace Cupa.Infrastructure.Persistence.Repositories
{
    public sealed class PictureRepository : BaseRepository<Picture>, IPictureRepository
    {
        public PictureRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
