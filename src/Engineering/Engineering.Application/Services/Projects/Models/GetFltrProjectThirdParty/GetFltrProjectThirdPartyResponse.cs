namespace Engineering.Application.Services.Projects.Models.GetProjectThirdParties;

public record GetFltrProjectThirdPartyResponse(
    List<GetFltrProjectThirdPartyModel> Data,
    int RowCount
    );

public record GetFltrProjectThirdPartyModel
{
    public long Id { get; set; }
    public long? UserId { get; set; }
    public long ThirdPartyId { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? FullName { get; set; }
    public bool? IsIndividual { get; set; }
}