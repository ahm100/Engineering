using CompanyModel = Engineering.Application.WebServices.MetaDataServices.Companies.Models.Company;

namespace Engineering.Application.WebServices.MetaDataServices.Companies.Queries.GetCompanyById;

public record GetCompanyByIdQuery(long Id) : IQuery<CompanyModel>;