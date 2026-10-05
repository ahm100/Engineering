namespace Engineering.Application.Services.Contracts.Contracts.GetContractTypeById;

public class GetContractTypeByIdValidator : AbstractValidator<GetContractTypeByIdRequest>
{
    public GetContractTypeByIdValidator()
    {
        RuleFor(oo => oo.ContractId)
            .IsPositive(GlobalCmts.ContractId);

        RuleFor(oo => oo.Id)
            .IsPositive(GlobalCmts.Id);
    }
}