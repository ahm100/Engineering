namespace Engineering.Application.Services.RequestMachineryManagements.Models.UpdateRequestMachineryAssignments;

public class UpdateRequestMachineryAssignmentsValidator : AbstractValidator<UpdateRequestMachineryAssignmentsRequest>
{
    public UpdateRequestMachineryAssignmentsValidator()
    {
        RuleFor(oo => oo.RequestMachineryId).GreaterThan(0).WithMessage(RequestMachineryErrors.InValidRequestMachinery);
    }
}
