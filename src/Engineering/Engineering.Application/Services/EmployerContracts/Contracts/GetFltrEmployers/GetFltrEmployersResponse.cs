
namespace Engineering.Application.Services.EmployerContracts.Contracts.GetFltrEmployers;

public record GetFltrEmployersResponse(
    List<GetFltrEmployersModel> Data,
    int RowCount
    );

public record GetFltrEmployersModel(
    long Id,
    string? FullName);