<%@ Page Title="" Language="C#" MasterPageFile="~/Comun/MasterPages/Principal.Master"
    AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ARP.Ejemplo.WebExterno.Default" %>

<%@ Register src="Controles/Permisos.ascx" tagname="Permisos" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <p>
        &nbsp;</p>
    <p>
        &nbsp;</p>
    <uc1:Permisos ID="Permisos1" runat="server" />
    <p>
        &nbsp;</p>
</asp:Content>