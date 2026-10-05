
namespace Engineering.Domain.Errors;

public static class ProjectOperationDetailErrors
{
    public static Error ValidateForScheduling = new("InvalidArguments", "هیچ ریزمتره ای برای شرح عملیات و آدرس مد نظر شما یافت نشد.", 422);
    public static Error DeductionsUnValid = new("InvalidArguments", "مجموع حجم کسورات از حجم ریزمتره نباید بزگتر باشد.", 422);
    public static Error SchedulingUpdateDate = new("InvalidArguments", "ریزمتره شما در حال انجام است به همین دلیل تاریخ شروع آن تغییر نمیابد، به همین دلیل تاریخ پایان با تاریخ شروع ریزمتره مغایرت دارد.", 422);
    public static Error CodeIsEmpty = new("InvalidArguments", "کد ریزمتره تکراری است.", 422);

    public static Error UnValidId = new("InvalidArguments", "شناسه برآورد نامعتبر است.", 422);
    public static Error ProjectOperationDetailNoDate = new("InvalidArguments", "ریمتره انتخابی شما تاریخ شروع ندارد، ابتدا تاریخ شروع را قراردهید.", 422);
    public static Error DateTimeNotValid = new("InvalidArguments", "تاریخ پایان باید بزرگتر از تاریخ شروع باشد.", 422);
    public static Error DateValidate = new("InvalidArguments", "باید هم تاریخ شروع و هم تاریخ پایان مقدار داشته باشید.", 422);
    public static Error DayAndHourIsZiro = new("InvalidArguments", "تاریخ شروع و پایان در صورت 0 بودن روز و ساعت باید مقدار داشته باشند.", 422);
    public static Error DateTimeNotValidForContract = new("InvalidArguments", "تاریخ شروع ریزمتره نمیتواند از تاریخ شروع قرارداد کارفرما این شرح عملیات پروژه کوچکتر باشد.", 422);
    public static Error DateTimeNotValidForDaily = new("InvalidArguments", "شما کارکرد روزانه ای دارید که تاریخ شروع کوچکتری نسبت به تاریخ شروع شما دارد.", 422);
    public static Error DateTimeNotValidForDaily2 = new("InvalidArguments", "تاریخ کارکرد روزانه شما نمیتواند کمتر از تاریخ شروع ریزمتره باشد.", 422);
    public static Error DateTimeNotValidForDaily3 = new("InvalidArguments", "ریزمتره شما تاریخ شروع ندارد لطفا ابتدا  تاریخ شروع ریزمتره را اضافه کنید", 422);
    public static Error StatusIsInvalid = new("InvalidArguments", "شما نمیتوانین اطلاعات ریزمتره مربوطه را بخاطر وضعیت فعلی(پایان کار یا تحویل موقت یا تحویل قطعی) تغییر دهید.", 422);
    public static Error UnValidPlanners = new("InvalidArguments", "شناسه ای از مسئولین برنامه ریزی نامعتبر است.", 422);
    public static Error UnValidImplementations = new("InvalidArguments", "شناسه ای از مسئولین اجرایی نامعتبر است.", 422);
    public static Error UnValidTechnicals = new("InvalidArguments", "شناسه ای از مسئولین فنی نامعتبر است.", 422);
    public static Error UnValidContractors = new("InvalidArguments", "شناسه ای از پیمانکار نامعتبر است.", 422);
    public static Error ProjectServicesNotValidate = new("InvalidArguments", "خدمات پروژه معتبر نیست.", 422);
    public static Error ContractorsNotFound = new("InvalidArguments", "پیمانکاری با این اطلاعات یافت نشد.", 204);
    public static Error UnValidServiceInfos = new("InvalidArguments", "شناسه ای از خدمات نامعتبر است.", 422);
    public static Error UnValidServiceInfoInOperationInfo = new("InvalidArguments", "شناسه ای از خدمات در شرح عملیات این خدمت وجود ندارد.", 422);
    public static Error UnValidExperts = new("InvalidArguments", "شناسه ای از متخصص نامعتبر است.", 422);
    public static Error ExpertStandardValueNotValid = new("InvalidArguments", "مقدار استاندارد متخصص وارد شده با مقدار استاندارد موجود برابر نیست، لطفا مجدد بررسی کنید.", 422);
    public static Error ProductStandardValueNotValid = new("InvalidArguments", "مقدار استاندارد کالا وارد شده با مقدار استاندارد موجود برابر نیست، لطفا مجدد بررسی کنید.", 422);
    public static Error MachineryStandardValueNotValid = new("InvalidArguments", "مقدار استاندارد ماشین آلات وارد شده با مقدار استاندارد موجود برابر نیست، لطفا مجدد بررسی کنید.", 422);
    public static Error UnValidMachineries = new("InvalidArguments", "شناسه ای از ماشین آلات و ابزار نامعتبر است.", 422);
    public static Error UnValidProducts = new("InvalidArguments", "شناسه ای از کالا نامعتبر است.", 422);
    public static Error CanNotCreateGoods = new("InvalidArguments", "شرح عملیات شما قابلیت افزودن کالای درحال ساخت را ندارد", 422);
    public static Error CanNotChangeProduct = new("InvalidArguments", "شما نمیتوانید در وضعیت پایان کار کالای در جریان خود را تغییر دهید", 422);

    public static Error ProductGroupIsInActive(string? name) => new("InvalidArguments", $"گروه کالای {name} غیر فعال میباشد لطفا از مسئول مربوطه پیگیری کنید.", 422);
    public static Error UnValidCategories = new("InvalidArguments", "شناسه ای از دسته بندی نامعتبر است.", 422);
    public static Error UnValidMeasureunits = new("InvalidArguments", "شناسه ای از واحد اندازگیری نامعتبر است.", 422);
    public static Error ProjectOperationIdIsEmpty = new("InvalidArguments", "شناسه شرح عملیات پروژه خالی است.", 422);
    public static Error OperationLocationIdIsEmpty = new("InvalidArguments", "شناسه موقعیت عملیات خالی است.", 422);
    public static Error RequestDataIsEmpty = new("InvalidArguments", "دیتای درخواستی نمیتواند خالی باشد", 422);
    public static Error ProjectOperationDetailIsDuplicate = new("InvalidArguments", "با این شرح عملیات پروژه و این موقعیت جزئی قبلا ریزمتره ای ثبت شده است.", 422);
    public static Error ProjectOperationDetailIsDuplicateInVizard = new("InvalidArguments", "در دیتاهای ارسالی امکان ایجاد ریزمتره تکراری وجود دارد، نباید شرح عملیات پروژه تکراری با موقعیت جزئی تکراری داشته باشید.", 422);
    public static Error IdIsEmpty = new("InvalidArguments", "شناسه ریزمتره خالی است.", 422);
    public static Error IdsIsEmpty = new("InvalidArguments", "لیست شناسه ریزمتره خالی است.", 422);
    public static Error UnValidIds = new("InvalidArguments", "شناسه های ریزمتره خالی است.", 422);
    public static Error ExpertIdIsEmpty = new("InvalidArguments", "شناسه متخصص خالی است.", 422);
    public static Error MachineryIdIsEmpty = new("InvalidArguments", "شناسه ماشین آلات و ابزار خالی است.", 422);
    public static Error ProductIdIsEmpty = new("InvalidArguments", "شناسه کالا خالی است.", 422);
    public static Error FinalAmountIsEmpty = new("InvalidArguments", "مقدار نهایی خالی است.", 422);
    public static Error ProjectOperationIsUnValid = new("InvalidArguments", "لطفا از شرح عملیات پروژه هایی استفاده کنید که از یک پروژه باشند.", 422);
    public static Error ProjectOperationIsNotEqual = new("InvalidArguments", "شرح عملیات پروژه ای معتبر نیست، اطلاعات خود را بررسی کنید لطفا.", 422);
    public static Error OperationLocationIsUnValid = new("InvalidArguments", "لطفا از موقعیت عملیات استفاده کنید که از یک مرکزهزینه باشند.", 422);
    public static Error OperationLocationIsNotEqual = new("InvalidArguments", "موقعیت عملیات ای معتبر نیست، اطلاعات خود را بررسی کنید لطفا.", 422);
    public static Error ProjectOperationIdsIsEmpty = new("InvalidArguments", "شناسه های شرح عملیات پروژه خالی است.", 422);
    public static Error ProjectIdIsEmpty = new("InvalidArguments", "شناسه پروژه خالی است.", 422);
    public static Error CostCenterIdIsEmpty = new("InvalidArguments", "شناسه مرکزهزینه خالی است.", 422);
    public static Error ServiceInfoIdsIsEmpty = new("InvalidArguments", "شناسه خدمات خالی است.", 422);
    public static Error ContactorIdIsEmpty = new("InvalidArguments", "شناسه کارفرما خالی است.", 422);
    public static Error ContactorsIdIsEmpty = new("InvalidArguments", "شناسه پیمانکار خالی است.", 422);
    public static Error ContactorsIdMustgreaterThanZero = new("InvalidArguments", "شناسه پیمانکار باید بزرگتر از صفر باشد.", 422);
    public static Error PriorityIsEmpty = new("InvalidArguments", "الویت خالی میباشد.", 422);
    public static Error PriorityIsLow = new("InvalidArguments", "الویت کمتر از 1 میباشد.", 422);
    public static Error DayIsEmpty = new("InvalidArguments", "زمان کار خالی میباشد.", 422);
    public static Error NotValidateType = new("InvalidArguments", "تایپ مدنظر صحیح نیست.", 422);
    public static Error HourIsEmpty = new("InvalidArguments", "زمان کار خالی میباشد.", 422);
    public static Error ProjectOperationDetailIdsEmpty = new("InvalidArguments", "شناسه ریزمتره ها خالی است.", 422);
    public static Error FinalAmountIsBiggerThanWorkLoad = new("InvalidArguments", "مقدار نهایی ریزمتره از حجم کل شرح عملیات بیشتر است.", 422);
    public static Error ExistsExpertInList = new("InvalidArguments", "شناسه ای از متخصص تکراری است.", 422);
    public static Error ExistsProductInList = new("InvalidArguments", "شناسه ای از کالا تکراری است.", 422);
    public static Error ExistsCategoryInList = new("InvalidArguments", "شناسه ای از دسته بندی تکراری است.", 422);
    public static Error ExistsMachineryInList = new("InvalidArguments", "شناسه ای از ماشین آلات و ابزار تکراری است.", 422);

    public static Error LengthIsEmpty = new("InvalidArguments", "طول خالی است.", 422);
    public static Error LengthIsLow = new("InvalidArguments", "طول کمتر از 1 است.", 422);
    public static Error LengthChangeableIsEmpty = new("InvalidArguments", "تغییر پذیری طول خالی است.", 422);
    public static Error ValidateLengthChangeable = new("InvalidArguments", "شما نمیتوانین مقدار طول را تغییر دهید.", 422);
    public static Error WidthIsEmpty = new("InvalidArguments", "عرض خالی است.", 422);
    public static Error WidthIsLow = new("InvalidArguments", "عرض کمتر از 1 است.", 422);
    public static Error WidthChangeableIsEmpty = new("InvalidArguments", "تغییر پذیری عرض خالی است.", 422);
    public static Error ValidateWidthChangeable = new("InvalidArguments", "شما نمیتوانین مقدار عرض را تغییر دهید.", 422);
    public static Error HeightIsEmpty = new("InvalidArguments", "ارتفاع خالی است.", 422);
    public static Error HeightIsLow = new("InvalidArguments", "ارتفاع کمتر از 1 است.", 422);
    public static Error HeightChangeableIsEmpty = new("InvalidArguments", "تغییر پذیری ارتفاع خالی است.", 422);
    public static Error ValidateHeightChangeable = new("InvalidArguments", "شما نمیتوانین مقدار ارتفاع را تغییر دهید.", 422);
    public static Error WeightIsEmpty = new("InvalidArguments", "وزن خالی است.", 422);
    public static Error WeightIsLow = new("InvalidArguments", "وزن کمتر از 1 است.", 422);
    public static Error WeightChangeableIsEmpty = new("InvalidArguments", "تغییر پذیری وزن خالی است.", 422);
    public static Error ValidateWeightChangeable = new("InvalidArguments", "شما نمیتوانین مقدار وزن را تغییر دهید.", 422);
    public static Error NumberIsEmpty = new("InvalidArguments", "تعداد خالی است.", 422);
    public static Error NumberIsLow = new("InvalidArguments", "تعداد کمتر از 1 است.", 422);
    public static Error NumberChangeableIsEmpty = new("InvalidArguments", "تغییر پذیری تعداد خالی است.", 422);
    public static Error ValidateNumberChangeable = new("InvalidArguments", "شما نمیتوانین مقدار تعداد را تغییر دهید.", 422);

    public static Error InValidProjectOperationDetails = new("InvalidArguments", "ریزمتره های انتخابی از یک شرح عملیات واحد نمیباشند.", 422);
    public static Error InValidStartDate = new("InvalidArguments", "تاریخ شروع نا معتبر است.", 422);
    public static Error OperationLocationCanNotChanged = new("InvalidArguments", "به دلیل داشتن کارکرد روزانه، موقعیت جزئی این ریزمتره قابل تغییر نمیباشد.", 422);
    public static Error ProjectManagerInfoIsUnValid = new("InvalidArguments", "اطلاعات دریافتی از مدیر پروژه برای تغییر موقعیت ریزمتره صحیح نمیباشد.", 422);
    public static Error OnlyProjectManagerCanChangeLocation = new("InvalidArguments", "درصورت داشتن کارکرد روزانه فقط مدیر پروژه میتواند موقعیت را ویرایش کند.", 422);
    public static Error InValidEndDate = new("InvalidArguments", "تاریخ پایان نا معتبر است.", 422);
    public static Error InValidCountDay = new("InvalidArguments", "تعداد روز نمیتواند صفر باشد یا عددی مثبت یا عددی منفی در نظر گرفته میشود.", 422);
    public static Error InValidDependantOperationInfos = new("InvalidArguments", "شرح عملیات ها انتخابی ریز متره ها با هم وابستگی دارند.", 422);

    public static Error ProjectOperationDetailWithIdNotFound = new("NotFound", "هیچ ریزمتره ای با این شناسه یافت نشد.", 404);
    public static Error InvalidRequestParams = new("NotFound", "باید یکی از موارد شناسه شرح عملیات پروژه یا شناسه های ریزمتره پر شده و ارسال شود.", 404);
    public static Error ProjectOperationDetailWithFilterNotFound = new("NotFound", "هیچ ریزمتره ای با این اطلاعات یافت نشد.", 404);
    public static Error ProjectOperationDetailContractorsWithFilterNotFound = new("NotFound", "هیچ پیمانکار ریزمتره ای با این اطلاعات یافت نشد.", 404);

    public static Error NoHaveDaily = new("NotFound", "ریزمتره مد نظر هیچ کارکرد روزانه‌ای ندارد و نمی‌تواند به وضعیت‌های متوقف شده، پایان کار، تحویل موقت و یا تحویل قطعی تغییر پیدا کند.", 422);
    public static Error CanNotToNotStarted = new("NotFound", "در صورت داشتن کارکرد روزانه شما نمیتوانین به وضعیت شروع نشده تغییر وضعیت دهید.", 422);
    public static Error HaveDailyStartDate = new("NotFound", "ریزمتره شما دارای کارکرد روزانه است، شما نمیتوانین تاریخ شروع ریزمتره خود را بزرگتر از تاریخ شروع کارکرد روزانه خود بکنین.", 422);
    public static Error GetsProjectOperationDetailContractorsFound = new("NotFound", "برای این ریزمتره هیچ پیمانکاری یافت نشد.", 204);
    public static Error GetsProjectOperationDetailContractorFound = new("NotFound", "برای این ریزمتره هیچ پیمانکاری یافت نشد.", 404);
    public static Error DataNotFound = new("NotFound", "هیچ ریزمتره ای با این فیلتر یافت نشد.", 204);
    public static Error DataNotFoundWithCode = new("NotFound", "هیچ ریزمتره ای با این فیلتر یافت نشد.", 404);
    public static Error ProjectOperationDetailProductIsNull = new("NotFound", "هیچ حجم مصرفی کلایی برای این ریزمتره وجود نداد.", 204);
    public static Error ProjectOperationDetailExpertIsNull = new("NotFound", "هیچ حجم مصرفی متخصصی برای این ریزمتره وجود نداد.", 204);
    public static Error ProjectOperationDetailMachineryIsNull = new("NotFound", "هیچ حجم مصرفی ماشین آلاتی برای این ریزمتره وجود نداد.", 204);

    public static Error ProjectChildNotFound = new("NotFound", "قرارداد کارفرمای انتخابی زیر شاخه ای ندارد.", 204);
    public static Error HistoryNotFound = new("NotFound", "ریزمتره انتخابی تاریخچه ای ندارد.", 204);
    public static Error DependencyWithIdNotFound = new("NotFound", "هیج وابستگی ای یافت نشد.", 204);
    public static Error CanNottDelete = new("InvalidArguments", "این برآورد به دلیل داشتن کارکرد روزانه قابل حذف نمیباشد.", 422);
    public static Error CanNottDeleteForContract = new("InvalidArguments", "این برآورد به دلیل داشتن قراردادپیمانکار قابل حذف نمیباشد.", 422);
    public static Error CanNottDeleteForGoods = new("InvalidArguments", "این برآورد به دلیل داشتن درخواست تامین قابل حذف نمیباشد.", 422);
    public static Error CanNottDeleteForGoodsDetails = new("InvalidArguments", "از گروه کالای این براورد در درخواست های تامین کالا استفاده شده است و قابل حذف نمیباشد.", 422);
    public static Error IsDeleted = new("NotFound", "این ریزمتره حذف شده است.", 422);
    public static Error IsDeletedDaily = new("NotFound", "این کارتابل حذف شده است.", 422);
    public static Error NotInDate = new("NotFound", "حذف کارکرد روزانه، فقط برای کارکرد روزانه های ثبت شده در همان روز امکان پذیر است.", 422);

    public static Error HaveEmployerStatusStatementProjectOperationDetails = new("NotFound", "از این کارکرد روزانه در صورت وضعیت استفاده شده است و نمیتوانین حذفش کنید. ", 422);
    public static Error ESSDoneVolume = new("NotFound", "حجم انجام شده صورت وضعیت کارفرمای شما به صورتی نیست که بتوانید این کارکر روزانه را حذف کنید", 422);

    public static Error ContractorExpertVolumeAssignedCanNotBeBigger = new("InvalidArguments", "جمع تخصیص داده شده ی ساعت های یک مهارت به پیمانکاران نمیتواند از کل ساعت های آن مهارت در ریزمتره بیشتر باشد.", 422);
    public static Error ContractorExpertNotFound = new("NotFound", "تخصیص مهارت به خدمتی با این شناسه پیدا نشد.", 404);
}