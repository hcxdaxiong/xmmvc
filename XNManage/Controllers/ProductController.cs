using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Web;
using System.Web.Mvc;
using XNManage.Common;
using XNManage.Models;
using XNManage.Repository;
using XNManage.ViewModel;

namespace XNManage.Controllers
{
    public class ProductController : Controller
    {
        //
        // GET: /Product/
        private readonly IProductRepository _repository;

        private IEnumerable<CategoryViewModel> _categoryViewModels = new List<CategoryViewModel>(); 


        public ProductController(IProductRepository repository)
        {
            _repository = repository;
        }

        #region 产品

        [LoginValid]
        public ActionResult Index()
        {
            //CategoryInit();
            //ProductsInits();
            return View();
        }


        public JsonResult GetAllPros( string sord = "asc", string sidx = "ProductName",int page = 1, 
            int rows = 10, bool _search = false,string searchField="", string searchOper="", string searchString="")
        {

            var rawpros = GetRawPros(_search, searchField, searchOper, searchString);
            var propertyInfo = typeof(Product).GetProperty(switchSidx(sidx));
            Func<Product, object> expn = e => propertyInfo.GetValue(e, null);

            var enumerable = rawpros as Product[] ?? rawpros.ToArray();
            IEnumerable<Product> pros = sord == "asc" ? enumerable.OrderBy(expn).Skip((page - 1) * rows).Take(rows).ToArray()
                : enumerable.OrderByDescending(expn).Skip((page - 1) * rows).Take(rows).ToArray();

            var objpros = new List<object>(pros);
            var jsonData = JqGridModel.GridData(page, rows, enumerable.Count(), objpros);
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        private IEnumerable<Product> GetRawPros(bool search = false, string searchField = "", string searchOper = "", string searchString = "")
        {
            var rawpros = _repository.FindAll();
            if (!search) return rawpros;

            switch (switchSidx(searchField))
            {
                case "ProductName":
                    switch (searchOper)
                    {
                        case "cn":
                            rawpros = rawpros.Where(n => n.ProductName.Contains(searchString));
                            break;
                        case "eq":
                            rawpros = rawpros.Where(n => n.ProductName == searchString);
                            break;
                        case "ne":
                            rawpros = rawpros.Where(n => n.ProductName != searchString);
                            break;
                        default:
                            rawpros = rawpros.Where(n => n.ProductName.Contains(searchString));
                            break;
                    }
                    break;

                case "Category":
                    switch (searchOper)
                    {
                        case "cn":
                            rawpros = rawpros.Where(n => n.Category.Contains(searchString));
                            break;
                        case "eq":
                            rawpros = rawpros.Where(n => n.Category == searchString);
                            break;
                        case "ne":
                            rawpros = rawpros.Where(n => n.Category != searchString);
                            break;
                        default:
                            rawpros = rawpros.Where(n => n.Category.Contains(searchString));
                            break;
                    }
                    break;
                case "Brand":
                    switch (searchOper)
                    {
                        case "cn":
                            rawpros = rawpros.Where(n => n.Brand.Contains(searchString));
                            break;
                        case "eq":
                            rawpros = rawpros.Where(n => n.Brand == searchString);
                            break;
                        case "ne":
                            rawpros = rawpros.Where(n => n.Brand != searchString);
                            break;
                        default:
                            rawpros = rawpros.Where(n => n.Brand.Contains(searchString));
                            break;
                    }
                    break;
                case "SaSaPrice":
                    switch (searchOper)
                    {
                        case "cn":
                            rawpros = rawpros.Where(n => n.SaSaPrice >= Convert.ToDecimal(searchString));
                            break;
                        case "eq":
                            rawpros = rawpros.Where(n => n.SaSaPrice == Convert.ToDecimal(searchString));
                            break;
                        case "ne":
                            rawpros = rawpros.Where(n => n.SaSaPrice != Convert.ToDecimal(searchString));
                            break;
                    }
                    break;
                case "ZhuanGui":
                    switch (searchOper)
                    {
                        case "cn":
                            rawpros = rawpros.Where(n => n.ZhuanGui >= Convert.ToDecimal(searchString));
                            break;
                        case "eq":
                            rawpros = rawpros.Where(n => n.ZhuanGui == Convert.ToDecimal(searchString));
                            break;
                        case "ne":
                            rawpros = rawpros.Where(n => n.ZhuanGui != Convert.ToDecimal(searchString));
                            break;
                    }
                    break;
                case "YaShi":
                    switch (searchOper)
                    {
                        case "cn":
                            rawpros = rawpros.Where(n => n.YaShi >= Convert.ToDecimal(searchString));
                            break;
                        case "eq":
                            rawpros = rawpros.Where(n => n.YaShi == Convert.ToDecimal(searchString));
                            break;
                        case "ne":
                            rawpros = rawpros.Where(n => n.YaShi != Convert.ToDecimal(searchString));
                            break;
                    }
                    break;
                case "ZhuoyuePrice":
                    switch (searchOper)
                    {
                        case "cn":
                            rawpros = rawpros.Where(n => n.ZhuoyuePrice >= Convert.ToDecimal(searchString));
                            break;
                        case "eq":
                            rawpros = rawpros.Where(n => n.ZhuoyuePrice == Convert.ToDecimal(searchString));
                            break;
                        case "ne":
                            rawpros = rawpros.Where(n => n.ZhuoyuePrice != Convert.ToDecimal(searchString));
                            break;
                    }
                    break;
                case "WanningPrice":
                    switch (searchOper)
                    {
                        case "cn":
                            rawpros = rawpros.Where(n => n.WanningPrice >= Convert.ToDecimal(searchString));
                            break;
                        case "eq":
                            rawpros = rawpros.Where(n => n.WanningPrice == Convert.ToDecimal(searchString));
                            break;
                        case "ne":
                            rawpros = rawpros.Where(n => n.WanningPrice != Convert.ToDecimal(searchString));
                            break;
                    }
                    break;
                case "QuchengshiPrice":
                    switch (searchOper)
                    {
                        case "cn":
                            rawpros = rawpros.Where(n => n.QuchengshiPrice >= Convert.ToDecimal(searchString));
                            break;
                        case "eq":
                            rawpros = rawpros.Where(n => n.QuchengshiPrice == Convert.ToDecimal(searchString));
                            break;
                        case "ne":
                            rawpros = rawpros.Where(n => n.QuchengshiPrice != Convert.ToDecimal(searchString));
                            break;
                    }
                    break;
                case "DrugstorePrice":
                    switch (searchOper)
                    {
                        case "cn":
                            rawpros = rawpros.Where(n => n.DrugstorePrice >= Convert.ToDecimal(searchString));
                            break;
                        case "eq":
                            rawpros = rawpros.Where(n => n.DrugstorePrice == Convert.ToDecimal(searchString));
                            break;
                        case "ne":
                            rawpros = rawpros.Where(n => n.DrugstorePrice != Convert.ToDecimal(searchString));
                            break;
                    }
                    break;
                case "Others":
                    switch (searchOper)
                    {
                        case "cn":
                            rawpros = rawpros.Where(n => n.Others.Contains(searchString));
                            break;
                        case "eq":
                            rawpros = rawpros.Where(n => n.Others ==searchString);
                            break;
                        case "ne":
                            rawpros = rawpros.Where(n => n.Others !=searchString);
                            break;
                    }
                    break;
                case "KaLaiPrice":
                    switch (searchOper)
                    {
                        case "cn":
                            rawpros = rawpros.Where(n => n.KaLaiPrice >= Convert.ToDecimal(searchString));
                            break;
                        case "eq":
                            rawpros = rawpros.Where(n => n.KaLaiPrice == Convert.ToDecimal(searchString));
                            break;
                        case "ne":
                            rawpros = rawpros.Where(n => n.KaLaiPrice != Convert.ToDecimal(searchString));
                            break;
                    }
                    break;

                default:
                    break;
            }

            return rawpros;
        } 


        /*
        public JsonResult GetData(GridSettings grid)
        {

            var query = _repository.FindAll();

            //filtring
            if (grid.IsSearch)
            {
                //And
                if (grid.Where.groupOp == "AND")
                    foreach (var rule in grid.Where.rules)
                        query = query.Where<Product>(
                            rule.field, rule.data,
                            (WhereOperation)StringEnum.Parse(typeof(WhereOperation), rule.op));
                else
                {
                    //Or
                    var temp = (new List<Product>()).AsQueryable();
                    foreach (var rule in grid.Where.rules)
                    {
                        var t = query.Where<Product>(
                        rule.field, rule.data,
                        (WhereOperation)StringEnum.Parse(typeof(WhereOperation), rule.op));
                        temp = temp.Concat<Product>(t);
                    }
                    //remove repeating records
                    query = temp.Distinct<Product>();
                }
            }

            //sorting
            query = query.OrderBy<Product>(grid.SortColumn,
                grid.SortOrder);

            //count
            var count = query.Count();

            //paging
            var data = query.Skip((grid.PageIndex - 1) * grid.PageSize).Take(grid.PageSize).ToArray();

            //converting in grid format
            var result = new
            {
                total = (int)Math.Ceiling((double)count / grid.PageSize),
                page = grid.PageIndex,
                records = count,
                rows = data.ToArray()
            };

            //convert to JSON and return to client
            return Json(result, JsonRequestBehavior.AllowGet);

        }
        */


        //这个函数其实不必要！index中可以写英文。列名和index没有关系
        private string switchSidx(string sidx)
        {
            switch (sidx)
            {
                case "型号":
                    return "ProductName";
                case "类别":
                    return "Category";
                case "品牌":
                    return "Brand";
                case "规格":
                    return "Standard";
                case "专柜":
                    return "ZhuanGui";
                case "莎莎":
                    return "SaSaPrice";
                case "雅施":
                    return "YaShi";
                case "卓越":
                    return "ZhuoyuePrice";
                case "卡莱美":
                    return "KaLaiPrice";
                case "万宁":
                    return "WanningPrice";
                case "屈臣氏":
                    return "QuchengshiPrice";
                case "药店":
                    return "DrugstorePrice";
                case "其他":
                    return "Others";
                case "描述":
                    return "Description";
                case "更新时间":
                    return "CreateTime";
                default:
                    return "ProductName";
            }
        }

        public string CategorySelectList()
        {
            var cs = _repository.AllCategories();
            var sb=new StringBuilder("<select>");

            foreach (var category in cs)
            {
                sb.Append(string.Format("<option value='{0}' >{1}</option>", category.CategoryName, category.CategoryName));
            }
            sb.Append("</select>");
            return sb.ToString();
        }

        [HttpPost]
        public ActionResult Edit(Product pro, string oper, int id)
        {
            if (oper == "edit")
            {
                _repository.Update(pro);
                pro.CreateTime = DateTime.Now;
            }
            return Json(pro, JsonRequestBehavior.AllowGet);
        }

        //
        // POST: /Product/Create

        [HttpPost]
        public ActionResult Create(Product product)
        {
            _repository.Add(product);
            var fu = _repository.FindByName(product.ProductName);
            return Json(fu, JsonRequestBehavior.AllowGet);
        }

     
       
        //
        // POST: /Product/Delete/5

        [HttpPost]
        public ActionResult Delete(int id)
        {
            _repository.Remove(id);
            return Json(id);
        }

        private void CategoryInit()
        {
            if (!_repository.AllCategories().Any())
            {
                _repository.AddCategorie(new Category
                {
                    CategoryName = "护肤品",
                    Description = "春天女生护肤专用"
                });
                _repository.AddCategorie(new Category()
                {
                    CategoryName = "防晒",
                    Description = "夏日喷雾防晒"
                }
               );
            }
        }
        private void ProductsInits()
        {
            if (!_repository.FindAll().Any())
            {
                _repository.Add(new Product()
                {
                    ProductName = "水宝宝",
                    Others = "防晒",
                    SaSaPrice = 78,
                    DrugstorePrice = 98
                });
            }
        }
        #endregion 

        #region 分类

        private IEnumerable<CategoryViewModel> initCategoryViewModels()
        {
            if (!_categoryViewModels.Any())
            {
                var cas = _repository.AllCategories();
                _categoryViewModels = cas.Select(n => new CategoryViewModel()
                {
                    CategoryId = n.CategoryId,
                    CategoryName = n.CategoryName,
                    Description = n.Description,
                    ProductCount = _repository.GetCategorieProducts(n.CategoryName).Count()
                });
            }
            return _categoryViewModels;
        }
        public JsonResult GetAllCategorys(string sord = "asc", string sidx = "CategoryName", int page = 1, int rows = 10)
        {
            IList<object> pros = new List<object>(FindCategoryViewModels(sord, sidx, (page - 1) * rows, rows));
            var jsonData = JqGridModel.GridData(page, rows, initCategoryViewModels().Count(), pros);
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        

        public IEnumerable<CategoryViewModel> FindCategoryViewModels(string sort = "asc",
            string property = "CategoryName", int skip = 0, int take = 10)
        {
            var propertyInfo = typeof(CategoryViewModel).GetProperty(property);
            Func<CategoryViewModel, object> expn = e => propertyInfo.GetValue(e, null);
            var fakeProfiles =initCategoryViewModels();
            IEnumerable<CategoryViewModel> pros = sort == "asc" ? fakeProfiles.OrderBy(expn).Skip(skip).Take(take).ToArray()
                : fakeProfiles.OrderByDescending(expn).Skip(skip).Take(take).ToArray();
            return pros;
        }

        public ActionResult EditCategory(CategoryViewModel model,string oper, int id)
        {
            if (oper == "edit")
            {
                var ca = new Category()
                {
                    CategoryId = model.CategoryId,
                    CategoryName = model.CategoryName,
                    Description = model.Description,
                };
                _repository.UpdateCategorie(ca);
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CategoryList()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateCategory(Category category)
        {
            _repository.AddCategorie(category);
            var fu = _repository.FindCategorieById(category.CategoryId);

            var cm = new CategoryViewModel()
            {
                CategoryName = fu.CategoryName,
                CategoryId = fu.CategoryId,
                Description = fu.Description,
                ProductCount = _repository.GetCategorieProducts(fu.CategoryName).Count()
            };

            return Json(cm, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeletCategory(int id)
        {
            _repository.RemoveCategorie(id);
            return Json(1);
        }

        public PartialViewResult JqgridInit()
        {

            return PartialView();
        }

        public JsonResult GetChartData()
        {
            var cms = initCategoryViewModels().ToList();
            var count =_repository.Count;
            var chartDatas = cms.Select(n => new ChartData
            {
                label = n.CategoryName,
                data =  Math.Round((double)n.ProductCount / count, 3),
            }).ToArray();
            return Json(chartDatas, JsonRequestBehavior.AllowGet);
        }

        #endregion 

    }
}
