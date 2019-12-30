using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XNManage.Common;
using XNManage.Models;

namespace XNManage.Controllers
{
    public class AdminController : BaseController
    {
        //
        // GET: /Admin/
         [LoginValid]
        public ActionResult Index()
        { 
            return View();
        }


    }
}
