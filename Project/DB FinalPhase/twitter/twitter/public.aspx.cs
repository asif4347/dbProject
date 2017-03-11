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
    public partial class _public : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            LoadGrid();

            DataTable dt = new DataTable();

            string connString =
         System.Configuration.ConfigurationManager.ConnectionStrings["sqlcon1"].ConnectionString;
            int uid = Convert.ToInt32(Session["UserID"]);
            // string strConnString = System.Configuration.ConfigurationManager.ConnectionStrings["conString"].ConnectionString;
            string strQuery = "select DP from UoT WHERE User_ID=@uid";
            SqlCommand cmd = new SqlCommand(strQuery);
            SqlConnection con = new SqlConnection(connString);
            SqlDataAdapter sda = new SqlDataAdapter();
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add("@uid", SqlDbType.Int);
            cmd.Parameters["@uid"].Value = uid;
            cmd.Connection = con;




            try
            {
                con.Open();
                sda.SelectCommand = cmd;
                sda.Fill(dt);
                GridView1.DataSource = dt;
                GridView1.DataBind();
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
            finally
            {
                con.Close();
                sda.Dispose();
                con.Dispose();
            }
        }


      




        protected void logingout(object sender, EventArgs e)
        {
            Session["UserID"] = 0;
            Response.Redirect("~/login.aspx");
        }

        void LoadGrid()
        {
            String hey = "get_tweets";
            Int32 i = Convert.ToInt32(Session["UserID"]);
            newdal objMyDal = new newdal();
            if (objMyDal.Get_tweets(i, hey).Tables.Count > 0)
            {
                tweet_grid.DataSource = objMyDal.Get_tweets(i, hey);
                tweet_grid.DataBind();
            }
            else
                Message.Text = Convert.ToString("No tweets to display\n");



            newdal objMyDal1 = new newdal();




            int found;

            found = objMyDal1.Get_followers(i);

            int ud = Convert.ToInt32(Session["UserID"]);
            imgurl.Text = Convert.ToString(objMyDal1.geturl(ud));
            followers_label.Text = Convert.ToString(found);




            newdal objMyDal2 = new newdal();

            int found2;

            found2 = objMyDal2.Get_following(i);



            following_label.Text = Convert.ToString(found2);




            newdal objMyDal3 = new newdal();

            int found3;

            found3 = objMyDal3.Get_tcount(i);



            tweets_label.Text = Convert.ToString(found3);

            String name;
            newdal objMyDal4 = new newdal();



            name = objMyDal4.Get_name(i);



            name_label.Text = Convert.ToString(name);
        }


        protected void tweet_func(object sender, EventArgs e)
        {
            String tweet;
            tweet = Tweet_Text.Text;
            Int32 uid = Convert.ToInt32(Session["UserID"]);


            if (!(string.IsNullOrEmpty(tweet)))
            {
                newdal objMyDal = new newdal();



                objMyDal.add_tweet(tweet, uid);

                tweet += "`";
                {
                    tweet_message.Text = Convert.ToString("Your Tweet has been published");
                    Tweet_Text.Text = Convert.ToString("");
                }
                string trend = "#";

                for (int j = 0; tweet[j] != '`'; j++)
                {

                    if (tweet[j] == '#')
                    {
                        trend = "#";
                        for (int k = j; tweet[k + 1] != ' ' && tweet[k + 1] != '`'; k++)
                        {
                            trend += tweet[k + 1];
                        }
                        objMyDal.int_rend(trend);


                    }
                }
                LoadGrid();
            }
        }

        protected void GV_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            

            if (e.CommandName == "retweet_func")
            {



                Int32 uid, tid = -1;
                uid = Convert.ToInt32(Session["UserID"]);
                Int32 index = Convert.ToInt32(e.CommandArgument);
                tweet_grid.Rows[index].Cells[1].Visible = false;
                tid = Convert.ToInt32(tweet_grid.Rows[index].Cells[1].Text);

                newdal objMyDal = new newdal();


                objMyDal.retweet_func1(uid, tid);

                

                tweet_message.Text = Convert.ToString("Ho gaya hai retweet");

                LoadGrid();

            }
        }

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


        protected void search(Object sender, EventArgs e)
        {


            if (!(string.IsNullOrEmpty(search_text.Text)))
            {
                string tweet = search_text.Text;
                if(tweet[0]!='#')
                {
                    TestSessionValue = ((TextBox)search_text).Text;
                    Page.Response.Redirect("~/search.aspx");
                }
                

                
                string trend;
                newdal objMyDal = new newdal();
                tweet +=' ';

                for (int j = 0; tweet[j] != '`'; j++)
                {

                    if (tweet[0] == '#')
                    {
                        
                        trend = "#";
                        for (int k = 0; tweet[k + 1] != ' '; k++)
                        {
                            trend += tweet[k +1];
                        }
                        Session["trnd"] = trend;
                        Page.Response.Redirect("~/searchtrend.aspx");


                    }
                }

                

            }

        }






        protected void btnUpload_Click(object sender, EventArgs e)
        {
            if (FileUpload1.PostedFile!=null)
            {
                int uid = Convert.ToInt32(Session["UserID"]);
                string FileName = Path.GetFileName(FileUpload1.PostedFile.FileName);

                //Save files to disk
                FileUpload1.SaveAs(Server.MapPath("Images/" + FileName));

                //Add Entry to DataBase
                string connString =
                System.Configuration.ConfigurationManager.ConnectionStrings["sqlcon1"].ConnectionString;
                SqlConnection con = new SqlConnection(connString);
                string strQuery = "Update UoT SET DP=@FilePath WHERE User_ID=@uid";
                SqlCommand cmd = new SqlCommand(strQuery);
                cmd.Parameters.AddWithValue("@FilePath", "Images/" + FileName);
                cmd.Parameters.Add("@uid", SqlDbType.Int);
                cmd.Parameters["@uid"].Value = uid;

                cmd.CommandType = CommandType.Text;
                cmd.Connection = con;
                try
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Response.Write(ex.Message);
                }
                finally
                {
                    con.Close();
                    con.Dispose();
                }
                Response.Redirect("~/public.aspx");
            }


        }

        protected void delete_me(object sender, EventArgs e)
        {
            Int32 uid;


            uid = Convert.ToInt32(Session["UserID"]);


            newdal objMyDal = new newdal();


            objMyDal.banda_blocker(uid);
            Response.Redirect("~/login.aspx");
        }

        

    }
}