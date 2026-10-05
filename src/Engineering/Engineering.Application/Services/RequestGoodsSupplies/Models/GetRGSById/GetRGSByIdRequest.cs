namespace Engineering.Application.Services.RequestGoodsSupplies.Models.GetRGSById;

public record GetRGSByIdRequest(
    long Id) : IHttpRequest;