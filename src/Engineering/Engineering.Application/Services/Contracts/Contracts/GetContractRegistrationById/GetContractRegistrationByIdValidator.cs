namespace Engineering.Application.Services.Contracts.Contracts.GetContractRegistrationById;

public class GetContractRegistrationByIdValidator : AbstractValidator<GetContractRegistrationByIdRequest>
{
    public GetContractRegistrationByIdValidator() => RuleFor(x => x.Id).IsPositive(GlobalCmts.Id);
}
