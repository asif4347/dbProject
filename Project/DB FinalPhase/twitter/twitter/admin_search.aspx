<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="admin_search.aspx.cs" Inherits="twitter.admin_search" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="publicpage.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:Gridview ID="banda_grid" runat="server"   OnRowCommand="GVRowCommand" GridLines="None" CssClass="mGrid">


        <Columns>
            <asp:ButtonField Text="block" CommandName="block_func"  />
            


        </Columns> 


    </asp:Gridview>

        
         <asp:Button ID="back_to_admin" Text="Back to Admin Page" runat="server" OnClick="admin_back" /><br/>


          <asp:Label ID="Message" runat="server"></asp:Label>

    </div>
    </form>
</body>
</html>
