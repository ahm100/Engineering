using Engineering.Application.Services.OperationInfos.Models.GetsOperationInfoExcelExporter;
using Engineering.Domain.Entities.OperationInfos;

namespace Engineering.Application.Mappers.OperationInfoTypes;

public class OperationInfosModelConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<OperationInfo, GetsOperationInfoExcelExporterModel>()
           .Map(d => d.Id, s => s.Id)
           .Map(d => d.OperationInfoName, s => s.OperationInfoName)
           .Map(d => d.OperationInfoCode, s => s.OperationInfoCode)
           .Map(d => d.OperationLatinName, s => s.OperationLatinName)
           .Map(d => d.Priority, s => s.Priority)
           .Map(d => d.UnitOfMeasurementId, s => s.UnitOfMeasurementId)
           .Map(d => d.IsActive, s => s.IsActive)
           ;

    }
}