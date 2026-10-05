using Engineering.Application.WebServices.WarehouseServices.Products.Models.CreateExitForConsumes;

namespace Engineering.Application.WebServices.WarehouseServices.Products.Models.CreateExitForRelocations;

public class CreateExitForRelocationsResponse
{
    [JsonProperty("value")]
    public CreateInvoiceResponse? Value { get; set; }

    //    [JsonProperty("isSuccess")]
    //    public bool? IsSuccess { get; set; }

    //    [JsonProperty("isSuccess")]
    //    public bool? IsFailure { get; set; }

    //    [JsonProperty("error")]
    //    public Error? Error { get; set; }

    //    [JsonProperty("pageIndex")]
    //    public int? PageIndex { get; set; }

    //    [JsonProperty("pageSize")]
    //    public int? PageSize { get; set; }
    //}

    //public class Error
    //{
    //    [JsonProperty("statusCode")]
    //    public int? statusCode { get; set; }
    //    [JsonProperty("code")]
    //    public string? code { get; set; }
    //    [JsonProperty("message")]
    //    public string? message { get; set; }
}
