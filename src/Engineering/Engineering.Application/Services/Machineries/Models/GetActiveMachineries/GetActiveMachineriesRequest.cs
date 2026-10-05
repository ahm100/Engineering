namespace Engineering.Application.Services.Machineries.Models.GetActiveMachineries;

public record GetActiveMachineriesRequest(
    string? FilterData,
    long? MachineriesGroupId,
    string? MachineryCode,
    string? MachineryName,
    int PageIndex,
    int PageSize
     ) : IHttpRequest;
