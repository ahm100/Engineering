namespace Engineering.Application.Services.RequestMachineries.Models.GetsFilteredMachineryContractor;

public record GetsFilteredMachineryContractorResponse(
    List<GetsFilteredMachineryContractorResponseModel> Data,
    int RowCount
    );
