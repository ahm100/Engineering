namespace Engineering.Application.Services.Seasons.Models.GetSeasonById;

public class GetSeasonByIdValidator : AbstractValidator<GetSeasonByIdRequest>
{
    public GetSeasonByIdValidator()
    {
        RuleFor(oo => oo.Id)
            .IsPositive(GlobalCmts.SeasonId);

    }
}
