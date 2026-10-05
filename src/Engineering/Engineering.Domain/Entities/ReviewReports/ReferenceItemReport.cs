using Engineering.Domain.Entities.RequestGoodsSupplies.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace Engineering.Domain.Entities.ReviewReports;

[NotMapped]
public class ReferenceItemReport
{
    public long Id { get; set; }

    public SupplyType SupplyType { get; set; }

    public string? Name { get; set; }

    public string? NameEn { get; set; }

    public string? Code { get; set; }

    public string? TechnicalCode { get; set; }

    public DateTime Created { get; set; }

    public long CompanyId { get; set; }
}