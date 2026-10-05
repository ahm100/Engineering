using Engineering.Domain.Entities.RequestMachineries.Enums;

namespace Engineering.Application.Services.RequestMachineryManagements.Models.GetRequestMachineryInquiries;

public record GetRequestMachineryInquiriesResponseModel
{
    public long RequestMachineryInquiryId { get; set; }
    public long ThirdPartyId { get; set; }
    public string? ThirdParty { get; set; } = string.Empty;
    public int Count { get; set; }
    public RequestMachineryUnit Unit { get; set; }
    public string UnitDescription => Unit.GetEnumDescription();
    public decimal UnitPrice { get; set; }
    public decimal TotalPrice { get; set; }
    public long CurrencyId { get; set; }
    public string? CurrencyName { get; set; } = string.Empty;
    public string? Description { get; set; } = string.Empty;
    public long? OperatorId { get; set; }
    public long? OperatorUserId { get; set; }
    public string? OperatorName { get; set; } = string.Empty;
    public decimal InquiryRequestedTime { get; set; }
    public DateTime Created { get; set; }
    public List<GetRequestMachineryInquiryDetailByIdDocumentModelResponse> Documents { get; set; } = new();
}
