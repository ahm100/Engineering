namespace Engineering.Application.WebServices.Treasury.PaymentOrders.Models.CreatePaymentOrderWithAutoDetail;

public record CreatePaymentOrderWithAutoDetailRequest(
    long CurrencyId,
    Guid? ThirdPartyGuid,
    string PaymentOrderTypeCode,
    long? ReferenceId,
    string? ReferenceNo,
    string? ReferenceDetails,
    Guid? CostCenterGuid,
    Guid? ProjectGuid,
    DateTime IssueDate,
    DateTime PaymentDate,
    decimal PrincipalAmount,
    decimal PackagingCost,
    decimal ShippingCost,
    decimal Discount,
    decimal ValueAddedTax,
    decimal Amount,
    string Description,
    long? BankAccountId,
    List<PaymentOrderAttachmentViaSubSystemRequest> Attachments,
    List<CreatePaymentOrderViaSubSystemCostDetailRequest> CostDetails,
    bool IsPettyCash,
    Guid? PettyCashGuid,
    long? CostCategoryId,
    long? CostGroupId,
    long? DocumentTypeId,
    long? PreferentialTypeId
    );

public record PaymentOrderAttachmentViaSubSystemRequest(
    string Code,
    string Title,
    string Url,
    int Type
    );

public record CreatePaymentOrderViaSubSystemCostDetailRequest(
    Guid CostCenterGuid,
    Guid? ProjectGuid,
    decimal Amount,
    Guid? CategoryGuid,
    Guid? BranchGuid,
    Guid? SeasonGuid,
    Guid? ProductGuid,
    decimal? RequestCount,
    decimal? TaxPrice,
    decimal? Discount,
    decimal? OriginalPrice,
    decimal? Price,
    decimal? OriginalUnitPrice,
    string? ServiceTitle,
    Guid? ThirdPartyGuid,
    long? CostCategoryId,
    long? CostGroupId,
    long? ReferenceId,
    long? DocumentTypeId,
    long? PreferentialTypeId
    );