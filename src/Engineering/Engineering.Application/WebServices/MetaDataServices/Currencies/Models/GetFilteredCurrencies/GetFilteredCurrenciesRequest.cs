namespace Engineering.Application.WebServices.MetaDataServices.Currencies.Models.GetsCurrencyFiltered;

public record GetFilteredCurrenciesRequest(string? Name, long? CountryId, string? FilterData, bool? IsActive, int PageIndex, int PageSize);
