namespace Engineering.Application.WebServices.WarehouseServices.WarehouseCategories.Models;

public record WarehouseCategory(
    long? Id,
    string? Title,
    string? Code,
    bool? IsActive,
    bool? HasChild
    );
