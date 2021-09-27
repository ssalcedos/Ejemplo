<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Login.ascx.cs" Inherits="ARP.Ejemplo.WebExterno.Controles.Login" %>
<style type="text/css">
    .style1
    {
        width: 100%;
    }
    .style2
    {
        width: 163px;
    }
</style>
<div class="inicioSesion">
    <div>
        <h1>
            Inicio de sesión
        </h1>
        <table class="style1">
            <tr>
                <td class="style2">
                    Dominio:<asp:Label ID="Label1" runat="server" CssClass="error" Text="*"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlDominios" runat="server">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="rfvDomino" runat="server" ControlToValidate="ddlDominios"
                        Display="None" ErrorMessage="'Dominio' es requerido." InitialValue="-1"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="style2">
                    Usuario:<asp:Label ID="Label2" runat="server" CssClass="error" Text="*"></asp:Label>
                    <br />
                </td>
                <td>
                    <asp:TextBox ID="txtUsuario" runat="server" MaxLength="20"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvUsuario" runat="server" ControlToValidate="txtUsuario"
                        Display="None" ErrorMessage="'Usuario' es requerido."></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="style2">
                    Contraseña:<asp:Label ID="Label3" runat="server" CssClass="error" Text="*"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtContrasena" runat="server" TextMode="Password" 
                        MaxLength="50"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvContrasena" runat="server" ControlToValidate="txtContrasena"
                        Display="None" ErrorMessage="'Contraseña' es requerido."></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="style2">
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style2">
                </td>
                <td>
                    <asp:LinkButton ID="lbkRecordarContrasena" runat="server" OnClick="lbkRecordarContrasena_Click" CausesValidation="False">Recordar Contraseña</asp:LinkButton>
                </td>
            </tr>
        </table>
        <br />
        <asp:Button ID="btnIngresar" runat="server" Text="Ingresar" OnClick="btnIngresar_Click" CssClass="validacion" />
    </div>
    <br />
    <br />
    <p style="text-align: center !important;">
        <asp:Label ID="lblError" runat="server" CssClass="error"></asp:Label>
        <asp:ValidationSummary ID="vsLogin" runat="server" CssClass="error" DisplayMode="List"
            Style="text-align: center" />
</div>
