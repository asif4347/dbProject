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
    public partial class login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void logsearch(object sender, EventArgs e)
        {

            String Name = signin.Text;
            String Pass = pass.Text;
            newdal objMyDal = new newdal();

            int found, admin;
            found = objMyDal.login(Name, Pass);

            if (found > 0)
            {
                Message.Text = Convert.ToString("Following Items Found");
                admin = objMyDal.getadmin(found);
                Session["UserID"] = found;
                if (admin == 0)
                {
                    if (objMyDal.block_account(found) == 1)
                    {
                        Session["UserID"] = 0;
                        Response.Redirect("~/accountblock.aspx");
                    }
                    else
                        Response.Redirect("~/public.aspx");
                }
                else if (admin == 1)
                    Response.Redirect("~/admin.aspx");

            }
            else if (found == 0)
            {
                Message.Text = Convert.ToString("Item Not Found");
            }
            else
            {
                Message.Text = Convert.ToString("Information Incomplete!");
            }

        }

        protected void signer(object sender, EventArgs e)
        {

            String Name = sun.Text;
            String Pass = supass.Text;
            String phone = sup.Text;
            String email = sue.Text;
            newdal objMyDal = new newdal();

            int found;
            found = objMyDal.signup(Name, email, phone, Pass);

            if (found > 0)
            {
                sm.Text = Convert.ToString("Signed up Succesfully!");
                Session["UserID"] = found;


                Response.Redirect("~/public.aspx");

            }
            else if (found == 0)
            {
                sm.Text = Convert.ToString("Incomplete Infromation!");
            }
            else if (found == -1)
            {
                sm.Text = Convert.ToString("Enter Unique Email or Phone!");
            }

        }

    }
}