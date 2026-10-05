
namespace Engineering.Application.Services.MachineTypes.Models.MachineTypeGroupDelete;

public record MachineTypeGroupDeleteRequest(
    List<long> Ids
    ) : IHttpRequest;
