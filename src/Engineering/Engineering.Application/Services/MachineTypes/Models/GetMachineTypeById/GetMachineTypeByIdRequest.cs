
namespace Engineering.Application.Services.MachineTypes.Models.GetMachineTypeById;

public record GetMachineTypeByIdRequest(
    long Id
     ) : IHttpRequest;
