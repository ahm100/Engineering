using Engineering.Domain.Entities.RequestMachineries;

namespace Engineering.Application.Services.RequestMachineryManagements.Commands.CreateRequestMachineryInquiryOperator;

public record CreateRequestMachineryInquiryOperatorCommand(long OperatorAssinmentId,
                                                           long OperatorAssinmentUserId,
                                                           RequestMachinery RequestMachinery) : ICommand<RequestMachineryInquiryOperator>;
