namespace Engineering.Application.Services.ProjectRisks.Queries.GetProjectRiskById;

public class GetProjectRiskByIdQueryValidator : AbstractValidator<GetProjectRiskByIdQuery>
{
    public GetProjectRiskByIdQueryValidator()
    {
        RuleFor(oo => oo.Id)
            .IsPositive(GlobalCmts.Id);
    }
}
