namespace Engineering.Application.Services.Advertisements.Commands.DeleteAdvertisement;

public class DeleteAdvertisementCommandValidator : AbstractValidator<DeleteAdvertisementCommand>
{
    public DeleteAdvertisementCommandValidator()
    {
        RuleFor(c => c.Id)
            .IsPositive(GlobalCmts.Id);
    }
}