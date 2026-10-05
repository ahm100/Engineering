namespace Engineering.Application.Services.ProcesVerbal.Queries.GetProcesVerbalDetailById;

public class GetProcesVerbalDetailByIdQueryValidator : AbstractValidator<GetProcesVerbalDetailByIdQuery>
{
    public GetProcesVerbalDetailByIdQueryValidator()
    {
        RuleFor(x => x.Id)
            .IsPositive(GlobalCmts.Id);
    }
}
