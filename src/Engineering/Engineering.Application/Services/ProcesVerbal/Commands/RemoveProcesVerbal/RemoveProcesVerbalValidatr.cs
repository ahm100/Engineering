namespace Engineering.Application.Services.ProcesVerbal.Commands.RemoveProcesVerbal;

public class RemoveProcesVerbalValidatr : AbstractValidator<RemoveProcesVerbalCommand>
{
    public RemoveProcesVerbalValidatr()
    {
        RuleFor(x => x.Id)
            .IsPositive(GlobalCmts.Id);
    }
}
