using Engineering.Application.Services.ProjectOperationDetailDeductions.Models.GetProjectOperationDetailDeductionById;
using Engineering.Application.Services.ProjectOperationDetailDeductions.Models.GetsDeductionByProjectOperationDetailId;
using Engineering.Domain.Entities.ProjectOperationDetails;

namespace Engineering.Application.Mappers.ProjectOperationDetails;

public class ProjectOperationDetailDeductionModelConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<ProjectOperationDetailDeduction, GetsDeductionByProjectOperationDetailIdModel>()
             .Map(d => d.Id, s => s.Id)
             .Map(d => d.ProjectOperationDetailId, s => s.ProjectOperationDetail.Id)
             .Map(d => d.Length, s => s.Length)
             .Map(d => d.Width, s => s.Width)
             .Map(d => d.Height, s => s.Height)
             .Map(d => d.Weight, s => s.Weight)
             .Map(d => d.FinalAmount, s => s.FinalAmount)
             .Map(d => d.Number, s => s.Number)
             .Map(d => d.PrivateName, s => s.ProjectOperationDetail.OperationLocation.PrivateName)
             .Map(d => d.PrivateCode, s => s.ProjectOperationDetail.OperationLocation.PrivateCode)
             .Map(d => d.PublicCode, s => s.ProjectOperationDetail.OperationLocation.PublicCode)
             .Map(d => d.PublicName, s => s.ProjectOperationDetail.OperationLocation.PublicName);

        config.NewConfig<ProjectOperationDetailDeduction, GetProjectOperationDetailDeductionByIdResponse>()
             .Map(d => d.Id, s => s.Id)
             .Map(d => d.ProjectOperationDetailId, s => s.ProjectOperationDetail.Id)
             .Map(d => d.Length, s => s.Length)
             .Map(d => d.Width, s => s.Width)
             .Map(d => d.Height, s => s.Height)
             .Map(d => d.Weight, s => s.Weight)
             .Map(d => d.PrivateName, s => s.ProjectOperationDetail.OperationLocation.PrivateName)
             .Map(d => d.PrivateCode, s => s.ProjectOperationDetail.OperationLocation.PrivateCode)
             .Map(d => d.PublicCode, s => s.ProjectOperationDetail.OperationLocation.PublicCode)
             .Map(d => d.PublicName, s => s.ProjectOperationDetail.OperationLocation.PublicName)
             .Map(d => d.FinalAmount, s => s.FinalAmount)
             .Map(d => d.Number, s => s.Number);
    }
}
