<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Header.ascx.cs" Inherits="ARP.Ejemplo.WebExterno.Comun.MasterPages.Header" %>
<div id="header">
    <div class="logo">
        <asp:Image ID="imgLogoColmena" runat="server" SkinID="LogoColmena" />
    </div>
    <div class="info">
        <asp:Panel ID="pnlInformation" runat="server" Visible="False">
            <div class="information">
                <div class="textData">
                    <asp:Label ID="lblNombreFuncionario" runat="server" CssClass="lblNombreFuncionario"></asp:Label>
                    |
                    <asp:Label ID="lblCargoUsuario" runat="server" Text="" CssClass="lblCargoUsuario"></asp:Label>
                    | <a href="#" id="Perfiles">Perfiles </a>|
                </div>
                <div class="btnData">
                    <%--<a href="../Ayuda/Default.html" id="linkAyuda" title="Ayuda"></a>--%>
                </div>
                <div runat="server" id="Perfiles_Lista" class="perfiles_Lista">
                </div>
            </div>
        </asp:Panel>
        <div class="portal">
            <div class="title">
                Aplicación de Ejemplo para Seguridad
            </div>
            <div class="star">
                <asp:Image ID="imgEstrella" runat="server" SkinID="LogoPortal" />
            </div>
            <div class="test">
                <asp:Label ID="lblAmbiente" runat="server" CssClass="LabelEstadoPortal">Ambiente de Pruebas</asp:Label>
            </div>
            <div runat="server" id="Div1" class="perfiles_lista" />
        </div>
    </div>
</div>
<div class="menu">
    <asp:HyperLink ID="lnkCambiarContraseña" runat="server" Visible="False">Cambiar contraseña</asp:HyperLink>
    <div class="tag">
    </div>
</div>
</div>
<br />
<%--<div class="navigation">
    <div class="buttons">
        <ul>
            <li><a runat="server" id="lnkInicio" href="~/Default.aspx">
                <asp:Image ID="imgInicio" runat="server" /></a></li>
            <li><a runat="server" id="lnkInstrumentos" href="~/Funcionario/Consentimiento.aspx">
                <asp:Image ID="imgInstrumentos" runat="server" /></a></li>
            <li><a runat="server" id="lnkConsultasEvaluador" href="~/Informes/ConsultasEvaluador.aspx">
                <asp:Image ID="imgConsultasEvaluador" runat="server" /></a></li>
            <li><a runat="server" id="lnkConsultasJefe" href="~/Jefe/PantallaInicialJefe.aspx">
                <asp:Image ID="imgConsultasJefe" runat="server" /></a></li>
            <li><a runat="server" id="lnkAdministracion" href="~/Administracion/InicioAdministrador.aspx">
                <asp:Image ID="imgAdministracion" runat="server" /></a></li>
        </ul>
    </div>
    <div class="bar">
    </div>
</div>--%>