using Engineering.Application.Services.EmployerContracts.Contracts.EContractFinancialCover;

namespace Engineering.Application.Services.EmployerContracts.Contracts.UpdateEContractFinancial;

public class UpdateEContractFinancialRequest : IHttpRequest
{
    public long Id { get; set; }
    public long CurrencyId { get; set; }
    public decimal PriceTolerance { get; set; }
    public List<UpdateEContractFinancialModel>? UpdateEContractFinancials { get; set; }
}

public class UpdateEContractFinancialModel
{
    public long Id { get; set; }
    public decimal? CurrencyRate { get; set; }
    public decimal AdvancePayment { get; set; }
    public string? Description { get; set; }
    public List<UpdateEmployerCostOverFinancialModel>? CostOvers { get; set; }
    public List<long>? DeleteCostOvers { get; set; }
    public List<UpdateProjectOperationFinancialModel>? ProjectOperations { get; set; }
}

public class UpdateProjectOperationFinancialModel
{
    public long EOperationId { get; set; }
    public decimal? TolerancePercentage { get; set; }
    public decimal Price { get; set; }
    public decimal IncreaseRate { get; set; }
    public string? Description { get; set; }
    public List<CreateEOProductModel>? CreateProducts { get; set; }
    public List<AssignEOProductModel>? AssignProducts { get; set; }
    public List<UpdateEOProductModel>? UpdateProducts { get; set; }
    public List<UpdateAssignEOProductModel>? UpdateAssignProducts { get; set; }
    public List<long>? DeletedProductGroups { get; set; }
    public List<CreateEOServiceModel>? CreateServices { get; set; }
    public List<AssignEOServiceModel>? AssignServices { get; set; }
    public List<UpdateEOServiceModel>? UpdateServices { get; set; }
    public List<UpdateAssignEOServiceModel>? UpdateAssignServices { get; set; }
    public List<long>? DeletedServices { get; set; }
};

public class UpdateEmployerCostOverFinancialModel
{
    public long Id { get; set; }
    public long CostOverId { get; set; }
    public decimal Percent { get; set; }
    public List<UpdateCostOverImpactFinancialModel>? CostOverImpacts { get; set; }
};

public class UpdateCostOverImpactFinancialModel
{
    public long? Id { get; set; }
    public long CostOverId { get; set; }
    public decimal Percent { get; set; }
};