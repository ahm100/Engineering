namespace Engineering.Application.Services.Contracts.Contracts.GetContractsByStatus;

public class GetContractsByStatusValidator : AbstractValidator<GetContractsByStatusRequest>
{
    public GetContractsByStatusValidator()
    {
        RuleFor(oo => oo.Status)
            .IsEnum(GlobalCmts.Status);

        RuleFor(oo => oo.PageIndex)
            .PageIndexZero(GlobalCmts.PageIndex);

        RuleFor(oo => oo.PageSize)
            .PageSizeZero(GlobalCmts.PageSize);

        RuleFor(oo => oo.OrderBy)
            .Must(orderBy => RuleExtensions.HasOnlyValidOrderFields<GetContractsByStatusModel>(orderBy))
            .WithMessage("Invalid OrderBy field.");
    }
}