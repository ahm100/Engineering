namespace Engineering.Application.Services.Projects.Models.GetProjectForPdf;

public class GetProjectForPdfValidator : AbstractValidator<GetProjectForPdfRequest>
{
    public GetProjectForPdfValidator()
    {
        RuleFor(oo => oo.Id).IsPositive(GlobalCmts.ProjectId);
    }
}
