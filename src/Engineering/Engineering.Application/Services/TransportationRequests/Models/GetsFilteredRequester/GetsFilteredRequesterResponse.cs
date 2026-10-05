
namespace Engineering.Application.Services.TransportationRequests.Models.GetsFilteredRequester;

public record GetsFilteredRequesterResponse(
    List<GetsFilteredRequesterResponseModel> Data,
    int RowCount
    );
