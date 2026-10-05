using ManagerModel = Engineering.Application.WebServices.MetaDataServices.Managers.Models.Manager;

namespace Engineering.Application.WebServices.MetaDataServices.Managers.Queries.GetManagerById;

public record GetManagerByIdQuery(
    long Id
    ) : IQuery<ManagerModel?>;
