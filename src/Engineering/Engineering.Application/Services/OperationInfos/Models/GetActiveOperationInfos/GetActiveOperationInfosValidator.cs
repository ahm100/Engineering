namespace Engineering.Application.Services.OperationInfos.Models.GetActiveOperationInfos;

public class GetActiveOperationInfosValidator : AbstractValidator<GetActiveOperationInfosRequest>
{
    public GetActiveOperationInfosValidator()
    {
        RuleFor(oo => oo.PageIndex).GreaterThanOrEqualTo(GlobalErrors.Zero).WithError(GlobalErrors.PageIndexNotValid).LessThanOrEqualTo(GlobalErrors.MaxIndex).WithError(GlobalErrors.PageIndexNotValid);
        RuleFor(oo => oo.PageSize).GreaterThanOrEqualTo(GlobalErrors.Zero).WithError(GlobalErrors.PageSizeNotValid).LessThanOrEqualTo(GlobalErrors.MaxSize).WithError(GlobalErrors.PageSizeNotValid);
    }
}
