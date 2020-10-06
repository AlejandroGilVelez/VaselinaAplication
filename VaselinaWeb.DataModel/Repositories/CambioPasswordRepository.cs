using Framework.Data;
using Framework.Models;
using Org.BouncyCastle.Math.EC.Rfc7748;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace VaselinaWeb.DataModel.Repositories
{
    public class CambioPasswordRepository : GenericRepository<CambioPassword>, ICambioPasswordRepository
    {
        private readonly DataContext context;

        public CambioPasswordRepository(DataContext context) : base(context)
        {
            this.context = context;
        }

        public Task<int> DeletePorUser(Guid id)
        {
            var cambioPasswordList = context.CambiosPasswords.Where(x => x.Usuario.Id == id).ToList();
            int cont = 0;

            foreach (var item in cambioPasswordList)
            {
                context.CambiosPasswords.Remove(item);
                cont++;
            }

            return Task.FromResult(cont);
        }
    }
}
