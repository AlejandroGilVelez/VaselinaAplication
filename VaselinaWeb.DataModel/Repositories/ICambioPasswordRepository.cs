using Framework.Models;
using System;
using System.Threading.Tasks;

namespace VaselinaWeb.DataModel.Repositories
{
    public interface ICambioPasswordRepository : IGenericRepository<CambioPassword>
    {
        Task<int> DeletePorUser(Guid id);
    }
}
