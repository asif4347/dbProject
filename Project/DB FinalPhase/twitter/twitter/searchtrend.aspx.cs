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
    public partial class searchtrend : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/public.aspx");
        }
        
       

    }
}