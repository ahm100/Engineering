namespace Engineering.Application.Services.OperationInfos.Queries.GetActiveOperationInfos;

public class GetActiveOperationInfosQueryValidator : AbstractValidator<GetActiveOperationInfosQuery>
{
    public GetActiveOperationInfosQueryValidator()
    {
        RuleFor(oo => oo.PageIndex).GreaterThanOrEqualTo(GlobalErrors.Zero).WithError(GlobalErrors.PageIndexNotValid).LessThanOrEqualTo(GlobalErrors.MaxIndex).WithError(GlobalErrors.PageIndexNotValid);
        RuleFor(oo => oo.PageSize).GreaterThanOrEqualTo(GlobalErrors.Zero).WithError(GlobalErrors.PageSizeNotValid).LessThanOrEqualTo(GlobalErrors.MaxSize).WithError(GlobalErrors.PageSizeNotValid);
    }
}