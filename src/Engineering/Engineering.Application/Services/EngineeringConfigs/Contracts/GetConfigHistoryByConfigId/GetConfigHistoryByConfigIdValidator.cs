namespace Engineering.Application.Services.EngineeringConfigs.Contracts.GetConfigHistoryByConfigId;

public class GetConfigHistoryByConfigIdValidator : AbstractValidator<GetConfigHistoryByConfigIdRequest>
{
    public GetConfigHistoryByConfigIdValidator()
    {
        RuleFor(c => c.ConfigId)
            .IsPositive(GlobalCmts.Id);
    }
}