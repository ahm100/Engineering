
namespace Engineering.Application.Extensions.CustomValidations;

public static class RegexPatterns
{
    public const string SafeTextTitle = "نمیتوانید از کاراکتر خاص";
    public const string SafeText = @"^[^!#@&^$%~]+$";
    public const string OnlyDigitsTitle = "فقط میتوانید از عدد";
    public const string OnlyDigits = @"^\d+$";
    public const string AlphaNumericTitle = "فقط میتوانید از حروف انگلیسی";
    public const string AlphaNumeric = @"^[a-zA-Z0-9]*$";
    public const string PersianLettersOnlyTitle = "فقط میتوانید از حروف فارسی";
    public const string PersianLettersOnly = @"^[آ-ی\s]+$";
    public const string EmailTitle = "فقط میتوانید از ایمیل";
    public const string Email = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
    public const string PhoneNumberTitle = "فقط میتوانید از شماره تلفن واقعی";
    public const string PhoneNumber = @"^09\d{9}$"; // ایران
    public const string NationalCodeTitle = "فقط میتوانید از کدملی";
    public const string NationalCode = @"^\d{10}$";// ایران
    public const string PostalCodeTitle = "فقط میتوانید از کدپستی عددی";
    public const string PostalCode = @"^[1-9][0-9]{9}$";// ایران

}
