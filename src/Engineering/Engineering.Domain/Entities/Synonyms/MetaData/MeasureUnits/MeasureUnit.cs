using Engineering.Domain.Entities.Synonyms.Warehouse.Groups;
using System.ComponentModel.DataAnnotations.Schema;

namespace Engineering.Domain.Entities.Synonyms.MetaData.MeasureUnits;

[NotMapped]
public class MeasureUnit : ActivateEntity<MeasureUnit, long>
{
    public string Name { get; private set; }
    public string? NameEn { get; private set; }
    public decimal ConversionFactor { get; private set; }
    public decimal Tolerance { get; private set; }
    public bool IsPrimary { get; private set; }
    public long CompanyId { get; private set; }
    public MeasureUnitGroup MeasureUnitGroup { get; private set; }
    public long MeasureUnitGroupId { get; private set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    private readonly IList<ViewGroup> _groups;
    public IEnumerable<ViewGroup> Groups => _groups.AsReadOnly();
    private MeasureUnit()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    {
        _groups = [];
    }
}