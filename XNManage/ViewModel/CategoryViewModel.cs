using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using XNManage.Models;

namespace XNManage.ViewModel
{
    public class CategoryViewModel
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }
        public int ProductCount { get; set; }
    }
}