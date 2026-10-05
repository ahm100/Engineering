using Engineering.Domain.Entities.OperationInfos;

namespace Engineering.Application.Services.PublicGroups.Queries.GetById;

public record GetPublicGroupByIdQuery(
    long Id
    ) : IQuery<PublicGroup>;
