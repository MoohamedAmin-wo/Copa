namespace Cupa.MidatR.ManagerControle.Commands.Handlers
{
    sealed class CreateClubHandler : IRequestHandler<CreateClubCommand, GlobalResponseDTO>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUnitOfWork _unitOfwork;
        private readonly IFilesServices _filesServices;

        public CreateClubHandler(UserManager<ApplicationUser> userManager, IUnitOfWork unitOfwork, IFilesServices filesServices)
        {
            _userManager = userManager;
            _unitOfwork = unitOfwork;
            _filesServices = filesServices;
        }

        public async Task<GlobalResponseDTO> Handle(CreateClubCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByNameAsync(request.UserId);
            if (user is null)
                return new GlobalResponseDTO { Message = ErrorMessages.UserNotFound };

            if (!await _userManager.IsInRoleAsync(user, CupaRoles.Manager))
                return new GlobalResponseDTO { Message = "you are not allowed to take this action " };

            var manager = await _unitOfwork.managers.FindSingleAsync(x => x.UserId.Equals(user.Id));
            if (manager is null)
                return new GlobalResponseDTO { Message = "you are not subscripe yet !" };

            var isAlreadyHasClub = await _unitOfwork.clubs.FindSingleAsync(x => x.ManagerId.Equals(manager.Id));
            if (isAlreadyHasClub != null)
                return new GlobalResponseDTO { Message = "you has permission to create one club !" };

            var club = new Club
            {
                ClubName = request.Model.clubName,
                CreatedBy = user.Id,
                ManagerId = manager.Id,
                Story = request.Model.story,
                About = request.Model.about,
                AdminsCount = 0,
            };


            if (request.Model.logo != null)
            {
                var uploadResult = await _filesServices.UploadFileAsync(request.Model.logo, "ClubsLogo");
                if (!uploadResult.IsSuccess)
                    club.LogoUrl = "Logo not uploaded ";

                club.LogoUrl = uploadResult.Url;
            }

            if (request.Model.ClubPicture != null)
            {
                var uploadResult = await _filesServices.UploadFileAsync(request.Model.ClubPicture, "ClubsPictures");
                if (!uploadResult.IsSuccess)
                    club.ClubPictureUrl = "Club picture not uploaded ";

                club.ClubPictureUrl = uploadResult.Url;
            }

            if (request.Model.MainShirt != null)
            {
                var uploadResult = await _filesServices.UploadFileAsync(request.Model.MainShirt, "ClubsShirts");
                if (!uploadResult.IsSuccess)
                    club.MainShirtUrl = "Main shirt picture not uploaded ";

                club.MainShirtUrl = uploadResult.Url;
            }

            var transaction = _unitOfwork.BeginTransactionAsync();
            try
            {
                await _unitOfwork.clubs.CreateAsync(club);
            }
            catch (Exception)
            {
                transaction.Rollback();
                return new GlobalResponseDTO { Message = "faild to create club !" };
            }

            await transaction.CommitAsync();
            await _unitOfwork.CommitAsync();

            return new GlobalResponseDTO { IsSuccess = true, Message = "club Created Successfully " };
        }
    }
}
