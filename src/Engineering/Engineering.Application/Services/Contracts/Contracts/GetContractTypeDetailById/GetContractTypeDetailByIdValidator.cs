namespace Engineering.Application.Services.Contracts.Contracts.GetContractTypeDetailById;

public class GetContractTypeDetailByIdValidator
    : AbstractValidator<GetContractTypeDetailByIdRequest>
{
    public GetContractTypeDetailByIdValidator()
    {
        RuleFor(oo => oo.ContractId)
            .IsPositive(GlobalCmts.ContractId);

        RuleFor(oo => oo.ContractTypeId)
            .IsPositive(ContractCmts.ContractTypeId);

        RuleFor(oo => oo.Id)
            .IsPositive(GlobalCmts.Id);
    }
}