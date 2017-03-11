using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace twitter.DAL
{
    public class newdal
    {


        public int login(String Name, String Pass)
        {


            string connString =
           System.Configuration.ConfigurationManager.ConnectionStrings["sqlcon1"].ConnectionString;
            int Found = 0;

            if (String.IsNullOrEmpty(Name) == false && String.IsNullOrEmpty(Pass) == false)
            {
                DataSet ds = new DataSet();
                SqlConnection con = new SqlConnection(connString);
                con.Open();
                SqlCommand cmd;
           
            
                try
                {
                    cmd = new SqlCommand("login_proc ", con); //name of your procedure
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@phone_no", SqlDbType.VarChar, 50);
                    cmd.Parameters.Add("@email", SqlDbType.VarChar, 50);
                    cmd.Parameters.Add("@password", SqlDbType.VarChar, 50);
                    cmd.Parameters.Add("@uid", SqlDbType.Int).Direction = ParameterDirection.Output;

                    // set parameter values
                    cmd.Parameters["@password"].Value = Pass;

                    if (Name[0] >= '0' && Name[0] <= '9')
                    {
                        cmd.Parameters["@phone_no"].Value = Name;
                        // cmd.Parameters["@email"].Value = "NULL";
                    }
                    else
                    {
                        cmd.Parameters["@email"].Value = Name;
                        // cmd.Parameters["@phone_no"].Value = "NULL";
                    }
                    cmd.ExecuteNonQuery();

                    //read output value 
                    object check = cmd.Parameters["@uid"].Value;
                    if (cmd.Parameters["@uid"].Value != DBNull.Value)
                        Found = Convert.ToInt32(cmd.Parameters["@uid"].Value); //convert to output parameter to interger format

                    con.Close();


                }
                catch (SqlException ex)
                {
                    Console.WriteLine("SQL Error" + ex.Message.ToString());

                }
                finally
                {
                    con.Close();
                }

                return Found;
            
            }
            return -1;
        }

        public void int_rend(string trend)
        {
            int check=0;
            int tid=0;
            int tr=0;

            string connString =
           System.Configuration.ConfigurationManager.ConnectionStrings["sqlcon1"].ConnectionString;

            DataSet ds = new DataSet();
            SqlConnection con = new SqlConnection(connString);
            con.Open();
            SqlCommand cmd;
            try
            {
                cmd = new SqlCommand("trendset", con); //name of your procedure
                cmd.CommandType = CommandType.StoredProcedure;



                cmd.Parameters.Add("@trend_txt", SqlDbType.VarChar);



                cmd.Parameters.Add("@tweetid", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@trendid", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@check", SqlDbType.Int).Direction = ParameterDirection.Output;


                cmd.Parameters["@trend_txt"].Value = trend;







                cmd.ExecuteNonQuery();

                 check = Convert.ToInt32(cmd.Parameters["@check"].Value);
                  tid = Convert.ToInt32(cmd.Parameters["@tweetid"].Value);
                  tr= Convert.ToInt32(cmd.Parameters["@trendid"].Value);









                con.Close();


            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error" + ex.Message.ToString());

            }
            finally
            {
                con.Close();
            }
        }

        public String geturl(int uid)
        {


            string connString =
           System.Configuration.ConfigurationManager.ConnectionStrings["sqlcon1"].ConnectionString;
            String u = "Images/DP.jpg";
            SqlConnection con = new SqlConnection(connString);
            con.Open();
            SqlCommand cmd;
            try
            {
                cmd = new SqlCommand("DPlink ", con); //name of your procedure
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@usr", SqlDbType.Int);
                cmd.Parameters.Add("@dis", SqlDbType.VarChar, 50).Direction = ParameterDirection.Output;

                // set parameter values
                cmd.Parameters["@usr"].Value = uid;

                cmd.ExecuteNonQuery();

                //read output value 
                object check = cmd.Parameters["@dis"].Value;
                u = Convert.ToString(cmd.Parameters["@dis"].Value); //convert to output parameter to interger format


                con.Close();


            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error" + ex.Message.ToString());

            }
            finally
            {
                con.Close();
            }
            return u;
        }



        public int getadmin(int id)
        {

            string connString =
            System.Configuration.ConfigurationManager.ConnectionStrings["sqlcon1"].ConnectionString;
            int Found = 0;
            DataSet ds = new DataSet();
            SqlConnection con = new SqlConnection(connString);
            con.Open();
            SqlCommand cmd;
            try
            {
                cmd = new SqlCommand("is_admin ", con); //name of your procedure
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@id", SqlDbType.VarChar, 50);
                cmd.Parameters.Add("@flag", SqlDbType.Int).Direction = ParameterDirection.Output;

                // set parameter values
                cmd.Parameters["@id"].Value = id;

                cmd.ExecuteNonQuery();

                //read output value 
                object check = cmd.Parameters["@flag"].Value;
                Found = Convert.ToInt32(cmd.Parameters["@flag"].Value); //convert to output parameter to interger format


                con.Close();


            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error" + ex.Message.ToString());

            }
            finally
            {
                con.Close();
            }

            return Found;
        }

        public int signup(String Name, String email, String Phone, String Pass)
        {

            string connString =
            System.Configuration.ConfigurationManager.ConnectionStrings["sqlcon1"].ConnectionString;

            int Found = 0;
            if (String.IsNullOrEmpty(Name) == false && String.IsNullOrEmpty(email) == false && String.IsNullOrEmpty(Phone) == false && String.IsNullOrEmpty(Pass) == false)
            {
                DataSet ds = new DataSet();
                SqlConnection con = new SqlConnection(connString);
                con.Open();
                SqlCommand cmd;
                try
                {
                    cmd = new SqlCommand("create_account ", con); //name of your procedure
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@name", SqlDbType.VarChar, 50);
                    cmd.Parameters.Add("@phone_no", SqlDbType.VarChar, 50);
                    cmd.Parameters.Add("@emailid", SqlDbType.VarChar, 50);
                    cmd.Parameters.Add("@password", SqlDbType.VarChar, 50);
                    cmd.Parameters.Add("@succes", SqlDbType.Int).Direction = ParameterDirection.Output;

                    // set parameter values
                    cmd.Parameters["@password"].Value = Pass;
                    cmd.Parameters["@name"].Value = Name;
                    cmd.Parameters["@phone_no"].Value = Phone;
                    cmd.Parameters["@emailid"].Value = email;

                    cmd.ExecuteNonQuery();

                    //read output value 
                    object check = cmd.Parameters["@succes"].Value;
                    if (cmd.Parameters["@succes"].Value != DBNull.Value)
                        Found = Convert.ToInt32(cmd.Parameters["@succes"].Value); //convert to output parameter to interger format


                    con.Close();


                }
                catch (SqlException ex)
                {
                    Console.WriteLine("SQL Error" + ex.Message.ToString());

                }
                finally
                {
                    con.Close();
                }

                return Found;
            }
            return 0;
        }

        public int block_account(Int32 uid)
        {
            int block = 0;

            string connString =
           System.Configuration.ConfigurationManager.ConnectionStrings["sqlcon1"].ConnectionString;

            DataSet ds = new DataSet();
            SqlConnection con = new SqlConnection(connString);
            con.Open();
            SqlCommand cmd;
            try
            {
                cmd = new SqlCommand("get_account ", con); //name of your procedure
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@uid", SqlDbType.Int);
                cmd.Parameters.Add("@block", SqlDbType.Int).Direction = ParameterDirection.Output;

                // set parameter values
                cmd.Parameters["@uid"].Value = uid;

                cmd.ExecuteNonQuery();

                //read output value 
                object check = cmd.Parameters["@block"].Value;
                if (cmd.Parameters["@block"].Value != DBNull.Value)
                    block = Convert.ToInt32(cmd.Parameters["@block"].Value); //convert to output parameter to interger format


                con.Close();


            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error" + ex.Message.ToString());

            }
            finally
            {
                con.Close();
            }

            return block;
        }


        public DataSet Get_tweets(Int32 id, String queryString) //to get the values of all the items from table Items and return the Dataset
    {

           string connString =
           System.Configuration.ConfigurationManager.ConnectionStrings["sqlcon1"].ConnectionString;

        int found=0;
        DataSet ds = new DataSet(); //declare and instantiate new dataset
        SqlConnection con = new SqlConnection(connString); //declare and instantiate new SQL connection
 
      
             
        con.Open(); // open sql Connection
        SqlCommand cmd;
        try
        {
            cmd = new SqlCommand("get_tweets", con);  //instantiate SQL command 
            cmd.CommandType = CommandType.StoredProcedure; //set type of sqL Command



            cmd.Parameters.Add("@id1", SqlDbType.Int);


            cmd.Parameters["@id1"].Value = id;

             cmd.ExecuteNonQuery();

           

           // if (found == 1)
            {
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    
                    da.Fill(ds); //Add the result  set  returned from SQLCommand to ds
                    
                }
            }
        }
        catch (SqlException ex)
        {
            Console.WriteLine("SQL Error" + ex.Message.ToString());
        }
        finally
        {
            con.Close();
        }

        return ds; //return the dataset
    }





      
        public int Get_followers(Int32 id)
        {

            string connString =
           System.Configuration.ConfigurationManager.ConnectionStrings["sqlcon1"].ConnectionString;
            int Found = 0;
            
            DataSet ds = new DataSet();
            SqlConnection con = new SqlConnection(connString);
            con.Open();
            SqlCommand cmd;
            try
            {
                cmd = new SqlCommand("get_fcount", con); //name of your procedure
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@id2", SqlDbType.Int);
                cmd.Parameters.Add("@count", SqlDbType.Int).Direction = ParameterDirection.Output;
            
                

                // set parameter values
                cmd.Parameters["@id2"].Value = id;
              

               cmd.ExecuteNonQuery();


               Found = Convert.ToInt32(cmd.Parameters["@count"].Value); //convert to output parameter to interger format


                // read output value 
               
                
                {
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(ds);

                    }

                }
                

                con.Close();


            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error" + ex.Message.ToString());

            }
            finally
            {
                con.Close();
            }

            return Found;
        }


        public int Get_following(Int32 id)
        {

            string connString =
           System.Configuration.ConfigurationManager.ConnectionStrings["sqlcon1"].ConnectionString;
            int Found = 0;

            DataSet ds = new DataSet();
            SqlConnection con = new SqlConnection(connString);
            con.Open();
            SqlCommand cmd;
            try
            {
                cmd = new SqlCommand("get_fby_count", con); //name of your procedure
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@id3", SqlDbType.Int);
                cmd.Parameters.Add("@count1", SqlDbType.Int).Direction = ParameterDirection.Output;



                // set parameter values
                cmd.Parameters["@id3"].Value = id;


                cmd.ExecuteNonQuery();


                Found = Convert.ToInt32(cmd.Parameters["@count1"].Value); //convert to output parameter to interger format


                // read output value 


                {
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(ds);

                    }

                }


                con.Close();


            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error" + ex.Message.ToString());

            }
            finally
            {
                con.Close();
            }

            return Found;
        }



        public int Get_tcount(Int32 id)
        {

            string connString =
           System.Configuration.ConfigurationManager.ConnectionStrings["sqlcon1"].ConnectionString;
            int Found = 0;

            DataSet ds = new DataSet();
            SqlConnection con = new SqlConnection(connString);
            con.Open();
            SqlCommand cmd;
            try
            {
                cmd = new SqlCommand("get_tcount", con); //name of your procedure
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@id4", SqlDbType.Int);
                cmd.Parameters.Add("@count2", SqlDbType.Int).Direction = ParameterDirection.Output;



                // set parameter values
                cmd.Parameters["@id4"].Value = id;


                cmd.ExecuteNonQuery();


                Found = Convert.ToInt32(cmd.Parameters["@count2"].Value); //convert to output parameter to interger format


                // read output value 


                {
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(ds);

                    }

                }


                con.Close();


            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error" + ex.Message.ToString());

            }
            finally
            {
                con.Close();
            }

            return Found;
        }


        public String Get_name(Int32 id)
        {

            string connString =
           System.Configuration.ConfigurationManager.ConnectionStrings["sqlcon1"].ConnectionString;
            String Found="no_name";

            DataSet ds = new DataSet();
            SqlConnection con = new SqlConnection(connString);
            con.Open();
            SqlCommand cmd;
            try
            {
                cmd = new SqlCommand("get_name", con); //name of your procedure
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@id5", SqlDbType.Int);
                cmd.Parameters.Add("@name", SqlDbType.VarChar, 50).Direction = ParameterDirection.Output;



                // set parameter values
                cmd.Parameters["@id5"].Value = id;


                cmd.ExecuteNonQuery();


                Found = Convert.ToString(cmd.Parameters["@name"].Value); //convert to output parameter to interger format


                // read output value 


                {
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(ds);

                    }

                }


                con.Close();


            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error" + ex.Message.ToString());

            }
            finally
            {
                con.Close();
            }

            return Found;
        }


        public void add_tweet(String tweet,  Int32 uid)
        {

            string connString =
           System.Configuration.ConfigurationManager.ConnectionStrings["sqlcon1"].ConnectionString;
            

            DataSet ds = new DataSet();
            SqlConnection con = new SqlConnection(connString);
            con.Open();
            SqlCommand cmd;
            try
            {
                cmd = new SqlCommand("new_tweet", con); //name of your procedure
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@u_id", SqlDbType.Int);
                cmd.Parameters.Add("@text", SqlDbType.VarChar, 250);
                cmd.Parameters.Add("@t_id", SqlDbType.Int).Direction = ParameterDirection.Output;



                // set parameter values
                cmd.Parameters["@u_id"].Value = uid;
                cmd.Parameters["@text"].Value =tweet;



                cmd.ExecuteNonQuery();

                int found =Convert.ToInt32(cmd.Parameters["@t_id"].Value);
                


               


               


                con.Close();


            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error" + ex.Message.ToString());

            }
            finally
            {
                con.Close();
            }

        


        }


       

            
        public void retweet_func1(Int32 uid, Int32 tid)
        {   

            string connString =
           System.Configuration.ConfigurationManager.ConnectionStrings["sqlcon1"].ConnectionString;
            

            DataSet ds = new DataSet();
            SqlConnection con = new SqlConnection(connString);
            con.Open();
            SqlCommand cmd;
            try
            {
                cmd = new SqlCommand("retweet", con); //name of your procedure
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@retweeter", SqlDbType.Int);
           
                cmd.Parameters.Add("@tweetid", SqlDbType.Int);




                cmd.Parameters["@retweeter"].Value = uid;
               cmd.Parameters["@tweetid"].Value = tid;
                



                cmd.ExecuteNonQuery();

            









                con.Close();


            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error" + ex.Message.ToString());

            }
            finally
            {
                con.Close();
            }


        }




        public DataSet search_g(String name) //to get the values of all the items from table Items and return the Dataset
        {

            string connString =
            System.Configuration.ConfigurationManager.ConnectionStrings["sqlcon1"].ConnectionString;

       
            DataSet ds = new DataSet(); //declare and instantiate new dataset
            SqlConnection con = new SqlConnection(connString); //declare and instantiate new SQL connection



            con.Open(); // open sql Connection
            SqlCommand cmd;
            try
            {
                cmd = new SqlCommand("search_results", con);  //instantiate SQL command 
                cmd.CommandType = CommandType.StoredProcedure; //set type of sqL Command



                cmd.Parameters.Add("@name", SqlDbType.VarChar, 50);


                cmd.Parameters["@name"].Value = name;

                cmd.ExecuteNonQuery();



                // if (found == 1)
                {
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {

                        da.Fill(ds); //Add the result  set  returned from SQLCommand to ds

                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error" + ex.Message.ToString());
            }
            finally
            {
                con.Close();
            }

            return ds; //return the dataset
        }


        public int follow_func1(Int32 uid, Int32 fid)
        {
            int found = 0;
            string connString =
           System.Configuration.ConfigurationManager.ConnectionStrings["sqlcon1"].ConnectionString;


            DataSet ds = new DataSet();
            SqlConnection con = new SqlConnection(connString);
            con.Open();
            SqlCommand cmd;
            try
            {
                cmd = new SqlCommand("Follow", con); //name of your procedure
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@uid", SqlDbType.Int);

                cmd.Parameters.Add("@fid", SqlDbType.Int);

                cmd.Parameters.Add("@flag", SqlDbType.Int).Direction = ParameterDirection.Output;




                cmd.Parameters["@uid"].Value = uid;
                cmd.Parameters["@fid"].Value = fid;




                cmd.ExecuteNonQuery();


                found = Convert.ToInt32(cmd.Parameters["@flag"].Value);


            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error" + ex.Message.ToString());

            }
            finally
            {
                con.Close();
            }

            return found;
        }


        public int unfollow_func1(Int32 uid, Int32 fid)
        {
            int found = -1 ;
            string connString =
           System.Configuration.ConfigurationManager.ConnectionStrings["sqlcon1"].ConnectionString;


            DataSet ds = new DataSet();
            SqlConnection con = new SqlConnection(connString);
            con.Open();
            SqlCommand cmd;
            try
            {
                cmd = new SqlCommand("Unfollow", con); //name of your procedure
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@uid", SqlDbType.Int);

                cmd.Parameters.Add("@fid", SqlDbType.Int);
                cmd.Parameters.Add("@flag", SqlDbType.Int).Direction = ParameterDirection.Output; 




                cmd.Parameters["@uid"].Value = uid;
                cmd.Parameters["@fid"].Value = fid;




                cmd.ExecuteNonQuery();

                
                found = Convert.ToInt32(cmd.Parameters["@flag"].Value);

                con.Close();


            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error" + ex.Message.ToString());

            }
            finally
            {
                con.Close();
            }

            return found;
        }



        public int message_func1(Int32 sid, Int32 rid, String msg)
        {
            
            string connString =
           System.Configuration.ConfigurationManager.ConnectionStrings["sqlcon1"].ConnectionString;
            int found=-1;

            DataSet ds = new DataSet();
            SqlConnection con = new SqlConnection(connString);
            con.Open();
            SqlCommand cmd;
            try
            {
                cmd = new SqlCommand("sendPM", con); //name of your procedure
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@sendId", SqlDbType.Int);

                cmd.Parameters.Add("@recvId", SqlDbType.Int);
                cmd.Parameters.Add("@msg", SqlDbType.VarChar, 500);
                cmd.Parameters.Add("@flag", SqlDbType.Int).Direction = ParameterDirection.Output; 



                cmd.Parameters["@sendId"].Value = sid;
                cmd.Parameters["@recvId"].Value = rid;
                cmd.Parameters["@msg"].Value = msg;




                cmd.ExecuteNonQuery();

                
                 found=Convert.ToInt32(cmd.Parameters["@flag"].Value);

                con.Close();


            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error" + ex.Message.ToString());

            }
            finally
            {
                con.Close();
            }

            return found;
        }



        public DataSet admin_tweets() //to get the values of all the items from table Items and return the Dataset
        {

            string connString =
            System.Configuration.ConfigurationManager.ConnectionStrings["sqlcon1"].ConnectionString;

       
            DataSet ds = new DataSet(); //declare and instantiate new dataset
            SqlConnection con = new SqlConnection(connString); //declare and instantiate new SQL connection



            con.Open(); // open sql Connection
            SqlCommand cmd;
            try
            {
                cmd = new SqlCommand("get_admin_tweets", con);  //instantiate SQL command 
                cmd.CommandType = CommandType.StoredProcedure; //set type of sqL Command



                cmd.ExecuteNonQuery();



                // if (found == 1)
                {
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {

                        da.Fill(ds); //Add the result  set  returned from SQLCommand to ds

                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error" + ex.Message.ToString());
            }
            finally
            {
                con.Close();
            }

            return ds; //return the dataset
        }



        public void tweet_blocker( Int32 tid)
        {

            string connString =
           System.Configuration.ConfigurationManager.ConnectionStrings["sqlcon1"].ConnectionString;


            DataSet ds = new DataSet();
            SqlConnection con = new SqlConnection(connString);
            con.Open();
            SqlCommand cmd;
            try
            {
                cmd = new SqlCommand("BlockTweet", con); //name of your procedure
                cmd.CommandType = CommandType.StoredProcedure;

                

                cmd.Parameters.Add("@tweetid", SqlDbType.Int);




               
                cmd.Parameters["@tweetid"].Value = tid;




                cmd.ExecuteNonQuery();











                con.Close();


            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error" + ex.Message.ToString());

            }
            finally
            {
                con.Close();
            }
        }



        public void banda_blocker(Int32 uid)
        {

            string connString =
           System.Configuration.ConfigurationManager.ConnectionStrings["sqlcon1"].ConnectionString;


            DataSet ds = new DataSet();
            SqlConnection con = new SqlConnection(connString);
            con.Open();
            SqlCommand cmd;
            try
            {
                cmd = new SqlCommand("BlockUser", con); //name of your procedure
                cmd.CommandType = CommandType.StoredProcedure;



                cmd.Parameters.Add("@usr", SqlDbType.Int);





                cmd.Parameters["@usr"].Value = uid;




                cmd.ExecuteNonQuery();











                con.Close();


            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error" + ex.Message.ToString());

            }
            finally
            {
                con.Close();
            }
        }






        public void diplay_trend(string trend, ref DataTable d)
        {
            string connString =
           System.Configuration.ConfigurationManager.ConnectionStrings["sqlcon1"].ConnectionString;


            DataSet ds = new DataSet();
            SqlConnection con = new SqlConnection(connString);
            con.Open();
            String query = "Select * from dbo.gettrends(@tren)";
            SqlCommand cmd;
            try
            {
                //cmd = new SqlCommand("gettrends", con); //name of your procedure
                //cmd.CommandType = CommandType.StoredProcedure;

                //cmd.Parameters.Add("@tren", SqlDbType.VarChar,50);


                //cmd.Parameters["@tren"].Value = trend;

                //cmd.ExecuteNonQuery();

                //using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                //{

                //    da.Fill(ds); //Add the result  set  returned from SQLCommand to ds

                //}
                //d = ds.Tables[0];
             /*   using()
                {
                
                }*/
            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error" + ex.Message.ToString());

            }
            finally
            {
                con.Close();
            }
            
        }
        



            


    }
}

