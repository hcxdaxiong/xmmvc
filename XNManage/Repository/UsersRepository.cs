using System;
using System.Collections.Generic;
using System.Linq;
using XNManage.Common;
using XNManage.Models;

namespace XNManage.Repository
{
    public class UsersRepository : IUsersRepository
    {
        private readonly XNDb _db = new XNDb();
        private int _totalItem;

        public void Add(User user)
        {
            if (user == null) return;
            if (string.IsNullOrWhiteSpace(user.UserName) || string.IsNullOrWhiteSpace(user.PassWord)) throw new ArgumentNullException("名称或密码不能为空");
            var olduser = _db.Users.SingleOrDefault(n => n.UserName == user.UserName);
            if (olduser != null) throw new ArgumentNullException("该用户名已经存在"); ;
            user.PassWord = Encrypt.GetMd5Code(user.PassWord);
            _db.Users.Add(user);
            _db.SaveChanges();
        }

        public bool Remove(int id)
        {
            var user = _db.Users.FirstOrDefault(n => n.UserId == id);
            if (user == null) return false;
            _db.Users.Remove(user);
            _db.SaveChanges();
            return true;
        }

        public bool Update(User user)
        {
            var u = _db.Users.FirstOrDefault(n => n.UserId == user.UserId);
            if (u != null)
            {
                u.Description = user.Description;
                u.UserName = user.UserName;
                u.Email = user.Email;
                //   u.PassWord = Encrypt.GetMd5Code(user.PassWord);
                _db.SaveChanges();
                return true;
            }
            return false;
        }

        /// <summary>
        /// Finds the specified order.
        /// </summary>
        /// <param name="sort">The sort.</param>
        /// <param name="property">The property.</param>
        /// <param name="skip">The skip.</param>
        /// <param name="take">The take.</param>
        /// <returns>IEnumerable{User}.</returns>
        public IEnumerable<User> Find(string sort = "asc", string property = "UserName", int skip = 0, int take = 10)
        {
            var propertyInfo = typeof(User).GetProperty(property);
            Func<User, object> expn = e => propertyInfo.GetValue(e, null);
            var fakeProfiles = _db.Users;
            IEnumerable<User> users = sort == "asc" ? fakeProfiles.OrderBy(expn).Skip(skip).Take(take).ToArray()
                : fakeProfiles.OrderByDescending(expn).Skip(skip).Take(take).ToArray();
            return users;
        }

        /// <summary>
        /// Finds the name of the by.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>User.</returns>
        public User FindByName(string name)
        {
            return _db.Users.FirstOrDefault(n => n.UserName == name);
        }

        public User FindById(int userid)
        {
            return _db.Users.FirstOrDefault(n => n.UserId == userid);
        }

        /// <summary>
        /// Changes the password.
        /// </summary>
        /// <param name="userid">The userid.</param>
        /// <param name="password">The password.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise</returns>
        public bool ChangePassword(int userid, string password)
        {
            var u = _db.Users.FirstOrDefault(n => n.UserId == userid);
            if (u != null)
            {
                u.PassWord = Encrypt.GetMd5Code(password);
                _db.SaveChanges();
                return true;
            }
            return false;
        }

        /// <summary>
        /// Checks the password.
        /// </summary>
        /// <param name="userid">The userid.</param>
        /// <param name="old">The old.</param>
        /// <param name="password">The password.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise</returns>
        public bool CheckPassword(int userid, string old, string password)
        {
            var u = _db.Users.FirstOrDefault(n => n.UserId == userid);
            return u != null && u.PassWord == Encrypt.GetMd5Code(password);
        }

        public IEnumerable<User> FindAll()
        {
            return _db.Users;
        }

        /// <summary>
        /// 这个属性没有用上
        /// </summary>
        public int Count
        {
            get
            {
                _totalItem = _db.Users.Count();
                return _totalItem;
            }
            set { _totalItem = value; }
        }
    }
}