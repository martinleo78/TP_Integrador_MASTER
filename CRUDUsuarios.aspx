<%@ Page Title="CRUD Usuarios" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CRUDUsuarios.aspx.cs" Inherits="TP_Integrador_Master.CRUDUsuarios" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <asp:Label ID="label_bienvenida" runat="server" Text="" CssClass="h3"></asp:Label>
    </div>
    <div class="mt-2">
        <label>Ingrese Usuario</label>
        <asp:TextBox id="txt_buscar_usuario" runat="server" />

        <label>Ingrese Privilegio</label>
        <asp:TextBox id="txt_buscar_privilegio" runat="server" />

        <asp:Button Text="Buscar" runat="server" OnClick="buscar_click" />
    </div>
     <div class="mt-2">
        <h4>Listado de usuarios</h4>
        <asp:Panel ID="tablaUsuarios" runat="server">
        </asp:Panel>
    </div>
</asp:Content>
