using System.ComponentModel.DataAnnotations.Schema;

namespace Engineering.Domain.Entities.Synonyms.MetaData.MeasureUnits;

[NotMapped]
public class MeasureUnitGroup : ActivateEntity<MeasureUnitGroup, long>
{
    public string Code { get; private set; }
    public string Name { get; private set; }
    public long CompanyId { get; private set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

    private readonly IList<MeasureUnit> _measureUnits;
    public IEnumerable<MeasureUnit> MeasureUnits => _measureUnits.AsReadOnly();

    private MeasureUnitGroup()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    {
    }
}