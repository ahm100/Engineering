namespace Engineering.Application.Services.Projects.Models.GetSummarizedProjectById;

public class GetSummarizedProjectByIdValidator : AbstractValidator<GetSummarizedProjectByIdRequest>
{
    public GetSummarizedProjectByIdValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1).WithError(ProjectErrors.IdIsEmpty);
    }
}
