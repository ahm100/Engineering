using Engineering.Domain.Entities.Synonyms.MetaData.Addresses;
using Engineering.Domain.Entities.Synonyms.MetaData.Regions;

namespace Engineering.Domain.Entities.Synonyms.MetaData.Cities;

public class ViewCity : ActivateEntity<ViewCity>
{
    public long ProvinceId { get; set; }
    public string Code { get; set; }
    public string? EnglishName { get; set; }
    public string? Iso { get; set; }
    public string Name { get; set; }

    private readonly IList<ViewAddress> _addresses;
    public IEnumerable<ViewAddress> Addresses => _addresses.AsReadOnly();

    private readonly IList<ViewRegion> _regions;
    public IEnumerable<ViewRegion> Regions => _regions.AsReadOnly();


#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    private ViewCity()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    {
        _addresses = [];
        _regions = [];
    }

}