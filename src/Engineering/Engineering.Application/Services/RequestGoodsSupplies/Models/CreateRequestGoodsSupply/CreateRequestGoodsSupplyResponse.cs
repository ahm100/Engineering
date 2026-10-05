using Engineering.Domain.Entities.OperationInfos;
using Engineering.Domain.Entities.ProjectOperationDetails;
using Engineering.Domain.Entities.ProjectOperations;

namespace Engineering.Application.Services.RequestGoodsSupplies.Models.CreateRequestGoodsSupply;

public record CreateRequestGoodsSupplyResponse
{
    public long Id { get; set; }
}

public record CreateRequestGoodsSupplyValidatorsResponse
{
    public required ProjectOperation ProjectOperation { get; set; }
    public ProjectOperationDetail? ProjectOperationDetail { get; set; }
    public OperationInfoSeason? OperationInfoSeason { get; set; }
}