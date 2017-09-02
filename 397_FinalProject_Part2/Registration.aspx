<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Registration.aspx.cs" Inherits="_397_FinalProject_Part2.Registration" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Registration</title>
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

    <form id="form" runat="server" class="form-horizontal">
        <h3 style="text-align: center;">Registration</h3>

        <div class="form-group">
            <asp:Label ID="lblUserName" runat="server" Text="User Name" CssClass="control-label col-sm-4"></asp:Label>
            <div class="col-sm-4">
                <asp:TextBox ID="tbxUserName" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="col-sm-4">
                <asp:Label ID="lblUserNameError" ForeColor="Red" runat="server" Text="" Visible="false"></asp:Label>
            </div>
            <br />
        </div>
        <!---User Name--->

        <div class="form-group">
            <asp:Label ID="lblPassword" runat="server" Text="Password" CssClass="control-label col-sm-4"></asp:Label>
            <div class="col-sm-4">
                <asp:TextBox ID="tbxPassword" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>
            </div>
            <div class="col-sm-4">
                <asp:Label ID="lblPasswordError" ForeColor="Red" runat="server" Text="" Visible="false"></asp:Label>
                <br />
                <asp:Label ID="lblPasswordError2" ForeColor="Red" runat="server" Text="" Visible="false"></asp:Label>

            </div>
            <br />
        </div>
        <!---Password--->

        <div class="form-group">
            <asp:Label ID="lblCPassword" runat="server" Text="Confirm Password" CssClass="control-label col-sm-4"></asp:Label>
            <div class="col-sm-4">
                <asp:TextBox ID="tbxConfirm" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>
            </div>
            <div class="col-sm-4">
                <asp:Label ID="lblConfirmError" ForeColor="Red" runat="server" Text="" Visible="false"></asp:Label>
                <br />
                <asp:Label ID="lblConfirmError2" ForeColor="Red" runat="server" Text="" Visible="false"></asp:Label>

            </div>
            <br />
        </div>
        <!---Confirm Password--->

        <div class="form-group">
            <asp:Label ID="lblMessage" runat="server" Text="Welcome" CssClass="control-label col-sm-4"></asp:Label>
            <div id="submitButtons" class="col-sm-4">
                <asp:Button ID="btnSubmit" runat="server" Text="Submit" class="btn btn-primary" OnClick="btnSubmit_Click" />
                &nbsp;&nbsp;&nbsp;
            </div>
            <br />
        </div>
        <!---Buttons--->

    </form>
</body>
</html>
