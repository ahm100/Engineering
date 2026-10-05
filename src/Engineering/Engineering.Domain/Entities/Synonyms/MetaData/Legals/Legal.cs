using System.ComponentModel.DataAnnotations.Schema;

namespace Engineering.Domain.Entities.Synonyms.MetaData.Legals;

[NotMapped]
public class ViewLegal : ActivateEntity<ViewLegal>
{
    private ViewLegal()
    {
    }

    public string? LogoUrl { get; private set; }
    public string? CompanyName { get; private set; }
    public string? RegistrationNo { get; private set; }
    public string? RegistrationDate { get; private set; }
    public string? RegistrationLocation { get; private set; }
}