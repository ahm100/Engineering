namespace Engineering.Application.Services.ProjectOperations.Models.GetPODate;

public class GetPODateValidator : AbstractValidator<GetPODateRequest>
{
    public GetPODateValidator()
    {
        RuleFor(oo => oo.ProjectOperationId)
            .IsPositive(ProjectErrors.IdIsEmpty);
    }
}