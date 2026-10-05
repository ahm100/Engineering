using Engineering.Domain.Entities.RequestRewards.Enums;

namespace Engineering.Application.Services.RequestRewards.Contracts.CreateRequestReward;

public record CreateRequestRewardRequest(
    List<CreateRequestRewardModel> Data
    ) : IHttpRequest;

public record CreateRequestRewardModel
{
    public long? Id { get; set; }
    public RequestRewardType Type { get; set; }
    public long CostCenterId { get; set; }
    public long? ProjectId { get; set; }
    public long? ProjectOperationId { get; set; }
    public long? ProjectOperationDetailId { get; set; }
    public List<CreateRequestRewardThirdPartyModel>? ThirdPartyIds { get; set; } = new();
    public decimal? OfferedPrice { get; set; }
    public long? CurrencyId { get; set; }
    public DateTime RegistrationDate { get; set; } = DateTime.Now;
    public string Description { get; set; } = string.Empty;
    public List<CreateRequestRewardDocumentModel>? Documents { get; set; }
    public List<CreateRequestRewardProductModel>? Products { get; set; } = new();
    public bool IsDeleted { get; set; }
}

public record CreateRequestRewardThirdPartyModel
{
    public long? Id { get; set; }
    public long ThirdPartyId { get; set; }
    public bool IsDeleted { get; set; }
}

public record CreateRequestRewardDocumentModel
{
    public long? Id { get; set; }
    public string Url { get; set; } = string.Empty;
    public bool IsDeleted { get; set; }
}

public record CreateRequestRewardProductModel
{
    public long? Id { get; set; }
    public long ProductId { get; set; }
    public long CurrencyId { get; set; }
    public int Count { get; set; }
    public decimal Price { get; set; }
    public bool IsDeleted { get; set; }
}