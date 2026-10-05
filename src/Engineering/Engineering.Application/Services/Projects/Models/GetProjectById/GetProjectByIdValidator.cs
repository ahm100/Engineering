namespace Engineering.Application.Services.Projects.Models.GetProjectById;

public class GetProjectByIdValidator : AbstractValidator<GetProjectByIdRequest>
{
    public GetProjectByIdValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1).WithError(ProjectErrors.IdIsEmpty);
    }
}
