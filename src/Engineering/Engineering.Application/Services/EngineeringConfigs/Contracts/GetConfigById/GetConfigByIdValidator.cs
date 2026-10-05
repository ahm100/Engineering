
namespace Engineering.Application.Services.EngineeringConfigs.Contracts.GetConfigById;

public class GetConfigByIdValidator : AbstractValidator<GetConfigByIdRequest>
{
    public GetConfigByIdValidator()
    {
        RuleFor(c => c.Id)
            .IsPositive(GlobalCmts.Id);
    }
}
