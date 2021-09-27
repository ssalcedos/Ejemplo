<%@ Page Language="C#" MasterPageFile="~/Comun/MasterPages/Principal.Master" AutoEventWireup="true"
    CodeBehind="InicioWeb.aspx.cs" Inherits="ARP.Ejemplo.WebExterno.Inicio.InicioWeb" %>

<%@ Register Src="../Controles/Login.ascx" TagName="Login" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    &nbsp;<uc1:Login ID="Login1" runat="server" />
</asp:Content>