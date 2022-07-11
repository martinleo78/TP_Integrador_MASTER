<%@ Page Title="Mantenimiento Usuarios" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MantenimientoUsuarios.aspx.cs" Inherits="TP_Integrador_Master.MantenimientoUsuarios" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">


    <div class="container">
        <div>
            <asp:Button Text="Mantenimiento" ID="buttonEdit" Class="btn btn-outline-dark m-2 w-auto" runat="server" OnClick="buttonEdit_Click" />
            <asp:Button Text="Nuevo" ID="buttonNew" Class="btn btn-outline-dark m-2 w-auto" runat="server" OnClick="buttonNew_Click" />
        </div>

        <div class="row ">
            <div id="crearUsuario" class="col mt-1" runat="server">
                <h2>Crear Usuario</h2>
                <div class="mb-3 row row-cols-3">
                    <label class="col-2 form-label">Nombre del usuario</label>
                    <asp:TextBox ID="txt_alta_usuario" Class="col form-control w-25" runat="server" />
                    <asp:Label ID="label_error_nombre" class="text-danger" runat="server"></asp:Label>
                </div>
                <div class="mb-3  row row-cols-3">
                    <label class="col-2 form-label">Contraseña</label>
                    <asp:TextBox ID="txt_alta_password" Class="col form-control w-25" TextMode="Password" runat="server" />
                    <asp:Label ID="label_error_pass" class="text-danger" runat="server"></asp:Label>
                </div>
                <div class="mb-3 row row-cols-3">
                    <label class="col-2 form-label">Confirmar Contraseña</label>
                    <asp:TextBox ID="txt_alta_password_confirm" Class="col form-control w-25" TextMode="Password" runat="server" />
                </div>
                <div class="mb-3 row">
                    <label class="col col-form-label">Permiso</label>
                    <div class="col-sm-10">
                        <asp:DropDownList ID="lstpermisoscrear" Class="form-control w-auto" runat="server">
                        </asp:DropDownList>
                    </div>
                </div>
                <asp:Button Text="Crear" Class="btn btn-outline-dark m-2 w-auto" runat="server" OnClick="nuevo_Click" />

            </div>

            <div id="mantenimientoUsuario" class="col mt-1" runat="server">
                <h2>Mantenimiento de usuarios</h2>
                <div class="mb-3 row row-cols-3">
                    <label class="col-2 col-form-label">Buscar Usuario</label>
                    <asp:TextBox ID="txt_buscar_usuario" Class="form-control w-25" runat="server" />
                    <asp:Button Text="Buscar" Class="btn btn-outline-dark ms-1 w-auto" runat="server" OnClick="buscar_click" />
                </div>

                <div class="mt-3">
                    <h3>Usuario seleccionado</h3>
                    <div class="mb-3 row row-cols-3">
                        <label class="col-2 col-form-label">Usuario</label>
                        <asp:TextBox ID="txt_usuario" Class="form-control-plaintext w-25" ReadOnly="true" runat="server" />
                        <asp:Label ID="label_error_usuario" Text="" class="text-danger" runat="server" />
                    </div>
                    <div class="mb-3 row row-cols-3">
                        <label class="col-2 col-form-label">Permiso</label>
                        <asp:DropDownList ID="lstpermisosactualizar" Class="form-control w-auto" runat="server"></asp:DropDownList>
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



