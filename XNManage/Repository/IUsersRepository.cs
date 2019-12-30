using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using XNManage.Models;

namespace XNManage.Repository
{
    public interface IUsersRepository  
    {
        void Add(User user);
        bool Remove(int  id);
        bool Update(User user);
        IEnumerable<User> Find(string sort = "asc", string property = "UserName", int skip = 0, int take = 10);
        User FindByName(string name);
        User FindById (int userid);
        bool CheckPassword(int userid, string old, string password);
        bool ChangePassword(int userid, string password);
        IEnumerable<User> FindAll();
        int Count { get; set; }
       
    }
}