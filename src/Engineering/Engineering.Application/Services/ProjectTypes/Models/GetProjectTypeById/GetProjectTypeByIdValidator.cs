namespace Engineering.Application.Services.ProjectTypes.Models.GetProjectTypeById;

public class GetProjectTypeByIdValidator : AbstractValidator<GetProjectTypeByIdRequest>
{
    public GetProjectTypeByIdValidator()
    {
        RuleFor(oo => oo.Id)
            .IsPositive(ProjectCmts.ProjectTypeId);

    }
}
