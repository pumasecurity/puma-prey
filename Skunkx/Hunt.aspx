<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Hunt.aspx.cs" Inherits="Puma.Prey.Skunk.Hunt" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    
    <asp:Label ID="lblProductName" runat="server" Text="Label"></asp:Label>
    <asp:Label ID="lDetails" runat="server" Text="Label"></asp:Label>
    <asp:Label ID="lblPrice" runat="server" Text="Label"></asp:Label>
    <asp:HyperLink ID="hlRate" runat="server">HyperLink</asp:HyperLink>
    <asp:HiddenField ID="hProdID" runat="server" />
</asp:Content>
