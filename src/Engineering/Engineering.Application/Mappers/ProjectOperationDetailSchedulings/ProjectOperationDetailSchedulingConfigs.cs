using Engineering.Application.Services.ProjectOperationDetailSchedulings.Models.GetSchedulingProjectOperationDetails;
using Engineering.Application.Services.ProjectOperationDetailSchedulings.Models.GetSchedulingProjectOperations;
using Engineering.Domain.Entities.OperationInfos;
using Engineering.Domain.Entities.OperationLocations;

namespace Engineering.Application.Mappers.ProjectOperationDetailSchedulings;

public class ProjectOperationDetailSchedulingConfigs : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<OperationInfo, GetSchedulingOperationInfosResponseModel>()
           .Map(d => d.OperationInfoId, s => s.Id)
           .Map(d => d.OperationInfoName, s => s.OperationInfoName)
           .Map(d => d.OperationInfoCode, s => s.OperationInfoCode);

        config.NewConfig<OperationLocation, GetSchedulingOperationLocationsResponseModel>()
           .Map(d => d.OperationLocationId, s => s.Id)
           .Map(d => d.PublicName, s => s.PublicName)
           .Map(d => d.PublicCode, s => s.PublicCode);

    }
}