namespace Engineering.Application.Services.OperationInfos.Queries.GetOperationInfoByIdByMachineries;

public class GetOperationInfoByIdByMachineriesQueryValidator : AbstractValidator<GetOperationInfoByIdByMachineriesQuery>
{
    public GetOperationInfoByIdByMachineriesQueryValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1).WithError(OperationInfoErrors.IdIsEmpty);
    }
}
