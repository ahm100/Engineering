namespace Engineering.Application.Services.ProjectRisks.Contracts.GetProjectRiskById;

public class GetProjectRiskByIdValidator : AbstractValidator<GetProjectRiskByIdRequest>
{
    public GetProjectRiskByIdValidator()
    {
        RuleFor(oo => oo.Id)
            .IsPositive(GlobalCmts.Id);
    }
}