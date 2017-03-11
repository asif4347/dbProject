<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="public.aspx.cs" Inherits="twitter._public" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
     <title></title>
    <link href="publicpage.css" type="text/css" rel="stylesheet" />
    <script src="DBP1.3.js" type="text/javascript"></script>
</head>
<body onload="rel()">
    <form id="form1" runat="server">
    
            <div id="option">
                <a id="home" href="public.aspx">
                    <img id="hm" src="Images/th.jpg"/>
                    Home
                    </a>
                <div id="tl"><img id="tlp" src="Images/tsl.png" />
                    </div>
                <div id="pullright">
                    <asp:Textbox ID="search_text" PlaceHolder="Search people" runat="server">
        </asp:Textbox> &nbsp;&nbsp;
                
        <asp:Button ID="search_button" OnClick="search" Style='border-radius:6px; color:white; background-color:#998ff6' Text="Search" runat="server" />
   
                     <asp:FileUpload ID="FileUpload1" style='width:75px;' runat="server"/>
    <asp:Button ID="btnUpload" runat="server" Text="Upload"
        OnClick="btnUpload_Click" />
                    <asp:Button ID="logout" Text="Logout" OnClick="logingout" runat="server"></asp:Button>

</div>
</div>
        <div>
<br />
    <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns = "false"
       Font-Names = "Arial" >
    <Columns>
    
       <asp:ImageField DataImageUrlField="DP" ControlStyle-Width="100"
        ControlStyle-Height = "100" HeaderText = "Preview Image"/>
    </Columns>
    </asp:GridView>
</div>

        <div style="margin-left: 1000px">

           <a href="messages.aspx" ><asp:Label ID="messge"  runat="server" Text="Messages"></asp:Label></a>
            </br></br>
            <asp:Button ID="Button1" runat="server" Text="Delete Account" OnClick="delete_me" />
        </div>
             
                
      
    <div id="pp">
        
             <div id="dashboard">   
            <div id="db2p">
        <img id="DP" src="Images/DP.jpg" />
        <span id="namae"><asp:Label ID="name_label"  runat="server"></asp:Label></span><br/>
        <span id="infoheads">Tweets&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Following&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Followed by</span>
            <br/>  <span id="val">  <a href="yourtweets.aspx"><asp:Label ID="tweets_label" runat="server"></asp:Label></a>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
           <a href="yourfollowing.aspx"><asp:Label ID="following_label"  runat="server"></asp:Label></a>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
         <a href="yourfollowers.aspx"><asp:Label ID="followers_label"  runat="server"></asp:Label></a>
            </span>
        </div>
                 </div>

        <div id="timeline">
        <div id="ct">
            <img id="cti" src="Images/DP.jpg" />
        <asp:Textbox ID="Tweet_Text" AutoComplete="off" aria-multiline="true" PlaceHolder="What's Happening?" runat="server">  </asp:Textbox><br/> 
                <asp:button ID="Tweet_Button" Text="Tweet" runat="server" OnClick="tweet_func" /><br/><br/>
       </div>
      
            <div id="tweets">
    <asp:GridView ID="tweet_grid"  runat="server" OnRowCommand="GV_RowCommand"  GridLines="None" CssClass="mGrid"  >
        

        <Columns>
            <asp:ButtonField Text="Retweet" CommandName="retweet_func"  />
        </Columns>
       
       

    </asp:GridView>
                </div>
        <br/><br/>
        
            </div>
        <asp:label ID="tweet_message" runat="server"></asp:label>
        <asp:label ID="Message" runat="server" />

        <asp:GridView ID="GridView1" Style='visibility:hidden;' Gridlines="None" runat="server" AutoGenerateColumns = "false"
       Font-Names = "Arial" >
    <Columns>
    
       <asp:ImageField DataImageUrlField="DP" ControlStyle-Width="100"
        ControlStyle-Height = "100"/>
    </Columns>
    </asp:GridView>
</div>
        <asp:TextBox id="imgurl" Text="Images/DP.jpg" style='visibility:hidden' runat="server"/>
    </form>
</body>
</html>
