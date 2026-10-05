namespace Engineering.Application.Services.Advertisements.Contracts.GetAdvertisementById;

public class GetAdvertisementByIdValidator : AbstractValidator<GetAdvertisementByIdRequest>
{
    public GetAdvertisementByIdValidator()
    {
        RuleFor(c => c.Id)
            .IsPositive(GlobalCmts.Id);
    }
}