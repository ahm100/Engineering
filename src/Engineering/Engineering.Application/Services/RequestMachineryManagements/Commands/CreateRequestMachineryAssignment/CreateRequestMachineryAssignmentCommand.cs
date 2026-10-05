using Engineering.Domain.Entities.RequestMachineries;

namespace Engineering.Application.Services.RequestMachineries.Commands.RequestMachineryAssignments;

public record CreateRequestMachineryAssignmentCommand(long? Id,
                                                      string MachineryIdentifier,
                                                      RequestMachinery RequestMachinery,
                                                      bool IsDeleted) : ICommand<RequestMachineryAssignment>;
