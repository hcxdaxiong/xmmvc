using System.Collections.Generic;
using XNManage.Models;

namespace XNManage.Repository
{
    public interface IProductRepository  
    {
        void Add(Product product);
        bool Remove(int id);
        bool Update(Product product);
        IEnumerable<Product> Find(string sort = "asc", string property = "ProductName", int skip = 0, int take = 10);
        Product FindByName(string name);
        Product FindById(int productid);
        IEnumerable<Product> FindAll();
        int Count { get; } 
        
        IEnumerable<Category> AllCategories();
        void AddCategorie(Category category);
        bool RemoveCategorie(int categoryid);
        bool UpdateCategorie(Category category);
        Category FindCategorieById(int categoryid);

        IEnumerable<Product> GetCategorieProducts(string category);
    }
}