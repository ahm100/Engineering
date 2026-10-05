namespace Engineering.Application.Services.RequestMachineryManagements.Queries.GetRequestMachineryOperators;

public record GetRequestMachineryOperatorsQuery(long RequestMachineryId, long MachineryId, long MachineryGroupId) : IQuery<List<GetRequestMachineryOperatorsQueryModel>>;
