<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Hunt.aspx.cs" Inherits="Skunk.Hunt" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Label ID="lblProductName" runat="server" Text=""></asp:Label>
            <asp:Label ID="lDetails" runat="server" Text=""></asp:Label>
            <asp:Label ID="lblPrice" runat="server" Text=""></asp:Label>
            <asp:HyperLink ID="hlRate" runat="server">Comments</asp:HyperLink>
            <asp:HiddenField ID="hProdID" runat="server" />
        </div>
    </form>
</body>
</html>
