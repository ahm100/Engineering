
namespace Engineering.Application.Services.GoodsManagerAssignments.Contracts.DeleteGoodsManagerAssignment;

public class DeleteGoodsManagerAssignmentValidator
    : AbstractValidator<DeleteGoodsManagerAssignmentRequest>
{
    public DeleteGoodsManagerAssignmentValidator()
    {
        RuleFor(oo => oo.OrganizationId)
            .IsPositive(GlobalCmts.Id);
    }
}