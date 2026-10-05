using Engineering.Domain.Entities.RequestMachineries;

namespace Engineering.Application.Services.RequestMachineryManagements.Commands.ConfirmRequestMachineryInquiry;

public record ConfirmRequestMachineryInquiryCommand(long RequestMachineryId,
                                                    long RequestMachineryInquiryId,
                                                    long ConfirmedUser) : ICommand<RequestMachineryInquiry>;
