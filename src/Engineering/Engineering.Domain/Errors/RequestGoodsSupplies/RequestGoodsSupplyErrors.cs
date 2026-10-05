
namespace Engineering.Domain.Errors;

public static class RequestGoodsSupplyErrors
{
    public static Error RequestGoodsSupplyWithIdNotFound = new("NotFound", "درخواست تامین کالا با این شناسه یافت نشد.", 404);
    public static Error ServiceReasonTypeIsNull = new("NotFound", "علت درخواست در درخواست خدماتی نمیتواند خالی باشد.", 404);
    public static Error ContractorIdCantBeNull = new("NotFound", "پیمانکار برای درخواست تأمین خرید برای پیمانکار الزامی است.", 404);

    public static Error RequestGoodsSupplyWithFilterNotFound = new("NotFound", "درخواست تامین کالا با این اطلاعات یافت نشد.", 204);
    public static Error RequestGoodsProductsNotFound = new("NotFound", "کالا یافت نشد.", 204);
    public static Error ProdouctNotFound = new("NotFound", "کالای درخواستی شما در سیستم انبار یافت نشد.", 404);
    public static Error ParentNotFound = new("NotFound", "درخواست تامین واحد پیدا نشد.", 404);
    public static Error UpdateListHaveAnotherProduct = new("NotFound", "در لیست ویرایش شما درخواستی وجود دارد که کالای متفاوتی دارد لطفا درخواست خود را اصلاح کنین.", 404);
    public static Error CreateListHaveAnotherProduct = new("NotFound", "در لیست ایجاد شما درخواستی وجود دارد که کالای متفاوتی دارد لطفا درخواست خود را اصلاح کنین.", 404);
    public static Error RequestGoodsProductGroupsNotFound = new("NotFound", "گروه کالا یافت نشد.", 204);
    public static Error RequestGoodsCreatorNotFound = new("NotFound", "ثبت کننده ای یافت نشد.", 204);
    public static Error RequestGoodsProductGroupNotFound = new("NotFound", "گروه یا دسته بندی کالایی یافت نشد.", 204);
    public static Error RequestGoodsProductNotFound = new("NotFound", "کالایی یافت نشد.", 204);

    public static Error SetToArchived = new("InvalidArguments", "وضعیت درخواست شما باید بسته شده یا برگشت درخواست باشد تا بتوانین به بایگانی تغییر وضعیت دهید.", 422);
    public static Error SetToClosed = new("InvalidArguments", "وضعیت درخواست شما باید تامین کامل باشد تا بتوانین به بسته شده تغییر وضعیت دهید.", 422);


    public static Error InValidDetails = new("InvalidArguments", "به ازای  کالاهای مورد درخواست تعیین تکلیف باید صورت بگیرد.", 422);
    public static Error InValidRequestGoodsSupplyStatus = new("InvalidArguments", "وضعیت درخواست تامین کالا برای ویرایش مناسب نمیباشد.", 422);
    public static Error InValidRequestGoodsSupplyStatusForDelete = new("InvalidArguments", "شما فقط در وضعیت های ثبت اولیه، ارسال مجدد، رد درخواست، برگشت درخواست میتوانین درخواست خود را حذف کنین.", 422);
    public static Error UpdateStatuses = new("InvalidArguments", "شما فقط در وضعیت های، ثبت اولیه، ثبت موقت، برگشت از کارشناس ارشد و برگشت از واحد تامین، میتوانین ویرایش کنین.", 422);
    public static Error InValidRequestGoodsSupplyIds = new("InvalidArguments", "شناسه های ارسالی صحیح نیستند.", 422);
    public static Error CategoryNotFound = new("InvalidArguments", "اطلاعات دسته بندی کالا ناظر یافت نشد.", 422);
    public static Error InValidStartDate = new("InvalidArguments", "تاریخ شروع معتبر نیست.", 422);
    public static Error CostCenterDontHaveWarehouse = new("InvalidArguments", "مرکزهزینه شما دارای انباری نمیباشد، لطفا از مسئول مربوطه بابت تخصیص انبار به مرکزهزینه پیگیری کنید.", 422);
    public static Error InValidEndDate = new("InvalidArguments", "تاریخ پایان معتبر نیست.", 422);
    public static Error InValidProjectOperationIds = new("InvalidArguments", "شناسه عملیات پروژه معتبر نیست.", 422);
    public static Error InValidCostCenter = new("InvalidArguments", "مرکز هزینه معتبر نیست.", 422);
    public static Error InValidOperationInfoSeasonId = new("InvalidArguments", "فصل معتبر نیست.", 422);
    public static Error InValidOperationInfoSeasonIdBiggerThanZero = new("InvalidArguments", "شناسه فصل خالی یا صفر نمیتواند باشد.", 422);
    public static Error InValidIdBiggerThanZero = new("InvalidArguments", "شناسه درخواست خالی یا صفر نمیتواند باشد.", 422);
    public static Error InValidProject = new("InvalidArguments", "پروژه معتبر نیست.", 422);
    public static Error ProjectIsNotOrganization = new("InvalidArguments", "پروژه از نوع واحد نمیباشد.", 422);
    public static Error ContractorIsEmpty = new("InvalidArguments", "شناشه پیمانکار نباید خالی باشد.", 422);
    public static Error ContractorTypeContractorIsEmpty = new("InvalidArguments", "شما در درخواست پیمانکاری باید پیمانکارتان مشخص باشد.", 422);
    public static Error NotContractorTypeContractorIsEmpty = new("InvalidArguments", "در درخواست شما پیمانکار یا طرف حساب باید به درستی انتخاب شود.", 422);
    public static Error InValidContractor = new("InvalidArguments", "برای این پیمانکار اطلاعاتی مبنی بر انجام خدمات برای این پروژه یافت نشد.", 422);
    public static Error InValidSeason = new("InvalidArguments", "رسته رشته فصل معتبر نیست.", 422);
    public static Error InValidProjectOperation = new("InvalidArguments", "عملیات پروژه معتبر نیست.", 422);
    public static Error InValidType = new("InvalidArguments", "نوع درخواست تامین کالا نامعتبر است", 422);
    public static Error DescriptionNeeded = new("InvalidArguments", "درج توضیح اجباری است.", 422);
    public static Error InValidRequestGoodsSupplyId = new("InvalidArguments", "شناسه درخواست تامین کالا نامعتبر است.", 422);
    public static Error InValidRequestGoodsSupplyDetails = new("InvalidArguments", "جزئیات درخواست تامین کالا مشخص شده نمیباشد.", 422);
    public static Error InValidRequestGoodsSupplyDetail = new("InvalidArguments", "کالای درخواست تامین کالا نامعتبر است.", 422);
    public static Error InValidRequestGoodsSupplyDetailProduct = new("InvalidArguments", "کالای درخواست تامین کالا نامعتبر است.", 422);
    public static Error InValidRequestGoodsSupplyDetailService = new("InvalidArguments", "خدمت درخواست تامین کالا نامعتبر است.", 422);
    public static Error InValidRequestGoodsSupplyDetailAds = new("InvalidArguments", "تبلیغات درخواست تامین کالا نامعتبر است.", 422);
    public static Error InValidRequestGoodsSupplyType = new("InvalidArguments", "نوع فرستاده شده درخواست تامین کالا نامعتبر است.", 422);
    public static Error InValidProjectOperationDetail = new("InvalidArguments", "ریز متره نامعتبر است.", 422);
    public static Error InValidPriceAndType = new("InvalidArguments", "امکان وارد کردن قیمت با درخواست سرپروژه ای وجود ندارد", 422);
    public static Error DetailsIsNull = new("InvalidArguments", "در درخواست شما کالایی یافت نشد لطفا کالایی انتخاب کنید.", 422);
    public static Error InValidProjectStatus = new("InvalidArguments", "پروژه شما در وضعیت های غیرمجاز برای ثبت درخواست تامین کالا قراردارد، لطفا با مدیر پروژه خود این موضوع رو در جریان بگذارید.", 422);
    public static Error CanNotDeleteDetails = new("InvalidArguments", "شما نمیتوانین تمام کالاهای این درخواست را حذف کنین، اگر میخواهید این کارا بکنید خود درخواست را حذف کنید.", 422);
    public static Error ProjectOperationDetailNotInProjectOperation = new("InvalidArguments", "ریزمتره انتخابی در شرح عملیات پروژه شما نمیباشد.", 422);
    public static Error CanNotChanged = new("InvalidArguments", "شما نمیتوانین وضعیت درخواست را به برگشت واحد تامین و ارسال مجدد به واحد تامین تغییر دهید.", 422);
    public static Error IsDeleted = new("NotFound", "درخواست تامین کالا حذف شده است.", 204);
    public static Error IsConfirmed = new("NotFound", "درخواست تامین قبل تایید شده است.", 204);
    public static Error ProjectOperationDetailJustOne = new("InvalidArguments", "شما فقط یک ریزمتره دارید لطفا به صورت دلخواه پر کنید.", 422);
    public static Error NoItemFound = new("InvalidArguments", "هیج آیتمی با فیلترهای مد نظر پیدا نشد.", 204);
    public static Error NoHaveDetails = new("InvalidArguments", "درخواست شما آیتمی ندارد و نمیتوانید بررسی کنید.", 422);
    public static Error DoesNotHaveTypeDetails = new("InvalidArguments", "درخواست شما آیتمی ندارد و نمیتوانید بررسی کنید.", 422);
    public static Error ContractorIsImportant = new("InvalidArguments", "انتخاب طرف حساب در درخواست سرپروژه ای اجباریست.", 422);
    public static Error CurenciesNotValid = new("InvalidArguments", "اطلاعات ارزی انتخاب شده صحیح نمیباشد.", 422);
    public static Error GroupsNotValid = new("InvalidArguments", "اطلاعات گروه کالای انتخاب شده صحیح نمیباشد.", 422);
    public static Error CategoriesNotValid = new("InvalidArguments", "اطلاعات دسته بندی کالای انتخاب شده صحیح نمیباشد.", 422);
    public static Error ProductsNotValid = new("InvalidArguments", "اطلاعات کالای انتخاب شده صحیح نمیباشد.", 422);
    public static Error ProductIsInActive = new("InvalidArguments", " کالایی غیر فعال میباشد لطفا از مسئول مربوطه پیگیری کنید.", 422);
    public static Error RequestCountMoreThanAssigned = new("InvalidArguments", "تعداد کالای درخواست شده توسط تامین بیشتر از تعداد مجاز تعریف شده در پروژه میباشد.", 422);


    public static Error InValidStatusForProjectManagerResend = new("InvalidArguments", "برای تغییر به وضعیت ارسال مجدد برای مدیرپروژه، وضعیت شما باید برگشت درخواست مدیرپروژه باشد.", 422);
    public static Error InValidStatusForProjectManagerPending = new("InvalidArguments", "برای تغییر به وضعیت در انتظار بررسی مدیرپروژه، وضعیت شما باید ثبت اولیه یا ارسال مجدد برای مدیرپروژه باشد.", 422);
    public static Error InValidStatusForProjectManagerConfirmed = new("InvalidArguments", "برای تغییر به وضعیت تایید مدیرپروژه، وضعیت شما باید در انتظار بررسی مدیرپروژه باشد.", 422);
    public static Error InValidStatusForProjectManagerReturned = new("InvalidArguments", "برای تغییر به وضعیت برگشت درخواست مدیرپروژه، وضعیت شما باید در انتظار بررسی مدیرپروژه باشد.", 422);
    public static Error InValidStatusForProjectManagerRejected = new("InvalidArguments", "برای تغییر به وضعیت رد درخواست مدیرپروژه، وضعیت شما باید در انتظار بررسی مدیرپروژه باشد.", 422);
    public static Error InValidStatusForResendForManagement = new("InvalidArguments", "برای تغییر به وضعیت ارسال مجدد برای کارشناس ارشد، وضعیت شما باید برگشت درخواست کارشناس ارشد باشد.", 422);
    public static Error InValidStatusForManagementPending = new("InvalidArguments", "برای تغییر به وضعیت در انتظار بررسی کارشناس ارشد، وضعیت شما باید ثبت اولیه یا ارسال مجدد برای کارشناس ارشد باشد.", 422);
    public static Error InValidStatusForManagementConfirmed = new("InvalidArguments", "برای تغییر به وضعیت تایید کارشناس ارشد، وضعیت شما باید در انتظار بررسی کارشناس ارشد باشد.", 422);
    public static Error InValidStatusForManagementReturned = new("InvalidArguments", "برای تغییر به وضعیت برگشت درخواست کارشناس ارشد، وضعیت شما باید در انتظار بررسی کارشناس ارشد باشد.", 422);
    public static Error InValidStatusForManagementRejected = new("InvalidArguments", "برای تغییر به وضعیت رد درخواست کارشناس ارشد، وضعیت شما باید در انتظار بررسی کارشناس ارشد باشد.", 422);
    public static Error InValidStatusForResendForSupplyUnit = new("InvalidArguments", "برای تغییر به وضعیت ارسال مجدد برای واحد تامین، وضعیت شما باید برگشت درخواست واحد تامین یا رد واحد تامین باشد.", 422);
    public static Error InValidStatusForSupplyUnitPending = new("InvalidArguments", "برای تغییر به وضعیت در انتظار بررسی واحد تامین، وضعیت شما باید تایید کارشناس ارشد باشد.", 422);
    public static Error InValidStatusForPendingForSupply = new("InvalidArguments", "برای تغییر به وضعیت تایید واحد تامین، وضعیت شما باید در انتظار بررسی واحد تامین باشد.", 422);
    public static Error InValidStatusForReturnedSupplyUnit = new("InvalidArguments", "برای تغییر به وضعیت برگشت درخواست واحد تامین، وضعیت شما باید در انتظار بررسی واحد تامین باشد.", 422);
    public static Error InValidStatusForSupplyUnitRejected = new("InvalidArguments", "برای تغییر به وضعیت رد درخواست واحد تامین، وضعیت شما باید در انتظار بررسی واحد تامین باشد.", 422);
    public static Error InValidStatusForSupplyUnitReturned = new("InvalidArguments", "برای تغییر به وضعیت برگشت درخواست واحد تامین، وضعیت شما باید در انتظار بررسی واحد تامین باشد.", 422);
    public static Error InValidStatusForPendingForConfirmed = new("InvalidArguments", "برای تغییر به وضعیت در انتظار تامین، وضعیت شما باید تایید واحد تامین باشد.", 422);
    public static Error InValidStatusForArchived = new("InvalidArguments", "برای تغییر به وضعیت بایگانی، وضعیت شما باید تایید واحد تامین باشد.", 422);
    public static Error InValidStatusForClosed = new("InvalidArguments", "برای تغییر به وضعیت بسته شده، وضعیت شما باید تامین کامل باشد.", 422);
    public static Error CanNotRejected = new("InvalidArguments", "به دلیل تامین شدن بخشی از این کالا شما نمیتوانین آن را رد کنین.", 422);
    public static Error InValidStatusForGoodsManagerConfirmed = new("InvalidArguments", "وضعیت فعلی برای تایید مدیر کالا معتبر نیست.", 422);
    public static Error InValidStatusForGoodsManagerReturned = new("InvalidArguments", "وضعیت فعلی برای برگشت درخواست مدیر کالا معتبر نیست.", 422);
    public static Error InValidStatusForGoodsManagerRejected = new("InvalidArguments", "وضعیت فعلی برای رد درخواست مدیر کالا معتبر نیست.", 422);
    public static Error InvalidColumnNames(string? names)
    {
        string msg = "";

        if (!string.IsNullOrWhiteSpace(names))
            msg += $"هدرهای ستون های {names} با تمپلیت همخوانی ندارد.";

        return new Error("ValidationData", msg.Trim(), 400);
    }
    public static string InvalidImportStatus(string? product)
    {
        if (!string.IsNullOrWhiteSpace(product))
            return $"کالای {product} فعال و یا موجود نیست.";
        else
            return $"کالای شما دارای پارت نامبر نمیباشد.";
    }
}

