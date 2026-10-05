namespace Engineering.Application.Services.Contracts.Contracts.GetContractForProcesVerbal;

public class GetContractForProcesVerbalValidator : AbstractValidator<GetContractForProcesVerbalRequest>
{
    public GetContractForProcesVerbalValidator()
    {
        RuleFor(x => x.ProjectId)
            .IsPositiveWithNullableInput(GlobalCmts.Id);
    }
}
