namespace Engineering.Application.Services.EmployerStatusStatements.Models.GetsFilteredEmployerRequester;

public record GetsFilteredEmployerRequesterResponse(
    List<GetsFilteredEmployerRequesterResponseModel> Data,
    int RowCount
    );

public record GetsFilteredEmployerRequesterResponseModel
{
    public long Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? FullName => FirstName + LastName;
    public string? OrganizationCode { get; set; }
    public string? DefaultPhoneNo { get; set; }
    public string? IdentityNo { get; set; }
};
