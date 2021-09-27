<%@ Page Title="" Language="C#" MasterPageFile="~/Comun/MasterPages/Principal.Master" AutoEventWireup="true" CodeBehind="InicioWin.aspx.cs" Inherits="ARP.Ejemplo.WebExterno.Inicio.InicioWin" %>

<%@ Register src="../Controles/ValidacionWin.ascx" tagname="ValidacionWin" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:ValidacionWin ID="ValidacionWin1" runat="server" />
</asp:Content>
