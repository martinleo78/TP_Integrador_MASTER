<%@ Page Title="Listar Usuarios" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ListarUsuarios.aspx.cs" Inherits="TP_Integrador_Master.ListarUsuarios" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="mt-4">
        <h4>Listado de usuarios</h4>
        <div class="row row-cols-5">
            <div class="col-auto">
                 <label>Ingrese Usuario</label>
            </div>
            <div class="col">
                 <asp:TextBox class="form-control" ID="txt_buscar_usuario" runat="server" />
            </div>
            <div class="col-auto">
                <label>Ingrese Privilegio</label>
            </div>
            <div class="col">
                <asp:TextBox class="form-control" ID="txt_buscar_privilegio" runat="server" />
            </div>
            <div class="col-auto">
                <asp:Button class="form-control" Text="Buscar" runat="server" OnClick="buscar_click" />
            </div>
        </div>

    </div>
    <div class="mt-2">
        <asp:Panel ID="tablaUsuarios" runat="server">
        </asp:Panel>
    </div>
</asp:Content>
