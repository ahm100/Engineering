using Engineering.ClientSdk.Enums;
using Engineering.Domain.Entities.BillOfLadings;
using Engineering.Domain.Entities.CostCenters;
using Engineering.Domain.Entities.Logistics;
using Engineering.Domain.Entities.MachineTypes;
using Engineering.Domain.Entities.Projects;
using Engineering.Domain.Entities.Synonyms.MetaData.Cities;
using Engineering.Domain.Entities.Synonyms.MetaData.ThirdParties;
using Engineering.Domain.Entities.Transportations.Enums;
using Engineering.Domain.Entities.Trips;

namespace Engineering.Domain.Entities.Transportations;

[Description(TransportationCmts.TransportationRequest)]
public class TransportationRequest : AuditableEntity<TransportationRequest>
{

    #region Cals

    [Description(TransportationCmts.CostGroupId)]
    public long? CostGroupId { get; private set; }

    [Description(TransportationCmts.CostCategoryId)]
    public long? CostCategoryId { get; private set; }

    [Description(TransportationCmts.StartingCityId)]
    public long? StartingCityId { get; private set; }

    public ViewCity? StartingCity { get; private set; }

    [Description(TransportationCmts.DestinationCityId)]
    public long? DestinationCityId { get; private set; }
    public ViewCity? DestinationCity { get; private set; }

    [Description(TransportationCmts.ImageLink)]
    public string? ImageLink { get; private set; }

    [Description(TransportationCmts.StartDate)]
    public DateTime? StartDate { get; private set; }

    [Description(TransportationCmts.EndDate)]
    public DateTime? EndDate { get; private set; }

    [Description(TransportationCmts.Description)]
    public string? Description { get; private set; } = string.Empty;

    [Description(TransportationCmts.DriverId)]
    public long? DriverId { get; private set; }

    public ViewThirdParty? Driver { get; private set; }

    [Description(TransportationCmts.DriverName)]
    public string? DriverName { get; private set; }

    [Description(TransportationCmts.CertificateNumber)]
    public string? CertificateNumber { get; private set; }

    [Description(TransportationCmts.PostageDate)]
    public DateTime? PostageDate { get; private set; }

    [Description(TransportationCmts.ReceivedDate)]
    public DateTime? ReceivedDate { get; private set; }

    [Description(TransportationCmts.BillOfLadingImage)]
    public string? BillOfLadingImage { get; private set; }

    [Description(TransportationCmts.DelivererName)]
    public string? DelivererName { get; private set; }

    [Description(TransportationCmts.RecipientName)]
    public string? RecipientName { get; private set; }

    [Description(TransportationCmts.FreightNumber)]
    public string? FreightNumber { get; private set; }

    [Description(TransportationCmts.LoadWeight)]
    public decimal? LoadWeight { get; private set; }

    [Description(TransportationCmts.Volume)]
    public decimal? Volume { get; private set; }

    [Description(TransportationCmts.PhoneNumber)]
    public string? PhoneNumber { get; private set; }

    [Description(TransportationCmts.CarSpecifications)]
    public string? CarSpecifications { get; private set; }

    [Description(TransportationCmts.NumberPlates)]
    public string? NumberPlates { get; private set; }

    [Description(TransportationCmts.AccountNumber)]
    public string? AccountNumber { get; private set; }

    [Description(TransportationCmts.BankId)]
    public long? BankId { get; private set; }

    [Description(TransportationCmts.CardNumber)]
    public string? CardNumber { get; private set; }

    [Description(TransportationCmts.AccountName)]
    public string? AccountName { get; private set; }

    [Description(TransportationCmts.IBAN)]
    public string? IBAN { get; private set; }

    [Description(TransportationCmts.Price)]
    public decimal? Price { get; private set; }

    [Description(TransportationCmts.CurrencyUnitId)]
    public long? CurrencyUnitId { get; private set; }

    [Description(TransportationCmts.AccountDescription)]
    public string? AccountDescription { get; private set; } = string.Empty;

    [Description(TransportationCmts.CarID)]
    public string? CarID { get; private set; } = string.Empty;

    [Description(TransportationCmts.TransportationRequestStatus)]
    public TransportationRequestStatus TransportationRequestStatus { get; private set; } = TransportationRequestStatus.InitialRegistration;

    [Description(TransportationCmts.TransportationPaymentType)]
    public TransportationPaymentType? TransportationPaymentType { get; private set; }

    [Description(TransportationCmts.ManagerDescription)]
    public string? ManagerDescription { get; private set; } = string.Empty;

    [Description(TransportationCmts.ConfirmUserId)]
    public long? ConfirmUserId { get; private set; }

    [Description(TransportationCmts.ConfirmDate)]
    public DateTime? ConfirmDate { get; private set; }

    [Description(TransportationCmts.CompanyId)]
    public long? CompanyId { get; private set; }

    [Description(TransportationCmts.SnapRequester)]
    public long? SnapRequester { get; private set; }

    [Description(TransportationCmts.SecondDestinationCityId)]
    public long? SecondDestinationCityId { get; private set; }

    [Description(TransportationCmts.FareAmount)]
    public decimal? FareAmount { get; private set; }

    [Description(TransportationCmts.StopRate)]
    public int? StopRate { get; private set; }

    [Description(TransportationCmts.DestinationAddress)]
    public string? DestinationAddress { get; private set; }

    [Description(TransportationCmts.SecondDestinationAddress)]
    public string? SecondDestinationAddress { get; private set; }

    [Description(TransportationCmts.StartingCityAddress)]
    public string? StartingCityAddress { get; private set; }

    [Description(TransportationCmts.PersonalPayment)]
    public bool? PersonalPayment { get; private set; }

    [Description(TransportationCmts.PassengerId)]
    public long? PassengerId { get; private set; }

    [Description(TransportationCmts.Passenger)]
    public string? Passenger { get; private set; }

    [Description(TransportationCmts.TicketPayerId)]
    public long? TicketPayerId { get; private set; }

    [Description(TransportationCmts.ReturnToStart)]
    public bool? ReturnToStart { get; private set; }

    [Description(TransportationCmts.PaymentOrderId)]
    public long? PaymentOrderId { get; private set; }

    [Description(TransportationCmts.PaymentDate)]
    public DateTime? PaymentDate { get; private set; }

    [Description(TransportationCmts.IsCredit)]
    public bool IsCredit { get; private set; }

    [Description(TransportationCmts.IsAggregate)]
    public bool IsAggregate { get; private set; } = false;

    [Description(TransportationCmts.DeliveryMethod)]
    public DeliveryMethod? DeliveryMethod { get; set; }

    [Description(TransportationCmts.DeliveryType)]
    public DeliveryType? DeliveryType { get; set; }

    [Description(TransportationCmts.RequestNumber)]
    public long? RequestNumber { get; private set; }

    public bool IsPaid => this.TransportationRequestStatus == TransportationRequestStatus.Paid ||
        this.TransportationRequestStatus == TransportationRequestStatus.SecurityConfirm;

    #endregion

    [Description(TransportationCmts.Transportation)]
    public Transportation? Transportation { get; set; }
    public long? TransportationId { get; set; }

    [Description(TransportationCmts.Trip)]
    public long? TripId { get; set; }
    public Trip? Trip { get; set; }

    [Description(TransportationCmts.MachineType)]
    public long? MachineTypeId { get; set; }
    public MachineType? MachineType { get; set; }

    [Description(TransportationCmts.Project)]
    public long? ProjectId { get; set; }
    public Project? Project { get; set; }

    [Description(TransportationCmts.BillOfLading)]
    public long? BillOfLadingId { get; set; }
    public BillOfLading? BillOfLading { get; set; }

    [Description(TransportationCmts.CostCenter)]
    public long? CostCenterId { get; set; }
    public CostCenter? CostCenter { get; set; }

    [Description(TransportationCmts.Season)]
    public long? SeasonId { get; set; }
    public Season? Season { get; set; }

    [Description(TransportationCmts.TransportationContractor)]
    public long? TransportationContractorId { get; set; }
    public TransportationContractor? TransportationContractor { get; set; }

    #region TransportationRequest Create
    public TransportationRequest(Transportation transportation, Trip trip, MachineType machine, BillOfLading? billOfLading, long startingCityId,
        long destinationCityId, string? imageLink, DateTime startDate, DateTime endDate, string? description, long? driverId, string? driverName,
        DateTime? postageDate, DateTime? receivedDate, string? billOfLadingImage, string? delivererName, string? recipientName, string? freightNumber,
        decimal? loadWeight, string? phoneNumber, string? carSpecifications, string? numberPlates, string? accountNumber, long? bankId, string? cardNumber,
        string? accountName, string? iBAN, decimal? price, long? currencyUnitId, string? accountDescription, string? carID, long? costGroupId,
        long? costCategoryId, string? startingCityAddress, string? destinationCityAddress, long? requestNumber, bool isCredit, long? companyId,
        TransportationContractor? transportationContractor) : this()
    {
        SetTransportation(transportation);
        SetTrip(trip);
        SetMachine(machine);
        SetBillOfLading(billOfLading);
        SetStartingCityId(startingCityId);
        SetDestinationCityId(destinationCityId);
        SetImageLink(imageLink);
        SetStartDate(startDate);
        SetEndDate(endDate);
        SetDescription(description);
        SetDriverId(driverId);
        SetDriverName(driverName);
        SetPostageDate(postageDate);
        SetReceivedDate(receivedDate);
        SetBillOfLadingImage(billOfLadingImage);
        SetDelivererName(delivererName);
        SetRecipientName(recipientName);
        SetFreightNumber(freightNumber);
        SetLoadWeight(loadWeight);
        SetPhoneNumber(phoneNumber);
        SetCarSpecifications(carSpecifications);
        SetNumberPlates(numberPlates);
        SetAccountNumber(accountNumber);
        SetBankId(bankId);
        SetCardNumber(cardNumber);
        SetAccountName(accountName);
        SetIBAN(iBAN);
        SetPrice(price);
        SetCurrencyUnitId(currencyUnitId);
        SetAccountDescription(accountDescription);
        SetCarID(carID);
        SetCompanyId(companyId);
        SetTransportationCostGroup(costGroupId);
        SetTransportationCostCategory(costCategoryId);
        SetStartingCityAddress(startingCityAddress);
        SetDestinationAddress(destinationCityAddress);
        SetRequestNumber(requestNumber);
        SetIsCredit(isCredit);
        SetTransportationPaymentType(Enums.TransportationPaymentType.NotPaid);
        SetTransportationContractor(transportationContractor);
    }
    #endregion

    #region Warehouse
    public TransportationRequest(
        Transportation? transportation,
        DeliveryMethod? deliveryMethod,
        DeliveryType? deliveryType,
        long? startingCityId,
        long? destinationCityId,
        string? startingCityAddress,
        string? destinationAddress,
        DateTime? postageDate,
        decimal? loadWeight,
        decimal? price,
        string? description,
        long? companyId,
        TransportationContractor transportationContractor,
        long? requestNumber,
        bool isCredit) : this()
    {
        SetTransportation(transportation);
        SetStartingCityId(startingCityId);
        SetDestinationCityId(destinationCityId);
        SetDescription(description);
        SetPostageDate(postageDate);
        SetLoadWeight(loadWeight);
        SetPrice(price);
        SetCompanyId(companyId);
        SetStartingCityAddress(startingCityAddress);
        SetDestinationAddress(destinationAddress);
        SetRequestNumber(requestNumber);
        SetIsCredit(isCredit);
        SetTransportationPaymentType(Enums.TransportationPaymentType.NotPaid);
        SetTransportationContractor(transportationContractor);
        SetTransportationRequestStatus(TransportationRequestStatus.DriverAssignment);
        SetDeliveryMethod(deliveryMethod);
        SetDeliveryType(deliveryType);
    }

    public TransportationRequest(
        DeliveryMethod? deliveryMethod,
        DeliveryType? deliveryType,
        long? startingCityId,
        long? destinationCityId,
        string? startingCityAddress,
        string? destinationAddress,
        DateTime? postageDate,
        decimal? price,
        string? description,
        long? companyId,
        TransportationContractor transportationContractor,
        long? requestNumber,
        string? freightNumber,
        bool isCredit,
        string? numberPlates,
        long? driverId,
        string? driverName,
        MachineType? machineType,
        string? carSpec,
        string? phoneNumber,
        decimal loadWeight) : this()
    {
        SetStartingCityId(startingCityId);
        SetDestinationCityId(destinationCityId);
        SetDescription(description);
        SetPostageDate(postageDate);
        SetPrice(price);
        SetCompanyId(companyId);
        SetStartingCityAddress(startingCityAddress);
        SetDestinationAddress(destinationAddress);
        SetRequestNumber(requestNumber);
        SetIsCredit(isCredit);
        SetTransportationPaymentType(Enums.TransportationPaymentType.NotPaid);
        SetTransportationContractor(transportationContractor);
        SetTransportationRequestStatus(TransportationRequestStatus.DriverAssignment);
        SetDeliveryMethod(deliveryMethod);
        SetDeliveryType(deliveryType);
        SetNumberPlates(numberPlates);
        SetDriverName(driverName);
        SetMachine(machineType);
        SetCarSpecifications(carSpec);
        SetPhoneNumber(phoneNumber);
        SetDriverId(driverId);
        SetFreightNumber(freightNumber);
        SetLoadWeight(loadWeight);
    }

    public void UpdateWarehouseTransport(
        TransportationRequestDetail detail,
        decimal transferPrice)
    {
        Price = transferPrice;
        _transportationRequestDetails.Add(detail);
    }

    public void AddWarehouseTransportDetail(
        TransportationRequestDetail detail)
    {
        _transportationRequestDetails.Add(detail);
    }

    public void UpdateWarehouseTransportExtras(
        TransportationRequestDetail detail,
        decimal transferPrice,
        decimal? volume,
        decimal? weight)
    {
        Price = transferPrice;
        Volume = volume;
        LoadWeight = weight;

        if (_transportationRequestDetails is null)
        {
            _transportationRequestDetails?.Add(detail);
        }
        else
        {
            _transportationRequestDetails.OrderByDescending(x => x.Id)
                .FirstOrDefault()?.Update(detail.GlobalFreightNumber, detail.ClassifiedFreightNumber, detail.Tax, detail.TransferPrice,
                detail.ServicePrice, detail.InsuranceNumber, detail.InsurancePrice, detail.ShippingCost, detail.ProductTotalPrice, detail.OutofRange,
                detail.OrderNumber, detail.TransportationRequest);
        }
    }

    public void UpdateWarehouseTransport(
        TransportationRequestDetail detail,
        MachineType machineType,
        decimal transferPrice,
        long driverId,
        string numberPlate,
        string? certificateNumber,
        decimal? volume,
        List<string>? documentUrls)
    {
        Price = transferPrice;
        MachineType = machineType;
        DriverId = driverId;
        NumberPlates = numberPlate;
        IsAggregate = true;
        CertificateNumber = certificateNumber;
        Volume = volume;
        _transportationRequestDetails.Add(detail);

        if (documentUrls != null && documentUrls.Count > 0)
            foreach (var item in documentUrls)
                _documents.Add(new TransportationRequestDocument(item, false, this));
    }

    public void AddBills(
        List<string>? documentUrls)
    {
        if (documentUrls != null && documentUrls.Count > 0)
            foreach (var item in documentUrls)
                _documents.Add(new TransportationRequestDocument(item, true, this));
    }

    public void UpdateAfterAggregate(
        decimal? transferPrice,
        string? description,
        MachineType? machineType,
        long? driverId,
        string? numberPlate,
        string? certificateNumber,
        DateTime? postageDate,
        long detailId,
        string? globalFreightNumber,
        string? classifiedFreightNumber,
        decimal? tax,
        decimal? detailTransferPrice,
        decimal? servicePrice,
        string? insuranceNumber,
        decimal? insurancePrice,
        decimal? shippingCost,
        decimal? productTotalPrice,
        decimal? outofRange,
        string? orderNumber,
        decimal? loadWeight,
        List<string>? documents,
        List<(long, decimal)>? warehousePrice
        )
    {
        Price = transferPrice;
        MachineType = machineType;
        DriverId = driverId;
        NumberPlates = numberPlate;
        Description = description;
        PostageDate = postageDate;
        LoadWeight = loadWeight;
        CertificateNumber = certificateNumber;

        _transportationRequestDetails.FirstOrDefault(x => x.Id == detailId)?.Update(globalFreightNumber,
            classifiedFreightNumber, tax, detailTransferPrice, servicePrice, insuranceNumber, insurancePrice,
            shippingCost, productTotalPrice, outofRange, orderNumber, this);

        if (warehousePrice != null && warehousePrice.Count > 0)
            foreach (var item in warehousePrice)
                _TransportationCargoPallets.FirstOrDefault(x => x.Id == item.Item1)?
                    .UpdateShippingPrice(item.Item2);

        _documents.ForEach(x => x.SoftDelete());
        if (documents != null && documents.Count > 0)
            foreach (var item in documents)
                _documents.Add(new TransportationRequestDocument(item, false, this));
    }
    #endregion

    #region SnapRequest Create
    public TransportationRequest(Transportation transportation, Trip trip, long? costGroupId,
        long? costCategoryId, long? startingCityId, long? destinationCityId, DateTime startDate, DateTime endDate, string? description,
        long? driverId, string? driverName, string? phoneNumber, string? carSpecifications, string? numberPlates, long? currencyUnitId, long? companyId,
        long? snapRequester, long? secondDestinationCityId, decimal? fareAmount, int? stopRate, string? destinationAddress, string? secondDestinationAddress,
        bool? personalPayment, string? startingCityAddress, bool? returnToStart, string? recipientName, long? requestNumber, bool isCredit) : this()
    {
        SetTransportation(transportation);
        SetTrip(trip);
        SetStartingCityId(startingCityId);
        SetDestinationCityId(destinationCityId);
        SetStartDate(startDate);
        SetEndDate(endDate);
        SetDescription(description);
        SetDriverId(driverId);
        SetDriverName(driverName);
        SetPhoneNumber(phoneNumber);
        SetCarSpecifications(carSpecifications);
        SetNumberPlates(numberPlates);
        SetCurrencyUnitId(currencyUnitId);
        SetCompanyId(companyId);
        SetTransportationCostGroup(costGroupId);
        SetTransportationCostCategory(costCategoryId);
        SetSnapRequester(snapRequester);
        SetSecondDestinationCityId(secondDestinationCityId);
        SetPrice(fareAmount);
        SetStopRate(stopRate);
        SetDestinationAddress(destinationAddress);
        SetSecondDestinationAddress(secondDestinationAddress);
        SetPersonalPayment(personalPayment);
        SetReturnToStart(returnToStart);
        SetStartingCityAddress(startingCityAddress);
        SetRecipientName(recipientName);
        SetRequestNumber(requestNumber);
        SetIsCredit(isCredit);
        SetTransportationPaymentType(Enums.TransportationPaymentType.NotPaid);
    }
    #endregion

    #region AirPlaneRequest
    public TransportationRequest(Transportation transportation, Trip trip, long? costGroupId,
        long? costCategoryId, long startingCityId, long destinationCityId, DateTime startDate,
        DateTime endDate, string? description, string? accountName, string? accountNumber, string? cardNumber,
        long? bankId, long? currencyUnitId, long? companyId, string? passenger, long? passengerId, long? ticketPayerId,
        decimal? fareAmount, string? destinationAddress, string? iban, long? requestNumber, bool isCredit) : this()
    {
        SetTransportation(transportation);
        SetTrip(trip);
        SetStartingCityId(startingCityId);
        SetDestinationCityId(destinationCityId);
        SetStartDate(startDate);
        SetEndDate(endDate);
        SetDescription(description);
        SetAccountName(accountName);
        SetAccountNumber(accountNumber);
        SetCardNumber(cardNumber);
        SetCurrencyUnitId(currencyUnitId);
        SetCompanyId(companyId);
        SetTransportationCostGroup(costGroupId);
        SetTransportationCostCategory(costCategoryId);
        SetPrice(fareAmount);
        SetDestinationAddress(destinationAddress);
        SetTicketPayerId(ticketPayerId);
        SetPassengerId(passengerId);
        SetPassenger(passenger);
        SetBankId(bankId);
        SetIBAN(iban);
        SetRequestNumber(requestNumber);
        SetIsCredit(isCredit);
        SetTransportationPaymentType(Enums.TransportationPaymentType.NotPaid);

    }
    #endregion

    #region Set data

    public void SetTransportationContractor(TransportationContractor? value)
    {
        TransportationContractor = value;
        TransportationContractorId = value?.Id;
    }

    public void SetSeason(Season? value)
    {
        Season = value;
        SeasonId = value?.Id;
    }
    public void SetPaymentOrderId(long? value)
    {
        PaymentOrderId = value;
    }
    public void SetPassenger(string? value)
    {
        Passenger = value;
    }
    public void SetPaymentDate(DateTime? value)
    {
        PaymentDate = value;
    }
    public void SetDeliveryMethod(DeliveryMethod? value)
    {
        DeliveryMethod = value;
    }
    public void SetDeliveryType(DeliveryType? value)
    {
        DeliveryType = value;
    }
    public void SetTransportationPaymentType(TransportationPaymentType? value)
    {
        TransportationPaymentType = value;
    }
    public void SetReturnToStart(bool? value)
    {
        ReturnToStart = value;
    }
    public void SetRequestNumber(long? value)
    {
        RequestNumber = value;
    }
    public void SetStartingCityAddress(string? value)
    {
        StartingCityAddress = value;
    }
    public void SetPassengerId(long? value)
    {
        PassengerId = value;
    }
    public void SetTicketPayerId(long? value)
    {
        TicketPayerId = value;
    }
    public void SetTransportation(Transportation? value)
    {
        Transportation = value;
        TransportationId = value?.Id;
    }
    public void SetTrip(Trip? value)
    {
        Trip = value;
        TripId = value?.Id;
    }
    public void SetMachine(MachineType? value)
    {
        MachineType = value;
        MachineTypeId = value?.Id;
    }
    public void SetProject(Project? value)
    {
        Project = value;
        ProjectId = value?.Id;
    }
    public void SetBillOfLading(BillOfLading? value)
    {
        BillOfLading = value;
        BillOfLadingId = value?.Id;
    }
    public void SetCostCenter(CostCenter? value)
    {
        CostCenter = value;
        CostCenterId = value?.Id;
    }
    public void SetTransportationCostCategory(long? value)
    {
        CostCategoryId = value;
    }
    public void SetTransportationCostGroup(long? value)
    {
        CostGroupId = value;
    }
    public void SetStartingCityId(long? value)
    {
        StartingCityId = value;
    }
    public void SetDestinationCityId(long? value)
    {
        DestinationCityId = value;
    }
    public void SetStartingCityIdForSnap(long? value)
    {
        StartingCityId = value;
    }
    public void SetDestinationCityIdForSnap(long? value)
    {
        DestinationCityId = value;
    }
    public void SetStartDate(DateTime value)
    {
        StartDate = value;
    }
    public void SetEndDate(DateTime value)
    {
        EndDate = value;
    }
    public void SetImageLink(string? value)
    {
        ImageLink = value;
    }
    public void SetDriverId(long? value)
    {
        DriverId = value;
    }
    public void SetDriverName(string? value)
    {
        DriverName = value;
    }
    public void SetDescription(string? value)
    {
        Description = value;
    }
    public void SetPostageDate(DateTime? value)
    {
        PostageDate = value;
    }
    public void SetReceivedDate(DateTime? value)
    {
        ReceivedDate = value;
    }
    public void SetBillOfLadingImage(string? value)
    {
        BillOfLadingImage = value;
    }
    public void SetDelivererName(string? value)
    {
        DelivererName = value;
    }
    public void SetRecipientName(string? value)
    {
        RecipientName = value;
    }
    public void SetPhoneNumber(string? value)
    {
        PhoneNumber = value;
    }
    public void SetCarSpecifications(string? value)
    {
        CarSpecifications = value;
    }
    public void SetNumberPlates(string? value)
    {
        NumberPlates = value;
    }
    public void SetCertificateNumbers(string? value)
    {
        CertificateNumber = value;
    }
    public void SetAccountNumber(string? value)
    {
        AccountNumber = value;
    }
    public void SetBankId(long? value)
    {
        BankId = value;
    }
    public void SetCardNumber(string? value)
    {
        CardNumber = value;
    }
    public void SetAccountName(string? value)
    {
        AccountName = value;
    }
    public void SetIBAN(string? value)
    {
        IBAN = value;
    }
    public void SetPrice(decimal? value)
    {
        Price = value;
    }
    public void SetVolume(decimal? value)
    {
        Volume = value;
    }
    public void SetCurrencyUnitId(long? value)
    {
        CurrencyUnitId = value;
    }
    public void SetAccountDescription(string? value)
    {
        AccountDescription = value;
    }
    public void SetFreightNumber(string? value)
    {
        FreightNumber = value;
    }
    public void SetCarID(string? value)
    {
        CarID = value;
    }
    public void SetLoadWeight(decimal? value)
    {
        LoadWeight = value;
    }
    public void SetManagerDescription(string? value)
    {
        ManagerDescription = value;
    }
    public void SetConfirmUserId(long? value)
    {
        ConfirmUserId = value;
    }
    public void SetCompanyId(long? value)
    {
        CompanyId = value;
    }
    public void SetConfirmDate(DateTime? value)
    {
        ConfirmDate = value;
    }
    public void SetSnapRequester(long? value)
    {
        SnapRequester = value;
    }
    public void SetSecondDestinationCityId(long? value)
    {
        SecondDestinationCityId = value;
    }
    public void SetFareAmount(decimal? value)
    {
        Price = value;
    }
    public void SetStopRate(int? value)
    {
        StopRate = value;
    }
    public void SetDestinationAddress(string? value)
    {
        DestinationAddress = value;
    }
    public void SetSecondDestinationAddress(string? value)
    {
        SecondDestinationAddress = value;
    }
    public void SetPersonalPayment(bool? value)
    {
        PersonalPayment = value;
    }
    public void SetTransportationRequestStatus(TransportationRequestStatus value)
    {
        TransportationRequestStatus = value;
    }
    public void SetIsDeleted()
    {
        IsDeleted = true;
    }

    public void SetIsCredit(bool value)
    {
        IsCredit = value;
    }

    #endregion

    #region Methods 
    public void AddHistory()
    {
        _histories.Add(new TransportationRequestHistory(
            StartDate,
            EndDate,
            Description,
            DriverId,
            DriverName,
            AccountNumber,
            BankId,
            CardNumber,
            AccountName,
            IBAN,
            Price,
            CurrencyUnitId,
            AccountDescription,
            TransportationPaymentType,
            TransportationRequestStatus,
            ManagerDescription,
            ConfirmUserId,
            ConfirmDate,
            PaymentOrderId,
            PaymentDate,
            this));
    }

    public void AddTransportationRequestCostCenter(TransportationRequestCostCenter transportationRequestCostCenter)
    {
        ArgumentNullException.ThrowIfNull(transportationRequestCostCenter);

        _transportationRequestCostCenters.Add(transportationRequestCostCenter);
    }

    public void AddTransportationRequestProject(TransportationRequestProject transportationRequestProject)
    {
        ArgumentNullException.ThrowIfNull(transportationRequestProject);

        _transportationRequestProjects.Add(transportationRequestProject);
    }

    public void AddTransportationRequestProjectOperationDetail(TransportationRequestProjectOperationDetail transportationRequestProjectOperationDetail)
    {
        ArgumentNullException.ThrowIfNull(transportationRequestProjectOperationDetail);

        _transportationRequestProjectOperationDetail.Add(transportationRequestProjectOperationDetail);
    }

    public void AddTransportationRequestProjectOperation(TransportationRequestProjectOperation transportationRequestProjectOperationDetail)
    {
        ArgumentNullException.ThrowIfNull(transportationRequestProjectOperationDetail);

        _transportationRequestProjectOperation.Add(transportationRequestProjectOperationDetail);
    }
    public void AddTransportationRequestDocumentService(TransportationRequestDocument document)
    {
        ArgumentNullException.ThrowIfNull(document);
        _documents.Add(document);
    }

    public void AddTransportationCargoPallet(TransportationCargoPallet pallet)
    {
        ArgumentNullException.ThrowIfNull(pallet);
        _TransportationCargoPallets.Add(pallet);
    }

    public void AddTransportationCargoPallet(List<TransportationCargoPallet> pallets)
    {
        ArgumentNullException.ThrowIfNull(pallets);
        _TransportationCargoPallets.AddRange(pallets);
    }

    #endregion

    /// <summary>
    ///  For EF core, never thoch this
    /// </summary>
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    private List<TransportationRequestCostCenter> _transportationRequestCostCenters;
    public IReadOnlyList<TransportationRequestCostCenter> TransportationRequestCostCenters => _transportationRequestCostCenters;

    private List<TransportationRequestProject> _transportationRequestProjects;
    public IReadOnlyList<TransportationRequestProject> TransportationRequestProjects => _transportationRequestProjects;

    private List<TransportationRequestProjectOperation> _transportationRequestProjectOperation;
    public IReadOnlyList<TransportationRequestProjectOperation> TransportationRequestProjectOperations => _transportationRequestProjectOperation;

    private List<TransportationRequestProjectOperationDetail> _transportationRequestProjectOperationDetail;
    public IReadOnlyList<TransportationRequestProjectOperationDetail> TransportationRequestProjectOperationDetails => _transportationRequestProjectOperationDetail;

    private List<TransportationRequestDocument> _documents;
    public IReadOnlyList<TransportationRequestDocument> TransportationRequestDocuments => _documents;

    private List<TransportationRequestHistory> _histories;
    public IReadOnlyList<TransportationRequestHistory> TransportationRequestHistories => _histories;

    private List<TransportationCargoPallet> _TransportationCargoPallets;
    public IReadOnlyList<TransportationCargoPallet> TransportationCargoPallets => _TransportationCargoPallets;

    private List<TransportationRequestDetail> _transportationRequestDetails;
    public IReadOnlyList<TransportationRequestDetail> TransportationRequestDetails => _transportationRequestDetails;

    private TransportationRequest()
    {
        _transportationRequestProjects = [];
        _transportationRequestCostCenters = [];
        _transportationRequestProjectOperation = [];
        _transportationRequestProjectOperationDetail = [];
        _documents = [];
        _histories = [];
        _TransportationCargoPallets = [];
        _transportationRequestDetails = [];
    }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
}
