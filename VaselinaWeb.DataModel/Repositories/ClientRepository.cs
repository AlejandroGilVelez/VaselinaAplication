using Framework.Data;
using Framework.Models;

namespace VaselinaWeb.DataModel.Repositories
{
    public class ClientRepository : GenericRepository<Client>, IClientRepository
    {
        public ClientRepository(DataContext context) : base(context)
        {
        }
    }
}
