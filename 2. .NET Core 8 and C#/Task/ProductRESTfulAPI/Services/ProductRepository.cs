using ProductRESTfulAPI.DbContexts;
using ProductRESTfulAPI.Models;

namespace ProductRESTfulAPI.Services;

public class ProductRepository : GenericRepository<Product>
{
	public ProductRepository(AppDbContext context) : base(context)
	{

	}
}
