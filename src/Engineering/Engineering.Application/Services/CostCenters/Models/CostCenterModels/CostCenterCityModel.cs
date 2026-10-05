using Engineering.Application.WebServices.MetaDataServices.Cities.Models;

namespace Engineering.Application.Services.CostCenters.Models.CostCenterModels;

public record CostCenterCityModel(
    long Id,
    string? Name,
    string? Code,
    bool? IsActive,
    Province? Province
    );
