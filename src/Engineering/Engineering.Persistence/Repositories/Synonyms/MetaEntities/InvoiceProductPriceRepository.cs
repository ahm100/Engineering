using Engineering.Application.Abstractions.Data.MetaEntities;
using Engineering.Domain.Entities.Synonyms.Warehouse.Invoices;

namespace Engineering.Persistence.Repositories.Synonyms.MetaEntities;

public class ViewInvoiceProductPriceRepository : BaseRepository<EngineeringDBContext, ViewInvoiceProductPrice>, IViewInvoiceProductPriceRepository
{
    public ViewInvoiceProductPriceRepository(EngineeringDBContext context) : base(context)
    {
    }
}
