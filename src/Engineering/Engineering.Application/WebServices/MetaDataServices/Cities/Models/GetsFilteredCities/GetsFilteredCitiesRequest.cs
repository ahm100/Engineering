namespace Engineering.Application.WebServices.MetaDataServices.Currencies.Models.GetsFilteredCities;

public record GetsFilteredCitiesRequest(long? ProvinceId, string? FilterData, bool? IsActive, int PageIndex, int PageSize);
