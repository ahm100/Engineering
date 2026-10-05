namespace Engineering.Application.WebServices.Commercial.Commerces.Commands.RemoveCommerce;

public record RemoveCommerceCommand(
    long Id
    ) : ICommand<bool?>;
