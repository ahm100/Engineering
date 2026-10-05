using Engineering.Domain.Entities.Transportations;

namespace Engineering.Application.Services.TransportationRequests.Commands.CreateTransportationRequestDocument;

public record CreateTransportationRequestDocumentCommand(
    string Url,
    TransportationRequest TransportationRequest) : ICommand<TransportationRequestDocument>;