using Framework.Data;
using Framework.Models;
using Microsoft.EntityFrameworkCore;

namespace VaselinaWeb.DataModel.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(DataContext context) : base(context)
        {
        }
    }
}
