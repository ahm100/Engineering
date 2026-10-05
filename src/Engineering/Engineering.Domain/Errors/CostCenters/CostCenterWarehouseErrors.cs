
namespace Engineering.Domain.Errors;

public static class CostCenterWarehouseErrors
{
    public static Error WarehouseIdIsDuplicate = new("Duplicate", "انبار انتخابی از قبل موجود است.", 409);

    public static Error WarehouseIdIsEmpty = new("InvalidArguments", "شناسه انبار نباید خالی باشد.", 422);
    public static Error WarehouseIdsIsEmpty = new("InvalidArguments", "شناسه  های انبار نباید خالی باشد.", 422);
    public static Error IsDefaultIsEmpty = new("InvalidArguments", "وضعیت انبار پیش فرض نباید خالی باشد.", 422);
    public static Error CostCenterIdIsEmpty = new("InvalidArguments", "شناسه مرکزهزینه نباید خالی باشد.", 422);
    public static Error ProductIdIsEmpty = new("InvalidArguments", "شناسه کالا نباید خالی باشد.", 422);
    public static Error ProductGroupIdIsEmpty = new("InvalidArguments", "شناسه گروه کالا نباید خالی باشد.", 422);
    public static Error NotFoundIdForDelete = new("InvalidArguments", "هیچ انباری با این شناسه یافت نشد.", 422);
    public static Error IsDefaultWarehouse = new("InvalidArguments", "این انبار مرکزهزینه در حالت پیش فرض قرار دارد و قابل حذف نیست.", 422);
    public static Error CostCenterWarehouseIdIsEmpty = new("InvalidArguments", "شناسه انبار مرکزهزینه نباید خالی باشد.", 422);
    public static Error NotFoundIdForUpdate = new("InvalidArguments", "هیچ انباری با این شناسه یافت نشد.", 422);
    public static Error WarehouseNotFound = new("InvalidArguments", "هیچ انباری با این شناسه یافت نشد.", 422);

    public static Error CantChnageIsDefault = new("InvalidArguments", "نمیتوانید این انبار را از پیش فرض بودن خارج کنید، انبار پیش فرض جدید را انتخاب کنید.", 422);

    public static Error IdNotFound = new("InvalidArguments", "هیچ انباری با این شناسه یافت نشد.", 404);
    public static Error DataIsNull = new("InvalidArguments", "هیچ انباری با این شناسه مرکزهزینه یافت نشد.", 204);
    public static Error IsDeleted = new("NotFound", "انبار مرکزهزینه حذف شده است.", 204);

}