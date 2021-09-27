<%@ Page Title="" Language="C#" MasterPageFile="~/Comun/MasterPages/Principal.Master" AutoEventWireup="true" CodeBehind="PruebaSEG2000.aspx.cs" Inherits="ARP.Ejemplo.WebExterno.Inicio.PruebaSEG2000" %>

<%@ Register src="../Controles/PruebaSEG2000.ascx" tagname="PruebaSEG2000" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:PruebaSEG2000 ID="PruebaSEG20001" runat="server" />
</asp:Content>
