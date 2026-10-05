namespace Engineering.Application.Services.ProjectOperationWbses.Contracts.GetPOWbsByPOId;

public class GetPOWbsByPOIdValidator : AbstractValidator<GetPOWbsByPOIdRequest>
{
    public GetPOWbsByPOIdValidator()
    {
        RuleFor(oo => oo.ProjectOperationId)
            .IsPositive(GlobalCmts.ProjectOperation);
    }
}