<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="search.aspx.cs" Inherits="twitter.search" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
        <link href="publicpage.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
      <div id="cent">
        Results<br/>
    <asp:Gridview ID="search_grid" runat="server"   OnRowCommand="GV_RowCommand" GridLines="None" CssClass="mGrid">


        <Columns>
            <asp:ButtonField Text="Message" CommandName="message_func"  />
            <asp:ButtonField Text="Follow" CommandName="follow_func"  />
            <asp:ButtonField Text="Unfollow" CommandName="unfollow_func"  />


        </Columns> 


    </asp:Gridview>
        &nbsp; &nbsp; &nbsp;
        <asp:Button ID="back_to_public" Text="Back to Public Page" runat="server" OnClick="back_public" /><br/>


        <br /><br/><br/><br/><br/>

        <asp:Textbox ID="msg_textbox" Placeholder="Enter Message" Visible="false" runat="server"></asp:Textbox>
        <asp:Button ID="send_button" OnClick="send_click" Text="Send" Visible="false" runat="server" />
     
        <asp:Label ID="Message" runat="server"></asp:Label>
    </div>
    </form>
</body>
</html>
