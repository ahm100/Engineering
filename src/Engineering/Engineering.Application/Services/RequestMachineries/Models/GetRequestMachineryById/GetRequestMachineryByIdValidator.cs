namespace Engineering.Application.Services.RequestMachineries.Models.GetRequestMachineryById;

public class GetRequestMachineryByIdValidator : AbstractValidator<GetRequestMachineryByIdRequest>
{
    public GetRequestMachineryByIdValidator()
    {
        RuleFor(oo => oo.RequestMachineryId).NotNull().WithError(RequestMachineryErrors.InValidMachinery);
    }
}
