using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using twitter.DAL;

namespace twitter
{
    public partial class admin_search : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            LoadGrid();
        }


        void LoadGrid()
        {
            String name = Convert.ToString(Session["banda"]);

            newdal objMyDal = new newdal();


            if (objMyDal.search_g(name).Tables.Count > 0)
            {
                banda_grid.DataSource = objMyDal.search_g(name);
                banda_grid.DataBind();
            }
            if (objMyDal.search_g(name).Tables.Count == 0)
                Message.Text = Convert.ToString("Ab aisa koi banda nahi\n");





        }


        protected void GVRowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "block_func")
            {
                Int32 uid;




                Int32 index = Convert.ToInt32(e.CommandArgument);


                uid = Convert.ToInt32(banda_grid.Rows[index].Cells[1].Text);





                newdal objMyDal = new newdal();


                objMyDal.banda_blocker(uid);

                LoadGrid();

            }
        }

        protected void admin_back(Object sender, EventArgs e)
        {

            Page.Response.Redirect("~/admin.aspx");

        }

    }
}