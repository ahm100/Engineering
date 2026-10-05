namespace Engineering.Application.WebServices.MetaDataServices.Companies.Models;

public record Company(
          long Id,
          string Code,
          string NameFa,
          string? NameEn,
          string? Address,
          DocumentGenerationType DocumentGenerationType,
          int? DocumentNumberStartsFrom,
          string? LogoUrl,
          string? EconomicCode,
          string? NationalID,
          string? SystemUrl,
          List<OrganizationType>? OrganizationTypes
);
