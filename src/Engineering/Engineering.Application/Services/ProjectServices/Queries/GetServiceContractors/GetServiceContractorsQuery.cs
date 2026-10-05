namespace Engineering.Application.Services.ProjectServices.Queries.GetServiceContractors;

public record GetServiceContractorsQuery(
        long? ProjectId,
        List<long> ServiceInfoIds
    ) : IQuery<List<long>?>;