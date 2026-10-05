using Engineering.Application.Services.CabinTypes.Models.CreateCabinType;

namespace Engineering.Application.Services.CabinTypes.Commands.CreateCabinType;

public record CreateCabinTypeCommand(
    string CabinTypeName,
    int CabinTypeCode,
    bool IsActive,
    long? CompanyId)
    : ICommand<CreateCabinTypeResponse>;