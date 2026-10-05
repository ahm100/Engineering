namespace Engineering.Application.Services.OperationLocations.Models.OperationLocationModels;

public record GetsActiveOperationLocationModel(
    long Id,
    string PrivateName,
    string PrivateCode,
    string PublicName,
    string PublicCode,
    string Path,
    string Coding
    );
