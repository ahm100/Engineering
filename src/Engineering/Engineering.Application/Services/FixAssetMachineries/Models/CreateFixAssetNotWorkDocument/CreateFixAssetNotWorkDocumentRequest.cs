namespace Engineering.Application.Services.FixAssetMachineries.Models.CreateFixAssetNotWorkDocument;

public record CreateFixAssetNotWorkDocumentRequest(
    long FixAssetNotWorkId,
    List<string>? Documents) : IHttpRequest;
