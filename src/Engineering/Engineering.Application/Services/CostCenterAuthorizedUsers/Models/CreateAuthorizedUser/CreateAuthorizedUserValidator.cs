namespace Engineering.Application.Services.CostCenterAuthorizedUsers.Models.CreateAuthorizedUser;

public class CreateAuthorizedUserValidator : AbstractValidator<CreateAuthorizedUserRequest>
{
    public CreateAuthorizedUserValidator()
    {
        RuleFor(oo => oo.CostCenterId)
            .IsPositive(GlobalCmts.CostCenterId);
        RuleFor(oo => oo.AuthorizedUserId)
            .IsPositive(CCenterCmts.AuthorizedUserId);
    }
}
