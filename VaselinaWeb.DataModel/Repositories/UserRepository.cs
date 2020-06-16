using Framework.Data;
using Framework.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace VaselinaWeb.DataModel.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private new readonly DataContext context;
        public UserRepository(DataContext context) : base(context)
        {
            this.context = context;
        }

        public async Task<User> ObtenerPorCorreo(string correo)
        {
            return await context.Users.FirstOrDefaultAsync(x => x.Activo && x.Correo.ToLower() == correo.ToLower());
        }
    }
}
