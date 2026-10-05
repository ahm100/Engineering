namespace Engineering.Application.Services.RequestMachineryManagements.Models.UpdateRequestMachineryAssignments;

public record UpdateRequestMachineryAssignmentsModel(long? Id,
                                                     string MachineryIdentifier,
                                                     bool IsDeleted);