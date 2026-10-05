namespace Engineering.Domain.Errors;

public static class TransportationRequestErrors
{
    public static Error DontAllowForAggregates = new("InvalidArguments", "ترابری پکینگ ارسالی تجمیع شده است و نمیتوان پیمانکار آن را تغییر داد.", 422);
    public static Error PackingsDuplicate = new("InvalidArguments", "برای پکینگ های ارسال مرسوله از قبل موجود است.", 422);
    public static Error IsActive = new("InvalidArguments", "وضعیت درخواست ترابری فعال میباشد.", 422);
    public static Error IsInactive = new("InvalidArguments", "وضعیت درخواست ترابری غیرفعال میباشد.", 422);
    public static Error CanNottDelete = new("InvalidArguments", "این درخواست ترابری به دلیل داشتن وابستگی اطلاعاتی قابل حذف نمیباشد.", 422);

    public static Error InValidConfirmedPaymentDate = new("InvalidArguments", "تاریخ پرداخت خالی است.", 422);
    public static Error InValidConfirmedBankAccountId = new("InvalidArguments", "اطلاعات شبا طرف حساب خالی است.", 422);
    public static Error InValidConfirmedPrice = new("InvalidArguments", "مبلغ پرداختی خالی است.", 422);
    public static Error InValidDescription = new("InvalidArguments", "توضیحات خالی است.", 422);
    public static Error InValidPayment = new("InvalidArguments", "صدور دستور پرداخت تنخواه گردان برای درخواستی که دارای پرداخت شخصی میباشد امکان پذیر نیست.", 422);
    public static Error InValidPersonalPayment = new("InvalidArguments", "در صورتی که پرداخت تنخواه گردان نباشد باید درخواست های انتخابی بصورت پرداخت شخصی صورت گرفته باشند.", 422);
    public static Error InValidthirdpartiesCount = new("InvalidArguments", "درخواست دهنده اسنپ های انتخابی بیش از یک نفر میباشد.", 422);
    public static Error InValidThirdPartyPayment = new("InvalidArguments", "درخواست دهنده اسنپ های انتخابی با طرف حساب انتخابی جهت پرداخت مغایرت دارد.", 422);
    public static Error InValidTypeOfTransport = new("InvalidArguments", "نوع ترابری نامعتبر است.", 422);
    public static Error UnvalidTransportationRequest = new("InvalidArguments", "درخواست ترابری نامعتبر است.", 422);
    public static Error UnvalidPackingWarehouse = new("InvalidArguments", "انبار های پکینگ نامعتبر است.", 422);
    public static Error UnvalidPacking = new("InvalidArguments", "پالت های محموله نامعتبر است.", 422);
    public static Error UnvalidShippingCost = new("InvalidArguments", "هزینه ارسال نامعتبر است.", 422);

    public static Error UnvalidPackingRequest(long? requestNumber) => new("InvalidArguments", $"پکینگ با شماره درخواست {requestNumber} در یک ترابری تجمیع شده میباشد ابتدا آن را آزاد کنید", 422);
    public static Error InvalidPayment(string? date, long? requestNumber) => new("InvalidArguments", $"دستور پرداخت درخواست  شماره {requestNumber} در تاریخ {date} ایجاد شده است.", 422);
    public static Error IdIsEmpty = new("InvalidArguments", "شناسه درخواست ترابری نامعتبر است.", 422);
    public static Error PalletIdsIsEmpty = new("InvalidArguments", "شناسه پالت های مرسوله نامعتبر است.", 422);
    public static Error CargoIsEmpty = new("InvalidArguments", "مرسوله ارسالی نامعتبر است.", 422);
    public static Error ShippingTypeIsUnvalid = new("InvalidArguments", "نحوه ارسال جهت ایجاد ترابری نامعتبر است.", 422);
    public static Error DeliveryTypeIsUnvalid = new("InvalidArguments", "نوع ارسال جهت ایجاد ترابری نامعتبر است.", 422);
    public static Error VolumeIsEmpty = new("InvalidArguments", "حجم درخواست ترابری خالی است.", 422);
    public static Error LoadWeightIsEmpty = new("InvalidArguments", "وزن درخواست ترابری خالی است.", 422);
    public static Error CurrencyIsEmpty = new("InvalidArguments", "شناسه ارز درخواست خالی است.", 422);
    public static Error SnapIdsIsEmpty = new("InvalidArguments", "شناسه های درخواست اسنپ خالی است.", 422);
    public static Error RequesterIdsAreEmpty = new("InvalidArguments", "شناسه های درخواست کننده ها خالی است", 422);
    public static Error TransportationId = new("InvalidArguments", "شناسه ترابری خالی است.", 422);
    public static Error StatusIsEmpty = new("InvalidArguments", "وضعیت خالی است.", 422);
    public static Error ManagerDescriptionIsEmpty = new("InvalidArguments", "توضیحات مدیر خالی است.", 422);
    public static Error TripIdIsEmpty = new("InvalidArguments", "شناسه سفر خالی است.", 422);
    public static Error MachineIdIsEmpty = new("InvalidArguments", "شناسه ماشین خالی است.", 422);
    public static Error ProjectIdIsEmpty = new("InvalidArguments", "شناسه پروژه خالی است.", 422);
    public static Error BillOfLadingIdIsEmpty = new("InvalidArguments", "شناسه بارنامه خالی است.", 422);
    public static Error RequestByIdIsEmpty = new("InvalidArguments", "شناسه درخواست دهنده خالی است.", 422);
    public static Error StartingCityIdIsEmpty = new("InvalidArguments", "شناسه شهر مبدا خالی است.", 422);
    public static Error StartingCityAddressIsEmpty = new("InvalidArguments", "آدرس شهر مبدا خالی است.", 422);
    public static Error DestinationCityIdIsEmpty = new("InvalidArguments", "شناسه شهر مقصد خالی است.", 422);
    public static Error DestinationCityAddressIsEmpty = new("InvalidArguments", "آدرس شهر مقصد خالی است.", 422);
    public static Error StartDateIsEmpty = new("InvalidArguments", " تاریخ شروع خالی است.", 422);
    public static Error EndDateIsEmpty = new("InvalidArguments", " تاریخ پایان خالی است.", 422);
    public static Error CostCenterIsEmpty = new("InvalidArguments", "مرکز هزینه خالی است.", 422);
    public static Error ProjectOperationIdsNotValid = new("InvalidArguments", "شناسه ای از شرح عملیات پروژه صحیح نمیباشد.", 422);
    public static Error ProjectOperationDetailIdsNotValid = new("InvalidArguments", "شناسه ای از ریز متره صحیح نمیباشد.", 422);
    public static Error RequestByIdNotValid = new("InvalidArguments", "شناسه درخواست دهنده صحیح نمیباشد.", 422);
    public static Error RequestNotValid = new("InvalidArguments", "درخواست نامعتبر است.", 422);
    public static Error DriverIdNotValid = new("InvalidArguments", "شناسه راننده صحیح نمیباشد.", 422);
    public static Error BankIdNotValid = new("InvalidArguments", "شناسه بانک صحیح نمیباشد.", 422);
    public static Error StartingCityIdNotValid = new("InvalidArguments", "شناسه شهر مبدا درست نمیباشد.", 422);
    public static Error DriverInfoUnValid = new("InvalidArguments", "شما یا نام راننده را وارد کنید یا شناسه راننده را..", 422);
    public static Error DestinationCityIdNotValid = new("InvalidArguments", "شناسه شهر مقصد درست نمیباشد.", 422);
    public static Error SecDestinationCityIdNotValid = new("InvalidArguments", "شناسه شهر مقصد دوم درست نمیباشد.", 422);
    public static Error SnapRequesterNotValid = new("InvalidArguments", "درخواست کننده اسنپ یافت نشد.", 422);
    public static Error AirPlanePassengerNotValid = new("InvalidArguments", "مسافر یافت نشد.", 422);
    public static Error AirPlaneTicketPayerNotValid = new("InvalidArguments", "پرداخت کننده بلیط یافت نشد.", 422);
    public static Error DateTimeNotValid = new("InvalidArguments", "تاریخ پایان باید بزرگتر از تاریخ شروع باشد.", 422);
    public static Error PostageDateIsNotValid = new("InvalidArguments", "تاریخ ارسال نامعتبر میباشد.", 422);
    public static Error PostageDateNotValid = new("InvalidArguments", "تاریخ دریافت نمیتواند کوچکتر از تاریخ ارسال باشد.", 422);
    public static Error UnValidCostCenters = new("InvalidArguments", "تعداد مرکز هزینه های ورودی شما با تعداد مراکز هزینه های برگردانده شده مطابقت ندارد.", 422);
    public static Error UnValidProjects = new("InvalidArguments", "تعداد پروژه های ورودی شما با تعداد پروژه های برگردانده شده مطابقت ندارد.", 422);
    public static Error UnValidCostGroups = new("InvalidArguments", "تعداد گروه هزینه های ورودی شما با تعداد گروه هزینه های برگردانده شده مطابقت ندارد.", 422);
    public static Error UnValidCostCategories = new("InvalidArguments", "تعداد دسته بندی  های ورودی شما با تعداد دسته بندی های برگردانده شده مطابقت ندارد.", 422);
    public static Error UnValidTrips = new("InvalidArguments", "تعداد نوع سفر های ورودی شما با تعداد نوع سفر های برگردانده شده مطابقت ندارد.", 422);
    public static Error UnValidCities = new("InvalidArguments", "تعداد شهر های ورودی شما با تعداد شهر های برگردانده شده مطابقت ندارد.", 422);
    public static Error UnValidThirdParties = new("InvalidArguments", "تعداد افراد ورودی شما با تعداد افراد برگردانده شده مطابقت ندارد.", 422);
    public static Error UnvalidParams = new("InvalidArguments", "فقط یکی از موارد کد یا شماره تماس را برای کاربر وارد نمایید.", 422);

    public static Error IsActiveIsEmpty = new("InvalidArguments", "وضعیت درخواست ترابری خالی است.", 422);
    public static Error UnvalidCargoState = new("InvalidArguments", "وضعیت مرسوله نامعتبر است.", 422);
    public static Error CargosNotfound = new("NotFound", "مرسوله یافت نشد.", 404);
    public static Error CargosPalletNotfound = new("NotFound", "پلت های مرسوله یافت نشد.", 204);
    public static Error CargosPalletWithNotfound = new("NotFound", "پلت مرسوله یافت نشد.", 404);
    public static Error CargosPalletTransport = new("NotFound", "پلت مرسوله دارای ترابری است و نمیتوان آن را اصلاح کرد.", 404);
    public static Error CargosPalletHaveTransport = new("InvalidArguments", "مرسوله شما دارای ترابری میباشد ابتدا باید  آن را آزاد سازی کنید.", 422);
    public static Error TransportationRequestWithIdNotFound = new("NotFound", "هیچ درخواستی با این شناسه یافت نشد.", 404);
    public static Error FilteredTransportationRequestNotFound = new("NotFound", "هیچ درخواست ترابری ای با این اطلاعات یافت نشد.", 204);
    public static Error FilteredSnapNotFound = new("NotFound", "هیچ درخواست اسنپی با این اطلاعات یافت نشد.", 204);
    public static Error FilteredAirPlaneNotFound = new("NotFound", "هیچ درخواست هواپیمایی با این اطلاعات یافت نشد.", 204);
    public static Error TransportationRequestChildNotFound = new("NotFound", "درخواست ترابری انتخابی هیچ زیرشاخه ای ندارد.", 204);
    public static Error IsDeleted = new("NotFound", "این درخواست ترابری حذف شده است.", 204);
    public static Error ShippingcostNotfound = new("InvalidArguments", "هزینه ارسالی برای مقصد پکینگ مد نظر یافت نشد.", 422);

    public static Error UnValidStatus = new("InvalidArguments", "وضعیت درخواست نامعتبر است.", 422);
    public static Error UnValidPackingStatus = new("InvalidArguments", "بسته در وضعیت عدم تایید حراست است.", 422);
    public static Error WarehouseCreate = new("InvalidArguments", "در بررسی و ایجاد مرسوله خطایی رخ داد.", 422);
    public static Error UserInfoNotFound = new("InvalidArguments", "اطلاعات کاربر یافت نشد", 422);
    public static Error UserIsUnValid = new("InvalidArguments", "شما ثبت کننده این درخواست نیستید و دسترسی ثبت تغییر در این درخواست را ندارید.", 422);
    public static Error UnValidId = new("InvalidArguments", "شناسه درخواست ترابری نامعتبر است.", 422);
    public static Error UnValidData = new("InvalidArguments", "اطلاعات وارد شده نامعتبر است.", 422);
    public static Error UnValidRequests = new("InvalidArguments", "درخواست های انتخابی نامعتبر است.", 422);
    public static Error UnValidDriver = new("InvalidArguments", " طرف حساب راننده نامعتبر است.", 422);
    public static Error UnValidPaymentThirdParty = new("InvalidArguments", " طرف حساب جهت پرداخت نامعتبر است.", 422);
    public static Error UnValidIban = new("InvalidArguments", "شماره شبای حساب یا حساب بانکی راننده را وارد کنید.", 422);
    public static Error UnNumberPlates = new("InvalidArguments", "تعداد کاراکتر های پلاک میبایست 7 رقم  و یک حرف باشد.", 422);
    public static Error NumberPlatesInValid = new("InvalidArguments", "پلاک نامعتبر است.", 422);
    public static Error DeliveryMethodInValid = new("InvalidArguments", "روش ارسال نامعتبر است.", 422);
    public static Error DeliveryTypeInValid = new("InvalidArguments", "نوع ارسال نامعتبر است.", 422);

    public static Error HaveChild = new("InvalidArguments", "درخواست ترابری انتخابی به دلیل وابستگی اطلاعاتی، قابل تغییر نمیباشد.", 422);

    public static Error PackingAlreadyConfirmed(long? requestNumber)
    {
        if (requestNumber.HasValue)
        {
            return new Error("PackingAlreadyConfirmed", $"بسته با شماره درخواست {requestNumber} تایید شده است و امکان اصلاح بسته‌بندی وجود ندارد", 400);
        }
        return new Error("PackingAlreadyConfirmed", $"بسته تایید شده است و امکان اصلاح بسته‌بندی وجود ندارد", 400);
    }
}