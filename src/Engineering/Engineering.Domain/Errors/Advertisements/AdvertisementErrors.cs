namespace Engineering.Domain.Errors.Advertisements;

public static class AdvertisementErrors
{
    public static Error NameIsDuplicate = new("Duplicate", "نام تبلیغات تکراری است.", 409);
    public static Error CodeIsDuplicate = new("Duplicate", "کد تبلیغات تکراری است.", 409);

    public static Error AdWithIdNotFound = new("NotFound", "هیچ تبلیغی ای با این شناسه یافت نشد.", 404);
    public static Error AdWithNameNotFound = new("NotFound", "هیچ تبلیغی ای با این نام یافت نشد.", 204);
    public static Error FilteredAdNotFound = new("NotFound", "هیچ تبلیغی ای با این اطلاعات یافت نشد.", 204);
}