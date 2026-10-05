using Engineering.ClientSdk.Enums;

namespace Engineering.ClientSdk.Models.TransportationContractor;

public class GetTransportationContractorRequestDto
{
    public List<long>? Ids { get; set; }
    public List<long>? ThirdPartyIds { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public List<DeliveryMethod>? deliveryMethods { get; set; }
    public List<DeliveryType>? deliveryTypes { get; set; }
    public string? FilterData { get; set; }
    public bool? IsActive { get; set; }
    public int PageIndex { get; set; }
    public int PageSize { get; set; }
}
