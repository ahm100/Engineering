namespace Engineering.Application.Services.CostCenterAuthorizedUsers.Models.CreateAuthorizedUsers;

public class CreateAuthorizedUsersValidator : AbstractValidator<CreateAuthorizedUsersRequest>
{
    public CreateAuthorizedUsersValidator()
    {
        RuleFor(oo => oo.CostCenterId)
            .IsPositive(GlobalCmts.CostCenterId);
    }
}
