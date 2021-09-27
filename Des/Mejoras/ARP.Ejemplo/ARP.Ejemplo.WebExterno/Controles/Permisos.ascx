<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Permisos.ascx.cs" Inherits="ARP.Ejemplo.WebExterno.Controles.Permisos" %>
<div class="Nombre">
    <table class="tblDatos" width="50%">
        <tr>
            <td class="TitulosTabla">
                <p>
                    <asp:Label ID="Label1" runat="server" Text="Lista de Perfiles:"></asp:Label>
                </p>
            </td>
            <td class="TitulosTabla">
                <p>
                    <asp:Label ID="Label2" runat="server" Text="Lista de permisos:"></asp:Label>
                </p>
            </td>
        </tr>
        <tr>
            <td class="DatosTabla">
                <asp:GridView ID="gvwPerfiles" runat="server" EnableModelValidation="True" Width="100%">
                    <Columns>
                        <asp:TemplateField>
                            <EditItemTemplate>
                                <asp:CheckBox ID="CheckBox1" runat="server" />
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:CheckBox ID="CheckBox1" runat="server" Checked="True" Enabled="False" />
                            </ItemTemplate>
                            <ItemStyle Width="10px" />
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </td>
            <td class="DatosTabla">
                <asp:GridView ID="gvwPermisos" runat="server" EnableModelValidation="True" Width="100%">
                    <Columns>
                        <asp:TemplateField>
                            <EditItemTemplate>
                                <asp:CheckBox ID="CheckBox1" runat="server" />
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:CheckBox ID="CheckBox1" runat="server" Checked="True" Enabled="False" />
                            </ItemTemplate>
                            <ItemStyle Width="10px" />
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
    </table>
</div>
