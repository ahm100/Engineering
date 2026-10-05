namespace Engineering.Application.Services.Contractors.Models.ContractorServices.GetContractorServicesByContractorId;

public record GetContractorServicesByContractorIdResponse(
    List<GetContractorServicesByContractorIdModel> Data,
    int RowCount
    );

