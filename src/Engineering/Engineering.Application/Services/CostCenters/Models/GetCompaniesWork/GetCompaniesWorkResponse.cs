namespace Engineering.Application.Services.CostCenters.Models.GetCompaniesWork;

public record GetCompaniesWorkResponse(
    List<GetCompaniesCostCentersModel> Data,
    int RowCount);

public class GetCompaniesCostCentersModel
{
    public long Id { get; set; }
    public string CostCenterCode { get; set; } = string.Empty;
    public string CostCenterName { get; set; } = string.Empty;
    public List<GetCompaniesProjectsModel>? Projects { get; set; }
}

public class GetCompaniesProjectsModel
{
    public long Id { get; set; }
    public string ProjectCode { get; set; } = string.Empty;
    public string ProjectName { get; set; } = string.Empty;

}


