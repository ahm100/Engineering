namespace Engineering.Application.WebServices.MetaDataServices.ContractorEmployees.Queries.GetById;

public class ContractorEmployeesByIdQueryValidator : AbstractValidator<ContractorEmployeesByIdQuery>
{
    public ContractorEmployeesByIdQueryValidator()
    {
        RuleFor(oo => oo.ContractorId).NotNull().GreaterThanOrEqualTo(1)
            .WithError(MetaDataErrors.IdIsEmpty);
        RuleFor(oo => oo.PageIndex).GreaterThanOrEqualTo(GlobalErrors.Zero).WithError(GlobalErrors.PageIndexNotValid).LessThanOrEqualTo(GlobalErrors.MaxIndex).WithError(GlobalErrors.PageIndexNotValid);
        RuleFor(oo => oo.PageSize).GreaterThanOrEqualTo(GlobalErrors.Zero).WithError(GlobalErrors.PageSizeNotValid).LessThanOrEqualTo(GlobalErrors.MaxSize).WithError(GlobalErrors.PageSizeNotValid);
    }
}
