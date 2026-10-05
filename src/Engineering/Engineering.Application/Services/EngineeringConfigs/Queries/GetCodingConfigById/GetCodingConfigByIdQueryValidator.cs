namespace Engineering.Application.Services.EngineeringConfigs.Queries.GetCodingConfigById;

public class GetCodingConfigByIdQueryValidator : AbstractValidator<GetCodingConfigByIdQuery>
{
    public GetCodingConfigByIdQueryValidator()
    {
        RuleFor(c => c.Id)
            .IsPositive(GlobalCmts.Id);
    }
}