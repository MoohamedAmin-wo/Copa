namespace Cupa.Application.Common;
public interface IUnitOfWork
{
    ISuccessStroriesRepository successStrories { get; }
    IClubPlayerRepository clubPlayer { get; }
    IPositionRepository position { get; }
    IManagerRepository managers { get; }
    IPictureRepository pictures { get; }
    IAddressRepository address { get; }
    IPlayerRepository player { get; }
    IAdminRepository admins { get; }
    IVideoRepository video { get; }
    IUsersRepository users { get; }
    IClubRepository clubs { get; }
    int Commit();
    Task<int> CommitAsync();
    Task<int> NumberOfModifiedRows();
    IDbContextTransaction BeginTransactionAsync();
}