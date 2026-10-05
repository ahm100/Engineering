namespace Engineering.Application.Services.ProjectOperationWbses.Contracts.GetProjectOperationWbsById;

public class GetProjectOperationWbsByIdValidator : AbstractValidator<GetProjectOperationWbsByIdRequest>
{
    public GetProjectOperationWbsByIdValidator()
    {
        RuleFor(oo => oo.Id)
            .IsPositive(GlobalCmts.Id);
    }
}