
namespace Engineering.Domain.Errors;

public static class RequestGoodsSupplyManagementErrors
{
    public static Error RequestGoodsSupplyManagementWithIdNotFound = new("NotFound", "کارشناس ارشد درخواست تامین کالا با این شناسه یافت نشد.", 404);

    public static Error InValidRequestGoodsSupplyManagementStatus = new("InvalidArguments", "وضعیت معتبر نیست.", 422);
    public static Error InValidProductId = new("InvalidArguments", "محصول معتبر نیست.", 422);
    public static Error InValidWarehouseId = new("InvalidArguments", "انبار معتبر نیست.", 422);
    public static Error InValidInvoiceId = new("InvalidArguments", "فاکتور معتبر نیست.", 422);
    public static Error InValidRequestedCount = new("InvalidArguments", "تعداد درخواستی معتبر نیست.", 422);
    public static Error InValidType = new("InvalidArguments", "نوع درخواست معتبر نیست.", 422);
    public static Error InValidProjectManagerId = new("InvalidArguments", "شناسه مدیر پروژه معتبر نیست.", 422);
    public static Error InRequestValidType = new("InvalidArguments", "این درخواست تامین، پیمانکاری نمیباشد.", 422);
    public static Error InValidRequestGoodsSupplyDetail = new("InvalidArguments", "کالای درخواست تامین کالا معتبر نیست.", 422);
    public static Error InValidRequestCount = new("InvalidArguments", "شما نمیتوانین از مقدار درخواست شده برای این درخواست بیشتر وارد کنید.", 422);
    public static Error CountNotEqual = new("InvalidArguments", "تمامی کالاهای نیاز به تامین انتخاب و بررسی نشده اند، لطفا بررسی را تکمیل کنید.", 422);
    public static Error InValidRequestGoodsSupplyType = new("InvalidArguments", "شما ابتدا باید به وضعیت درحال بررسی بروید و بعد درخواستتان را تایید کنید.", 422);
    public static Error InValidSupplyType = new("InvalidArguments", "نوع درخواست شما برای تایید صحیح نمیباشد.", 422);
    public static Error InValidRequestGoodsSupplyIsConfirmed = new("InvalidArguments", "وضعیت شما در حال حاضر در انتظار تامین است، لطفا منتظر ادامه روند باشید.", 422);
    public static Error InValidRequestGoodsSupplyManagementId = new("InvalidArguments", "شناسه کارشناس ارشد درخواست تامین کالا نامعتبر است", 422);
    public static Error InValidCostCenterInvoice = new("InvalidArguments", "در ثبت درخواست خروج مصرفی باید از انبارهای مرکزهزینه خود استفاده کنید.", 422);
    public static Error DestinationWarehouseIdIsNull = new("InvalidArguments", "در ثبت درخواست باید انبار مقصد مشخص شده باشد.", 422);
    public static Error DestinationWarehouseNotInCostCenter = new("InvalidArguments", "انبار مقصد جزئی از انبارهای مرکزهینه نمیباشد.", 422);
    public static Error RequestGoodsSupplyManagementIdIsEmpty = new("InvalidArguments", "شناسه درخواست کارشناس ارشد انبار خالی می باشد", 422);
    public static Error InvoiceIdIsEmpty = new("InvalidArguments", "شناسه فاکتور انبار خالی می باشد", 422);
    public static Error ProductIdIsEmpty = new("InvalidArguments", "شناسه کالا خالی می باشد", 422);
    public static Error RequestGoodsSupplyDetailIdIsEmpty = new("InvalidArguments", "شناسه کالای، درخواست کالا خالی می باشد.", 422);
    public static Error RequestGoodsSupplyIdIsEmpty = new("InvalidArguments", "شناسه درخواست کالا خالی می باشد.", 422);
}
