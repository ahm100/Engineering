namespace Engineering.Application.Services.TransportationRequests.Models.GetById;

public record GetTransportationRequestDocumentByIdDocumentModel
{
    public long Id { get; set; }
    public string Url { get; set; } = string.Empty;
}