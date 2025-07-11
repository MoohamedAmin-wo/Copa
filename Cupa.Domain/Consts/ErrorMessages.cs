namespace Cupa.Domain.Consts;
public static class ErrorMessages
{
    public const string UnHandledServerError = "SomeThing went wrong !";
    public const string EmailNotconfirmed = "This email not confirmed !";
    public const string EmailConfirmed = "This email is Already confirmed !";
    public const string UnRegisteredUser = "Email isn't Registered ,\nTry to create account first! ";
    public const string SuccessfullUpdates = "Update complete successfully ";
    public const string MaxSizeProfilePicture = "Max allowed size is 1 MB !";
    public const string NotAllowedExtenssions = "Only [jpg , png , jpeg] allowed !";
    public const string UserHasRole = "User is already assigned to this role !";
    public const string UnExistedRoleOrUser = "User or role isn't exist !";
    public const string CannotRemoveUserRole = "Can't Remove user role from any User !";
    public const string InvalidConfirmationCode = "confirmation code is invalid !";
    public const string NullConfirmationCode = "confirmation code can't be null or Empty !";
    public const string OldPasswordMatchesNewPassword = "This is your current password , please pick a new one";
    public const string EmailIsTaken = "Email is already registered !";
    public const string UserNameIsTaken = "This Username is Taken !";
    public const string UserNotFound = "Can't find user related to this email !";
    public const string EmailNotSent = "Unable to send emails now !";

    public const string PasswordResetSuccessfully = "successfully reset your password ";
    public const string InvalidCredinatials = "Invalid Email or Password !";
    public const string InvalidPassword = "Invalid Password !";
    public const string InValidEmail = "Please enter a valid email !";
    public const string RequiredField = "Required field";
    public const string MaxLength = "Length cannot be more than {1} characters";
    public const string MinLength = "The {0} must be at least {2} and at max {1} characters long.";
    public const string Duplicated = "Another record with the same {0} is already exists!";
    public const string NotAllowedExtension = "Only .png, .jpg, .jpeg files are allowed!";
    public const string MaxSize = "File cannot be more that 2 MB!";
    public const string NotAllowFutureDates = "Date cannot be in the future!";
    public const string InvalidRange = "{0} should be between {1} and {2}!";
    public const string ConfirmPasswordNotMatch = "The password and confirmation password do not match.";
    public const string WeakPassword = "Passwords contain an uppercase character, lowercase character, a digit, and a non-alphanumeric character. Passwords must be at least 8 characters long";
    public const string InvalidUsername = "Username can only contain letters or digits.";
    public const string OnlyEnglishLetters = "Only English letters are allowed.";
    public const string OnlyArabicLetters = "Only Arabic letters are allowed.";
    public const string OnlyNumbersAndLetters = "Only Arabic/English letters or digits are allowed.";
    public const string DenySpecialCharacters = "Special characters are not allowed.";
    public const string InvalidMobileNumber = "Invalid mobile number.";
    public const string InvalidNationalId = "Invalid national ID.";
    public const string InvalidSerialNumber = "Invalid serial number.";
    public const string EmptyImage = "Please select an image.";
    public const string BlackListedSubscriber = "This subscriber is blacklisted.";
    public const string InactiveSubscriber = "This subscriber is inactive.";
    public const string InValidPhonenumber = "Invalid mobile number !";

    public const string BlockedUserOfAction = "you are not allowed to take this action !";
    public const string UnAuthorizedUser = "Not authorized to take this action !";
    public const string UnableToUpdateCurrentRecord = "can't update this record !";
    public const string ErrorFindExistClub = "No club was defined yet !";
    public const string ErrorFindExistUser = "this email doesn't belong to any user , please check it !";
    public const string AdminRoleAssigned = "this user is already an admin";
    public const string AdminRoleAssignedToAnotherclub = "this user is already an admin with another club";
    public const string EmailIsRegisteredButNeedConfirmation = "Email is registered , but need confirmation !";
    public const string RegisterCompleteSuccessfully = "Register complete , Check inbox to confirm your accout.";


    // pictures 
    public const string MaxAllowedPicturesCountForFreePlayer = "can't add more than 10 pictures for each player !";
    public const string NotValidPictureExtensions = "only allowed [.jpg , .png , .jpeg] extensions for Pictures!";
    public const string NotValidPictureSize = "Max size for pictures is 2MB!";

    // Videos
    public const string NotValidVideoExtensions = "only allowed [.MP4 , .MOV] extensions for Vidoes !";
    public const string NotValidVideoDuration = "Video Duration can't be more than 3 minutes !";
    public const string NotValidVideoSize = "Max size for Videos is 15MB !";

    //Pdf files
    public const string NotValidPdfFileExtension = "only allowed [.Pdf] extension for Pdf files !";
    public const string NotValidPdfFileSize = "Max size for Pdf Files is 1MB !";



}
