namespace Engineering.Application.Services.RequestGoodsSupplies.Models.DeleteRGS;

public record DeleteRGSRequest(
    long Id) : IHttpRequest;