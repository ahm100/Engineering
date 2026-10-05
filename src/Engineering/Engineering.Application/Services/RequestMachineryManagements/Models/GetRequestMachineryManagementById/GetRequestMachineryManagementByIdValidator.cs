namespace Engineering.Application.Services.RequestMachineryManagements.Models.GetRequestMachineryManagementById;

public class GetRequestMachineryManagementByIdValidator : AbstractValidator<GetRequestMachineryManagementByIdRequest>
{
    public GetRequestMachineryManagementByIdValidator()
    {
        RuleFor(oo => oo.RequestMachineryId).NotNull().WithError(RequestMachineryErrors.InValidMachinery);
    }
}
