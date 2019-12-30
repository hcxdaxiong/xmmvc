using System;
using System.Collections.Generic;
using System.Linq;
using XNManage.Common;
using XNManage.Models;

namespace XNManage.Repository
{
    public class ProductRepository:IProductRepository
    {
        private readonly XNDb _db = new XNDb();

        public void Add(Product product)
        {
            if (product == null) return;
            var olduser = _db.Products.SingleOrDefault(n => n.ProductName == product.ProductName);
            if (olduser != null) throw new ArgumentNullException("该产品已经存在");
            _db.Products.Add(product);
            _db.SaveChanges();
        }

        public bool Remove(int id)
        {
            var pro = _db.Products.FirstOrDefault(n => n.ProductId == id);
            if (pro == null) return false;
            _db.Products.Remove(pro);
            _db.SaveChanges();
            return true;
        }

        public IEnumerable<Category> AllCategories()
        {
            return _db.Categories;
        }

        public void AddCategorie(Category category)
        {
            var olduser = _db.Categories.SingleOrDefault(n => n.CategoryName == category.CategoryName);
            if (olduser != null) throw new ArgumentNullException("该分类已经存在"); 
            _db.Categories.Add(category);
            _db.SaveChanges();
        }

        public bool RemoveCategorie(int categoryid)
        {
            var u = _db.Categories.FirstOrDefault(n => n.CategoryId == categoryid);
            if (u != null)
            {
                _db.Categories.Remove(u);
                _db.SaveChanges();
                return true;
            }
            return false;
        }


        public IEnumerable<Product> GetCategorieProducts(string category)
        {
            return _db.Products.Where(n => n.Category == category);
        }

        public bool UpdateCategorie(Category category)
        {
            if (category == null||string.IsNullOrWhiteSpace(category.CategoryName)) return false;
            var u = _db.Categories.FirstOrDefault(n => n.CategoryId == category.CategoryId);
            if (u == null) return false;
            if (u.CategoryName != category.CategoryName)
            {
                //所有的产品都要跟着改
                var prods = _db.Products.Where(n => n.Category == u.CategoryName);
                foreach (var product in prods)
                {
                    product.Category = category.CategoryName;
                }
                u.CategoryName = category.CategoryName;
            }
              
            u.Description = category.Description; 
            _db.SaveChanges(); return true;
        }

        public Category FindCategorieById(int categoryid)
        {
           return _db.Categories.FirstOrDefault(n => n.CategoryId == categoryid);
        }


        public bool Update(Product product)
        {
            var u = _db.Products.FirstOrDefault(n => n.ProductId == product.ProductId);
            if (u == null) return false;
            u.ProductName = product.ProductName;
            u.Category = product.Category;
            u.Brand = product.Brand;
            u.Standard = product.Standard;
            u.SaSaPrice = product.SaSaPrice;
            u.ZhuoyuePrice = product.ZhuoyuePrice;
            u.KaLaiPrice = product.KaLaiPrice;
            u.WanningPrice = product.WanningPrice;
            u.QuchengshiPrice = product.QuchengshiPrice;
            u.DrugstorePrice = product.DrugstorePrice;
            u.Others = product.Others;
            u.ZhuanGui = product.ZhuanGui;
            u.YaShi = product.YaShi;
            u.CreateTime = DateTime.Now;
            _db.SaveChanges();
            return true;
        }

        public IEnumerable<Product> Find(string sort = "asc", string property = "ProductName", int skip = 0, int take = 10)
        {
            var propertyInfo = typeof(Product).GetProperty(property);
            Func<Product, object> expn = e => propertyInfo.GetValue(e, null);
            var rawpros = _db.Products;
            IEnumerable<Product> pros = sort == "asc" ? rawpros.OrderBy(expn).Skip(skip).Take(take).ToArray()
                : rawpros.OrderByDescending(expn).Skip(skip).Take(take).ToArray();
            return pros;
        }

        public Product FindByName(string name)
        {
            return _db.Products.FirstOrDefault(n => n.ProductName == name);
        }

        public Product FindById(int productid)
        {
            return _db.Products.FirstOrDefault(n => n.ProductId == productid);
        }

        public IEnumerable<Product> FindAll()
        {
            return _db.Products;
        }

        public int Count
        {
            get { return _db.Users.Count(); }
        }
    }
}