namespace Engineering.Application.Services.ProcesVerbal.Contracts.GetProcesVerbalDetailById;

public class GetProcesVerbalDetailByIdValidator : AbstractValidator<GetProcesVerbalDetailByIdRequest>
{
    public GetProcesVerbalDetailByIdValidator()
    {
        RuleFor(x => x.Id)
            .IsPositive(GlobalCmts.Id);
    }
}
