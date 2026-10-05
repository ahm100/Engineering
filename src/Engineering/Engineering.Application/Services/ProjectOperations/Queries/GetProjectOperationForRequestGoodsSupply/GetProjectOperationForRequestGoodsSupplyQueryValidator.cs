namespace Engineering.Application.Services.ProjectOperations.Queries.GetProjectOperationForRequestGoodsSupply;

public class GetProjectOperationForRequestGoodsSupplyQueryValidator : AbstractValidator<GetProjectOperationForRequestGoodsSupplyQuery>
{
    public GetProjectOperationForRequestGoodsSupplyQueryValidator()
    {
        RuleFor(oo => oo.Id).NotNull().WithError(ProjectOperationErrors.IdIsEmpty);
    }
}
