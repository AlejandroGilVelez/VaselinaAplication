using Framework.Data;
using Framework.Models;

namespace VaselinaWeb.DataModel.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(DataContext context) : base(context)
        {
        }
    }
}
