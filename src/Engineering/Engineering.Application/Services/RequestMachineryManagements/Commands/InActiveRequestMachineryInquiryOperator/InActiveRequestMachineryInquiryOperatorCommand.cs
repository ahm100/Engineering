using Engineering.Domain.Entities.RequestMachineries;

namespace Engineering.Application.Services.RequestMachineryManagements.Commands.InActiveRequestMachineryInquiryOperator;

public record InActiveRequestMachineryInquiryOperatorCommand(long RequestMachineryId,
                                                             long OperatorId) : ICommand<RequestMachineryInquiryOperator>;
