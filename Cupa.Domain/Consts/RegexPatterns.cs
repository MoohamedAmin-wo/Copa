﻿namespace Cupa.Domain.Consts;
public static class RegexPatterns
{
    public const string Password = @"^(?=.*[0-9])(?=.*[a-z])(?=.*[A-Z])(?=.*[!@#$%^&*()\[\]{}\-_+=~`|:;""'<>,./?]).{8,}$";
    public const string Username = "^[a-zA-Z0-9-._@+]*$";
    public const string CharactersOnly_Eng = "^[a-zA-Z-_ ]*$";
    public const string CharactersOnly_Ar = "^[\u0600-\u065F\u066A-\u06EF\u06FA-\u06FF ]*$";
    public const string NumbersAndChrOnly_ArEng = "^(?=.*[\u0600-\u065F\u066A-\u06EF\u06FA-\u06FFa-zA-Z])[\u0600-\u065F\u066A-\u06EF\u06FA-\u06FFa-zA-Z0-9 _-]+$";
    public const string DenySpecialCharacters = "^[^<>!#%$]*$";
    public const string MobileNumber = "^01[0,1,2,5]{1}[0-9]{8}$";
    public const string NationalId = "^[2,3]{1}[0-9]{13}$";
    public const string EmailAddress = @"^[a-zA-Z0-9_]+@[a-zA-Z0-9]+\.(com|org|co|sa)$";
    public const string ConfirmationCode = @"^\d+$";
    public const string AllowedPosition = @"^(1[0-2]|[1-9])$";
}
