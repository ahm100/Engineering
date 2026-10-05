namespace Engineering.Domain.Errors;

public static class ProjectErrors
{
    public static Error NotAllowed = new("InvalidArguments", "پروژه شما در وضعیت مناسب برای ادامه فرایند نمیباشد، لطفا از مدیر پروژه خود پیگیری کنید.", 422);
    public static Error CanNotDelete = new("InvalidArguments", "این دیتا به دلیل داشتن وابستگی اطلاعاتی قابل حذف نمیباشد.", 422);
    public static Error CostCenterHasDependencies = new("InvalidArguments", "این مرکز هزینه به دلیل داشتن وابستگی اطلاعاتی در پروژه قابل حذف نمیباشد.", 422);
    public static Error NameIsDuplicate = new("Duplicate", "نام پروژه تکراری است.", 409);
    public static Error CodeIsDuplicate = new("Duplicate", "کد پروژه تکراری است.", 409);
    public static Error TypeNameIsDuplicate = new("Duplicate", "نام نوع پروژه تکراری است.", 409);
    public static Error TypeCodeIsDuplicate = new("Duplicate", "کد نوع پروژه تکراری است.", 409);
    public static Error ProjectHaveContracts = new("InvalidArguments", "پروژه شما چندین قراردادکارفرما دارد شما نمیتوانین، کارفرمای خود را تغییر دهید.", 422);

    public static Error ThirdPartiesNotFound = new("InvalidArguments", "طرف حسابی یافت نشد.", 204);
    public static Error ThirdPartiesWithIdsNotFound = new("InvalidArguments", "طرف حسابی با این شناسه ها یافت نشد.", 422);

    public static Error IsActive = new("InvalidArguments", "وضعیت پروژه فعال میباشد.", 422);
    public static Error IsInactive = new("InvalidArguments", "وضعیت پروژه غیرفعال میباشد.", 422);
    public static Error ProjectCanNotEdit = new("NotFound", "شما نمیتوانین به وضعیت شروع نشده بروید.", 422);
    public static Error CanNotChangeStatus = new("NotFound", "شما نمیتوانین وضعیت پروژه رو به شروع نشده تغییر دهید.", 422);
    public static Error CanNotDeletePP = new("InvalidArguments", "به دلیل وجود درخواست تامین روی کالا امکان حذف نمیباشد", 422);
    public static Error PThirdPartyNotFound = new("InvalidArguments", "طرف حسابی که میخواهید پاک کنید جزو اعضای دارای دسترسی پروژه نیست", 422);

    public static Error ThirdPartyNotFound = new("InvalidArguments", "طرف حسابی پیدا نشد", 422);

    public static Error OnlyCreatorCanAssignUsers = new("InvalidArguments", "تنها افرادی که دارای دسترسی به پروژه هستند یا ایجاد کننده آن هستند قابلیت ویرایش پروژه را دارند.", 422);

    public static Error FailToGetData = new("NotFound", "اطلاعات به درستی دریافت نشد.", 404);
    public static Error ProjectWithIdNotFound = new("NotFound", "هیچ پروژه ای با این شناسه یافت نشد.", 404);
    public static Error ProjectWithIdsNotFound = new("NotFound", "هیچ پروژه ای با این شناسه ها یافت نشد.", 404);
    public static Error ProjectProductWithIdsNotFound = new("NotFound", "هیچ کالای پروژه ای با این شناسه ها یافت نشد.", 404);
    public static Error ProjectProductWithIdsIsDeleted = new("NotFound", " کالای پروژه ای با این شناسه حذف شده است.", 404);
    public static Error ProjectNotInCostCenter = new("NotFound", "پروژه انتخابی برای این مرکزهزینه نمیباشد.", 404);
    public static Error YouHaveProjectOperationDetails = new("NotFound", "آدرس شما دارای ریزمتره میباشد و شما نمیتوانین پروژه را تغییر دهید.", 404);
    public static Error ProjectTypeWithIdNotFound = new("NotFound", "هیچ نوع پروژه ای با این شناسه یافت نشد.", 404);
    public static Error ProjectWithCodeNotFound = new("NotFound", "هیچ پروژه ای با این کد یافت نشد.", 404);
    public static Error ProjectWithCodesNotFound = new("NotFound", "هیچ پروژه ای با این کدها یافت نشد.", 204);
    public static Error ProjectOperationWithDetailsNotFound = new("NotFound", "برای پروژه شما شرح عملیاتی با مشخصات یافت نشد.", 204);
    public static Error ProjectWithNameNotFound = new("NotFound", "هیچ پروژه ای با این نام یافت نشد.", 404);
    public static Error FilteredProjectNotFound = new("NotFound", "هیچ پروژه ای با این اطلاعات یافت نشد.", 204);
    public static Error GroupNotFound = new("NotFound", "هیچ گروه کالایی یافت نشد.", 204);
    public static Error CategoryNotFound = new("NotFound", "هیچ دسته بندی یافت نشد.", 204);
    public static Error ProjectChildNotFound = new("NotFound", "پروژه انتخابی زیر شاخه ای ندارد.", 204);
    public static Error IsDeleted = new("NotFound", "این پروژه حذف شده است.", 204);
    public static Error IsDeletedForType = new("NotFound", "این گروه پروژه حذف شده است.", 204);
    public static Error ProjectNotFound = new("InvalidArguments", "پروژه ای با این شناسه پیدا نشد.", 204);

    public static Error CanNotDeleteForEmContracts = new("NotAccess", "بدلیل دارا بودن قرارداد پیمانکاری این پروژه قابل حذف نمیباشد.", 422);
    public static Error CanNotDeleteForProjectOpration = new("NotAccess", "بدلیل دارا بودن شرح عملیات این پروژه قابل حذف نمیباشد.", 422);
    public static Error CanNotDeleteForFidProducts = new("NotAccess", "بدلیل دارا بودن کالای امانی این پروژه قابل حذف نمیباشد.", 422);
    public static Error CanNotDeleteForReqGoodsSupplies = new("NotAccess", "بدلیل دارا بودن درخواست تامین کالا این پروژه قابل حذف نمیباشد.", 422);
    public static Error CanNotDeleteForProject = new("NotAccess", "بدلیل دارا بودن پروژه این گروه پروژه قابل حذف نمیباشد.", 422);
    public static Error PackageNotFound = new("InvalidArguments", "برای کالای انتخابی شما بسته بندی ای یافت نشد لطفا از مسئول کالا پیگیری لازمه را انجام دهید.", 422);

    public static Error CostCenterNotSet = new("InvalidArguments", "برای انجام این عملیات نیاز است مرکزهزینه پروژه مشخص شود.", 422);
    public static Error CategoryIsAssigned = new("InvalidArguments", "دسته بندی از قبل به پروژه متصل میباشد. برای اتصال گروه کالا دسته بندی ها را حذف کنید.", 422);
    public static Error GroupIsAssigned = new("InvalidArguments", "گروه کالا از قبل به پروژه متصل میباشد. برای اتصال دسته بندی کالا گروه کالاها را حذف کنید.", 422);
    public static Error UnAssignBothType = new("InvalidArguments", "باید حداقل یک نوع را برای پروژه تعریف کنید", 422);
    public static Error AssignBothType = new("InvalidArguments", "نمیتوان جفت دسته بندی و گروه کالا برای پروژه تعریف کرد.", 422);
    public static Error UnValidId = new("InvalidArguments", "شناسه پروژه نامعتبر است.", 422);
    public static Error UnValidManagerId = new("InvalidArguments", "شناسه مدیر پروژه نامعتبر است.", 422);
    public static Error UnValidIdInIds = new("InvalidArguments", "شناسه ای از شناسه های ارسالی نامعتبر است.", 422);
    public static Error IdIsEmpty = new("InvalidArguments", "شناسه پروژه خالی است.", 422);
    public static Error IdsIsEmpty = new("InvalidArguments", "شناسه پروژه ها خالی است.", 422);
    public static Error ProjectTypeIdIsEmpty = new("InvalidArguments", "شناسه نوع پروژه خالی است.", 422);
    public static Error ProjectNameIsEmpty = new("InvalidArguments", " نام پروژه خالی است.", 422);
    public static Error ProjectCodeIsEmpty = new("InvalidArguments", " کد پروژه خالی است.", 422);
    public static Error ProjectCodesIsEmpty = new("InvalidArguments", " کدهای پروژه خالی است.", 422);
    public static Error EmployerIdIsEmpty = new("InvalidArguments", "شناسه کارفرما خالی است.", 422);
    public static Error ProjectManagerIdIsEmpty = new("InvalidArguments", "شناسه مدیر پروژه خالی است.", 422);
    public static Error ProjectManagerIdGreaterThanZero = new("InvalidArguments", "شناسه مدیر پروژه باید بزرگتر از صفر باشد.", 422);
    public static Error CategoryIdIsEmpty = new("InvalidArguments", "شناسه رسته خالی است.", 422);
    public static Error CostCenterIdIsEmpty = new("InvalidArguments", "شناسه مرکزهزینه خالی است.", 422);
    public static Error StatusIsEmpty = new("InvalidArguments", "نوع وضعیت پروژه خالی است.", 422);
    public static Error IsActiveIsEmpty = new("InvalidArguments", "وضعیت پروژه خالی است.", 422);
    public static Error ContractualIsEmpty = new("InvalidArguments", "قرارداد پذیری پروژه خالی است.", 422);
    public static Error HaveContract = new("InvalidArguments", "پروژه شما دارای قرارداد میباشد نمیتوانین بدونه قراردادش کنید.", 422);
    public static Error HasProduct = new("InvalidArguments", "پروژه شما دارای کالای تامین شده و یا در جریان میباشد و نمیتوانید آن را به بدون کالا تغییر دهید.", 422);
    public static Error HasProductIsFalse = new("InvalidArguments", "پروژه شما کالا پذیر نمیباشد.", 422);
    public static Error EmployerSymbolIsEmpty = new("InvalidArguments", "سمبل کارفرما خالی میباشد!", 422);
    public static Error CostCenterCodeIsEmpty = new("InvalidArguments", "کد مرکزهزینه خالی میباشد!", 422);
    public static Error FilterDataIsEmpty = new("InvalidArguments", "فیلتر دیتا خالی میباشد!", 422);
    public static Error WarehouseNotFound = new("InvalidArguments", "انبار مرکز هزینه پروژه پیدا نشد!", 422);

    public static Error FltrProductNotFound = new("InvalidArguments", "کالایی با مشخصات وارد شده به پروژه تخصیص داده نشده است!", 422);
    public static Error ProjectDoesNotHaveProduct = new("InvalidArguments", "به پروژه کالایی تخصیص داده نشده است!", 422);
    public static Error ProjectDoesNotHaveThisProduct = new("InvalidArguments", "به پروژه همچین کالایی تخصیص داده نشده است!", 422);
    public static Error OIDoesNotHaveProduct = new("InvalidArguments", "به شرح عملیات کالایی تخصیص داده نشده است!", 422);

    public static Error TolerancePercentageIsEmpty = new("InvalidArguments", "درصد تلورانس خالی میباشد!", 422);
    public static Error HaveChild = new("InvalidArguments", "پروژه بدلیل وابستگی اطلاعاتی قابل تغییر نیست.", 422);
    public static Error GroupWithIdNotFound = new("NotFound", "هیچ گروه کالایی با این شناسه در انبار پیدا نشد.", 204);
    public static Error GroupWithIdAssigned = new("Duplicate", "گروه کالا با این شناسه به پروژه متصل میباشد.", 409);
    public static Error CategoryWithIdAssigned = new("Duplicate", "دسته بندی گروه کالایی با این شناسه به پروژه متصل میباشد.", 409);
    public static Error CategoryWithIdNotFound = new("NotFound", "هیچ دسته بندی گروه کالایی با این شناسه در انبار پیدا نشد.", 204);

    public static Error DontHaveAccessToProject = new("InvalidArguments", "شما به پروژه دسترسی ندارید!", 422);

    public static Error CityIsNull = new("InvalidArguments", "انتخاب شهر برای پروژه بدون مرکز هزینه الزامی است.", 422);

    public static Error DocumentNotFound = new("DocumentNotFound", "داکیومنت پروژه پیدا نشد.", 422);
    public static Error DocNotFound = new("NotFound", "هیچ داکیومنتی با این شناسه یافت نشد.", 204);
    public static Error DocFileNotFound = new("NotFound", "هیچ فایل داکیومنتی با این شناسه یافت نشد.", 204);
    public static Error DocIsDeleted = new("NotFound", "این داکیومنت قبلا حذف شده است.", 204);
    public static Error ProjectDocNotFound = new("NotFound", "هیچ داکیومنتی برای این پروژه یافت نشد.", 204);

    public static Error UnitOrgCantHaveCC = new("DocumentNotFound", "واحد سازمانی نمیتواند مرکز هزینه داشته باشد.", 422);
    public static Error OrganizationIdIsEmpty = new("DocumentNotFound", "برای واحد سازمانی باید سازمانی انتخاب کنید.", 422);
    public static Error OrganizationManagerIsEmpty = new("DocumentNotFound", "برای سازمان باید سرپرستی منتصب کنید.", 422);
    public static Error OrganizationNotFound = new("DocumentNotFound", "سازمان پیدا نشد.", 422);
    public static Error UnitOrgCantHaveEmployer = new("DocumentNotFound", "واحد سازمانی نمیتواند کارفرما داشته باشد.", 422);
    public static Error UnitOrgCantHaveType = new("DocumentNotFound", "واحد سازمانی نمیتواند نوع پروژه داشته باشد.", 422);

    public static Error OnlyProjectManagerCanRequestCostCenter = new("InvalidArguments", "تنها مدیر پروژه مجاز به ثبت درخواست مرکز هزینه برای این پروژه می‌باشد.", 422);
    public static Error CostCenterRequestNotFound = new("NotFound", "درخواست مرکز هزینه یافت نشد.", 404);
    public static Error CostCenterRequestCannotBeEdited = new("InvalidArguments", "این درخواست در وضعیت فعلی قابل ویرایش یا حذف نمی‌باشد.", 422);
    public static Error CostCenterRequestIsNotActive = new("InvalidArguments", "امکان ایجاد مرکز هزینه برای این درخواست وجود ندارد.", 422);
    public static Error CostCenterCityMustMatchProject = new("InvalidArguments", "شهر مرکز هزینه باید با شهر پروژه یکسان باشد.", 422);
    public static Error ProjectHasNoCity = new("InvalidArguments", "پروژه فاقد شهر است؛ ابتدا شهر پروژه را ثبت کنید.", 422);

    public static Error MppFileEmptyTask = new("InvalidArguments", "هیچ فایل داکیومنتی با این شناسه یافت نشد.", 422);
    public static Error CannotDefineWorkingTimeForClosedDays = new("InvalidArguments", "برای روز غیرکاری نمی‌توان بازه کاری تعریف کرد.", 422);
    public static Error MppFileDuplicateTasks = new("InvalidArguments", "UID تکراری در فعالیت‌های فایل وجود دارد.", 422);
    public static Error MppFileDuplicateCalanders = new("InvalidArguments", "UID تکراری در تقویم‌های فایل وجود دارد.", 422);
    public static Error WorkingTimeMisMatch = new("InvalidArguments", "زمان شروع باید قبل از زمان پایان باشد.", 422);
    public static Error WorkingTimeInvalid = new("InvalidArguments", "بازه کاری با بازه دیگری تداخل دارد.", 422);
    public static Error EditDurationUnAvailableForSummaries = new("InvalidArguments", "ویرایش زمان کاری برای تسک های والد امکان پذیر نمی‌باشد", 422);

    public static Error ProjectTaskNotFound = new("NotFound", "عملیات زمانبندی شده ای با این شناسه وجود ندارد.", 422);

    public static Error ColumnTitleDuplicate = new("InvalidArguments", "عنوان تکراری می‌باشد", 422);
    public static Error ColumnNotFound = new("InvalidArguments", "ستون مورد نظر پیدا نشد", 422);
    public static Error SystemColumnCannotBeDeleted = new("InvalidArguments", "ستون مورد نظر اصلی می باشد", 422);

    public static Error ColumnTitleRequired = new("InvalidArguments", "عنوان ستون نمی‌تواند خالی باشد.", 422);
    public static Error SystemColumnCannotBeModified = new("InvalidArguments", "ستون‌های اصلی قابل ویرایش نمی‌باشند.", 422);
    public static Error ColumnTypeCannotBeChanged = new("InvalidArguments", "نوع ستون قابل تغییر نمی‌باشد.", 422);
    public static Error ColumnDataTypeHasValues = new("InvalidArguments", "به دلیل وجود مقدار ثبت‌شده در این ستون، نوع داده آن قابل تغییر نیست.", 422);

    public static Error ColumnIsNotCustom = new("InvalidArguments", "فقط برای ستون‌های سفارشی می‌توان مقدار ثبت کرد.", 422);
    public static Error ColumnAndTaskScheduleMismatch = new("InvalidArguments", "فعالیت و ستون متعلق به یک برنامه زمان‌بندی نمی‌باشند.", 422);
    public static Error ColumnValueInvalid = new("InvalidArguments", "مقدار وارد شده با نوع داده ستون مطابقت ندارد.", 422);

    public static Error PredecessorSelfReference = new("NotFound", "عملیات زمانبندی نمی تواند به خودش متصل شود.", 422);
    public static Error DuplicatePredecessor = new("NotFound", "عملیات زمانبندی تکراری وارد شده است.", 422);
    public static Error CalendarNotFound = new("NotFound", "تقویم زمانبندی پیدا نشد.", 422);
    public static Error ProjectTaskPercentOutOfRange = new("InvalidArguments", "مقدار وارد شده خارج از محدوده مجاز می‌باشد.", 422);
    public static Error InvalidPredecessorFormat = new("InvalidArguments", "لطفا ورودی را با ساختار معتبر وارد نمایید.", 422);
    public static Error InvalidCalendarDefinition = new("InvalidArguments", "لطفا ورودی را با ساختار معتبر وارد نمایید.", 422);

    public static Error ProjectImportAlreadyExist = new("InvalidArguments", "برنامه زمانبندی ای برای این پروژه از قبل وجود دارد.", 422);
    public static Error ProjectImportNotExist = new("InvalidArguments", "برنامه زمانبندی ای برای این پروژه از قبل وجود دارد.", 422);

    public static Error DependencyCycle = new("InvalidArguments", "وابستگی های حلقه ای وجود دارد.", 422);
    public static Error StartDateAfterEndDate = new("InvalidArguments", "زمان شروع نمی‌تواند بعد از زمان پایان باشد.", 422);

    public static Error MppFileInvalidHierarchy = new("InvalidArguments", "ساختار نادرست در فایل زمانبندی", 422);
    public static Error MppFileNoStartDate = new("InvalidArguments", "نبود تاریخ استارت فایل زمانبندی", 422);
    public static Error CalendarWithoutWorkingTime = new("InvalidArguments", "تقویم بدون زمانبندی می‌باشد", 422);
    public static Error MppImportFailed = new("InvalidArguments", "بارگذاری فایل زمانبندی ناموفق بود", 422);
    public static Error SummaryTaskDurationNotEditable = new("InvalidArguments", "مدت زمان فعالیت‌های سرگروه قابل ویرایش نیست.", 422);
    public static Error ProjectTaskTitleInvalid = new("InvalidArguments", "عنوان فعالیت خالی است یا بیش از ۲۵۰ کاراکتر می‌باشد.", 422);
    public static readonly Error PredecessorCycle = new("Project.PredecessorCycle", "این وابستگی باعث ایجاد حلقه در برنامه زمانی می‌شود.", 422);
    public static Error MppExportTooManyCustomColumns = new("InvalidArguments", "تعداد ستون‌های سفارشی از حداکثر مجاز خروجی MPP بیشتر است.", 422);
    public static readonly Error PredecessorWithAncestorOrDescendant = new( "Project.PredecessorWithAncestorOrDescendant", "یک فعالیت نمی‌تواند به فعالیت پدر یا زیرمجموعه خود وابسته باشد.", 422);
}