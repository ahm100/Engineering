namespace Engineering.Domain.Errors;

public static class TransportationContractorErrors
{
    public static Error IdIsEmpty = new("InvalidArguments", "شناسه مقدار ندارد", 422);
    public static Error IdIsNull = new("InvalidArguments", "شناسه خالی است", 422);
    public static Error ContractorIdIsNull = new("InvalidArguments", "شناسه پیمانکار خالی است", 422);

    public static Error FilteredTransportationContractorNotFound
        = new Error("TransportationContractor.FilteredTransportationContractorNotFound", "اطلاعات پیمانکاران حمل یافت نشد", 204);

    public static Error HistoryNotfound
        = new Error("TransportationContractor.HistoryNotfound", "تاریخچه یافت نشد.", 204);

    public static Error FilteredTransportationContractorPersonnelNotFound
        = new Error("TransportationContractor.FilteredTransportationContractorPersonnelNotFound", "اطلاعات پرسنل پیمانکاران حمل یافت نشد", 204);

    public static Error FilteredTransportationContractorMachineNotFound
        = new Error("TransportationContractor.FilteredTransportationContractorMachineNotFound", "اطلاعات ماشین پیمانکاران حمل یافت نشد", 204);

    public static Error TransportationContractorNotFound
        = new Error("TransportationContractor.TransportationContractorNotFound", "پیمانکار حمل یافت نشد", 404);

    public static Error TransportationContractorPersonnelNotFound
        = new Error("TransportationContractor.TransportationContractorPersonnelNotFound", "پرسنل پیمانکار حمل یافت نشد", 404);

    public static Error TransportationContractorMachineNotFound
        = new Error("TransportationContractor.TransportationContractorMachineNotFound", "ماشین پیمانکار حمل یافت نشد", 404);

    public static Error WarehouseNotFound
        = new Error("TransportationContractor.WarehouseNotFound", "انبار یافت نشد", 404);

    public static Error InvoiceNotFound
        = new Error("TransportationContractor.InvoiceNotFound", "حواله کالا یافت نشد", 404);

    public static Error PackingTypeNotValid
        = new Error("TransportationContractor.PackingTypeNotValid", "پکینگ های انتخابی باید از نوع خروج باشند.", 422);

    public static Error PackingStatusNotValid
        = new Error("TransportationContractor.PackingStatusNotValid", "پکینگ های انتخابی باید در وضعیت ارسال نشده باشند.", 422);

    public static Error DestinationNotValid
        = new Error("TransportationContractor.PackingStatusNotValid", "مقصد نامعتبر است.", 422);

    public static Error PackingNotFound
        = new Error("TransportationContractor.PackingNotFound", "پکینگ یافت نشد", 404);

    public static Error ThirdPartiesNotFound
        = new Error("TransportationContractor.ThirdPartiesNotFound", "طرف حساب یافت نشد", 404);

    public static Error ProductsNotFound
        = new Error("TransportationContractor.ProductsNotFound", "کالا یافت نشد", 404);

    public static Error ShippingNotFound
        = new Error("TransportationContractor.ShippingNotFound", "هزینه ارسال یافت نشد", 404);

    public static Error TransportationContractorIsDuplicate
        = new Error("TransportationContractor.TransportationContractorIsDuplicate", "پیمانکار حمل تکراری است", 409);

    public static Error ContractorCount
        = new Error("TransportationContractor.ContractorCount", "پیمانکار های پیکینگ ها یکسان نیستند", 409);

    public static Error DeliveryCount
        = new Error("TransportationContractor.DeliveryCount", "روش یا نوع ارسال متفاوت موجود است", 409);

    public static Error UpdateFeild
        = new Error("TransportationContractor.UpdateFeild", "ویرایش پکینگ های ارسالی با خطا مواجه شد.", 409);

    public static Error UpdateStatusFeild
        = new Error("TransportationContractor.UpdateStatusFeild", "تغییر وضعیت پکینگ های ارسالی با خطا مواجه شد.", 409);

    public static Error MoreThanOneIsFixed
        = new Error("TransportationContractor.MoreThanOneIsFixed", "بیش از یک مقدار ثابت مازاد تعریف شده است.", 400);

    public static Error DuplicateUntilWeight
        = new Error("TransportationContractor.MoreThanOneIsFixed", "بیش از یک مقدار ثابت مازاد تعریف شده است.", 400);
}