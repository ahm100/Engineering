
namespace Engineering.Domain.Errors;

public static class ProjectOperationErrors
{
    public static Error CantCreateThisRelation = new("InvalidArguments", "شما نمیتوانید به همین شرح عملیات پروژه وابستگی داشته باشید.", 422);
    public static Error CantCreateChildRelation = new("InvalidArguments", "شما نمیتوانید به شرح عملیات پروژه هایی که با همین شرح عملیات پروژه وابستگی دارند، وابستگی ایجاد کنید.", 422);
    public static Error IsInactive = new("InvalidArguments", "وضعیت شرح عملیات پروژه غیرفعال میباشد.", 422);
    public static Error CanNottDeleteForDependencies = new("InvalidArguments", "این شرح عملیات پروژه به دلیل داشتن وابستگی شرح عملیات پروژه قرارداد قابل حذف نمیباشد.", 422);
    public static Error CanNotRealatedtwoSamePriority = new("InvalidArguments", "بدلیل برابری الویت بین عملیات های مدنظر امکان برقراری وابستگی وجود ندارد.", 422);
    public static Error CanNotRealatedforDependencyPriority = new("InvalidArguments", "شرح عملیات پروژهی که می خواهید برای درج وابستگی استفاده کنید، اولویت پایینتری نسبت به شرح عملیات پروژه مدنظر شما دارد.", 422);
    public static Error CanNotaddDependency = new("InvalidArguments", "شما با الویت خالی نمیتوانید وابستگی داشته باشید.", 422);
    public static Error FilterDataIsEmpty = new("InvalidArguments", "دیتای فیلتر خالی است!", 422);
    public static Error RelationDayIsLow = new("InvalidArguments", "شما نمیتوانین با نوع وابستگی تاریخ شروع، روزکاری رو منفی وارد کنین.", 422);
    public static Error PriorityIsOne = new("InvalidArguments", "با الویت 1 شما نمیتوانین وابستگی داشته باشید.", 422);
    public static Error PriorityIsNull = new("InvalidArguments", "با الویت خالی شما نمیتوانید وابستگی داشته باشید.", 422);
    public static Error PriorityIsGreatertThanDependenciesPriority = new("InvalidArguments", "مقداراولویت وارد شده،الویت پایینتری از اولویت شرح عملیات پروژه های وابسته به خود دارد", 422);
    public static Error PriorityIsLesstThanDependenciesPriority = new("InvalidArguments", "مقداراولویت وارد شده،الویت بالاتری از اولویت شرح عملیات پروژه ای که به آن وابسته است دارد", 422);
    public static Error IsDeletedDependency = new("NotFound", "وابستگی شرح عملیات پروژه حذف شده است.", 204);
    public static Error SetPriorityIsEmpty = new("InvalidArguments", "الویت دهی شرح عملیات پروژه خالی هست!", 422);
    public static Error ProjectOperationDependencyIsEmpty = new("InvalidArguments", " ,وابستگی شرح عملیات پروژه خالی هست!", 422);
    public static Error HaveDaily = new("InvalidArguments", "در ریزمتره شما کارکرد روزانه ای وجود دارد که تاریخ شروعش از تاریخ شروع مد نظر شما کمتر میباشد و شما نمیتوانین این تغییر را انجام دهید.", 422);
    public static Error HaveChild = new("InvalidArguments", "شرح عملیات پروژه دارای وابستگی اطلاعاتی است و قابل تغییر نمیباشد.", 422);

    public static Error UnValidId = new("InvalidArguments", "شناسه عملیات پروژه نامعتبر است.", 422);
    public static Error UnValidIds = new("InvalidArguments", "هیچ شناسه شرح عملیات پروژه ای وارد نشده است.", 422);
    public static Error UnValidData = new("InvalidArguments", "شرح عملیات پروژه با این اطلاعات وجود ندارد.", 422);
    public static Error IsExsist = new("InvalidArguments", "شما نمیتوانین در یک پروژه شرح عملیات پروژه با واحد یکسان داشته باشید.", 422);
    public static Error IsExsistForDelete = new("InvalidArguments", "به دلیل حذف شدن این شرح عملیات پروژه از قرارداد با مشکل شرح عملیات های پروژه بدون قرارداد تکراری رو به رو میشیم، شما نمیتوانین این شرح عملیات را حذف کنید. برای حذف حتما واحدش را عوض کنید.", 422);
    public static Error SelectProject = new("InvalidArguments", "درصوت انتخاب نکردن قراردادکارفرما باید پروژه را مشخص کنید.", 422);
    public static Error SelectValidateData = new("InvalidArguments", "قرارداد کارفرما با پروژه انتخابی همخوانی ندارد", 422);

    public static Error ProjectChildNotFound = new("NotFound", "ریزمتره ای با این شرح عملیات های پروژه یافت نشد.", 204);
    public static Error ProjectOperationChildNotFound = new("NotFound", "ریزمتره ای با این شرح عملیات پروژه یافت نشد.", 204);
    public static Error DataNotFoundWithFilters = new("NotFound", "شرح عملیات پروژه ای با فیلتر های درخواست شده یافت نشد.", 204);
    public static Error OperationInfoNotFoundWithFilters = new("NotFound", "شرح عملیات ای با فیلتر های درخواست شده یافت نشد.", 204);
    public static Error DataNotFoundWithOpAndPId = new("NotFound", "برای این شرح عملیات و پروژه هیچ شرح عملیات پروژه ای یافت نشد.", 422);
    public static Error ProjectOperationWithIdNotFound = new("NotFound", "هیچ عملیات پروژه ای با این شناسه یافت نشد.", 204);
    public static Error ProjectOperationDoesNotHaveEstimate = new("NotFound", "عملیات پروژه دارای تاریخ تقریبی شروع و پایان نیست.", 204);
    public static Error NotFound = new("NotFound", "هیچ عملیات پروژه ای با این شناسه یافت نشد.", 404);
    public static Error ProjectOperationWithFilterNotFound = new("NotFound", "هیچ عملیات پروژه ای با این فیلتر یافت نشد.", 204);
    public static Error StandardIsNull = new("NotFound", "هیچ استانداردی با این شناسه یافت نشد.", 204);
    public static Error HaveContract = new("InvalidArguments", "شرح عملیات پروژه انتخاب شده قرارداد دارد.", 422);
    public static Error DiffrentProject = new("InvalidArguments", "شرح عملیات پروژه انتخاب شده پروژه یکی با قرارداد ندارد.", 422);
    public static Error DependencyWithIdNotFound = new("NotFound", "هیج وابستگی ای یافت نشد.", 204);
    public static Error CanNottDelete = new("InvalidArguments", "این عملیات پروژه به دلیل داشتن وابستگی اطلاعاتی قابل حذف نمیباشد.", 422);
    public static Error CanNottDeleteForProjectOperationDetail = new("InvalidArguments", "این عملیات پروژه به دلیل دارا بودن ریزمتره قابل حذف نمیباشد.", 422);
    public static Error IsDeleted = new("NotFound", "این شرح عملیات پروژه حذف شده است.", 204);

    public static Error IdIsEmpty = new("InvalidArguments", "شناسه شرح عملیات پروژه خالی است.", 422);
    public static Error ChangePriceShouldBePositive = new("InvalidArguments", "قیمت باید مثبت باشد.", 422);
    public static Error ChangePriceIsEmpty = new("InvalidArguments", "قیمت نمیتواند خالی باشد.", 422);
    public static Error IdsAreEmpty = new("InvalidArguments", "شناسه شرح عملیات پروژه ها خالی است.", 204);
    public static Error IdIsEmptyForDelete = new("InvalidArguments", "شناسه شرح عملیات پروژه برای حذف خالی است.", 422);
    public static Error PriorityIsEmpty = new("InvalidArguments", "الویت شرح عملیات پروژه خالی است.", 422);
    public static Error ProjectOperationIdIsEmpty = new("InvalidArguments", "شناسه شرح عملیات پروژه خالی است.", 422);
    public static Error OperationInfoIdIsEmpty = new("InvalidArguments", "شناسه شرح عملیات پروژه خالی است.", 422);
    public static Error ProjectOperationIsEmpty = new("InvalidArguments", "شرح عملیات پروژه خالی است.", 422);
    public static Error ProjectOperationModelIsEmpty = new("InvalidArguments", "دیتای ورودی خالی است!", 422);
    public static Error ProjectIdIsEmpty = new("InvalidArguments", "شناسه پروژه خالی است.", 422);
    public static Error PriorityCanNot1 = new("InvalidArguments", "الویت نمیتواند یک یا کمتر از یک باشد.", 422);
    public static Error EmployerContractIdIsEmpty = new("InvalidArguments", "شناسه قرارداد خالی است.", 422);
    public static Error WorkloadIsEmpty = new("InvalidArguments", "حجم کار خالی است.", 422);
    public static Error WorkloadCanNotChanged = new("InvalidArguments", "به دلیل بدون قرارداد بودن این شرح عملیات پروژه شما نمیتوانین مقدار حجم را تغییر دهید.", 422);
    public static Error OperationInfoChangedFailed = new("InvalidArguments", "خطایی در جابه جایی اطلاعات صورت گرفته است لطفا با پشتیبانی فنی ارتباط بگیرید.", 422);
    public static Error OperationInfoChangedFailedForService = new("InvalidArguments", "تغییر شرح عملیات به علت نداشتن خدمات مشترک با شرح عملیات جدید در ریزمتره ها با خطا مواجه شد.", 422);
    public static Error OperationInfoCanNotChanged = new("InvalidArguments", "به دلیل داشتن درخواست تامین یا کارکرد روزانه شما نمیتوانین شرح عملیات را تغییر دهید.", 422);
    public static Error OperationInfoCanNotChangedForRequests = new("InvalidArguments", "کالا های شرح عملیات جدید با کالا های شرح عملیات قبلی بدلیل داشتن درخواست تامین باید یکسان باشد.", 422);
    public static Error OperationInfoCanNotChangedForValue = new("InvalidArguments", "حجم کالای شرح عملیات جدید کمتر از حجم کالا موجود در درخواست تامین ریزمتره های این شرح عملیات است.", 422);
    public static Error OperationInfoCanNotChanged2 = new("InvalidArguments", "به دلیل تکراری شدن شرح عملیات های پروژه نمیتوانین، شرح عملیات را تغییر دهید.", 422);
    public static Error OldOperationInfoHaved = new("InvalidArguments", "شما شرح عملیات پروژه ای با همین واحد و شرح عملیات بدونه قرارداد دارید.", 422);
    public static Error NewOperationInfoHaved = new("InvalidArguments", "در قرارداد شما شرح عملیات پروژه ای با همین مشخصات وجود دارد.", 422);
    public static Error TolerancePercentageIsEmpty = new("InvalidArguments", "درصد تلرانس خالی است.", 422);
    public static Error MeasurementIdIsEmpty = new("InvalidArguments", "واحد سنجش خالی است.", 422);
    public static Error ProjectOperationStatusIsEmpty = new("InvalidArguments", "وضعیت خالی است.", 422);
    public static Error CanNottChanegToNotStarted = new("InvalidArguments", "شما نمیتوانید به وضعیت شروع نشده تغییر وضعیت دهید.", 422);
    public static Error StartDateIsEmpty = new("InvalidArguments", "تاریخ شروع خالی است!", 422);
    public static Error EndDateIsEmpty = new("InvalidArguments", "تاریخ پایان خالی است!", 422);
    public static Error CostCenterIdIsEmpty = new("InvalidArguments", "شناسه مرکزهزینه خالی است!", 422);
    public static Error EmployerIdIsEmpty = new("InvalidArguments", "شناسه کارفرما خالی است!", 422);
    public static Error PriorityCanNot0 = new("InvalidArguments", "الویت نمیتواند صفر باشد.", 422);
    public static Error CanNotGetDependency = new("InvalidArguments", "شما با الویت 1 یا خالی نمیتوانین وابستگی داشته باشید.", 422);
    public static Error CanNotRealated = new("InvalidArguments", "به دلیل وابستگی شرح عملیات پروژه های دیگر به شرح عملیات پروژه انتخابی امکان تغییر اولویت وجود نخواهد داشت.", 422);
    public static Error ProjectOperationDependencyWithIdNotFound = new("NotFound", "هیچ شرح عمللیات پروژه ای برای وابستگی با این شناسه یافت نشد.", 204);
    public static Error ProjectOperationActionNotFound = new("NotFound", "هیچ جزییات عملیاتی پیدا نشد.", 204);

    public static Error DifferentProjects = new("DifferentProjects", "دو عملیات پروژه در پروژه های متفاوت را نمیتوان به یکدیگر متصل کرد.", 422);
    public static Error CanDependOnSelf = new("CanDependOnSelf", "عملیات پروژه را نمیتوان به خود متصل کرد.", 422);
    public static Error CreatesCycle = new("CreatesCycle", "ایجاد وابستگی میان این دو عملیات باعث ایجاد حلقه میشود.", 422);


    public static Error HaveDependency = new("HaveDependency", "این دو شرح عملیات از قبل به هم وابستگی دارند.", 422);

    public static Error InvalidOperationInfoCodes = new Error("ValidationData", "کد شرح عملیات های زیر در سیستم یافت نشدند", 400);
    public static Error InvalidExcelData = new Error("ValidationData", "برخی از ردیف های فایل اکسل نامعتبر هستند", 400);
    public static Error CrititcalPONotFound = new("NotFound", "هیچ عملیات پروژه بحرانی ای یافت نشد.", 204);
    public static Error InvalidImportStatus(string? oInfo, string? projects, string? oLocations)
    {
        string msg = "";

        if (!string.IsNullOrWhiteSpace(projects))
            msg += $"پروژه های {projects} فعال نیستند. ";

        if (!string.IsNullOrWhiteSpace(oInfo))
            msg += $"شرح عملیات های {oInfo} فعال نیستند. ";

        if (!string.IsNullOrWhiteSpace(oLocations))
            msg += $" موقعیت های مکانی {oLocations} فعال نیستند. ";

        return new Error("ValidationData", msg.Trim(), 400);
    }
}
