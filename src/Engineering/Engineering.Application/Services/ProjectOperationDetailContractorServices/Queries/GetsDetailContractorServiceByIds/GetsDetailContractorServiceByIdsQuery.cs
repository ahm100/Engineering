using Engineering.Domain.Entities.ProjectOperationDetails;

namespace Engineering.Application.Services.DetailContractorServices.Queries.GetsDetailContractorServiceByIds;

public record GetsDetailContractorServiceByIdsQuery(
    List<long> Items
    ) : IQuery<List<ProjectOperationDetailContractorService>>;
