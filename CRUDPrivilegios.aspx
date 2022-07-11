<%@ Page Title="CRUD Privilegios" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CRUDPrivilegios.aspx.cs" Inherits="TP_Integrador_Master.CRUDPrivilegios" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">


    <div class="container">
        <div>
            <asp:Button Text="Mantenimiento" ID="buttonEdit" Class="btn btn-outline-dark m-2 w-auto" runat="server" OnClick="buttonEdit_Click" />
            <asp:Button Text="Nuevo" ID="buttonNew" Class="btn btn-outline-dark m-2 w-auto" runat="server" OnClick="buttonNew_Click" />
        </div>

        <div class="row ">
            <div id="crearPrivilegio" class="col mt-1" runat="server">
                <h2>Crear Privilegio</h2>
                <div class="mb-3 row row-cols-3">
                    <label class="col-2 form-label">Nombre del Privilegio</label>
                    <asp:TextBox ID="txt_alta_Privilegio" Class="col form-control w-25" runat="server" />
                    <asp:Label ID="label_error_nuevo" class="text-danger" runat="server"></asp:Label>
                </div>
                <div class="mb-3  row row-cols-3">
                    <label class="col-2 form-label">Listar Usuarios</label>
                    <asp:CheckBox ID="listar_checkbox" runat="server" />
                </div>
                <div class="mb-3  row row-cols-3">
                    <label class="col-2 form-label">Mantenimiento Usuarios</label>
                    <asp:CheckBox ID="mantenimiento_checkbox" runat="server" />
                </div>
                <div class="mb-3  row row-cols-3">
                    <label class="col-2 form-label">CRUD Privilegios</label>
                    <asp:CheckBox ID="privilegios_checkbox" runat="server" />
                </div>
                <asp:Button Text="Crear" Class="btn btn-outline-dark m-2 w-auto" runat="server" OnClick="nuevo_Click" />
            </div>
            <div id="mantenimientoPrivilegio" class="col mt-1" runat="server">
                <h2>Mantenimiento de Privilegios</h2>
                <div class="mb-3 row row-cols-3">
                    <label class="col-2 col-form-label">Buscar Privilegio</label>
                    <asp:TextBox ID="txt_buscar_Privilegio" Class="form-control w-25" runat="server" />
                    <asp:Button Text="Buscar" Class="btn btn-outline-dark ms-1 w-auto" runat="server" OnClick="buscar_click" />
                </div>
                <div class="mt-3">
                    <h3>Privilegio seleccionado</h3>
                    <div class="mb-3 row row-cols-3">
                        <label class="col-2 col-form-label">Privilegio</label>
                        <asp:TextBox ID="txt_mantiene_Privilegio" Class="form-control-plaintext w-25" ReadOnly="true" runat="server" />
                        <asp:Label ID="label_error_mantenimiento" Text="" class="text-danger" runat="server" />
                    </div>
                    <div class="mb-3  row row-cols-3">
                        <label class="col-2 form-label">Listar Usuarios</label>
                        <asp:CheckBox ID="listar_M_checkbox" runat="server" />
                    </div>
                    <div class="mb-3  row row-cols-3">
                        <label class="col-2 form-label">Mantenimiento Usuarios</label>
                        <asp:CheckBox ID="mantenimiento_M_checkbox" runat="server" />
                    </div>
                    <div class="mb-3  row row-cols-3">
                        <label class="col-2 form-label">CRUD Privilegios</label>
                        <asp:CheckBox ID="privilegios_M_checkbox" runat="server" />
                    </div>
                    <div class="mb-3 row row-cols-3">
                        <label class="col-2 col-form-label">Cantidad Usuarios</label>
                        <asp:TextBox ID="txt_usuarios_Privilegio" Class="form-control-plaintext w-25" ReadOnly="true" runat="server" />
                        <asp:Label ID="label_mantiene_privilegio" Text="" class="text-danger" runat="server" />
                    </div>
                </div>

                <div class="mb-3 row row-cols-3">
                    <div class="col-2">
                        <asp:Button Text="Editar" Class="btn btn-outline-dark m-2 w-auto" runat="server" OnClick="editar_Click" />
                    </div>
                    <div class="col-2">
                        <asp:Button Text="Borrar" Class="btn btn-outline-dark m-2 w-auto" runat="server" OnClick="borrar_Click" />
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
