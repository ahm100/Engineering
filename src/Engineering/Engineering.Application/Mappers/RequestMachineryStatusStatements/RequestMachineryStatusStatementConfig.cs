using Engineering.Application.Services.RequestMachineryStatusStatements.Models.GetFilteredRequestMachineryStatusStatement;
using Engineering.Application.Services.RequestMachineryStatusStatements.Models.GetRequestMachineryStatusStatementById;
using Engineering.Domain.Entities.RequestMachineryStatusStatements;

namespace Engineering.Application.Mappers.RequestMachineryStatusStatements;

public class RequestMachineryStatusStatementConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
        config.NewConfig<RequestMachineryStatusStatement, GetRequestMachineryStatusStatementByIdResponse>()
           .Map(d => d.Id, s => s.Id)
           .Map(d => d.ContractorId, s => s.ContractorId)
           .Map(d => d.FromDate, s => s.FromDate)
           .Map(d => d.ToDate, s => s.ToDate)
           .Map(d => d.Status, s => s.Status)
           .Map(d => d.TotalRequestedCount, s => s.TotalRequestedCount)
           .Map(d => d.TotalFinalPrice, s => s.TotalFinalPrice)
           .Map(d => d.ContractorFinalPrice, s => s.ContractorPrice)
           .Map(d => d.Description, s => s.Description)
           .Map(d => d.ContractorIBAN, s => s.IBAN)
           .Map(d => d.Created, s => s.Created)
           .Map(d => d.PaymentDate, s => s.PaymentDate)
           .Map(d => d.Season, s => s.Season.SeasonName)
           .Map(d => d.SeasonId, s => s.Season.Id)
           .Map(d => d.Branch, s => s.Season.Branch.BranchName)
           .Map(d => d.BranchId, s => s.Season.Branch.Id)
           .Map(d => d.Category, s => s.Season.Branch.Category.CategoryName)
           .Map(d => d.CategoryId, s => s.Season.Branch.Category.Id)
           ;
#pragma warning restore CS8602 // Dereference of a possibly null reference.

#pragma warning disable CS8602 // Dereference of a possibly null reference.
        config.NewConfig<RequestMachineryStatusStatement, GetFilteredRequestMachineryStatusStatementModel>()
           .Map(d => d.Id, s => s.Id)
           .Map(d => d.ContractorId, s => s.ContractorId)
           .Map(d => d.FromDate, s => s.FromDate)
           .Map(d => d.ToDate, s => s.ToDate)
           .Map(d => d.Status, s => s.Status)
           .Map(d => d.TotalRequestedCount, s => s.TotalRequestedCount)
           .Map(d => d.TotalFinalPrice, s => s.TotalFinalPrice)
           .Map(d => d.ContractorFinalPrice, s => s.ContractorPrice)
           .Map(d => d.Description, s => s.Description)
           .Map(d => d.ContractorIBAN, s => s.IBAN)
           .Map(d => d.Created, s => s.Created)
           .Map(d => d.PaymentDate, s => s.PaymentDate)
           ;
#pragma warning restore CS8602 // Dereference of a possibly null reference.
    }
}
