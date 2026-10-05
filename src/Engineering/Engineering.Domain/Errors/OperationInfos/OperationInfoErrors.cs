
namespace Engineering.Domain.Errors;

public static class OperationInfoErrors
{
    public static Error NameIsDuplicate = new("Duplicate", "نام شرح عملیات تکراری است.", 409);
    public static Error CodeIsDuplicate = new("Duplicate", "کد شرح عملیات تکراری است.", 409);
    public static Error PublicGroupIsDuplicate = new("Duplicate", "درخواست شامل گروه کالای تکراری است.", 409);

    public static Error IsActive = new("InvalidArguments", "وضعیت شرح عملیات فعال میباشد.", 422);
    public static Error CantCreateThisRelation = new("InvalidArguments", "شما نمیتوانید به همین شرح عملیات وابستگی داشته باشید.", 422);
    public static Error CantCreateChildRelation = new("InvalidArguments", "شما نمیتوانید به شرح عملیات هایی که با همین شرح عملیات وابستگی دارند، وابستگی ایجاد کنید.", 422);
    public static Error IsInactive = new("InvalidArguments", "وضعیت شرح عملیات غیرفعال میباشد.", 422);
    public static Error CanNottDelete = new("InvalidArguments", "این شرح عملیات به دلیل داشتن وابستگی اطلاعاتی قابل حذف نمیباشد.", 422);
    public static Error CanNottDeleteForServiceInfo = new("InvalidArguments", "این شرح عملیات به دلیل داشتن خدمات قابل حذف نمیباشد.", 422);
    public static Error CanNottDeleteForProjectOperations = new("InvalidArguments", "این شرح عملیات به دلیل داشتن شرح عملیات پروژه قابل حذف نمیباشد.", 422);
    public static Error CanNottDeleteForProjectOperationDetails = new("InvalidArguments", "این شرح عملیات به دلیل داشتن ریزمتره قابل حذف نمیباشد.", 422);
    public static Error CanNottDeleteForDependencies = new("InvalidArguments", "این شرح عملیات به دلیل داشتن وابستگی شرح عملیات قرارداد قابل حذف نمیباشد.", 422);
    public static Error CanNottDeleteForConsideration = new("InvalidArguments", "این شرح عملیات به دلیل داشتن خدمات شرح عملیات قابل حذف نمیباشد.", 422);
    public static Error UnValidExperts = new("InvalidArguments", "شناسه ای از متخصص نامعتبر است.", 422);
    public static Error UnValidMachineries = new("InvalidArguments", "شناسه ای از ماشین آلات و ابزار نامعتبر است.", 422);
    public static Error ExistsMachineryInList = new("InvalidArguments", "شناسه ای از ماشین آلات و ابزار تکراری است.", 422);
    public static Error ExistsExpertInList = new("InvalidArguments", "شناسه ای از متخصص تکراری است.", 422);
    public static Error ExistsProductInList = new("InvalidArguments", "شناسه ای از کالا تکراری است.", 422);
    public static Error ExistsCategoryInList = new("InvalidArguments", "شناسه ای از دسته بندی تکراری است.", 422);
    public static Error UnValidProducts = new("InvalidArguments", "شناسه ای از کالا نامعتبر است.", 422);
    public static Error ProductGroupIsInActive = new("InvalidArguments", "گروه کالایی غیر فعال میباشد لطفا از مسئول مربوطه پیگیری کنید.", 422);
    public static Error UnValidCategories = new("InvalidArguments", "شناسه ای از دسته بندی نامعتبر است.", 422);
    public static Error ServiceInfoIsNeed = new("InvalidArguments", "شما باید حداقل یک خدمت را انتخاب کنید.", 422);
    public static Error SeasonIdIsEmpty = new("InvalidArguments", "شناسه فصل خالی است.", 422);
    public static Error NonStandardIdIsEmpty = new("InvalidArguments", "شناسه کالای غیر استاندارد خالی است.", 422);
    public static Error ContractorIsEmpty = new("InvalidArguments", "شناسه پیمانکار خالی است.", 422);
    public static Error ContractorMustGreaterThanZero = new("InvalidArguments", "شناسه پیمانکار باید از صفر بزرگتر باشد است.", 422);
    public static Error CanNotRealated = new("InvalidArguments", "به دلیل وابستگی شرح عملیات های دیگر به شرح عملیات انتخابی امکان تغییر اولویت وجود نخواهد داشت.", 422);
    public static Error CanNotRealatedtwoSamePriority = new("InvalidArguments", "بدلیل برابری الویت بین عملیات های مدنظر امکان برقراری وابستگی وجود ندارد.", 422);
    public static Error CanNotRealatedforDependencyPriority = new("InvalidArguments", "شرح عملیاتی که می خواهید برای درج وابستگی استفاده کنید، اولویت پایینتری نسبت به شرح عملیات مدنظر شما دارد.", 422);
    public static Error CanNotGetDependency = new("InvalidArguments", "شما با الویت 1 نمیتوانید وابستگی داشته باشید.", 422);
    public static Error CanNotaddDependency = new("InvalidArguments", "شما با الویت خالی نمیتوانید وابستگی داشته باشید.", 422);
    public static Error FilterDataIsEmpty = new("InvalidArguments", "دیتای فیلتر خالی است!", 422);
    public static Error WorkingDayIsLow = new("InvalidArguments", "شما نمیتوانین با نوع وابستگی تاریخ شروع، روزکاری رو منفی وارد کنین.", 422);
    public static Error PriorityIsOne = new("InvalidArguments", "با الویت 1 شما نمیتوانین وابستگی داشته باشید.", 422);
    public static Error PriorityIsNull = new("InvalidArguments", "با الویت خالی شما نمیتوانید وابستگی داشته باشید.", 422);
    public static Error PriorityIsGreatertThanDependenciesPriority = new("InvalidArguments", "مقداراولویت وارد شده،الویت پایینتری از اولویت شرح عملیات های وابسته به خود دارد", 422);
    public static Error PriorityIsLesstThanDependenciesPriority = new("InvalidArguments", "مقداراولویت وارد شده،الویت بالاتری از اولویت شرح عملیاتی که به آن وابسته است دارد", 422);
    public static Error CanNotDeleteForContractorServices = new("InvalidArguments", "شما نمیتوانین خدمتی را از شرح عملیات حذف کنین که در، خدمات ریزمتره استفاده شده است.", 422);

    public static Error OperationInfoWithIdNotFound = new("NotFound", "هیچ شرح عملیاتی با این شناسه یافت نشد.", 204);
    public static Error OperationInfoActionWithIdNotFound = new("NotFound", "هیچ عملیاتی با این شناسه یافت نشد.", 204);
    public static Error OperationInfoActionIsDeleted = new("NotFound", "فعالیت پاک شده است.", 404);
    public static Error DependencyWithIdNotFound = new("NotFound", "هیچ وابستگی ای با این شناسه یافت نشد.", 204);
    public static Error FilteredDependencyNotFound = new("NotFound", "شرح عملیات مدنظر، شرح عملیات وابسته ای به خود ندارد.", 204);
    public static Error OperationInfoDependencyWithIdNotFound = new("NotFound", "هیچ شرح عمللیاتی برای وابستگی با این شناسه یافت نشد.", 204);
    public static Error OperationInfoWithCodeNotFound = new("NotFound", "هیچ شرح عملیات ای با این شناسه یافت نشد.", 404);
    public static Error OperationInfoWithCodesNotFound = new("NotFound", "هیچ شرح عملیات ای با این کدها یافت نشد.", 404);
    public static Error OperationInfoWithNameNotFound = new("NotFound", "هیچ شرح عملیاتی با این نام یافت نشد.", 404);
    public static Error OperationInfoNonStandardWithIdNotFound = new("NotFound", "هیچ گروه کالای عمومی با این شناسه یافت نشد.", 404);
    public static Error OperationInfoNonStandardNotFound = new("NotFound", "هیچ گروه کالای عمومی با این اطلاعات یافت نشد.", 204);
    public static Error FilteredOperationInfoNotFound = new("NotFound", "هیچ شرح عملیاتی با این اطلاعات یافت نشد.", 204);
    public static Error OperationInfoChildNotFound = new("NotFound", "شرح عملیات انتخابی زیرشاخه ای ندارد.", 204);
    public static Error OperationInfoContractorsNotFound = new("NotFound", "پیمانکاری با اطلاعات درخواستی یافت نشد.", 204);
    public static Error IsDeleted = new("NotFound", "شرح عملیات حذف شده است.", 204);
    public static Error NonstandardIsDeleted = new("NotFound", "کالای غیر استاندارد شرح عملیات حذف شده است.", 204);
    public static Error IsDeletedDependency = new("NotFound", "وابستگی شرح عملیات حذف شده است.", 204);

    public static Error UnValidId = new("InvalidArguments", "شرح عملیات با این شناسه نامعتبر است.", 422);
    public static Error UnValidIds = new("InvalidArguments", "هیچ شناسه شرح عملیاتی وارد نشده است.", 422);
    public static Error IdIsEmpty = new("InvalidArguments", "شناسه خالی است.", 422);
    public static Error OInfoIdIsEmpty = new("InvalidArguments", "شناسه شرح عملیات خالی است.", 422);
    public static Error Actui = IdIsEmpty = new("InvalidArguments", "شناسه خالی است.", 422);
    public static Error NonstandardIdsIsEmpty = new("InvalidArguments", "شناسه های گروه کالا خالی است.", 422);
    public static Error SeasonIsEmpty = new("InvalidArguments", "فصل خالی است.", 422);
    public static Error PriorityCanNot0 = new("InvalidArguments", "الویت نمیتواند صفر یا کمتر از صفر باشد.", 422);
    public static Error PriorityCanNot1 = new("InvalidArguments", "الویت نمیتواند یک یا کمتر از یک باشد.", 422);
    public static Error OperationInfoNameIsEmpty = new("InvalidArguments", "نام خالی است.", 422);
    public static Error OperationInfoCodeIsEmpty = new("InvalidArguments", "کد خالی است.", 422);
    public static Error UnitOfMeasurementIdIsEmpty = new("InvalidArguments", "واحد اندازگیری خالی است.", 422);
    public static Error UnitOfMeasurementNameIsNotFound = new("InvalidArguments", "واحد اندازگیری پیدا نشد.", 422);
    public static Error IsActiveIsEmpty = new("InvalidArguments", "وضعیت خالی است.", 422);
    public static Error SetPriorityIsEmpty = new("InvalidArguments", "الویت دهی شرح عملیات خالی هست!", 422);
    public static Error ExpertStandardsIsEmpty = new("InvalidArguments", " مهارت شرح عملیات خالی هست!", 422);
    public static Error GoodsStandardsIsEmpty = new("InvalidArguments", " کالاهای شرح عملیات خالی هست!", 422);
    public static Error MachineryStandardsIsEmpty = new("InvalidArguments", " ابزار های شرح عملیات خالی هست!", 422);
    public static Error OperationInfoDependencyIsEmpty = new("InvalidArguments", " ,وابستگی شرح عملیات خالی هست!", 422);
    public static Error ServiceInfoIdsIsEmpty = new("InvalidArguments", " , شناسه های خدمات شرح عملیات خالی هست!", 422);

    public static Error HaveChild = new("InvalidArguments", "شرح عملیات دارای وابستگی اطلاعاتی است و قابل تغییر نمیباشد.", 422);

    public static Error SeasonIdsIsEmpty = new("InvalidArguments", "لیست فصل ها خالی میباشد.", 422);
    public static Error SeasonIdsLessThanOrEqualZero = new("InvalidArguments", "شناسه ای از فصل ها خالی یا برابر با صفر است.", 422);
    public static Error SeasonIsInActive = new("ValidationData", "فصل انتخاب شده غیرفعال میباشد.", 400);
    public static Error BranchIsInActive = new("ValidationData", "رشته انتخاب شده غیرفعال میباشد.", 400);
    public static Error CategoryIsInActive = new("ValidationData", "رسته انتخاب شده غیرفعال میباشد.", 400);
    public static Error SeasonIdsIsNull = new("InvalidArguments", "فصلی انتخاب نشده است.", 422);

    public static Error MeasureUnitNotFound = new("NotFound", "هیچ واحد اندازه گیری با این نام یافت نشد.", 404);
    public static Error InvalidUpdateStatus(string? categories, string? branchs, string? seasons)
    {
        string msg = "";

        if (!string.IsNullOrWhiteSpace(categories))
            msg += $"رسته های {categories} فعال نیستند. ";

        if (!string.IsNullOrWhiteSpace(branchs))
            msg += $"رشته های {branchs} فعال نیستند. ";

        if (!string.IsNullOrWhiteSpace(seasons))
            msg += $"فصل های {seasons} فعال نیستند.";

        return new Error("ValidationData", msg.Trim(), 400);
    }

    public static Error InvalidImportStatus(string? unitMesures, string? categories, string? branchs, string? seasons)
    {
        string msg = "";

        if (!string.IsNullOrWhiteSpace(unitMesures))
            msg += $"واحد های اندازه گیری {unitMesures} فعال نیستند. ";

        if (!string.IsNullOrWhiteSpace(branchs))
            msg += $"رشته های {branchs} فعال نیستند. ";

        if (!string.IsNullOrWhiteSpace(seasons))
            msg += $"فصل های {seasons} فعال نیستند.";

        return new Error("ValidationData", msg.Trim(), 400);
    }
}