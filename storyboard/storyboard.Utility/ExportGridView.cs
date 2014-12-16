using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web;

namespace storyboard.Utility
{
    public class ExportGridView
    {
        public static void GridViewExport(GridView objGridView, string strFileName, string strHeaderText)
        {
            #region Working
            try
            {
                HttpContext.Current.Response.Clear();
                HttpContext.Current.Response.Buffer = true;
                HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=" + strFileName + ".xls");
                HttpContext.Current.Response.Charset = "";
                HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
                StringWriter sw = new StringWriter();
                HtmlTextWriter hw = new HtmlTextWriter(sw);
                objGridView.AllowPaging = false;

                #region Table Header
                GridViewRow gv = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Normal);
                TableCell tbCell = new TableCell();
                tbCell.ColumnSpan = objGridView.HeaderRow.Cells.Count;
                tbCell.Text = strHeaderText;
                tbCell.Attributes.Add("style", "text-align: center; vertical-align: middle; height: 35px; background-color: #EFF3FB");
                gv.Cells.Add(tbCell);
                objGridView.Controls[0].Controls.AddAt(0, gv);
                #endregion Table Header

                //Change the Header Row back to white color
                objGridView.HeaderRow.Style.Add("background-color", "#FFFFFF");

                //Apply style to Individual Cells
                for (int i = 0; i < objGridView.HeaderRow.Cells.Count; i++)
                {
                    objGridView.HeaderRow.Cells[i].Style.Add("background-color", "#507CD1");
                }

                for (int i = 0; i < objGridView.Rows.Count; i++)
                {
                    GridViewRow row = objGridView.Rows[i];
                    //Change Color back to white
                    row.BackColor = System.Drawing.Color.White;
                    //Apply text style to each Row
                    row.Attributes.Add("class", "textmode");
                    //Apply style to Individual Cells of Alternating Row
                    if (i % 2 != 0)
                    {
                        for (int j = 0; j < objGridView.HeaderRow.Cells.Count; j++)
                        {
                            row.Cells[j].Style.Add("background-color", "#EFF3FB");
                        }
                    }
                }
                objGridView.RenderControl(hw);
                //style to format numbers to string
                string style = @"<style> .textmode { mso-number-format:\@; } </style>";
                HttpContext.Current.Response.Write(style);
                HttpContext.Current.Response.Output.Write(sw.ToString());
                HttpContext.Current.Response.Flush();
                HttpContext.Current.Response.End();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            #endregion Working
        }
    }
}
