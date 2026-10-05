using Engineering.Domain.Entities.RequestContractors;

namespace Engineering.Application.Services.RequestContractors.Queries.GetRequestContractorById;

public record GetRequestContractorByIdQuery(long RequestContractorId) : IQuery<RequestContractor>;

