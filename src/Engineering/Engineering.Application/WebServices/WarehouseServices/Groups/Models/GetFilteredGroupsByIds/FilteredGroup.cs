namespace Engineering.Application.WebServices.WarehouseServices.Groups.Models.GetFilteredGroupsByIds;

public record FilteredGroup(
    long Id,
    string? Name,
    string? CommercialName,
    string? Code,
    List<string>? Urls,
    string? Description,
    bool? IsActive,
    long? MeasureUnitId,
    string? MeasureUnitName
    );

public class GetGroupWithCategoryId
{
    public long GroupId { get; set; }
    public long CategoryId { get; set; }
};