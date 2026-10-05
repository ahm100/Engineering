using Engineering.Domain.Entities.RequestMachineries;

namespace Engineering.Application.Services.RequestMachineryManagements.Commands.ActiveRequestMachineryInquiryOperator;

public record ActiveRequestMachineryInquiryOperatorCommand(long RequestMachineryId,
                                                           long OperatorId) : ICommand<RequestMachineryInquiryOperator>;
