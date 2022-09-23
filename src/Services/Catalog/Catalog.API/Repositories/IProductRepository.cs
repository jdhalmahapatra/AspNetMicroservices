using System;
using Catalog.API.Entities;

namespace Catalog.API.Repositories
{
    public interface IProductRepository
    {
        //For: Get All Products 
        Task<IEnumerable<Product>> GetProducts();

        //For: Get Procut by Id
        Task<Product> GetProductById(string id);

        //For: Get Products by Name
        Task<IEnumerable<Product>> GetProductsByName(string name);

        //For: Get Products by Category
        Task<IEnumerable<Product>> GetProductsByCategory(string category);

        //For: Create Prouct
        Task CreateProduct(Product product);

        //For: Udpate Product
        Task<bool> UpdateProduct(Product product);

        //For: Delete Product by Id
        Task<bool> DeleteProdcut(string id);

    }
}

