using Engineering.Domain.Entities.RequestMachineries;

namespace Engineering.Application.Services.RequestMachineries.Commands.UpdateRequestMachineryOperatorAppoinment;

public record UpdateRequestMachineryOperatorAppoinmentCommand(long RequestMachineryId,
                                                              long OperatorAppoinmentId,
                                                              long OperatorAppoinmentUserId) : ICommand<RequestMachinery>;
