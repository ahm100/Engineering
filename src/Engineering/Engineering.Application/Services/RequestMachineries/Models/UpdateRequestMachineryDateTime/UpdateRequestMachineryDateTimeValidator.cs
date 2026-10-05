namespace Engineering.Application.Services.RequestMachineries.Models.UpdateRequestMachineryDateTime;

public class UpdateRequestMachineryDateTimeValidator : AbstractValidator<UpdateRequestMachineryDateTimeRequest>
{
    public UpdateRequestMachineryDateTimeValidator()
    {
        RuleFor(oo => oo.RequestMachineryId).NotNull().NotEmpty().WithError(RequestMachineryErrors.InValidRequestMachinery)
            .GreaterThanOrEqualTo(1).WithError(GlobalErrors.IdMustGreaterZiro);

        RuleFor(x => x.FromDate)
            .NotEmpty().WithMessage(RequestMachineryErrors.InValidFromDate)
            .NotNull().WithMessage(RequestMachineryErrors.InValidFromDate);

        RuleFor(x => x.ToDate)
            .NotEmpty().WithMessage(RequestMachineryErrors.InValidToDate)
            .NotNull().WithMessage(RequestMachineryErrors.InValidToDate);

        RuleFor(x => x.FromTime)
            .NotEmpty().WithMessage(RequestMachineryErrors.InValidFromTime)
            .NotNull().WithMessage(RequestMachineryErrors.InValidFromTime)
            .LessThan(x => x.ToTime).WithMessage(RequestMachineryErrors.MustBeforeToTime);

        RuleFor(x => x.ToTime)
            .NotEmpty().WithMessage(RequestMachineryErrors.InValidToTime)
            .NotNull().WithMessage(RequestMachineryErrors.InValidToTime)
            .GreaterThan(x => x.FromTime).WithMessage(RequestMachineryErrors.MustBeforeToTime);
    }
}
