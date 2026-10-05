namespace Engineering.Application.Services.RequestMachineryManagements.Models.UpdateRequestMachineryAssignments;

public record UpdateRequestMachineryAssignmentsRequest(long RequestMachineryId,
                                                       List<UpdateRequestMachineryAssignmentsModel>? MachineryIdentifiers) : IHttpRequest;
