using Framework.Data;
using Framework.Models;

namespace VaselinaWeb.DataModel.Repositories
{
    public class ProviderRepository : GenericRepository<Provider>, IProviderRepository
    {
        public ProviderRepository(DataContext context) : base(context)
        {
        }
    }
}
