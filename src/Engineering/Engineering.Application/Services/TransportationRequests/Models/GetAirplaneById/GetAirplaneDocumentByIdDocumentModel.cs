namespace Engineering.Application.Services.TransportationRequests.Models.GetAirplaneById;

public record GetAirplaneDocumentByIdDocumentModel
{
    public long Id { get; set; }
    public string Url { get; set; } = string.Empty;
}