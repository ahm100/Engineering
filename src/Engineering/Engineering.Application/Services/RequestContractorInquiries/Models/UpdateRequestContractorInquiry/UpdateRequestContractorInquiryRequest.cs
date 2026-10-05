using Engineering.Domain.Entities.RequestContractors.Enums;

namespace Engineering.Application.Services.RequestContractorInquiries.Models.UpdateRequestContractorInquiry;

public record UpdateRequestContractorInquiryRequest(
    long Id,
    long ContractorId,
    RequestContractorType Type,
    long CurrencyId,
    decimal Amount,
    decimal? Discount,
    decimal? Tax,
    DateTime? FromDate,
    DateTime? ToDate,
    string? Description,
    List<string>? Documents) : IHttpRequest;
