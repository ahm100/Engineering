using Engineering.Domain.Entities.RequestMachineries;
using Engineering.Domain.Entities.RequestMachineries.Enums;

namespace Engineering.Application.Services.RequestMachineryManagements.Commands.UpdateRequestMachineryInquiry;

public record UpdateRequestMachineryInquiryCommand(long ThirdPartyId,
                                                   int Count,
                                                   RequestMachineryUnit Unit,
                                                   decimal UnitPrice,
                                                   decimal TotalPrice,
                                                   long CurrencyId,
                                                   string? Description,
                                                   decimal InquiryRequestedTime,
                                                   long RequestMachineryInquiryId) : ICommand<RequestMachineryInquiry>;
