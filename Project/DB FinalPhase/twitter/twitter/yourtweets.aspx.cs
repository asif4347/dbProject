using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace twitter
{
    public partial class yourtweets : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void back_public(Object sender, EventArgs e)
        {

            Page.Response.Redirect("public.aspx");

        }
    }
}