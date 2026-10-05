namespace Engineering.Application.Services.ProjectWbses.Queries.GetProjectByImportId;

public class GetProjectByImportIdQueryValidator : AbstractValidator<GetProjectByImportIdQuery>
{
    public GetProjectByImportIdQueryValidator()
    {
        RuleFor(e => e.ImportId)
            .IsPositive(GlobalCmts.Id);
    }
}
