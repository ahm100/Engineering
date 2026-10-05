namespace Engineering.Application.Services.Machineries.Models.GetMachineries;

public record GetMachineriesRequest(
    string? FilterData,
    long? MachineriesGroupId,
    string? MachineryName,
    string? MachineryCode,
    bool? IsActive,
    string[]? OrderBy,
    int PageIndex,
    int PageSize
     ) : IHttpRequest;
