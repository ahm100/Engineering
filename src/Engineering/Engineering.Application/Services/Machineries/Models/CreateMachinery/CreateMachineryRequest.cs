namespace Engineering.Application.Services.Machineries.Models.CreateMachinery;

public record CreateMachineryRequest(
    long MachineriesGroupId,
    string MachineryCode,
    string MachineryName,
    bool IsActive
     ) : IHttpRequest;
