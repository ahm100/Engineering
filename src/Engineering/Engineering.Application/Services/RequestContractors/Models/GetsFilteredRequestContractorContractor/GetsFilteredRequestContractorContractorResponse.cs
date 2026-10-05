namespace Engineering.Application.Services.RequestContractors.Models.GetsFilteredRequestContractorContractor;

public record GetsFilteredRequestContractorContractorResponse(
    List<GetsFilteredRequestContractorContractorResponseModel> Data,
    int RowCount
    );
