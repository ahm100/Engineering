using Engineering.Domain.Entities.RequestMachineries;
using Engineering.Domain.Entities.RequestMachineries.Enums;

namespace Engineering.Application.Services.RequestMachineryManagements.Commands.CreateRequestMachineryInquiry;

public record CreateRequestMachineryInquiryCommand(long ThirdPartyId,
                                                   long CurrencyId,
                                                   int Count,
                                                   RequestMachineryUnit Unit,
                                                   decimal UnitPrice,
                                                   decimal TotalPrice,
                                                   string? Description,
                                                   decimal InquiryRequestedTime,
                                                   RequestMachineryInquiryOperator RequestMachineryInquiryOperator) : ICommand<RequestMachineryInquiry>;
