using Engineering.Domain.Entities.RequestContractors.Enums;

namespace Engineering.Application.Services.RequestContractorInquiries.Models.CreateRequestContractorInquiry;

public record CreateRequestContractorInquiryRequestModel(
    long ContractorId,
    RequestContractorType Type,
    long CurrencyId,
    decimal Amount,
    decimal? Discount,
    decimal? Tax,
    DateTime? FromDate,
    DateTime? ToDate,
    string? Description,
    List<string>? Documents);
