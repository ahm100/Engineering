namespace Engineering.Application.Services.RequestMachineryManagements.Models.SetRequestMachineryOnProject;

public record SetRequestMachineryOnProjectRequest(long RequestMachineryId,
                                                  string? Description,
                                                  List<SetRequestMachineryMachineryAppoinmentMachinery>? MachineryIdentifiers) : IHttpRequest;
