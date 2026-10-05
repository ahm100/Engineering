namespace Engineering.Application.Services.Machineries.Models.UpdateMachinery;

public record UpdateMachineryRequest(
    long Id,
    long MachineriesGroupId,
    string MachineryName,
    string MachineryCode,
    bool IsActive
     ) : IHttpRequest;
