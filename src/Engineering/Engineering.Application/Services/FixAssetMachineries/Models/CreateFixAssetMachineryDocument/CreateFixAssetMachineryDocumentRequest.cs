namespace Engineering.Application.Services.FixAssetMachineries.Models.CreateFixAssetMachineryDocument;

public record CreateFixAssetMachineryDocumentRequest(
    long FixAssetMachineryId,
    List<string>? Documents) : IHttpRequest;
