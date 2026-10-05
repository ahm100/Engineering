namespace Engineering.Application.Services.Advertisements.Contracts.DeleteAdvertisement;

public class DeleteAdvertisementValidator : AbstractValidator<DeleteAdvertisementRequest>
{
    public DeleteAdvertisementValidator()
    {
        RuleFor(c => c.Id)
            .IsPositive(GlobalCmts.Id);
    }
}