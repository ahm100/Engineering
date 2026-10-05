namespace Engineering.Domain.Errors.EmployerEmployees;

public static class EmployerEmployeeErrors
{
    public static Error IdIsEmpty = new("InvalidArguments", "شناسه خالی است!", 422);
    public static Error ThirdPartyNotFound = new("InvalidArguments", "طرف حساب هایی با این شناسه ها پیدا نشد!", 422);
    public static Error EmployerNotFound = new("InvalidArguments", "کارفرمایی با این شناسه پیدا نشد!", 422);

    public static Error EmployerEmployeeNotFound = new("InvalidArguments", " پرسنلی با این شناسه پیدا نشد!", 422);

    public static Error EmployeeForEmployerNotFound = new("InvalidArguments", " پرسنلی برای کارفرما پیدا نشد!", 204);
}