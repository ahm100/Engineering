using Engineering.Application.Services.TelegramChats.Models.GetTelegramChatById;
using Engineering.Application.Services.TelegramChats.Models.GetTelegramChatByName;
using Engineering.Application.Services.TelegramChats.Models.TelegramChatModels;
using Engineering.Domain.Entities.TelegramChats;

namespace Engineering.Application.Mappers.TelegramChats;

#pragma warning disable CS8602 // Dereference of a possibly null reference.
public class TelegramChatsModelConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<TelegramChat, GetTelegramChatsWithChildModel>()
            .Map(d => d.ProjectId, s => s.Project.Id)
            .Map(d => d.CostCenterId, s => s.CostCenter.Id)
            .Map(d => d.ProjectName, s => s.Project.ProjectName)
            .Map(d => d.CostCenterName, s => s.CostCenter.CostCenterName);

        config.NewConfig<TelegramChat, GetTelegramChatByNameResponse>()
            .Map(d => d.ProjectId, s => s.Project.Id)
            .Map(d => d.CostCenterId, s => s.CostCenter.Id)
            .Map(d => d.ProjectName, s => s.Project.ProjectName)
            .Map(d => d.CostCenterName, s => s.CostCenter.CostCenterName);

        config.NewConfig<TelegramChat, GetTelegramChatByIdResponse>()
            .Map(d => d.ProjectId, s => s.Project.Id)
            .Map(d => d.CostCenterId, s => s.CostCenter.Id)
            .Map(d => d.ProjectName, s => s.Project.ProjectName)
            .Map(d => d.CostCenterName, s => s.CostCenter.CostCenterName);

        config.NewConfig<TelegramChat, GetsActiveTelegramChatModel>()
            .Map(d => d.ProjectId, s => s.Project.Id)
            .Map(d => d.CostCenterId, s => s.CostCenter.Id)
            .Map(d => d.ProjectName, s => s.Project.ProjectName)
            .Map(d => d.CostCenterName, s => s.CostCenter.CostCenterName);
    }
}
#pragma warning restore CS8602 // Dereference of a possibly null reference.