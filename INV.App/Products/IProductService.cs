using INV.Domain.Entities.Products;
using INV.Domain.Shared;

namespace INV.App.Products
{
    public interface IProductService
    {
        Task<Result> CreateProduct(Product product);

        Task<int> SetProducts(Product product);

        ValueTask<Result> RemoveProduct(Guid id);

        Task<List<Product>> GetProducts();

        ValueTask<ProductInfo> GetProductById(Guid id);
    }
}