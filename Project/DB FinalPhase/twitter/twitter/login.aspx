<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="twitter.login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
   <title>Twitter Login</title>
    <link href="DBP1.3.css" type="text/css" rel="stylesheet" />
    <script src="DBP1.3.js" type="text/javascript"></script>
</head>
<body onload="reset()">
    <form id="form1" runat="server">
    <div>
        <div id="welcome">
        <h1 id="mytitle" class="wn">
            Welcome to Twitter
        </h1>
        <p class="wn">
            Connect with your friends — and other fascinating people. Get in-the-moment updates on the things that interest you.
             And watch events unfold, in real time, from every angle.
        </p>
        </div>
        <div id="login">
            <img id="tsl" src="Images/tsl.png" />
            <asp:TextBox id="signin" placeholder="Email or Phone" type="text" runat="server"/>
            <br />
            <br />
            <asp:TextBox id="pass" placeholder="Password" type="password" runat="server"/>
            <br />
            <br />
            <asp:Button id="lgb" Text="Log in" OnClick="logsearch" OnClientClick="login()" runat="server" />
            <asp:label ID="Message" runat="server" />
            <br />
            <br/>
        </div>
        <div id="new">
             <h2 id="ntt"><strong id="n">New to Twitter?</strong>Sign up</h2>
            <asp:TextBox id="sun" placeholder="Full Name" type="text" runat="server"/>
            <asp:TextBox id="sue" placeholder="Email" type="text" runat="server"/>
            <br />
            <br />
            <asp:TextBox id="sup" placeholder="Phone" type="text" runat="server"/>
            <asp:TextBox id="supass" placeholder="Password" type="password" runat="server"/>
            <br />
            <br />
            <asp:Button id="signup" Text="Sign up for Twitter" OnClick="signer" runat="server" />
            <asp:label ID="sm" runat="server" />
            <br />
        </div>
    </div>
    </form>
</body>
</html>

