namespace Engineering.Application.Services.Contracts.Contracts.GetContractById;

public class GetContractByIdValidator : AbstractValidator<GetContractByIdRequest>
{
    public GetContractByIdValidator()
    {
        RuleFor(oo => oo.Id)
            .IsPositive(GlobalCmts.Id);
    }
}