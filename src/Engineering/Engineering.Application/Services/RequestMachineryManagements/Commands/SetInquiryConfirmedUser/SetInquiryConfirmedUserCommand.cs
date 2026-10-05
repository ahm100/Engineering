using Engineering.Domain.Entities.RequestMachineries;

namespace Engineering.Application.Services.RequestMachineryManagements.Commands.SetInquiryConfirmedUser;

public record SetInquiryConfirmedUserCommand(RequestMachineryInquiry RequestMachineryInquiry,
                                             long ConfirmedUser) : ICommand<RequestMachineryInquiry>;
