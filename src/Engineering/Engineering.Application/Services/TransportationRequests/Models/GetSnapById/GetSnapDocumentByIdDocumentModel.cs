namespace Engineering.Application.Services.TransportationRequests.Models.GetSnapById;

public record GetSnapDocumentByIdDocumentModel
{
    public long Id { get; set; }
    public string Url { get; set; } = string.Empty;
}