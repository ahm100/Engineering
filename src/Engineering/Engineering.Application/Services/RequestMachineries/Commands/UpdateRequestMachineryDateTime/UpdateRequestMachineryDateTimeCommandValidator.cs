namespace Engineering.Application.Services.RequestMachineries.Commands.UpdateRequestMachineryDateTime;

public class UpdateRequestMachineryDateTimeCommandValidator : AbstractValidator<UpdateRequestMachineryDateTimeCommand>
{
    public UpdateRequestMachineryDateTimeCommandValidator()
    {
        RuleFor(oo => oo.RequestMachineryId).NotNull().NotEmpty().WithError(RequestMachineryErrors.InValidRequestMachinery)
            .GreaterThanOrEqualTo(1).WithError(GlobalErrors.IdMustGreaterZiro);

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
