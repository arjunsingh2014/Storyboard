using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using storyboard.Entity;
using storyboard.DAL;
using storyboard.Utility;

namespace storyboard.BLL.Data
{
    /// <summary>
    /// All the Database transaction methods should be written in this class and must be static
    /// </summary>
    internal class UserManagerDB
    {
        #region Save or Update User

        internal static int SaveUser(User user)
        {
            int returnValue = default(int);
            try
            {
                string spName = DALCommon.SAVE_USER;
                SqlParameter[] param = new SqlParameter[]
                    {
                        new SqlParameter("@Id", user.ID),
                        new SqlParameter("@FirstName", user.FirstName),
                        new SqlParameter("@MiddleName", user.MiddleName),
                        new SqlParameter("@LastName", user.LastName),
                        new SqlParameter("@UserName", user.UserName),
                        new SqlParameter("@Password", user.Password),
                        new SqlParameter("@Email", user.Email),
                        new SqlParameter("@CreatedBy", user.CreatedBy),
                        new SqlParameter("@CreatedDate", user.CreatedDate),
                        new SqlParameter("@ModifiedBy", user.ModifiedBy),
                        new SqlParameter("@ModifiedDate", user.ModifiedDate),
                        new SqlParameter("@Active", user.Active)
                    };
                returnValue = SqlHelper.ExecuteNonQuery(ConfigurationSettings.ConnectionString, CommandType.StoredProcedure, spName, param);
            }
            catch (Exception ex)
            {
                //Log Exception in logger here....
                ex.Log(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            }

            //Return Result
            return returnValue;
        }

        #endregion

        #region Get User

        internal static List<User> Get(int ID)
        {
            List<User> userList = new List<User>();
            try
            {
                string spName = DALCommon.GET_USER;
                SqlParameter[] param = new SqlParameter[]
                    {
                        new SqlParameter("@ID", ID)
                    };
                userList = SqlHelper.GetEntityList<User>(ConfigurationSettings.ConnectionString, CommandType.StoredProcedure, spName, param);
            }
            catch (Exception ex)
            {
                //Log Exception in logger here....
                ex.Log(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            }
            return userList;
        }

        #endregion
    }
}
