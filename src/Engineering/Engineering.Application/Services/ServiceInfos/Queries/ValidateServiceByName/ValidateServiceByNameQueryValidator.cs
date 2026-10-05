namespace Engineering.Application.Services.ServiceInfos.Queries.ValidateServiceByName;

public class ValidateServiceByNameQueryValidator : AbstractValidator<ValidateServiceByNameQuery>
{
    public ValidateServiceByNameQueryValidator()
    {
        RuleFor(oo => oo.ServiceInfoName).NotEmpty().WithError(ServiceInfoErrors.ServiceInfoCodeIsEmpty);
        RuleFor(oo => oo.MeasurementId).NotNull().WithError(ServiceInfoErrors.UnitOfMeasurementIdIsEmpty);
    }
}
