namespace Engineering.Domain.Cmts;

public static class TransportationContractorCmts
{
    public const string TransportationRequestDetailId = "شناسه جزییات درخواست";
    public const string TransportationRequestId = "شناسه درخواست ترابری";
    public const string TransportationRequest = "درخواست ترابری";
    public const string TransportationContractorId = "شناسه پیمانکار حمل";
    public const string TransportationContractor = "پیمانکار حمل";
    public const string TransportationContractorMachine = "ماشین های پیمانکار";
    public const string TransportationContractorPersonnelMachine = "ماشین های پرسنل پیمانکار";
    public const string TransportationContractorPersonnelId = "شناسه پرسنل پیمانکار حمل";
    public const string TransportationContractorManagerId = "شناسه سرپرست پیمانکار حمل";
    public const string TransportationContractorPersonnels = "پرسنل های پیمانکار حمل";
    public const string TransportationContractorManagers = "سرپرست های پیمانکار حمل";
    public const string TransportationContractorDocuments = "پیوست های پیمانکار حمل";
    public const string ShippingCosts = "هزینه های ارسال";
    public const string PriceWeights = "قیمت وزن بار";
    public const string TransportationContractorPersonnel = "پرسنل پیمانکار حمل";
    public const string FirstName = "نام";
    public const string LastName = "نام خانوادگی";
    public const string PhoneNumber = "شماره تماس";
    public const string IdentityNo = "کدملی";
    public const string CompanyName = "نام شرکت";
    public const string RegistrationNo = "کد شناسه ثبت";
    public const string StartOfContract = "شروع قرارداد";
    public const string Title = "عنوان";
    public const string EndOfContract = "پایان قرارداد";
    public const string Type = "نوع محاسبه پیمانکار حمل";
    public const string Address = "آدرس";
    public const string PostalCode = "کدپستی";
    public const string CityId = "شهر";
    public const string ThirdPartyId = "طرف حساب";
    public const string SecondPrefix = "مقدار دوم بارنامه داخلی";
    public const string FirstPrefix = "مقدار اول بارنامه داخلی";
    public const string TaxPercent = "درصد مالیات";
    public const string ServicePrice = "هزینه خدمات";
    public const string FixedNumber = "عدد ثابت";
    public const string PercentageValue = "درصد محاسبه";
    public const string DeliveryMethod = "روش ارسالی";
    public const string DeliveryType = "نوع ارسال";
    public const string PackingShippingType = "نوع ترابری";
    public const string PostageDate = "تاریخ ارسال";
    public const string VehicleName = "عنوان وسیله نقلیه";
    public const string NumberPlate = "پلاک";
    public const string DriverPhoneNumber = "شماره تماس راننده";
    public const string Driver = "راننده";
}

public static class ShippingCostCmts
{
    public const string TransportationContractorId = "شناسه پیمانکار حمل";
    public const string TransportationContractor = "پیمانکار حمل";
    public const string TransportationContractors = "پیمانکاران حمل";
    public const string ShippingCostId = "شناسه هزینه ارسال";
    public const string ShippingCost = "هزینه ارسال";
    public const string ShippingCosts = "هزینه های ارسال";
    public const string MachineTypeId = "شناسه نوع ماشین";
    public const string MachineType = "نوع ماشین";
    public const string MachineTypes = "نوع ماشین ها";
    public const string Count = "تعداد";
    public const string LoadWeight = "وزن بار";
    public const string DestinationCity = "شهر مقصد";
    public const string SourceCity = "شهر مبدا";
    public const string Price = "قیمت";
    public const string Tax = "مالیات";
    public const string FromDate = "از تاریخ";
    public const string ToDate = "تا تاریخ";
    public const string ThirdParty = "طرف حساب";
    public const string ThirdPartyCompany = "کمپانی طرف حساب";
    public const string Latitude = "عرض جغرافیایی";
    public const string Longitude = "طول جغرافیایی";
}

public static class TransportationContractorPriceWeightCmts
{
    public const string TransportationContractorId = "شناسه پیمانکار حمل";
    public const string TransportationContractor = "پیمانکار حمل";
    public const string TransportationContractorPriceWeightId = "شناسه هزینه ارسال بر حسب وزن";
    public const string TransportationContractorPriceWeight = "هزینه ارسال بر حسب وزن";
    public const string TransportationContractorPriceWeights = "هزینه های ارسال بر حسب وزن";
    public const string Measureunit = "واحد";
    public const string Price = "هزینه";
    public const string UntilWeight = "تا وزن";
    public const string IsFixed = "ثابت";
}

public static class TransportationContractorInsuranceCmts
{
    public const string TransportationContractorId = "شناسه پیمانکار حمل";
    public const string TransportationContractor = "پیمانکار حمل";
    public const string TransportationContractorInsuranceId = "شناسه هزینه بیمه ای";
    public const string TransportationContractorInsurance = "هزینه بیمه ای";
    public const string TransportationContractorInsurances = "هزینه های بیمه ای";
    public const string MinProductPrice = "کمترین ارزش بار";
    public const string MaxProductPrice = "بیشترین ارزش بار";
    public const string FixedPrice = "مقدار ثابت";
    public const string Multiplication = "ضرب";
    public const string Division = "تقسیم";
    public const string Subtraction = "تفریق";
    public const string Addition = "جمع";
}

public static class TransportationRequestWarehouseComment
{
    public const string TransportationRequestId = "شناسه درخواست ترابری";
    public const string TransportationRequest = "درخواست ترابری";
    public const string TransportationRequestWarehouseId = "شناسه انبار درخواست ترابری";
    public const string TransportationRequestWarehouse = "انبار درخواست ترابری";
    public const string TransportationRequestWarehouseProductId = "شناسه کالای انبار درخواست ترابری";
    public const string TransportationRequestWarehouseProduct = "کالای انبار درخواست ترابری";
    public const string WarehouseId = "شناسه انبار";
    public const string TransportationCargoPallet = "شناسه پلت مرسوله";
    public const string TransportationCargo = "شناسه مرسوله";
    public const string Warehouse = "انبار";
    public const string Weight = "وزن بار";
    public const string ThirdPartyId = "شناسه طرف حساب";
    public const string ThirdParty = "طرف حساب";
    public const string ThirdPartyName = "نام طرف حساب";
    public const string PackingId = "شناسه پکینگ";
    public const string PackingShippingId = "شناسه حمل و نقل پکینگ";
    public const string PackingProductId = "شناسه کالای پکینگ";
    public const string PackingPalletId = "شناسه پالت پکینگ";
    public const string PackingAddressId = "شناسه آدرس پکینگ";
    public const string PackingNumber = "شماره پکینگ";
    public const string Packing = "پکینگ";
    public const string Price = "ارزش بار";
    public const string ShippingCostId = "شناسه هزینه ارسال";
    public const string ShippingCost = "هزینه ارسال";
    public const string Quantity = "تعداد";
    public const string ProductId = "هزینه ارسال";
    public const string Product = "هزینه ارسال";
    public const string PalletNumber = "هزینه ارسال";
    public const string Address = "آدرس";
    public const string SecurityConfirm = "تایید حراست";
}

public static class TransportationContractorMachineCmts
{
    public const string NumberPlate = "پلاک";
    public const string Vin = "شماره شاسی";
    public const string Color = "رنگ";
    public const string TransportationContractorMachineId = "شناسه ماشین پیمانکار";
    public const string TransportationContractorMachine = "ماشین پیمانکار";
    public const string TransportationContractorPersonnel = "خدمه ماشین پیمانکار";
    public const string contractorPersonnelId = "شناسه پرسنل";
    public const string MachineTypeId = "شناسه نوع ماشین";
    public const string DriverId = "راننده";
    public const string MachineType = "نوع ماشین";
    public const string TransportationContractorId = "شناسه پیمانکار حمل";
    public const string TransportationContractor = "پیمانکار حمل";
    public const string CertificateNumber = "CertificateNumber";
}