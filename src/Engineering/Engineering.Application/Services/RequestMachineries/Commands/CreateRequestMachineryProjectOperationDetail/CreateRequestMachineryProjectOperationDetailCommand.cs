using Engineering.Domain.Entities.ProjectOperationDetails;
using Engineering.Domain.Entities.RequestMachineries;

namespace Engineering.Application.Services.RequestMachineries.Commands.CreateRequestMachineryProjectOperationDetail;

public record CreateRequestMachineryProjectOperationDetailCommand(long? Id,
                                                                  ProjectOperationDetail ProjectOperationDetail,
                                                                  RequestMachinery RequestMachinery,
                                                                  bool IsDeleted) : ICommand<RequestMachineryProjectOperationDetail>;
