namespace Engineering.Domain.Errors.WbsTemplates;

public static class WbsTemplateErrors
{
    public static Error WbsTemplateWithIdNotFound = new("NotFound", "هیچ شکست کار ای با این شناسه یافت نشد.", 404);
    public static Error WbsTemplateWithFilterNotFound = new("NotFound", "هیچ شکست کار ای با این فیلترها یافت نشد.", 404);

    ///ProjectWbs
    public static Error ProjectWbsWithIdNotFound = new("NotFound", "هیچ شکست کاری برای پروژه با این شناسه یافت نشد.", 404);
    public static Error ProjectWbsWithParentIdNotFound = new("NotFound", "هیچ شکست کاری برای پروژه با شناسه والد یافت نشد.", 404);
    public static Error ParentProjectWbsWithIdNotFound = new("NotFound", "هیچ شکست کار با شناسه والد یافت نشد.", 404);

    public static Error CircularReference = new("CircularReference", "والد انتخاب‌شده باعث ایجاد چرخه در ساختار شکست کار می‌شود و قابل انتخاب نیست.", 422);

    public static Error ProjectOperationWbsWithIdNotFound = new("NotFound", "هیچ شکست کاری برای شرح عملیات پروژه با این شناسه یافت نشد.", 404);

    public static Error ProjectOperationIdsNotFound = new("CircularReference", "شرح عملیات هایی با این شناسه ها پیدا نشد.", 422);

    public static Error WbsTemplateCodeDuplicate = new("CircularReference", "کد شکست کار تکراری است.", 422);
    public static Error WbsTemplateNameDuplicate = new("CircularReference", "نام شکست کار تکراری است.", 422);

    public static Error ProjectWbsCodeDuplicate = new("CircularReference", "کد شکست کار پروژه در والد و تمپلیت نمیتواند تکراری باشد.", 422);
    public static Error ProjectWbsNameDuplicate = new("CircularReference", "نام شکست کار پروژه در والد و تمپلیت نمیتواند تکراری باشد.", 422);

    public static Error ProjectWbsHasPOWbs = new("HasPOWbs", "شکست کار پروژه دارای شکست کار عملیات پروژه میباشد.", 422);
    public static Error ProjectWbsHasChild = new("HasChild", "شکست کار پروژه دارای زیرمجموعه میباشد.", 422);

    public static Error WbsTemplateHasProjectWbs = new("HasProjectWbs", "شکست کار دارای شکست کار پروژه میباشد.", 422);
}
