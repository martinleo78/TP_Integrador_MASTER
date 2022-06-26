<%@ Page Title="Crea o Modificar Usuario" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CreaUsuario.aspx.cs" Inherits="TP_Integrador_Master.CreaUsuario" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <asp:Label ID="label_bienvenida" runat="server" Text="" CssClass="h3"></asp:Label>

         <div class="mt-2">
        <label>Ingrese Usuario</label>
        <asp:TextBox id="txt_buscar_usuario" runat="server" />

        <asp:Button Text="Buscar" runat="server" OnClick="buscar_click" />
    </div>
     <div class="mt-2">
        <h4>Listado de usuarios</h4>
        <asp:Panel ID="tablaUsuarios" runat="server">
        </asp:Panel>
    </div>

    </div>
</asp:Content>