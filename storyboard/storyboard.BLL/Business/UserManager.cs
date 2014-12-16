using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using storyboard.Entity;
using storyboard.BLL.Data;

namespace storyboard.BLL.Business
{
    /// <summary>
    /// This is a common class and use to intract with UI Layer and DB Layer.
    /// all method declation should be public here
    /// </summary>
    public class UserManager
    {
        #region Methods

        public int SaveUser(User user)
        {
            return UserManagerDB.SaveUser(user);
        }
        public List<User> Get(int ID)
        {
            return UserManagerDB.Get(ID);
        }

        #endregion
    }
}
