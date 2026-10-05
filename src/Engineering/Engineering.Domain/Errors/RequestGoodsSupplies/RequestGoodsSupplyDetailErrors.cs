
namespace Engineering.Domain.Errors;

public static class RequestGoodsSupplyDetailErrors
{
    public static Error RequestGoodsSupplyDetailWithIdNotFound = new("NotFound", "درخواست تامین کالا با این شناسه یافت نشد.", 404);
    public static Error RequestGoodsSupplyDetailProductsNotFound = new("NotFound", "کالا ها یافت نشد.", 204);
    public static Error RequestGoodsSupplyGroupsNotFound = new("NotFound", "گروه ها یافت نشد.", 204);
    public static Error RequestGoodsSupplyScaleNotFound = new("NotFound", "موردی از پردازش اطلاعات برای مقایسه برگردانده نشد", 204);

    public static Error InValidRequestGoodsDuplicate = new("InvalidArguments", "قبلا برای این درخواست مقایسه قبض و مقدار درخواستی صورت گرفته است.", 422);
    public static Error InValidStatusForDraft = new("InvalidArguments", "درخواست تامین شما کالایی در وضعیت نامناسب دارد شما نمیتوانین مجدد به پیش نویس برگردین.", 422);
    public static Error InValidRequestGoodsBillDuplicate = new("InvalidArguments", "قبض تکراری در فایل های ارسالی موجود است.", 422);
    public static Error InValidRequestGoodsSupplyDetailStatus = new("InvalidArguments", "در این وضعیت شما نمیتوانین ویرایش کنید.", 422);
    public static Error InValidPMChangeStatus = new("InvalidArguments", "وضعیت شما قابل تغییر وضعیت دادن توسط مدیر پروژه نمیباشد.", 422);
    public static Error InValidManagementChangeStatus = new("InvalidArguments", "وضعیت شما قابل تغییر وضعیت دادن توسط کارشناس ارشد نمیباشد.", 422);
    public static Error InValidRequestGoodsSupplyRequest = new("InvalidArguments", "باید برای دیدن کالاها نوع کالا را مشخص کنید.", 422);
    public static Error UploadFailed = new("InvalidArguments", "آپلود عکس با مشکل مواجه شد.", 422);
    public static Error RunFailed = new("InvalidArguments", "پردازش با مشکل مواجه شد.", 422);
    public static Error InValidRequestGoodsSupplyDetailStatusForDelete = new("InvalidArguments", "وضعیت کالای درخواست تامین کالا برای حذف باید ثبت اولیه یا برگشت باشد.", 422);
    public static Error CanNotDelete = new("InvalidArguments", "اگر میخواهید تنها کالای خود را حذف کنید، لطفا درخواست خود را حذف کنید.", 422);
    public static Error InValidRequestGoodsSupplyDetailIds = new("InvalidArguments", "شناسه های ارسالی صحیح نیستند.", 422);
    public static Error InValidConsumableVolumeProduct = new("InvalidArguments", "کالا های مصرفی ریز متره نا معتبر است", 422);
    public static Error InValidRequestedCount = new("InvalidArguments", "تعداد درخواستی نا معتبر است", 422);
    public static Error RequestedCount = new("InvalidArguments", "تعداد درخواستی از تعداد براورد شده برای این ریزمتره بیشتر میشود.", 422);
    public static Error ProductCountLow = new("InvalidArguments", "تعداد درخواستی از تعداد باقی مانده کالا برای این پروژه بیشتر میشود.", 422);
    public static Error InValidDelivaryDeadLine = new("InvalidArguments", "مهلت تحویل نا معتبر است", 422);
    public static Error InValidImportance = new("InvalidArguments", "درجه اهمیت نا معتبر است", 422);
    public static Error InValidCheckGroup = new("InvalidArguments", "بررسی از گروه کالا باید مشخص شود، درحال حاضر خالی است.", 422);
    public static Error InValidRequestGoodsSupply = new("InvalidArguments", "درخواست تامین کالا نا معتبر است", 422);
    public static Error InValidRequestGoodsSupplyDetailId = new("InvalidArguments", "شناسه کالای درخواستی نا معتبر است.", 422);
    public static Error InValidProductGroupId = new("InvalidArguments", "گروه محصول نا معتبر است.", 422);
    public static Error InValidType = new("InvalidArguments", "نوع تعیین مقدار نا معتبر است.", 422);
    public static Error InValidProduct = new("InvalidArguments", "کالا های مصرفی نا معتبر است", 422);
    public static Error InValidAlternate = new("InvalidArguments", "کالا های جایگزین نا معتبر است", 422);
    public static Error WarehouseIsUnvaild = new("InvalidArguments", "انبار مقصد شما جزئی از انبارهای مرکزهزینه نمیباشد.", 422);
    public static Error WarehouseIsNull = new("InvalidArguments", "انباری انتخاب نشده است.", 422);
    public static Error WarehouseCanNotSuportProduct = new("InvalidArguments", "انبار مدنظر شما کالای مدنظر را ساپورت نمیکند.", 422);
    public static Error CostCenteWarehousesCanNotSuportProduct = new("InvalidArguments", "مرکزهزینه شما انباری برای درخواست بین انباری، برای ساپورت کالای مدنظر ندارد، لطفا ابتدا انباره های مرکزهزینه های خودرا به روزکنین.", 422);
    public static Error IsDeleted = new("NotFound", "شرح درخواست تامین کالا حذف شده است.", 204);
    public static Error InValidWarehouse = new("InvalidArguments", " کالای مورد نظر غیر فعال شده یا به انبار تخصیص داده نشده است.", 422);
    public static Error CostCenterWarehouseCanNotAsset = new("InvalidArguments", "انبار مرکزهزینه شما قابلیت تخصیص این کالارا ندارد.", 422);
    public static Error InValidWarehouse2 = new("InvalidArguments", " کالای مورد نظر غیر فعال شده یا به انبار تخصیص داده نشده است.", 204);
    public static Error WarehouseInventoryIsNull = new("InvalidArguments", " انباری کالای مد نظر شما را موجودی ندارد.", 422);
    public static Error CostCenterInValidWarehouse = new("InvalidArguments", "انباری برای کالای انتخابی در مرکزهزینه شما وجود نداردو.", 422);
    public static Error WarehouseNotFound = new("InvalidArguments", " انباری یافت نشد.", 422);
    public static Error CostCenterWarehouseNotFound = new("InvalidArguments", " مرکزهزینه انتخابی انباری یافت نشد.", 422);
    public static Error UrlNotFound = new("InvalidArguments", "پیوستی یافت نگردید.", 204);

    public static Error ProductForRGSNotFound = new("InvalidArguments", "کالایی با این شناسه برای این درخواست تامین یافت نشد.", 422);

    public static Error ProductNoHavePackage = new("InvalidArguments", " برای کالای انتخابی شما بسته بندی ای یافت نشد لطفا از مسئول کالا پیگیری لازمه را انجام دهید.", 422);
    public static Error PackageIdNotValidate = new("InvalidArguments", "بسته بندی انتخابی شما برای این کالا درست نمیباشد لطفا بسته بندی درست را انتخاب کنید.", 422);
    public static Error PackageIdNotFound = new("InvalidArguments", " هیچ اطلاعات بسته بندی ای یافت نشد لطفا با مسئول مربوطه برای افزودن بسته بندی کالا ارتباط بگیرید.", 422);
}
