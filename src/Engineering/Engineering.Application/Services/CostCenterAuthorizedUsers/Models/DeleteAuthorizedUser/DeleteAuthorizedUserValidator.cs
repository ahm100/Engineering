namespace Engineering.Application.Services.CostCenterAuthorizedUsers.Models.DeleteAuthorizedUser;

public class DeleteAuthorizedUserValidator : AbstractValidator<DeleteAuthorizedUserRequest>
{
    public DeleteAuthorizedUserValidator()
    {
        RuleFor(oo => oo.CostCenterId)
            .IsPositive(GlobalCmts.CostCenterId);
        RuleFor(oo => oo.UserId)
            .IsPositive(CCenterCmts.AuthorizedUserId);
    }
}
