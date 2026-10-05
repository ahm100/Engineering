namespace Engineering.Application.Services.EngineeringConfigs.Contracts.GetCodingConfigById;

public class GetCodingConfigByIdValidator : AbstractValidator<GetCodingConfigByIdRequest>
{
    public GetCodingConfigByIdValidator()
    {
        RuleFor(c => c.Id)
            .IsPositive(GlobalCmts.Id);
    }
}