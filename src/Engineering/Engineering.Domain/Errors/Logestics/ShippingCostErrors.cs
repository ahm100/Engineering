namespace Engineering.Domain.Errors;

public static class ShippingCostErrors
{
    public static Error ThirdPartyIdIsEmpty = new("InvalidArguments", "شناسه طرف حساب نامعتبر است.", 422);
    public static Error ThirdPartyCompanyIsEmpty = new("InvalidArguments", "شناسه کمپانی نماینده نامعتبر است.", 422);
    public static Error RegionIdIsEmpty = new("InvalidArguments", "شناسه ناحیه نامعتبر است.", 422);
    public static Error CityIdIsEmpty = new("InvalidArguments", "شناسه شهر نامعتبر است.", 422);
    public static Error PriceIsEmpty = new("InvalidArguments", "مبلغ نامعتبر است.", 422);
    public static Error IdIsEmpty = new("InvalidArguments", "شناسه هزینه ارسال مقدار ندارد.", 422);
    public static Error IdIsNull = new("InvalidArguments", "شناسه هزینه ارسال خالی است.", 422);
    public static Error TaxIsNull = new("InvalidArguments", "مالیات نامعتبر.", 422);
    public static Error PriceIsNull = new("InvalidArguments", "قیمت نامعتبر است.", 422);
    public static Error LoadWeightIsNull = new("InvalidArguments", "وزن نامعتبر است.", 422);
    public static Error CountIsNull = new("InvalidArguments", "تعداد نامعتبر است.", 422);
    public static Error UntilWeightIsNull = new("InvalidArguments", "تا وزن نامعتبر است.", 422);

    public static Error HistoryNotfound
        = new Error("TransportationContractor.HistoryNotfound", "تاریخچه یافت نشد.", 204);

    public static Error FilteredShippingCostNotFound
        = new Error("ShippingCost.ShippingCostNotFound", "اطلاعات پیمانکاران حمل یافت نشد", 204);

    public static Error ShippingCostNotFound
        = new Error("ShippingCost.ShippingCostNotFound", "هزینه ارسال یافت نشد", 404);

    public static Error SourceNotFound
        = new Error("ShippingCost.SourceNotFound", "مبدا یافت نشد", 404);

    public static Error DestinationNotFound
        = new Error("ShippingCost.DestinationNotFound", "مقصد یافت نشد", 404);

    public static Error ThirdPartiesNotFound
        = new Error("ShippingCost.ThirdPartiesNotFound", "طرف حساب یافت نشد", 404);

    public static Error ShippingCostIsDuplicate
        = new Error("ShippingCost.ShippingCostIsDuplicate", "هزینه ارسال تکراری است", 409);
}