using Cupa.Application.Common;

namespace Cupa.Application.Services.ClubServices
{
    public sealed class ClubManagmentServices(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager) : IClubManagmentServices
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager = userManager;

        public async Task<Club?> GetCurrentClubAsync(ApplicationUser user)
        {
            if (user is null)
                return null;

            if (await _userManager.IsInRoleAsync(user, CupaRoles.Manager))
            {
                var manager = await _unitOfWork.managers.FindSingleAsync(x => x.UserId.Equals(user.Id));
                if (manager is null)
                    return null;

                return await _unitOfWork.clubs.FindSingleAsync(x => x.ManagerId.Equals(manager.Id));
            }

            if (await _userManager.IsInRoleAsync(user, CupaRoles.Admin))
            {
                var admin = await _unitOfWork.admins.FindSingleAsync(x => x.UserId.Equals(user.Id));
                if (admin is null)
                    return null;

                return await _unitOfWork.clubs.FindSingleAsync(x => x.Id.Equals(admin.ClubId));
            }

            return null;
        }
    }
}
