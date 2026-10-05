namespace Engineering.Domain.Errors.WebServices;

public static class GlobalErrors
{
    public const int Zero = 0;
    public const int One = 1;
    public const int MaxIndex = 10000;
    public const int MaxSize = 10000;

    public static Error RequiredEmpty(string fieldName) =>
        new("InvalidArguments", $"{fieldName} اجباری است", 422);

    public static Error NoDuplicates(string fieldName) =>
        new("InvalidArguments", $"{fieldName} نمیتواند تکراری باشد.", 422);

    public static Error RequiredNull(string fieldName) =>
        new("InvalidArguments", $"وارد کردن {fieldName} الزامی است", 422);

    public static Error GreaterThanOrEqualTo(string fieldName) =>
        new("InvalidArguments", $"{fieldName} باید بزرگتر یا برابر صفر باشد.", 422);

    public static Error LessThanOrEqualTo(string fieldName, int count) =>
        new("InvalidArguments", $"{fieldName} باید کوچکتر یا برابر با {count} باشد.", 422);

    public static Error RequiredGreaterThanZero(string fieldName) =>
        new("InvalidArguments", $"{fieldName} باید بزرگتر از صفر باشد", 422);

    public static Error RequiredMaxLength(string fieldName, int maxLength) =>
        new("InvalidArguments", $"برای {fieldName} نهایت میتوانید {maxLength} کاراکتر وارد کنید.", 422);

    public static Error RequiredRegex(string fieldName, string maxLength) =>
        new("InvalidArguments", $"برای {fieldName} شما {maxLength} استفاده کنید.", 422);

    public static Error InvalidEnumValue(string fieldName) =>
        new("InvalidArguments", $"{fieldName} مقدار نامعتبری دارد.", 422);

    public static Error MustBeToday(string fieldName) =>
        new("InvalidArguments", $"{fieldName} باید برابر امروز باشد", 422);

    public static Error MustBeGreaterThan(string fieldName, string compareToName) =>
        new("InvalidArguments", $"{fieldName} باید بزرگتر از {compareToName} باشد", 422);

    public static Error MustBeLessThan(string fieldName, string compareToName) =>
        new("InvalidArguments", $"{fieldName} باید کوچکتر از {compareToName} باشد", 422);

    public static Error LegacyIdIsExist = new("InvalidArguments", "شناسه قدیمی وجود دارد، از شناسه دیگری باید استفاده کنید.", 422);
    public static Error CodeIsEmpty = new("InvalidArguments", "کد مقدار ندارد", 422);
    public static Error CodeIsNull = new("InvalidArguments", "کد خالی است", 422);
    public static Error NameIsEmpty = new("InvalidArguments", "نام مقدار ندارد", 422);
    public static Error NameIsNull = new("InvalidArguments", "نام خالی است", 422);
    public static Error MaxCharIs250 = new("InvalidArguments", "شما نمیتوانید بیش از 250 کاراکتر وارد کنید.", 422);
    public static Error MaxCharIs1500 = new("InvalidArguments", "شما نمیتوانید بیش از 1500 کاراکتر وارد کنید.", 422);
    public static Error MaxCharIs25 = new("InvalidArguments", "شما نمیتوانید بیش از 25 کاراکتر وارد کنید.", 422);

    public static Error UrlIsEmpty = new("InvalidArguments", "فایل مقدار ندارد", 422);
    public static Error UrlIsNull = new("InvalidArguments", "فایل خالی است", 422);

    public static Error IdsIsEmpty = new("InvalidArguments", "لیست شناسه ها خالی میباشد.", 422);
    public static Error ValueIsNull = new("InvalidArguments", "پارامتر ورودی شما خالی است.", 422);
    public static Error IdsLessThanOrEqualZero = new("InvalidArguments", "شناسه ای خالی یا برابر با صفر است.", 422);
    public static Error IdsIsNull = new("InvalidArguments", "شناسه ای انتخاب نشده است.", 422);
    public static Error InvalidCompany = new("Duplicate", "سازمان شما یافت نشد.", 409);

    public static Error StatusIsNull = new("InvalidArguments", "وضعیت درخواست خالی است.", 422);
    public static Error StatusNotInEnum = new("InvalidArguments", "وضعیت انتخابی مجاز نیست.", 422);

    public static Error TypeIsNull = new("InvalidArguments", "نوع درخواست خالی است.", 422);
    public static Error TypeNotInEnum = new("InvalidArguments", "نوع انتخابی مجاز نیست.", 422);

    public static Error IdMustGreatIdIsNullForUpdateerZiro = new("InvalidArguments", "شناسه مدنظر شما برای ویرایش خالی است.", 422);
    //TODO : Resolve Zero wrong in IdMustGreaterZiro Error 
    public static Error IdMustGreaterZiro = new("InvalidArguments", "شناسه انتخابی باید بزرگتر از صفر باشد.", 422);

    public static Error PricesMusbeGreaterThanOrEqualZiro = new("InvalidArguments", "عدد انتخابی شما باید بزرگتر یا برابر صفر باشد.", 422);

    public static Error IdsNotEqual = new("InvalidArguments", "شناسه تکراری در درخواستتان وجود دارد، لطفا مجدد بررسی کنید.", 422);
    public static Error ExcelImporteredHaveNameDuplicate = new("Duplicate", "در اکسل آپلود شده نام تکراری وجود دارد.", 409);
    public static Error ExcelImporteredHaveCodeDuplicate = new("Duplicate", "در اکسل آپلود شده کد تکراری وجود دارد.", 409);
    public static Error ExcelImporteredCanNotBeMore1MG = new("Duplicate", "فایل وارد شده نمیتواند بیشتر از 1 مگابایت باشد.", 409);
    public static Error ImporteredCanNotBeMore10MG = new("Duplicate", "فایل وارد شده نمیتواند بیشتر از 10 مگابایت باشد.", 409);
    public static Error HaveDuplicateKey = new("Duplicate", "نام یا کدی در اکسل وارد شده که از قبل وجود دارد.", 409);
    public static Error ErrorOnReadFile = new("Duplicate", "خطا در خواندن اطلاعات رخ داده است.", 409);
    public static Error ImporteredFileMustBeExcel = new("Duplicate", "فرمت فایل اکسل باید xlsx باشد.", 409);
    public static Error ImporteredFileMustBeMpp = new("Duplicate", "فرمت فایل ماکروسافت پروژه باید mpp باشد.", 409);
    public static Error FileIsEmpty = new("InvalidArguments", " فایل خالی است!", 422);
    public static Error StartDateIsNull = new("InvalidArguments", "تاریخ شروع خالی است.", 422);
    public static Error EndDateIsNull = new("InvalidArguments", "تاریخ پایان خالی است.", 422);
    public static Error EndDateCanNotSmallerToStartDate = new("InvalidArguments", "تاریخ پایان نمیتواند کوچکتر از تاریخ شروع باشد.", 422);
    public static Error StartDateCanNotBigerToEndDate = new("InvalidArguments", "تاریخ شروع نمیتواند بزرگتر از تاریخ پایان باشد.", 422);

    public static Error Regex = new("InvalidArguments", "شما نمیتوانید از کاراکتر های خاص مانند ()!@#$%^&*|~ استفاده کنید.", 422);

    public static Error PageIndexNotValid = new Error("ValidationData", $"شماره صفحه بین {Zero} تا {MaxIndex} میتواند باشد.", 400);
    public static Error PageIndexRequired = new Error("ValidationData", $"در صورت وارد کردن اندازه صفحه، وارد کردن شماره صفحه اجباری است.", 400);
    public static Error PageSizeNotValid = new Error("ValidationData", $"اندازه صفحه بین {Zero} تا {MaxSize} میتواند باشد.", 400);

    public static Error IsActive = new("InvalidArguments", "وضعیت فعال میباشد.", 422);
    public static Error InActive = new("InvalidArguments", "وضعیت غیرفعال میباشد.", 422);
    public static Error InValidRequest = new("InvalidArguments", "پارامترهای ورودی اشتباه یا غیر مجاز است .", 422);

    public static Error MustBeGreaterThanOrEqualTo(string fieldName, string compareToName) =>
                                                   new("InvalidArguments", $"{fieldName} باید بزرگتر یا برابر {compareToName} باشد", 422);
    public static Error MustBeBetween(string fieldName, object min, object max) =>
                                      new("InvalidArguments", $"{fieldName} باید بین {min} و {max} باشد", 422);
}