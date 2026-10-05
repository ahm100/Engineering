namespace Engineering.Application.Services.Projects.Models.GetContractorProjects;

public class GetContractorProjectsValidator : AbstractValidator<GetContractorProjectsRequest>
{
    public GetContractorProjectsValidator()
    {
        RuleFor(oo => oo.ContractorId).NotNull().WithError(GlobalErrors.IdsIsNull)
            .GreaterThanOrEqualTo(GlobalErrors.One).WithError(GlobalErrors.IdMustGreaterZiro);
        RuleFor(oo => oo.PageIndex).GreaterThanOrEqualTo(GlobalErrors.Zero).WithError(GlobalErrors.PageIndexNotValid)
            .LessThanOrEqualTo(GlobalErrors.MaxIndex).WithError(GlobalErrors.PageIndexNotValid);
        RuleFor(oo => oo.PageSize).GreaterThanOrEqualTo(GlobalErrors.Zero).WithError(GlobalErrors.PageSizeNotValid)
            .LessThanOrEqualTo(GlobalErrors.MaxSize).WithError(GlobalErrors.PageSizeNotValid);
        When(oo => oo.PageSize > 0, () =>
        {
            RuleFor(oo => oo.PageIndex).GreaterThanOrEqualTo(GlobalErrors.One).WithError(GlobalErrors.PageIndexRequired)
                .LessThanOrEqualTo(GlobalErrors.MaxIndex).WithError(GlobalErrors.PageIndexRequired);
        });
    }
}
