namespace Engineering.Application.Services.Projects.Queries.GetProjectForPdf;

public class GetProjectForPdfQueryValidator : AbstractValidator<GetProjectForPdfQuery>
{
    public GetProjectForPdfQueryValidator()
    {
        RuleFor(oo => oo.Id).IsPositive(GlobalCmts.ProjectId);
    }
}
