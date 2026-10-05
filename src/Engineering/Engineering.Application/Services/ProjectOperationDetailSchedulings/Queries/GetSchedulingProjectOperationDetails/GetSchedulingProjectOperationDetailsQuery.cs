using Engineering.Domain.Entities.ProjectOperationDetails;

namespace Engineering.Application.Services.ProjectOperationDetailSchedulings.Queries.GetSchedulingProjectOperationDetails;

public record GetSchedulingProjectOperationDetailsQuery(long CostCenterId,
                                                        long ProjectId,
                                                        long ProjectOperationId) : IQuery<List<ProjectOperationDetail>>;
