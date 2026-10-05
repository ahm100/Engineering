namespace Engineering.Application.Services.EmployerContracts.Contracts.EContractFinancialCover;

public class EContractFinancialCoverRequest : IHttpRequest
{
    public long Id { get; set; }
    public decimal? PriceTolerance { get; set; }
    public long CurrencyId { get; set; }
    public List<EContractFinancialCoverModel> EContracts { get; set; } = new();
}

public class EContractFinancialCoverModel
{
    public long Id { get; set; }
    public decimal? CurrencyRate { get; set; }
    public decimal AdvancePayment { get; set; }
    public string? Description { get; set; }
    public List<CreateEmployerCostOverFinancialModel>? CostOvers { get; set; }
    public List<long>? DeleteCostOvers { get; set; }
    public List<ProjectOperationFinancialModel>? ProjectOperations { get; set; }
}

public class ProjectOperationFinancialModel
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

public class CreateEmployerCostOverFinancialModel
{
    public long Id { get; set; }
    public long CostOverId { get; set; }
    public decimal Percent { get; set; }
    public List<CreateCostOverImpactFinancialModel>? CostOverImpacts { get; set; }
};

public class CreateCostOverImpactFinancialModel
{
    public long? Id { get; set; }
    public long CostOverId { get; set; }
    public decimal Percent { get; set; }
};


public class CreateEOProductModel
{
    public long ProductGroupId { get; set; }
    public long? ProductId { get; set; }
    public decimal? MinPrice { get; set; }
    public decimal MaxPrice { get; set; }
    public int? Count { get; set; }
    public decimal Tax { get; set; }
    public decimal TaxPercent { get; set; }
    public decimal TransportationCost { get; set; }
    public decimal TransportationCostPercent { get; set; }
    public decimal ProfitCost { get; set; }
    public decimal ProfitCostPercent { get; set; }
    public decimal OtherCost { get; set; }
    public decimal OtherCostPercent { get; set; }
    public decimal IncreaseRate { get; set; }
    public string? Description { get; set; } = string.Empty;
}

public class UpdateEOProductModel
{
    public long Id { get; set; }
    public long ProductGroupId { get; set; }
    public decimal? MinPrice { get; set; }
    public decimal MaxPrice { get; set; }
    public int? Count { get; set; }
    public decimal Tax { get; set; }
    public decimal TaxPercent { get; set; }
    public decimal TransportationCost { get; set; }
    public decimal TransportationCostPercent { get; set; }
    public decimal ProfitCost { get; set; }
    public decimal ProfitCostPercent { get; set; }
    public decimal OtherCost { get; set; }
    public decimal OtherCostPercent { get; set; }
    public decimal IncreaseRate { get; set; }
    public string? Description { get; set; } = string.Empty;
}

public class AssignEOProductModel
{
    public long ProductGroupId { get; set; }
    public long? ProductId { get; set; }
    public decimal? MinPrice { get; set; }
    public decimal MaxPrice { get; set; }
    public int? Count { get; set; }
    public decimal Tax { get; set; }
    public decimal TaxPercent { get; set; }
    public decimal TransportationCost { get; set; }
    public decimal TransportationCostPercent { get; set; }
    public decimal ProfitCost { get; set; }
    public decimal ProfitCostPercent { get; set; }
    public decimal OtherCost { get; set; }
    public decimal OtherCostPercent { get; set; }
    public decimal IncreaseRate { get; set; }
    public string? Description { get; set; }
}

public class UpdateAssignEOProductModel
{
    public long Id { get; set; }
    public long ProductGroupId { get; set; }
    public decimal? MinPrice { get; set; }
    public decimal MaxPrice { get; set; }
    public int? Count { get; set; }
    public decimal Tax { get; set; }
    public decimal TaxPercent { get; set; }
    public decimal TransportationCost { get; set; }
    public decimal TransportationCostPercent { get; set; }
    public decimal ProfitCost { get; set; }
    public decimal ProfitCostPercent { get; set; }
    public decimal OtherCost { get; set; }
    public decimal OtherCostPercent { get; set; }
    public decimal IncreaseRate { get; set; }
    public string? Description { get; set; } = string.Empty;
}

public class CreateEOServiceModel
{
    public long ServiceId { get; set; }
    public decimal? MinPrice { get; set; }
    public decimal MaxPrice { get; set; }
    public decimal Tax { get; set; }
    public decimal TaxPercent { get; set; }
    public decimal TransportationCost { get; set; }
    public decimal TransportationCostPercent { get; set; }
    public decimal ProfitCost { get; set; }
    public decimal ProfitCostPercent { get; set; }
    public decimal OtherCost { get; set; }
    public decimal OtherCostPercent { get; set; }
    public decimal IncreaseRate { get; set; }
    public string? Description { get; set; }
}

public class UpdateEOServiceModel
{
    public long Id { get; set; }
    public decimal? MinPrice { get; set; }
    public decimal MaxPrice { get; set; }
    public decimal Tax { get; set; }
    public decimal TaxPercent { get; set; }
    public decimal TransportationCost { get; set; }
    public decimal TransportationCostPercent { get; set; }
    public decimal ProfitCost { get; set; }
    public decimal ProfitCostPercent { get; set; }
    public decimal OtherCost { get; set; }
    public decimal OtherCostPercent { get; set; }
    public decimal IncreaseRate { get; set; }
    public string? Description { get; set; } = string.Empty;
}

public class AssignEOServiceModel
{
    public long ServiceId { get; set; }
    public decimal? MinPrice { get; set; }
    public decimal MaxPrice { get; set; }
    public decimal Tax { get; set; }
    public decimal TaxPercent { get; set; }
    public decimal TransportationCost { get; set; }
    public decimal TransportationCostPercent { get; set; }
    public decimal ProfitCost { get; set; }
    public decimal ProfitCostPercent { get; set; }
    public decimal OtherCost { get; set; }
    public decimal OtherCostPercent { get; set; }
    public decimal IncreaseRate { get; set; }
    public string? Description { get; set; }
}

public class UpdateAssignEOServiceModel
{
    public long Id { get; set; }
    public decimal? MinPrice { get; set; }
    public decimal MaxPrice { get; set; }
    public decimal Tax { get; set; }
    public decimal TaxPercent { get; set; }
    public decimal TransportationCost { get; set; }
    public decimal TransportationCostPercent { get; set; }
    public decimal ProfitCost { get; set; }
    public decimal ProfitCostPercent { get; set; }
    public decimal OtherCost { get; set; }
    public decimal OtherCostPercent { get; set; }
    public decimal IncreaseRate { get; set; }
    public string? Description { get; set; } = string.Empty;
}
