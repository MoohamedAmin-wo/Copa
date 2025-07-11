using Cupa.Application.Services.ClubServices;
using Cupa.MidatR.ManagerControle.Commands.DTOs;
using Humanizer;
namespace Cupa.MidatR.ManagerControle.Commands.Handlers;
sealed class CreatePlayerHandler(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager, IFilesServices filesServices, IEmailSender emailSender,  IClubManagmentServices clubManagmentServices) : IRequestHandler<CreatePlayerCommand, GlobalResponseDTO>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly IFilesServices _filesServices = filesServices;
    private readonly IEmailSender _emailSender = emailSender;
    private readonly IClubManagmentServices _clubManagmentServices = clubManagmentServices;

    public async Task<GlobalResponseDTO> Handle(CreatePlayerCommand req, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByNameAsync(req.UserId);
        if (user is null)
            return new GlobalResponseDTO { Message = ErrorMessages.UserNotFound };

        var currentClub = await _clubManagmentServices.GetCurrentClubAsync(user);
        if (currentClub is null)
            return new GlobalResponseDTO { Message = "No club was define !" };

        var isExistPlayer = await _userManager.FindByEmailAsync(req.Model.Email);
        if (isExistPlayer != null)
        {
            return new GlobalResponseDTO { Message = "This email is already used for another player" };
        }

        var playerUser = new ApplicationUser
        {
            FirstName = req.Model.Firstname,
            LastName = req.Model.Lastname,
            Email = req.Model.Email,
            BirthDate = req.Model.Birthdate,
            EmailConfirmed = false,
            PhoneNumber = req.Model.Phone,
            NormalizedEmail = req.Model.Email.ToUpperInvariant(),
            UserName = req.Model.Nickname,
            NormalizedUserName = req.Model.Nickname.ToUpperInvariant(),
        };

        if (playerUser.Age < 7)
            return new GlobalResponseDTO { Message = "can't create new payer record with age less than 7 years old !" };

        var createUserResult = await _userManager.CreateAsync(playerUser, CupaDefaults.DefaultPassword);
        if (!createUserResult.Succeeded)
            return new GlobalResponseDTO { Message = string.Join(",", createUserResult.Errors.Select(x => x.Description)) };

        try
        {
            await _userManager.AddToRoleAsync(playerUser, CupaRoles.User);
        }
        catch (Exception ex)
        {
            return new GlobalResponseDTO { Message = $"unable to add this user to the role, Exception Details {ex.Message} " };
        }

        var convertedPositionValue = int.TryParse(req.Model.PositionId, out int Value);
        if (!convertedPositionValue)
        {
            return new GlobalResponseDTO { Message = "please select a valid position !" };
        }

        var isValidPosition = await _unitOfWork.position.FindSingleAsync(x => x.Id.Equals(Value));
        if (isValidPosition is null)
        {
            return new GlobalResponseDTO { Message = "Position not found !" };
        }

        var player = new Player
        {
            NickName = req.Model.Nickname,
            PreefAbout = "un defined",
            StoryAbout = "un defined",
            UserId = playerUser.Id,
            PositionId = Value,
            IsFree = false,
            CreatedBy = user.Id,
            IsBinned = false,
            Views = 0
        };

        try
        {
            await _unitOfWork.player.CreateAsync(player);
        }
        catch (Exception ex)
        {
            return new GlobalResponseDTO { Message = $"Error creating new Player , Exception Details {ex.Message}" };
        }

        try
        {
            await _userManager.AddToRoleAsync(playerUser, CupaRoles.Player);
        }
        catch (Exception ex)
        {
            return new GlobalResponseDTO { Message = $"unable to add this user to the role, Exception Details {ex.Message} " };
        }

        var clubPlayer = new ClubPlayer
        {
            ClubId = currentClub.Id,
            IsAvaliableForSale = false,
            JoinedClubOn = req.Model.JoinedClubOn,
            PlayerId = player.Id,
            HasCurrentContract = true,
            ContractDuration = req.Model.ContractDuration.Humanize(),
            Number = req.Model.Number
        };


        if (req.Model.ContractPicture != null)
        {
            var upload = await _filesServices.UploadFileAsync(req.Model.ContractPicture, "PlayersContracts");

            if (!upload.IsSuccess)
            {
                return new GlobalResponseDTO { Message = upload.Message };
            }
            clubPlayer.ContractPictureUrl = upload.Url;
        }
        
        if (req.Model.ProfilePicture != null)
        {
            var upload = await _filesServices.UploadFileAsync(req.Model.ProfilePicture, "ClubPlayersPictures");

            if (!upload.IsSuccess)
            {
                return new GlobalResponseDTO { Message = upload.Message };
            }
            player.ProfilePictureUrl = upload.Url;
        }

        if (req.Model.Video != null)
        {
            await _unitOfWork.CommitAsync();
            var upload = await _filesServices.UploadFileAsync(req.Model.Video, "ClubPlayersVideos");
            if (!upload.IsSuccess)
            {
                return new GlobalResponseDTO { Message = upload.Message };
            }

            var video = new Video
            {
                CreatedBy = user.Id,
                PlayerId = player.Id,
                Url = upload.Url,
                VideoUid = upload.Uid,
            };

            try
            {
                await _unitOfWork.video.CreateAsync(video);
            }
            catch (Exception)
            {
            }
        }

        try
        {
            await _unitOfWork.clubPlayer.CreateAsync(clubPlayer);
        }
        catch (Exception ex)
        {
            return new GlobalResponseDTO { Message = $"Error creating Club player , Exception Details {ex.Message}" };
        }

        currentClub.PlayersCount++;
        await _unitOfWork.CommitAsync();

        await _emailSender.SendEmailAsync(req.Model.Email, $"Congratulations about joining {currentClub.ClubName} club",
             $"<h2>You can login now to preview your player profile <p>your current password is {CupaDefaults.DefaultPassword}</p></h3></h2>");


        return new GlobalResponseDTO { IsSuccess = true, Message = "Created Successfully " };
    }
}