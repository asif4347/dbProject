<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="admin.aspx.cs" Inherits="twitter.T_admin" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
     <title></title>
    <link href="publicpage.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <asp:Textbox ID="admin_search_text" PlaceHolder="Search people" runat="server">
        </asp:Textbox> &nbsp;&nbsp; 
        <asp:Button ID="search_button" OnClick="admin_search" Text="Search" runat="server" />
        <asp:Button ID="logout" Text="Logout" OnClick="logingout" Style="float:right; margin-right:20px; background-color:#559BCE" runat="server"></asp:Button>
        <br/>

        <asp:Button ID="Button1" Text="Public Page" runat="server" OnClick="Button1_Click"  /></br></br>
       

        
        Select Tweets to block

        <asp:Gridview ID="admin_tweet_grid" runat="server"  OnRowCommand="GVRowCommand" GridLines="None" CssClass="mGrid">


        <Columns>
            <asp:ButtonField Text="block" CommandName="block_func"  />
   


        </Columns> 


    </asp:Gridview><br/><br/><br/><br/><br/>

        <asp:Label ID="Message" runat="server"></asp:Label>

    </div>
    </form>
</body>
</html>
