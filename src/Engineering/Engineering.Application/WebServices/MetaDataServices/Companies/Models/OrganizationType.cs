namespace Engineering.Application.WebServices.MetaDataServices.Companies.Models;

public record OrganizationType(
          long Id,
          string Code,
          string NameFa,
          string? NameEn,
          string? Description,
          bool IsAddressAllowed,
          bool IsDocumentAllowed,
          string RootPath,
          long? CompanyId,
          Company? Company,
          long? OrganizationTypeId,
          OrganizationType? OrganizationTypeParent
    );

