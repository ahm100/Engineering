namespace Engineering.Application.Services.ProcesVerbal.Commands.EditProcesVerbal;

public class EditProcesVerbalCommandValidator : AbstractValidator<EditProcesVerbalCommand>
{
    public EditProcesVerbalCommandValidator()
    {
        RuleFor(x => x.Id)
            .IsPositive(ProcesVerbalCmts.ProcesVerbalId);
    }
}
