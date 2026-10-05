using EmployerModel = Engineering.Application.WebServices.MetaDataServices.Employers.Models.Employer;

namespace Engineering.Application.WebServices.MetaDataServices.Employers.Queries.GetEmployerById;

public record GetEmployerByIdQuery(
    long Id
    ) : IQuery<EmployerModel?>;
