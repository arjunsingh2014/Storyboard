using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace storyboard.DAL
{
    /// <summary>
    /// This Class Mainly use to store Stored Procedure, Function or any kind of DB object names at common place
    /// All the variables containing object name must be declared as 'constant'
    /// Use only variable names during database calls
    /// </summary>
    public class DALCommon
    {
        //ALL THE STORED PROCEDURE NAME AS A Constant 
        #region STORED PROCEDURE NAME

        public const string SAVE_USER = "UserSave";
        public const string GET_USER = "UserGet";

        #endregion
    }
}
