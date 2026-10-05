namespace Engineering.Application.Services.RequestGoodsSupplies.Models.GetRequestGoodsSupplyCreators;

public record GetRequestGoodsSupplyCreatorsResponseModel
{
    public long? Id { get; set; }
    public string? FirstName { get; set; } = string.Empty;
    public string? LastName { get; set; } = string.Empty;
    public string? FullName => FirstName + " " + LastName;
    public string? Nickname { get; set; } = string.Empty;
};
