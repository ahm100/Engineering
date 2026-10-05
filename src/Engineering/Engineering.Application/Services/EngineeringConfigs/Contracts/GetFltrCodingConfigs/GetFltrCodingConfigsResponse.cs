using Engineering.Domain.Entities.EngineeringConfig.Enum;

namespace Engineering.Application.Services.EngineeringConfigs.Contracts.GetFltrCodingConfigs;

public record GetFltrCodingConfigsResponse(
    List<GetFltrCodingConfigsModel> Data,
    int RowCount
    );

public class GetFltrCodingConfigsModel
{
    public long Id { get; set; }
    public CodingAlgorithmType Type { get; set; }
    public string? TypeDescription => Type.GetEnumDescription();
    public string Prefix { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    public long CreatorId { get; set; }
    public string Creator { get; set; } = string.Empty;
    public DateTime Created { get; set; }
    public string CreatedShamsi => Created.ToShamsi();
    public DateTime? Updated { get; set; }
    public string? UpdatedShamsi => Updated.ToShamsi();
}