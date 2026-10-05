using Engineering.Domain.Entities.RequestMachineries;

namespace Engineering.Application.Services.RequestMachineries.Queries.GetRequestMachineryByIdIncludeLess;

public record GetRequestMachineryByIdIncludeLessQuery(long RequestMachineryId) : IQuery<RequestMachinery>;

