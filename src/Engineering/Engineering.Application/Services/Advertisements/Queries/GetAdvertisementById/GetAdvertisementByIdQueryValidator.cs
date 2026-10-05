namespace Engineering.Application.Services.Advertisements.Queries.GetAdvertisementById;

public class GetAdvertisementByIdQueryValidator : AbstractValidator<GetAdvertisementByIdQuery>
{
    public GetAdvertisementByIdQueryValidator()
    {
        RuleFor(c => c.Id)
            .IsPositive(GlobalCmts.Id);
    }
}