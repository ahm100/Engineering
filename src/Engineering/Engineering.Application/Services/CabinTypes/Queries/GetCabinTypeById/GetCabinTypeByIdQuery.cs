using Engineering.Application.Services.CabinTypes.Models.GetCabinTypeById;

namespace Engineering.Application.Services.CabinTypes.Queries.GetCabinTypeById;

public record GetCabinTypeByIdQuery(
    long Id)
    : IQuery<GetCabinTypeByIdResponse>;