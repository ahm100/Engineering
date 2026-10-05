namespace Engineering.Application.Services.GoodsManagerAssignments.Contracts.CreateGoodsManagerAssignment;

public class CreateGoodsManagerAssignmentValidator
    : AbstractValidator<CreateGoodsManagerAssignmentRequest>
{
    public CreateGoodsManagerAssignmentValidator()
    {
        RuleFor(x => x.OrganizationId)
            .IsPositive(GlobalCmts.Id);
    }
}