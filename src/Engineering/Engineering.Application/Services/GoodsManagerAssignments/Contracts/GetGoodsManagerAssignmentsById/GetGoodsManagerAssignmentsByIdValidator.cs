namespace Engineering.Application.Services.GoodsManagerAssignments.Contracts.GetGoodsManagerAssignmentsById;

public class GetGoodsManagerAssignmentsByIdValidator
    : AbstractValidator<GetGoodsManagerAssignmentsByIdRequest>
{
    public GetGoodsManagerAssignmentsByIdValidator()
    {
        RuleFor(x => x.Id)
            .IsPositive(GlobalCmts.Id);
    }
}