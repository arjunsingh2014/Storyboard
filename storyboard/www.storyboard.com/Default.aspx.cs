using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using storyboard.Utility;

namespace www.storyboard.com
{
    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                try
                {

                }
                catch (Exception ex)
                {
                    ex.Log(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                }
            }
        }
    }
}
