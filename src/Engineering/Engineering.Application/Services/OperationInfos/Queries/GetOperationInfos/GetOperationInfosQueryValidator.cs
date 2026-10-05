namespace Engineering.Application.Services.OperationInfos.Queries.GetOperationInfos;

public class GetOperationInfosQueryValidator : AbstractValidator<GetOperationInfosQuery>
{
    public GetOperationInfosQueryValidator()
    {
        RuleForEach(oo => oo.Ids)
            .IsPositive(GlobalCmts.Id);
    }
}