namespace Engineering.Application.WebServices.WarehouseServices.Groups.Models.GetActiveGroups;

public record ActiveGroupsModel(
    long Id,
    string? Name,
    string? CommercialName,
    string? Code,
    long? MeasureUnitId,
    long? CategoryId,
    string? MeasureUnitName,
    bool? IsSerialGeneratedAutomatically,
    long? BrandModelId
    );

