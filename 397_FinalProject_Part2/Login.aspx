<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="_397_FinalProject_Part2.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login</title>
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <link href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css" rel="stylesheet" />
    <style type="text/css">
        form {
            font-weight: bold;
        }
    </style>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.12.4/jquery.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery-easing/1.3/jquery.easing.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery.touchswipe/1.6.4/jquery.touchSwipe.min.js"></script>
</head>
<body>

    <header class="container">
        <nav class="collapse navbar-collapse navbar-light">
            <ul class="nav navbar-nav" id="navBar">
                <li><a href="Store.aspx">Store</a></li>
                <li><a href="Login.aspx">Login </a></li>
                <li><a href="Registration.aspx">Become a member</a></li>
                
            </ul>
        </nav>
    </header>

    <form id="form" class="form-horizontal col-sm-offset-3 col-sm-6" runat="server">
        <h3>Login to your account</h3>
        <div class="form-group">
            <div class="input-group">
                <span class="input-group-addon">
                    <span class="glyphicon glyphicon-user"></span>
                </span>
                <asp:TextBox ID="tbxUserName" Text="" CssClass="form-control" runat="server"></asp:TextBox>
            </div>
        </div>
        <div class="form-group">
            <div class="input-group">
                <span class="input-group-addon">
                    <span class="glyphicon glyphicon-log-in"></span>
                </span>
                <asp:TextBox ID="tbxPassword" Text="" CssClass="form-control" runat="server" TextMode="Password"></asp:TextBox>
            </div>
        </div>
        <hr />
        <div class="form-group">
            <a href="Registration.aspx">Create an account</a>
            <br />
            <br />
            <asp:Button CssClass="btn btn-success" ID="btnLogin" runat="server" Text="Login" OnClick="btnLogin_Click" />
            <br />
            <asp:Label ID="lblMessage" runat="server" Text="" Visible="false"></asp:Label>
        </div>
    </form>
</body>
</html>
