<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Store.aspx.cs" Inherits="_397_FinalProject_Part2.Store" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Store</title>
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

    <form id="form1" class="form-horizontal col-sm-offset-3 col-sm-6" runat="server">
        <h3 style="text-align: center;">Currently Offered Books</h3>
        <div class=" col-sm-offset-3">
            <asp:GridView ID="gvDisplay" runat="server" CellPadding="4" GridLines="Horizontal" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" Width="508px" ForeColor="Black">
                <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
                <HeaderStyle BackColor="#333333" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Right" />
                <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
                <SortedAscendingCellStyle BackColor="#F7F7F7" />
                <SortedAscendingHeaderStyle BackColor="#4B4B4B" />
                <SortedDescendingCellStyle BackColor="#E5E5E5" />
                <SortedDescendingHeaderStyle BackColor="#242121" />
            </asp:GridView>
        </div>

        <h3 style="text-align: center;">Add a book:</h3>
        <div class="form-group">
            <asp:Label ID="lblAdd" runat="server" Text="Book" CssClass="control-label col-sm-4"></asp:Label>

            <div class="col-sm-4">
                <asp:DropDownList ID="ddlBooks" class="form-control" runat="server"></asp:DropDownList>
            </div>

            <div class="col-sm-4">
                <asp:Button ID="btnAdd" runat="server" Text="Add" class="btn btn-primary" OnClick="btnAdd_Click" />
                &nbsp;&nbsp;&nbsp;
                <asp:Label ID="lblError" runat="server" Text="" CssClass="control-label col-sm-4"></asp:Label>
            </div>

            <br />
        </div>
        <div class="form-group">
            <asp:Label ID="lblQuantity" runat="server" Text="Quantity" CssClass="control-label col-sm-4"></asp:Label>
            <div class="col-sm-4">
                <asp:TextBox ID="tbxQuantity" type="number" Text="1" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
        </div>

        <h3 style="text-align: center;">Your Order 
            <asp:Button ID="btnCheckOut" runat="server" Text="Check Out" class="btn btn-primary" OnClick="btnCheckOut_Click" />
        </h3>

        <div class=" col-sm-offset-3">
            <asp:GridView ID="gvCart" runat="server" CellPadding="4" GridLines="Horizontal" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" Width="508px" ForeColor="Black">
                <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
                <HeaderStyle BackColor="#333333" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Right" />
                <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
                <SortedAscendingCellStyle BackColor="#F7F7F7" />
                <SortedAscendingHeaderStyle BackColor="#4B4B4B" />
                <SortedDescendingCellStyle BackColor="#E5E5E5" />
                <SortedDescendingHeaderStyle BackColor="#242121" />
            </asp:GridView>
        </div>
        <br />
        <h3 style="text-align: center;">Modify Order</h3>
        <br />

        <div class="form-group">
            <asp:Label ID="lblModify" runat="server" Text="Your Books" CssClass="control-label col-sm-4"></asp:Label>

            <div class="col-sm-4">
                <asp:DropDownList ID="ddlModify" class="form-control" runat="server"></asp:DropDownList>
            </div>

            <div class="col-sm-4">
                <asp:Button ID="btnModify" runat="server" Text="Modify" class="btn btn-primary" OnClick="btnModify_Click" />
                &nbsp;&nbsp;&nbsp;
                <asp:Label ID="lblModifyError" runat="server" Text="" CssClass="control-label col-sm-4"></asp:Label>
            </div>

            <br />
        </div>
        <div class="form-group">
            <asp:Label ID="lblNewQuantity" runat="server" Text="Quantity" CssClass="control-label col-sm-4"></asp:Label>
            <div class="col-sm-4">
                <asp:TextBox ID="tbxModify" type="number" Text="1" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
        </div>


    </form>
</body>
</html>
