using Engineering.Domain.Entities.ProjectOperations;
using Engineering.Domain.Entities.RequestMachineries;

namespace Engineering.Application.Services.RequestMachineries.Commands.CreateRequestMachineryProjectOperation;

public record CreateRequestMachineryProjectOperationCommand(long? Id,
                                                            ProjectOperation ProjectOperation,
                                                            RequestMachinery RequestMachinery,
                                                            bool IsDeleted) : ICommand<RequestMachineryProjectOperation>;