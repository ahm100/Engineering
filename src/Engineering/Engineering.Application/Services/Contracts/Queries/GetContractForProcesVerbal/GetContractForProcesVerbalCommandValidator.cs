namespace Engineering.Application.Services.Contracts.Queries.GetContractsByProjectIdForProcesVerbal;

public class GetContractForProcesVerbalCommandValidator : AbstractValidator<GetContractForProcesVerbalCommand>
{
    public GetContractForProcesVerbalCommandValidator()
    {
        RuleFor(x => x.ProjectId)
            .IsPositiveWithNullableInput(GlobalCmts.ProjectId);
    }
}
