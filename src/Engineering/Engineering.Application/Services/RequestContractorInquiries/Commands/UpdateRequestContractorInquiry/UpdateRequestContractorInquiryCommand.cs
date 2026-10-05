using Engineering.Domain.Entities.RequestContractors;
using Engineering.Domain.Entities.RequestContractors.Enums;

namespace Engineering.Application.Services.RequestContractorInquiries.Commands.UpdateRequestContractorInquiry;

public record UpdateRequestContractorInquiryCommand(
    RequestContractorInquiry RequestContractorInquiry,
    long ContractorId,
    RequestContractorType Type,
    long CurrencyId,
    decimal TotalAmount,
    decimal Amount,
    decimal? Discount,
    decimal? Tax,
    DateTime? FromDate,
    DateTime? ToDate,
    string? Description) : ICommand<RequestContractorInquiry>;
