
namespace Engineering.Application.Services.ProjectOperationDetailContractorServices.Models.GetsByFilter;

public record GetsContractorServiceByFilterResponse(
    List<GetsContractorServiceByFilterModel> Data,
    int RowCount
    );
