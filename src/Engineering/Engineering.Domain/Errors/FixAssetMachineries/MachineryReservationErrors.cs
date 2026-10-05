namespace Engineering.Domain.Errors;

public static class MachineryReservationErrors
{
    public static Error MachineryReservationNotFoundWithId = new("NotFound", "رزرو ماشین آلات با این شناسه یافت نشد.", 404);

    public static Error IdIsEmpty = new("InvalidArguments", "شناسه خالی است.", 422);
    public static Error FixIdIsEmpty = new("InvalidArguments", "شناسه موجودی ماشین آلات خالی است.", 422);
    public static Error RequestIdIsEmpty = new("InvalidArguments", "شناسه درخواست ماشین آلات خالی است.", 422);
    public static Error IdsIsEmpty = new("InvalidArguments", "شناسه ها خالی است.", 204);
    public static Error InValidType = new("InvalidArguments", "واحد رزرو ماشین آلات نامعتبر است.", 422);
    public static Error InValidStatus = new("InvalidArguments", "وضعیت رزرو ماشین آلات نامعتبر است.", 422);
    public static Error InValidFixMachinery = new("InvalidArguments", "موجودی ماشین آلات نامعتبر است.", 422);
    public static Error InValidRequestMachinery = new("InvalidArguments", "درخواست ماشین آلات نامعتبر است.", 422);
    public static Error InValidStartDate = new("InvalidArguments", "تاریخ شروع نامعتبر است.", 422);
    public static Error InValidEndDate = new("InvalidArguments", "تاریخ پایان نامعتبر است.", 422);
    public static Error InValidDates = new("InvalidArguments", "تاریخ پایان از تاریخ شروع کوچیکتر است.", 422);
    public static Error InValidMachineryReservation = new("InvalidArguments", "رزرو ماشین آلات نامعتبر است.", 422);
    public static Error InValidMachineryReservationId = new("InvalidArguments", "شناسه رزرو نامعتبر است", 422);
    public static Error IsDeleted = new("NotFound", "رزرو ماشین آلات حذف شده است.", 204);
    public static Error FilteredMachineryReservationNotFound = new("NotFound", "هیچ رزرو ماشین آلاتی با این اطلاعات یافت نشد.", 204);
    public static Error DuplicateDate(string? startDate, TimeSpan? startTime, string? endDate, TimeSpan? endTime, RequestMachinery requestMachinery) => new("InvalidArguments", $"تاریخ یا ساعت رزرو ارسالی با تاریخ یا ساعت رزرو (تاریخ {startDate} ساعت {startTime} تا تاریخ {endDate} ساعت {endTime}) در شماره درخواست {requestMachinery.RequestNumber} تداخل دارد.", 422);
    public static Error DatesAreInFixDates = new("InvalidArguments", "تاریخ رزرو باید بین تاریخ شروع و پایان موجودی ماشین باشد.", 422);
    public static Error CantWork = new("InvalidArguments", "ماشین انتخابی در بازه انتخابی امکان فعالیت ندارد.", 422);
    public static Error InvalidMachinery = new("InvalidArguments", "ماشین آلات موجودی با ماشین آلات درخواست متفاوت است.", 422);

}
