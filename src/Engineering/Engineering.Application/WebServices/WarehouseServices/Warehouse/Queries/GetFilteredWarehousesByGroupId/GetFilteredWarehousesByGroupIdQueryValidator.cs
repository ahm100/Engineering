namespace Engineering.Application.WebServices.WarehouseServices.Groups.Queries.GetFilteredWarehousesByGroupId;

public class GetFilteredWarehousesByGroupIdQueryValidator : AbstractValidator<GetFilteredWarehousesByGroupIdQuery>
{
    public GetFilteredWarehousesByGroupIdQueryValidator()
    {
        RuleFor(oo => oo.WarehouseIds).NotEmpty().WithError(MetaDataErrors.IdIsEmpty);
        RuleFor(oo => oo.PageIndex).GreaterThanOrEqualTo(GlobalErrors.Zero).WithError(GlobalErrors.PageIndexNotValid).LessThanOrEqualTo(GlobalErrors.MaxIndex).WithError(GlobalErrors.PageIndexNotValid);
        RuleFor(oo => oo.PageSize).GreaterThanOrEqualTo(GlobalErrors.Zero).WithError(GlobalErrors.PageSizeNotValid).LessThanOrEqualTo(GlobalErrors.MaxSize).WithError(GlobalErrors.PageSizeNotValid);
    }
}