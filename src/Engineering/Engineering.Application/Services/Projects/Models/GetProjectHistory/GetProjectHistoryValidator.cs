namespace Engineering.Application.Services.Projects.Models.GetProjectHistory;

public class GetProjectHistoryValidator : AbstractValidator<GetProjectHistoryRequest>
{
    public GetProjectHistoryValidator()
    {
        RuleFor(oo => oo.Id).IsPositive(GlobalCmts.Id);
    }
}
