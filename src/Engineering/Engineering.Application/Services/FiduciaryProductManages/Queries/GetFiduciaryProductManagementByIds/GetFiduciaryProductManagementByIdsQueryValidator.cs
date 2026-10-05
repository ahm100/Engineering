namespace Engineering.Application.Services.FiduciaryProductManages.Queries.GetFiduciaryProductManagementById;

public class GetFiduciaryProductManagementByIdsQueryValidator : AbstractValidator<GetFiduciaryProductManagementByIdsQuery>
{
    public GetFiduciaryProductManagementByIdsQueryValidator()
    {
        RuleFor(oo => oo.Ids).NotNull().WithError(FiduciaryProductErrors.InValidFiduciaryProductId);
    }
}
