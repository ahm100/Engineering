namespace Engineering.Application.Services.RequestContractors.Models.GetsFilteredRequestContractorRequester;

public record GetsFilteredRequestContractorRequesterResponse(
    List<GetsFilteredRequestContractorRequesterResponseModel> Data,
    int RowCount
    );
