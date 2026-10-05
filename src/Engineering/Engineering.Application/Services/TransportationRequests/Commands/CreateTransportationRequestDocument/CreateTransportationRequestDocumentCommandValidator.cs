namespace Engineering.Application.Services.TransportationRequests.Commands.CreateTransportationRequestDocument;

public class CreateTransportationRequestDocumentCommandValidator : AbstractValidator<CreateTransportationRequestDocumentCommand>
{
    public CreateTransportationRequestDocumentCommandValidator()
    {
        RuleFor(oo => oo.Url).NotEmpty().WithError(TransportationRequestDocumentErrors.InValidUrl);
        RuleFor(oo => oo.TransportationRequest).NotEmpty().WithError(TransportationRequestDocumentErrors.InValidTransportationRequest);
    }
}
