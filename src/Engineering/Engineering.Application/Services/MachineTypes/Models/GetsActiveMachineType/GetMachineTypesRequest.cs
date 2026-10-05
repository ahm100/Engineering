
namespace Engineering.Application.Services.MachineTypes.Models.GetsActiveMachineType;

public record GetsActiveMachineTypeRequest(
    string? FilterData,
    int PageIndex,
    int PageSize
     ) : IHttpRequest;

