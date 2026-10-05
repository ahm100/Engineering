namespace Engineering.Application.Services.RequestGoodsSupplies.Models.GetsRequestGoodSupplyRequester;


public record GetsRequestGoodSupplyRequesterModel
{
    public long Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Nickname { get; set; }
    public string? FullName => FirstName + " " + LastName;
    public string? OrganizationCode { get; set; }
    public string? DefaultPhoneNo { get; set; }
    public string? IdentityNo { get; set; }
}
