
namespace MinimalWebAPI.Repositories
{
    public interface IProductRepository
    {
        Product GetProduct(int id);
        List<Product> GetProducts();
        void AddProduct(Product product);
        void UpdateProduct(Product product);
        void DeleteProduct(Product product);
        void DeleteProduct(int Id);
    }
}
