using Framework.Models;
using System.Threading.Tasks;

namespace VaselinaWeb.DataModel.Repositories
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<User> ObtenerPorCorreo(string correo);
    }
}
