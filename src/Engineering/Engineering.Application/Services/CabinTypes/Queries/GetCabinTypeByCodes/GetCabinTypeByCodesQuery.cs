using Engineering.Domain.Entities.MachineTypes;

namespace Engineering.Application.Services.CabinTypes.Queries.GetCabinTypeByCodes;

public record GetCabinTypeByCodesQuery(
    List<int> CabinTypeCodes,
    long? CompanyId)
    : IQuery<List<CabinType>?>;