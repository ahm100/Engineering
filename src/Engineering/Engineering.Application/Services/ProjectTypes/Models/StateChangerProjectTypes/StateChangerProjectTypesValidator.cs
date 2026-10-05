
namespace Engineering.Application.Services.ProjectTypes.Models.StateChangerProjectTypes;

public class StateChangerProjectTypesValidator : AbstractValidator<StateChangerProjectTypesRequest>
{
    public StateChangerProjectTypesValidator()
    {
        RuleForEach(oo => oo.Ids)
            .IsPositive(ProjectCmts.ProjectTypeId);
    }
}
