using Engineering.Application.Services.DailyProjectOperations.Models.GetDailyProjectOperationHistories;
using Engineering.Domain.Entities.DailyProjectOperations;

namespace Engineering.Application.Mappers.DailyProjectOperations;

public class GetDailyProjectOperationHistoriesDetailDocumentModelConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<DailyProjectOperationDocument, GetDailyProjectOperationHistoriesDetailDocumentModel>()
           .Map(d => d.DailyProjectOperationDocumentId, s => s.Id)
           .Map(d => d.Url, s => s.Url);
    }
}