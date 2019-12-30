using System;
using System.Collections.Generic;
using System.Globalization;
using System.Web.Mvc;
using XNManage.Common;
using XNManage.Models;
using XNManage.Repository;
using XNManage.ViewModel;

namespace XNManage.Controllers
{
    public class UserController : BaseController
    {
        //
        // GET: /User/

        private IUsersRepository _repository;

        public UserController(IUsersRepository repository)
        {
            _repository = repository;
        }

        [LoginValid]
        public ActionResult Index()
        {
            var users = _repository.FindAll();
            return View(users);
        }


        public JsonResult GetAllUsers(string sord = "asc", string sidx = "UserName", int page = 1, int rows = 10)
        {
            IList<object> users = new List<object>(_repository.Find(sord, sidx, (page - 1) * rows, rows));
            var jsonData = JqGridModel.GridData(page, rows, _repository.Count, users);
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

    
        //
        // POST: /User/Create

        [HttpPost]
        public ActionResult Create(User user)
        {
             _repository.Add(user);
            var fu = _repository.FindByName(user.UserName);
            return Json(fu, JsonRequestBehavior.AllowGet);
        }

    

        //
        // POST: /User/Edit/5

        [HttpPost]
        public ActionResult Edit(User user, string oper,int id)
        {
            if (oper == "edit")
            {
                _repository.Update(user);
            }
            return Json(user, JsonRequestBehavior.AllowGet);
        }


        public ActionResult EditInfo()
        {

            return View();
        }

        /// <summary>
        /// 检查旧密码是否正确
        /// </summary>
        /// <param name="code"></param>
        /// <param name="oldcode"></param>
        /// <returns></returns>
        public JsonResult CheckOldCode(string code, string oldcode)
        {
            return _repository.CheckPassword(CheckValid(),oldcode,code) ? Json(1) : Json(0);
        }

        public JsonResult SaveNewCode(string newcode)
        {
            return _repository.ChangePassword(CheckValid(), newcode)?Json(1):Json(0);
        }

        //
        // POST: /User/Delete/5

        [HttpPost]
        public ActionResult Delete(int id)
        {
            _repository.Remove(id);
            return Json(id);
        }
        #region 登陆退出

        public ActionResult Logon()
        {
            DataBaseInit();
            if (CheckValid() != -1)
            {
                return RedirectToAction("Index", "Admin");
            }
            return View();
        }

        [HttpPost]
        public ActionResult Logon(LogOnModel model)
        {
            var user = _repository.FindByName(model.UserName);
            if (user != null)
            {
                var pd = Encrypt.GetMd5Code(model.Password);
                if (pd == user.PassWord)
                {
                    Session["uid"] = user.UserId;
                    Session["uname"] = user.UserName;
                    Session.Timeout = 600;
                    if (model.RememberMe)//存储密码及用户名
                    {
                        var httpCookie = Response.Cookies["fdpwd"];
                        if (httpCookie != null)
                        {
                            httpCookie.Value = pd;
                            httpCookie.Expires = DateTime.Now.AddDays(30);
                        }
                        var uidCookie = Response.Cookies["xnuid"];//密码不用存吧？
                        if (uidCookie != null)
                        {
                            uidCookie.Value = user.UserId.ToString(CultureInfo.InvariantCulture);
                            uidCookie.Expires = DateTime.Now.AddDays(30);
                        }
                    }

                    return Json(user.UserId);
                }

                return Json("密码错误");
            }
            return Json("用户名不存在");
        }

        /// <summary>
        /// 退出函数 还需要处理，退出时统计退出时间,然后关闭网页。
        /// </summary>
        /// <returns></returns>
        public ActionResult LogOff()
        {
            Session.RemoveAll();
            Session.Abandon();
            var httpCookie = Response.Cookies["fdpwd"];
            if (httpCookie != null)
                httpCookie.Expires = DateTime.Now.AddDays(0);
            var uidCookie = Response.Cookies["xnuid"];
            if (uidCookie != null)
            {
                uidCookie.Expires = DateTime.Now.AddDays(0);
            }
            return RedirectToAction("Logon");
        }

        #endregion 
    }
}
