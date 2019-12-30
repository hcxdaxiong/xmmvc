using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XNManage.Common;
using XNManage.Models;

namespace XNManage.Controllers
{
    public class BaseController : Controller
    {
        private int _userId;
        private User _curUser;

        public int CheckValid()
        {
            if (_userId != 0)
            {
                return _userId;
            }

            if (Session["uid"] != null)
            {
                _userId = Convert.ToInt16(Session["uid"].ToString());
                return _userId;
            }
            var cook = Request.Cookies["xnuid"];
            if (cook != null)
            {
                var uid = Convert.ToInt16(cook.Value);
                Session["uid"] = uid;
                Session.Timeout = 600;
              
                _userId = uid;
                return uid;
            }
            return -1;
        }

        public void DataBaseInit()
        {
            using ( var db = new XNDb())
            {
                if (db.Users.FirstOrDefault(n => n.UserName == "Admin") == null)
                {
                    db.Users.Add(new User()
                    {
                        UserName = "Admin",
                        Description = "管理员",
                        PassWord = Encrypt.GetMd5Code("admin"),
                    });
                    db.SaveChanges();
                }
            }
        }

        public User GetMyself()
        {
            using (var db = new XNDb())
            {
                var id = CheckValid();
                if (id != -1)
                {
                    return _curUser ?? (_curUser = db.Users.FirstOrDefault(n => n.UserId == id));
                }

            }
            return null;
        }
    }
}