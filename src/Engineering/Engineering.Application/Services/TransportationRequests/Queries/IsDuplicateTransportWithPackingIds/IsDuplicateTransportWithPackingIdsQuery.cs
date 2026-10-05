namespace Engineering.Application.Services.TransportationRequests.Queries.IsDuplicateTransportWithPackingIds;

public record IsDuplicateTransportWithPackingIdsQuery(
    List<long> PackingIds
    ) : IQuery<bool?>;
