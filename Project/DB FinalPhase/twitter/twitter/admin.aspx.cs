using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using twitter.DAL;
using System.IO;
using System.Data.SqlClient;

namespace twitter
{
    public partial class T_admin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            LoadGrid();
        }


        void LoadGrid()
        {

            newdal objMyDal = new newdal();
            if (objMyDal.admin_tweets().Tables.Count > 0)
            {
                admin_tweet_grid.DataSource = objMyDal.admin_tweets();
                admin_tweet_grid.DataBind();
            }
            else
                Message.Text = Convert.ToString("No tweets to display\n");


        }


        protected void GVRowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "block_func")
            {
                Int32 tid;




                Int32 index = Convert.ToInt32(e.CommandArgument);


                tid = Convert.ToInt32(admin_tweet_grid.Rows[index].Cells[1].Text);





                newdal objMyDal = new newdal();


                objMyDal.tweet_blocker(tid);

                LoadGrid();

            }
        }



        protected void admin_search(Object sender, EventArgs e)
        {
            string text = (admin_search_text).Text;
            if (!(string.IsNullOrEmpty(text)))
            {
                Session["banda"] = ((TextBox)admin_search_text).Text;

                Page.Response.Redirect("~/admin_search.aspx");
            }

        }

        protected void logingout(object sender, EventArgs e)
        {
            Session["UserID"] = 0;
            Response.Redirect("~/login.aspx");
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/public.aspx");
        }

    }
}