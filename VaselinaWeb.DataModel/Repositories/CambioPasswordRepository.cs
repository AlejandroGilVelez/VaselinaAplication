using Framework.Data;
using Framework.Models;

namespace VaselinaWeb.DataModel.Repositories
{
    public class CambioPasswordRepository : GenericRepository<CambioPassword>, ICambioPasswordRepository
    {
        public CambioPasswordRepository(DataContext context) : base(context)
        {
        }
    }
}
