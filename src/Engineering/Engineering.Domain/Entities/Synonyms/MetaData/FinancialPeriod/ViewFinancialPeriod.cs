using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engineering.Domain.Entities.Synonyms.MetaData.FinancialPeriod
{

    public class ViewFinancialPeriod : ActivateEntity<ViewFinancialPeriod>
    {
        public string Code { get; private set; }
        public string NameFa { get; private set; }
        public string NameEn { get; private set; }

#pragma warning disable CS8618
        private ViewFinancialPeriod() { }
#pragma warning restore CS8618
    }
}
