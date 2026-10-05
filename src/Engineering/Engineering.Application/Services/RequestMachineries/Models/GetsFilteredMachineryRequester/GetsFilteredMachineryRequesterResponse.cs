namespace Engineering.Application.Services.RequestMachineries.Models.GetsFilteredMachineryRequester;

public record GetsFilteredMachineryRequesterResponse(
    List<GetsFilteredMachineryRequesterResponseModel> Data,
    int RowCount
    );
