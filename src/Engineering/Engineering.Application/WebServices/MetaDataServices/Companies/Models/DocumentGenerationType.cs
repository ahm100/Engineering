using System.ComponentModel;

namespace Engineering.Application.WebServices.MetaDataServices.Companies.Models;

public enum DocumentGenerationType
{
    [Description("ماهانه")] Monthly = 1,

    [Description("سالانه")] Yearly = 2,
}
