namespace Cupa.MidatR.Users.Commands.Handlers;
internal sealed class UpdateUserAddressCommandHandler(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager) : IRequestHandler<UpdateUserAddressCommand, GlobalResponseDTO>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly UserManager<ApplicationUser> _userManager = userManager;

    public async Task<GlobalResponseDTO> Handle(UpdateUserAddressCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByNameAsync(request.UserId);
        if (user is null)
            return new GlobalResponseDTO { Message = ErrorMessages.UserNotFound };


        var userAddress = await _unitOfWork.address.FindSingleAsync(x => x.UserId.Equals(user.Id));
        if (userAddress != null)
        {
            userAddress!.Country = request.Model.Country;
            userAddress.Governrate = request.Model.Governrate;
            userAddress.City = request.Model.City;
            userAddress.Regoin = request.Model.Regoin;
            userAddress.Street = request.Model.Street;
        }

        else
        {
            var address = new Address
            {
                Country = request.Model.Country,
                Governrate = request.Model.Governrate,
                City = request.Model.City,
                Regoin = request.Model.Regoin,
                Street = request.Model.Street,
                UserId = user.Id

            };

            try
            {
                await _unitOfWork.address.CreateAsync(address);
            }
            catch (Exception)
            {
                return new GlobalResponseDTO { Message = ErrorMessages.UnHandledServerError };
            }
        }

        user.UpdatedBy = user.Id;
        user.UpdatedOn = DateTime.UtcNow;

        try
        {
            await _userManager.UpdateAsync(user);
        }
        catch (Exception)
        {
            return new GlobalResponseDTO { Message = ErrorMessages.UnHandledServerError };
        }

        await _unitOfWork.CommitAsync();

        return new GlobalResponseDTO { IsSuccess = true, Message = "Address updated Successfully " };
    }
}