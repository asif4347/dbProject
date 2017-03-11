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
        public partial class search : System.Web.UI.Page
        {
            static int i = 0;
            public static string TestSessionValue
            {
                get
                {
                    object value = HttpContext.Current.Session["TestSessionValue"];
                    return value == null ? "" : (string)value;
                }
                set
                {
                    HttpContext.Current.Session["TestSessionValue"] = value;
                }
            }


            public static string TestSessionValue2
            {
                get
                {
                    object value = HttpContext.Current.Session["TestSessionValue2"];
                    return value == null ? "" : (string)value;
                }
                set
                {
                    HttpContext.Current.Session["TestSessionValue2"] = value;
                }
            }



            protected void Page_Load(object sender, EventArgs e)
            {
                LoadGrid();
            }

            void LoadGrid()
            {
                String name = TestSessionValue;

                newdal objMyDal = new newdal();


                if (objMyDal.search_g(name).Tables.Count > 0)
                {
                    search_grid.DataSource = objMyDal.search_g(name);
                    search_grid.DataBind();
                }
                else
                    Message.Text = Convert.ToString("Yeh tou kisi ka naam nahi hai\n");

            }

            protected void GV_RowCommand(object sender, GridViewCommandEventArgs e)
            {



                if (e.CommandName == "message_func")
                {
                    Int32 id;

                    Int32 index = Convert.ToInt32(e.CommandArgument);


                    id = Convert.ToInt32(search_grid.Rows[index].Cells[3].Text);

                    Session["rec_id"] = Convert.ToInt32(id);


                    msg_textbox.Visible = true;
                    send_button.Visible = true;

                }


                if (e.CommandName == "follow_func")
                {
                    Int32 uid = Convert.ToInt32(Session["UserID"]), fid = 0;
                    int flag;

                    Int32 index = Convert.ToInt32(e.CommandArgument);

                    fid = Convert.ToInt32(search_grid.Rows[index].Cells[3].Text);

                    newdal objMyDal = new newdal();


                    flag = objMyDal.follow_func1(uid, fid);


                    if (flag == 1)
                        Message.Text = Convert.ToString("Person Followed");

                    if (flag == 0)
                        Message.Text = Convert.ToString("You're following this person anyway");




                }

                if (e.CommandName == "unfollow_func")
                {

                    Int32 uid = Convert.ToInt32(Session["UserID"]), fid = 0;
                    int flag;
                    Int32 index = Convert.ToInt32(e.CommandArgument);

                    fid = Convert.ToInt32(search_grid.Rows[index].Cells[3].Text);

                    newdal objMyDal = new newdal();


                    flag = objMyDal.unfollow_func1(uid, fid);


                    if (flag == 1)
                        Message.Text = Convert.ToString("Person Unfollowed");

                    if (flag == 0)
                        Message.Text = Convert.ToString("You're not following this person anyway");





                }




            }

            protected void back_public(Object sender, EventArgs e)
            {

                Page.Response.Redirect("public.aspx");

            }

            protected void send_click(Object sender, System.EventArgs e)
            {



                String text;
                Int32 sid = 1, rid;


                text = msg_textbox.Text;





                rid = Convert.ToInt32(Session["rec_id"]);
                sid = Convert.ToInt32(Session["UserID"]);
                newdal objMyDal = new newdal();

                int flag = objMyDal.message_func1(sid, rid, text);

                if (flag == 1)
                    Message.Text = Convert.ToString("Message sent");

                if (flag == 0)
                    Message.Text = Convert.ToString("Cannot msg this person");




                msg_textbox.Text = string.Empty;



            }


        }
   
}