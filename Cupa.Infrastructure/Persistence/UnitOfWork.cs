namespace Cupa.Infrastructure.Persistence;
public class UnitOfWork : IUnitOfWork
{

    private readonly ApplicationDbContext _context;
    public ISuccessStroriesRepository successStrories { get; }
    public IClubPlayerRepository clubPlayer { get; }
    public IPositionRepository position { get; }
    public IPictureRepository pictures { get; }
    public IManagerRepository managers { get; }
    public IAddressRepository address { get; }
    public IPlayerRepository player { get; }
    public IAdminRepository admins { get; }
    public IVideoRepository video { get; }
    public IUsersRepository users { get; }
    public IClubRepository clubs { get; }

    public UnitOfWork(ApplicationDbContext context, IAddressRepository address, IUsersRepository users, IManagerRepository owners, IPictureRepository pictures, IClubRepository clubs, IAdminRepository admins, IVideoRepository video, IPlayerRepository player, IPositionRepository position, ISuccessStroriesRepository successStrories, IClubPlayerRepository clubPlayer)
    {
        this.successStrories = successStrories;
        this.clubPlayer = clubPlayer;
        this.pictures = pictures;
        this.position = position;
        this.address = address;
        this.managers = owners;
        this.player = player;
        this.admins = admins;
        this.video = video;
        this.users = users;
        this.clubs = clubs;
        _context = context;
    }

    public int Commit() => _context.SaveChanges();

    public async Task<int> CommitAsync() => await _context.SaveChangesAsync();

    public IDbContextTransaction BeginTransactionAsync() => _context.Database.BeginTransaction();


    public async Task<int> NumberOfModifiedRows() => await _context.SaveChangesAsync();

}
