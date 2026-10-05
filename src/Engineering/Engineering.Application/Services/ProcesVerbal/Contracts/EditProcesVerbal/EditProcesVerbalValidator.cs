namespace Engineering.Application.Services.ProcesVerbal.Contracts.EditProcesVerbal;

public class EditProcesVerbalValidator : AbstractValidator<EditProcesVerbalRequest>
{
    public EditProcesVerbalValidator()
    {
        RuleFor(x => x.Id)
            .IsPositive(ProcesVerbalCmts.ProcesVerbalId);
    }
}
