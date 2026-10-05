namespace Engineering.Domain.Entities.Transportations;

[Description(TransportationCmts.TransportationRequestDetail)]
public class TransportationRequestDetail : AuditableEntity<TransportationRequestDetail>
{
    [Description(TransportationCmts.GlobalFreightNumber)]
    public string? GlobalFreightNumber { get; private set; }

    [Description(TransportationCmts.ClassifiedFreightNumber)]
    public string? ClassifiedFreightNumber { get; private set; }

    [Description(TransportationCmts.Tax)]
    public decimal? Tax { get; private set; }

    [Description(TransportationCmts.TransferPrice)]
    public decimal? TransferPrice { get; private set; }

    [Description(TransportationCmts.ServicePrice)]
    public decimal? ServicePrice { get; private set; }

    [Description(TransportationCmts.InsuranceNumber)]
    public string? InsuranceNumber { get; private set; }

    [Description(TransportationCmts.InsurancePrice)]
    public decimal? InsurancePrice { get; private set; }

    [Description(TransportationCmts.ShippingCost)]
    public decimal? ShippingCost { get; private set; }

    [Description(TransportationCmts.ProductTotalPrice)]
    public decimal? ProductTotalPrice { get; private set; }

    [Description(TransportationCmts.OutofRange)]
    public decimal? OutofRange { get; private set; }

    [Description(TransportationCmts.OrderNumber)]
    public string? OrderNumber { get; private set; }

    [Description(TransportationCmts.TransportationRequest)]
    public long TransportationRequestId { get; set; }
    public TransportationRequest TransportationRequest { get; set; }

    public TransportationRequestDetail(
        string? globalFreightNumber,
        string? classifiedFreightNumber,
        decimal? tax,
        decimal? transferPrice,
        decimal? servicePrice,
        string? insuranceNumber,
        decimal? insurancePrice,
        decimal? shippingCost,
        decimal? productTotalPrice,
        decimal? outofRange,
        string? orderNumber,
        TransportationRequest transportationRequest) : this()
    {
        SetGlobalFreightNumber(globalFreightNumber);
        SetClassifiedFreightNumber(classifiedFreightNumber);
        SetTax(tax);
        SetTransferPrice(transferPrice);
        SetServicePrice(servicePrice);
        SetInsuranceNumber(insuranceNumber);
        SetInsurancePrice(insurancePrice);
        SetShippingCost(shippingCost);
        SetProductTotalPrice(productTotalPrice);
        SetOutofRange(outofRange);
        SetOrderNumber(orderNumber);
        SetTransportationRequest(transportationRequest);
    }

    public void Update(
    string? globalFreightNumber,
    string? classifiedFreightNumber,
    decimal? tax,
    decimal? transferPrice,
    decimal? servicePrice,
    string? insuranceNumber,
    decimal? insurancePrice,
    decimal? shippingCost,
    decimal? productTotalPrice,
    decimal? outofRange,
    string? orderNumber,
    TransportationRequest transportationRequest
    )
    {
        GlobalFreightNumber = globalFreightNumber;
        ClassifiedFreightNumber = classifiedFreightNumber;
        Tax = tax;
        TransferPrice = transferPrice;
        ServicePrice = servicePrice;
        InsuranceNumber = insuranceNumber;
        InsurancePrice = insurancePrice;
        ShippingCost = shippingCost;
        ProductTotalPrice = productTotalPrice;
        OutofRange = outofRange;
        OrderNumber = orderNumber;
        TransportationRequest = transportationRequest;
        TransportationRequestId = transportationRequest.Id;
    }

    public void SetGlobalFreightNumber(string? globalFreightNumber)
    {
        GlobalFreightNumber = globalFreightNumber;
    }

    public void SetClassifiedFreightNumber(string? classifiedFreightNumber)
    {
        ClassifiedFreightNumber = classifiedFreightNumber;
    }

    public void SetTax(decimal? tax)
    {
        Tax = tax;
    }

    public void SetTransferPrice(decimal? transferPrice)
    {
        TransferPrice = transferPrice;
    }

    public void SetServicePrice(decimal? servicePrice)
    {
        ServicePrice = servicePrice;
    }

    public void SetInsuranceNumber(string? insuranceNumber)
    {
        InsuranceNumber = insuranceNumber;
    }

    public void SetInsurancePrice(decimal? insurancePrice)
    {
        InsurancePrice = insurancePrice;
    }

    public void SetShippingCost(decimal? shippingCost)
    {
        ShippingCost = shippingCost;
    }

    public void SetProductTotalPrice(decimal? productTotalPrice)
    {
        ProductTotalPrice = productTotalPrice;
    }

    public void SetOutofRange(decimal? outofRange)
    {
        OutofRange = outofRange;
    }

    public void SetOrderNumber(string? orderNumber)
    {
        OrderNumber = orderNumber;
    }

    public void SetTransportationRequest(TransportationRequest transportationRequest)
    {
        TransportationRequest = Guard.Against.Null(transportationRequest, nameof(transportationRequest));
        TransportationRequestId = Guard.Against.Null(transportationRequest.Id, nameof(transportationRequest.Id));
    }

    /// <summary>
    ///  For EF core, never touch this
    /// </summary>
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private TransportationRequestDetail()
    {
    }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
}
