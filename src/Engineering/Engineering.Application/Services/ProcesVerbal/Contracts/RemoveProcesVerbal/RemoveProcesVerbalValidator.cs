namespace Engineering.Application.Services.ProcesVerbal.Contracts.RemoveProcesVerbal;

public class RemoveProcesVerbalValidator : AbstractValidator<RemoveProcesVerbalRequest>
{
    public RemoveProcesVerbalValidator()
    {
        RuleFor(x => x.Id)
            .IsPositive(GlobalCmts.Id);
    }
}
