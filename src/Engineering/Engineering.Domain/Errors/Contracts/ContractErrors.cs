namespace Engineering.Domain.Errors;

public static class ContractErrors
{
    public static Error ProjectDoesNotBelongToCompany =
        new(
            "InvalidArguments",
            "پروژه متعلق به شرکت جاری نمی‌باشد.",
            422);

    public static Error ContractNotFound =
        new(
            "NotFound",
            "قرارداد با شناسه مشخص شده یافت نشد.",
            404);

    public static Error ContractPartyNotFound =
        new(
            "InvalidArguments",
            "طرف قرارداد با شناسه مشخص شده یافت نشد.",
            422);

    public static Error ContractTypeNotFound =
        new(
            "NotFound",
            "نوع قرارداد با شناسه مشخص شده یافت نشد.",
            404);

    public static Error ContractMustHaveAtLeastOneType =
        new(
            "ContractMustHaveAtLeastOneType",
            "قرارداد باید حداقل یک نوع قرارداد داشته باشد.",
            400);

    public static Error ContractTypeKindIsDuplicate =
        new(
            "Duplicate",
            "نوع قرارداد انتخاب‌شده قبلاً در این قرارداد ثبت شده است.",
            409);

    public static Error ContractPricingMethodCostPlusDisabled =
        new(
            "InvalidArguments",
            "روش قیمت‌گذاری هزینه به‌علاوه (Cost Plus) در حال حاضر غیرفعال است.",
            422);

    public static Error ContractTypeDetailNotFound =
    new(
        "NotFound",
        "جزئیات نوع قرارداد با شناسه مشخص شده یافت نشد.",
        404);

    public static Error ContractTypeDetailSourceInvalid =
        new(
            "InvalidArguments",
            "منبع انتخاب‌شده برای جزئیات قرارداد معتبر نیست یا متعلق به پروژه قرارداد نمی‌باشد.",
            422);

    public static Error ContractTypeDetailSourceIsDuplicate =
        new(
            "Duplicate",
            "منبع انتخاب‌شده قبلاً در این نوع قرارداد ثبت شده است.",
            409);

    public static Error ContractTypeDetailSourceHasNoRemainingQuantity =
        new(
            "InvalidArguments",
            "برای منبع انتخاب‌شده مقدار قابل تخصیصی باقی نمانده است.",
            422);

    public static Error ContractTypeDetailQuantityExceeded =
        new(
            "InvalidArguments",
            "مقدار واردشده بیشتر از مقدار قابل تخصیص منبع انتخاب‌شده است.",
            422);

    public static Error ContractTypeDetailContractPartyMismatch =
        new(
            "InvalidArguments",
            "پیمانکار خدمت انتخاب‌شده با طرف قرارداد مطابقت ندارد.",
            422);

    public static Error ContractTypeDetailTermsInvalid =
        new(
            "InvalidArguments",
            "اطلاعات تجاری جزئیات قرارداد با نوع قرارداد یا روش قیمت‌گذاری مطابقت ندارد.",
            422);

    public static Error ContractTypeDetailAdjustmentTermsInvalid =
        new(
            "InvalidArguments",
            "اطلاعات شرایط تعدیل جزئیات قرارداد معتبر نیست.",
            400);

    public static Error ContractTypeDetailEstimateUnitPriceNotFound =
        new(
            "InvalidArguments",
            "قیمت واحد برآورد برای منبع انتخاب‌شده مشخص نیست.",
            422);

    public static Error ContractTypeStructureCannotChangeWithDetails =
        new(
            "Conflict",
            "نوع قرارداد یا روش قیمت‌گذاری پس از ثبت جزئیات قرارداد قابل تغییر نمی‌باشد.",
            409);

    public static Error ContractGuaranteeNotFound =
        new(
            "NotFound",
            "تضمین قرارداد با شناسه مشخص شده یافت نشد.",
            404);

    public static Error ContractGuaranteePercentageInvalid =
        new(
            "InvalidArguments",
            "درصد تضمین باید بیشتر از صفر و حداکثر ۱۰۰ باشد.",
            422);

    public static Error ContractGuaranteeDatesInvalid =
        new(
            "InvalidArguments",
            "تاریخ انقضای تضمین نمی‌تواند قبل از تاریخ صدور باشد.",
            422);

    public static Error ContractFinancialInformationNotFound =
        new(
            "NotFound",
            "اطلاعات مالی قرارداد یافت نشد.",
            404);

    public static Error ContractFinancialInformationAlreadyExists =
        new(
            "Conflict",
            "اطلاعات مالی این قرارداد قبلاً ثبت شده است.",
            409);

    public static Error ContractFinancialCurrencyNotFound =
        new(
            "InvalidArguments",
            "واحد پول انتخاب‌شده یافت نشد.",
            422);

    public static Error ContractFinancialInitialAmountMustBePositiveForPrepayment =
        new(
            "InvalidArguments",
            "برای ثبت پیش‌پرداخت، مبلغ اولیه محاسبه‌شده قرارداد باید بیشتر از صفر باشد.",
            422);

    public static Error ContractFinancialCeilingAmountIsRequired =
        new(
            "InvalidArguments",
            "سقف مبلغ قرارداد برای ساختار قیمت‌گذاری این قرارداد الزامی است.",
            422);

    public static Error ContractFinancialTermsInvalid =
        new(
            "InvalidArguments",
            "اطلاعات مالی قرارداد معتبر نیست.",
            422);

    public static Error ContractTypeDetailAdjustmentNotAllowed =
        new(
            "Conflict",
            "این قرارداد مشمول تعدیل نیست و ثبت تعدیل برای جزئیات آن مجاز نمی‌باشد.",
            409);

    public static Error ContractAdjustmentCannotBeDisabledWithExistingDetails =
        new(
            "Conflict",
            "تا زمانی که برای جزئیات قرارداد تعدیل ثبت شده است، امکان غیرفعال کردن مشمول تعدیل وجود ندارد.",
            409);

    public static Error ContractTypeDetailAdjustmentEligibilityInvalid =
        new(
            "Conflict",
            "جزئیات قراردادی که مشمول تعدیل نیست نمی‌تواند دارای اطلاعات تعدیل باشد.",
            409);

    public static Error ContractAdjustmentConfigurationNotFound =
        new("NotFound", "پیکربندی تعدیل قرارداد یافت نشد.", 404);
    public static Error ContractAdjustmentConfigurationAlreadyExists =
        new("Conflict", "پیکربندی فعال تعدیل برای قرارداد قبلاً ثبت شده است.", 409);
    public static Error ContractAdjustmentConfigurationNotAllowed =
        new("Conflict", "این قرارداد امکان ثبت پیکربندی تعدیل را ندارد.", 409);
    public static Error ContractAdjustmentConfigurationScopeInvalid =
        new("InvalidArguments", "دامنه پیکربندی تعدیل قرارداد معتبر نیست.", 422);
    public static Error ContractAdjustmentManagedByConfiguration =
        new("Conflict", "تعدیل جزئیات این قرارداد از طریق پیکربندی تعدیل قرارداد مدیریت می‌شود.", 409);
    public static Error ContractAdjustmentConfigurationMustBeRemovedBeforeDisabling =
        new("Conflict", "پیش از غیرفعال کردن مشمول تعدیل، پیکربندی تعدیل قرارداد باید حذف شود.", 409);

    public static Error ContractTypeDetailAdjustmentPriceIndexInvalid =
    new(
        "InvalidArguments",
        "مرجع یا شاخص تعدیل انتخاب‌شده معتبر یا فعال نیست.",
        422);

    public static Error ContractChangeNotFound =
        new("NotFound", "تغییر قرارداد یافت نشد.", 404);

    public static Error ContractChangeNumberAlreadyExists =
        new("Conflict", "شماره تغییر قرارداد قبلاً ثبت شده است.", 409);

    public static Error ContractChangeCannotBeCreatedInCurrentStatus =
        new(
            "Conflict",
            "ثبت تغییر یا الحاقیه قرارداد در وضعیت جاری قرارداد مجاز نیست.",
            409);

    public static Error ContractBaselineCannotChangeAfterContractChange =
        new(
            "Conflict",
            "پس از ثبت تغییرات قرارداد، ساختار و اطلاعات پایه قرارداد باید از مسیر الحاقیه و تغییرات قرارداد مدیریت شود.",
            409);

    public static Error ContractBaselineCannotChangeInCurrentStatus =
        new(
            "Conflict",
            "امکان تغییر اطلاعات پایه قرارداد در وضعیت جاری چرخه عمر قرارداد وجود ندارد.",
            409);

    public static Error ContractChangeOnlyLatestCanBeModified =
        new("InvalidArguments", "فقط آخرین تغییر قرارداد قابل ویرایش یا حذف است.", 422);

    public static Error ContractChangeDateInvalid =
        new("InvalidArguments", "تاریخ تغییر قرارداد با ترتیب زمانی قرارداد سازگار نیست.", 422);

    public static Error ContractChangeDurationInvalid =
        new("InvalidArguments", "مدت نهایی قرارداد باید بیشتر از صفر باشد.", 422);

    public static Error ContractChangeItemInvalid =
        new("InvalidArguments", "قلم تغییر قرارداد معتبر نیست.", 422);

    public static Error ContractChangeItemDuplicate =
        new("Conflict", "یک قلم تغییر قرارداد بیش از یک بار ثبت شده است.", 409);

    public static Error ContractChangeItemNoEffectiveChange =
        new("InvalidArguments", "مقدار جدید قلم باید با مقدار فعلی متفاوت باشد.", 422);

    public static Error ContractChangeSourceItemNotSupported =
        new("InvalidArguments", "افزودن منبع جدید برای این نوع یا روش قیمت‌گذاری قرارداد پشتیبانی نمی‌شود.", 422);

    public static Error ContractChangeSourceUnitPriceNotFound =
        new("InvalidArguments", "مبلغ واحد برآوردی منبع پروژه یافت نشد.", 422);

    public static Error ContractChangeWouldExceedSourceQuantity =
        new("InvalidArguments", "مقدار جدید از ظرفیت قابل تخصیص منبع بیشتر است.", 422);

    public static Error ContractFinancialCeilingAmountCannotBeLessThanCurrentContractAmount =
        new("InvalidArguments", "سقف مبلغ قرارداد نمی‌تواند از مبلغ جاری قرارداد کمتر باشد.", 422);

    public static Error ContractChangeWouldMakeContractAmountNegative =
        new("InvalidArguments", "مبلغ نهایی قرارداد نمی‌تواند منفی باشد.", 422);
    public static Error ReferenceNotFound =
        new(
            "NotFound",
            "مرجع شاخص تعدیل یافت نشد.",
            404);

    public static Error IndexNotFound =
        new(
            "NotFound",
            "شاخص تعدیل یافت نشد.",
            404);

    public static Error ContractAdjustmentReferenceCodeAlreadyExists =
        new(
            "Conflict",
            "کد مرجع تعدیل قبلاً ثبت شده است.",
            409);

    public static Error ContractAdjustmentIndexCodeAlreadyExists =
        new(
            "Conflict",
            "کد شاخص تعدیل برای این مرجع قبلاً ثبت شده است.",
            409);

    public static Error ContractStatusTransitionInvalid =
        new(
            "Conflict",
            "تغییر وضعیت قرارداد از وضعیت فعلی به وضعیت انتخاب‌شده مجاز نمی‌باشد.",
            409);

    public static Error ContractStatusOperationInvalid =
        new("Conflict", "عملیات تغییر وضعیت قرارداد معتبر نیست.", 409);

    public static Error ContractStatusEffectiveDateRequired =
        new("InvalidArguments", "تاریخ اثرگذاری تغییر وضعیت قرارداد الزامی است.", 422);

    public static Error ContractStatusReasonRequired =
        new("InvalidArguments", "علت تغییر وضعیت قرارداد الزامی است.", 422);

    public static Error ContractStatusDocumentsRequired =
        new("InvalidArguments", "ثبت مستندات برای این عملیات تغییر وضعیت قرارداد الزامی است.", 422);

    public static Error ContractSuspensionDurationInvalid =
        new("InvalidArguments", "مدت تعلیق قرارداد باید بیشتر از صفر باشد.", 422);

    public static Error ContractActivationBeforeStartDate =
        new("Conflict", "فعال‌سازی قرارداد پیش از تاریخ شروع قرارداد مجاز نیست.", 409);

    public static Error ContractStatusChangeMustUseBusinessOperation =
        new("Conflict", "تغییر وضعیت قرارداد پس از نهایی‌سازی باید از مسیر عملیات چرخه عمر انجام شود.", 409);

    public static Error ContractNumberAlreadyExists =
        new("Conflict", "شماره قرارداد قبلاً ثبت شده است.", 409);

    public static Error ContractRegistrationTypeInvalid =
        new("InvalidArguments", "ترکیب نوع قرارداد برای ثبت قرارداد معتبر نیست.", 422);

    public static Error ContractStructureIsNotCompatibleWithRegistrationView =
        new("Conflict", "ساختار قیمت‌گذاری قرارداد در نمای ثبت یکپارچه قابل نمایش نیست.", 409);

    public static Error ContractRegistrationEndDateMismatch =
        new("InvalidArguments", "تاریخ پایان واردشده با تاریخ محاسبه‌شده قرارداد مطابقت ندارد.", 422);

    public static Error ContractRegistrationGridFilterInvalid =
        new("InvalidArguments", "فیلتر شماره قرارداد معتبر نیست.", 422);

    public static Error ContractRegistrationNewAmountMismatch =
        new("InvalidArguments", "مبلغ جدید قرارداد با مبلغ محاسبه‌شده مطابقت ندارد.", 422);

    public static Error ContractRegistrationNewEndDateMismatch =
        new("InvalidArguments", "تاریخ پایان جدید با تاریخ محاسبه‌شده مطابقت ندارد.", 422);

    public static Error ContractChangeModeInvalid =
        new("Conflict", "شیوه ثبت تغییر قرارداد با عملیات درخواستی سازگار نیست.", 409);

    public static Error ContractRegistrationIsNotPending =
        new("Conflict", "ثبت قرارداد در وضعیت انتظار نهایی‌سازی قرار ندارد.", 409);

    public static Error ContractRegistrationMustBeFinalizedBeforeStatusChange =
        new("Conflict", "پیش از تغییر وضعیت قرارداد، فرایند ثبت قرارداد باید نهایی شود.", 409);

    public static Error ContractRegistrationStatusMustBeDraft =
        new("InvalidArguments", "وضعیت قرارداد در ثبت اولیه فقط می‌تواند پیش‌نویس باشد.", 422);
}
