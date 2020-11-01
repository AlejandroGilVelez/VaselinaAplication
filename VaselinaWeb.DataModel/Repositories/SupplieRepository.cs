using Framework.Data;
using Framework.Models;

namespace VaselinaWeb.DataModel.Repositories
{
    public class SupplieRepository : GenericRepository<Supplie>, ISupplierRepository
    {
        public SupplieRepository(DataContext context) : base(context)
        {
        }
    }
}
