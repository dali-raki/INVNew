using INV.Domain.Entities.Products;
using INV.Domain.Shared;

namespace INV.App.Products
{
    public interface IProductService
    {
        ValueTask<Result> CreateProduct(Product product);

        ValueTask<Result> SetProducts(Product product);

        ValueTask<Result> RemoveProduct(Guid id);

        ValueTask<Result<List<Product>>> GetProducts();

        ValueTask<Result<ProductInfo>> GetProductById(Guid id);
    }
}