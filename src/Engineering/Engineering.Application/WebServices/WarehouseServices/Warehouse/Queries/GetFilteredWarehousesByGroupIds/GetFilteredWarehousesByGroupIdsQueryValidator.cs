namespace Engineering.Application.WebServices.WarehouseServices.Groups.Queries.GetFilteredWarehousesByGroupIds;

public class GetFilteredWarehousesByGroupIdsQueryValidator : AbstractValidator<GetFilteredWarehousesByGroupIdsQuery>
{
    public GetFilteredWarehousesByGroupIdsQueryValidator()
    {
        RuleFor(oo => oo.WarehouseIds).NotEmpty().WithError(MetaDataErrors.IdIsEmpty);
        RuleFor(oo => oo.GroupIds).NotEmpty().WithError(MetaDataErrors.IdIsEmpty);
        RuleFor(oo => oo.PageIndex).GreaterThanOrEqualTo(GlobalErrors.Zero).WithError(GlobalErrors.PageIndexNotValid).LessThanOrEqualTo(GlobalErrors.MaxIndex).WithError(GlobalErrors.PageIndexNotValid);
        RuleFor(oo => oo.PageSize).GreaterThanOrEqualTo(GlobalErrors.Zero).WithError(GlobalErrors.PageSizeNotValid).LessThanOrEqualTo(GlobalErrors.MaxSize).WithError(GlobalErrors.PageSizeNotValid);
    }
}