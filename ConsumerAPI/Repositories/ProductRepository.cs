using ConsumerAPI.Data;
using ConsumerAPI.Models.Products;
using ConsumerAPI.Repositories.IRepository;

namespace ConsumerAPI.Repositories
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly NorthwindContext _db;
        public ProductRepository(NorthwindContext db) : base(db)
        {
            _db = db;            
        }
    }
}
