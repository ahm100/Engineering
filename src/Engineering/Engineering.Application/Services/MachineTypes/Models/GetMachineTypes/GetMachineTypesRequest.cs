
namespace Engineering.Application.Services.MachineTypes.Models.GetMachineTypes;

public record GetMachineTypesRequest(
    string? FilterData,
    bool? IsActive,
    string[]? OrderBy,
    int PageIndex,
    int PageSize
     ) : IHttpRequest;

