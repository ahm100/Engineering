namespace Engineering.Application.Services.TransportationRequests.Commands.DeleteTransportationRequestDocument;

public class DeleteTransportationRequestDocumentCommandValidator : AbstractValidator<DeleteTransportationRequestDocumentCommand>
{
    public DeleteTransportationRequestDocumentCommandValidator()
    {
        RuleFor(oo => oo.TransportationRequestDocumentId).NotNull().WithError(TransportationRequestDocumentErrors.InValidTransportationRequestDocumentId);
    }
}
