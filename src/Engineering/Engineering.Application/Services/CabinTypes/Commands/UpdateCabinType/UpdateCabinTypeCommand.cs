using Engineering.Application.Services.CabinTypes.Models.UpdateCabinType;

namespace Engineering.Application.Services.CabinTypes.Commands.UpdateCabinType;

public record UpdateCabinTypeCommand(
    long Id,
    string CabinTypeName,
    bool IsActive,
    long? CompanyId)
    : ICommand<UpdateCabinTypeResponse>;