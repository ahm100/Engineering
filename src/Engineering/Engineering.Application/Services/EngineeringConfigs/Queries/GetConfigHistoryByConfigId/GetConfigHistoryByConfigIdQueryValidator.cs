namespace Engineering.Application.Services.EngineeringConfigs.Queries.GetConfigHistoryByConfigId;

public class GetConfigHistoryByConfigIdQueryValidator : AbstractValidator<GetConfigHistoryByConfigIdQuery>
{
    public GetConfigHistoryByConfigIdQueryValidator()
    {
        RuleFor(c => c.ConfigId)
            .IsPositive(GlobalCmts.Id);
    }
}