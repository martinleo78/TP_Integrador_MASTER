<%@ Page Title="Crea o Modificar Usuario" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CreaUsuario.aspx.cs" Inherits="TP_Integrador_Master.CreaUsuario" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="p-2 m-2 border border-dark">
        <h2>Crear Usuario</h2>
        <div class="mb-3 row">
            <label class="col-sm-2 col-form-label">Nombre del usuario</label>
            <asp:TextBox ID="txt_alta_usuario" Class="form-control w-25" runat="server" />
            <asp:Label ID="label_error_nombre" class="text-danger" runat="server"></asp:Label>
        </div>
        <div class="mb-3 row">
            <label class="col-sm-2 col-form-label">Contraseña</label>
            <asp:TextBox ID="txt_alta_password" Class="form-control w-25" TextMode="Password" runat="server" />
            <asp:Label ID="label_error_pass" class="text-danger" runat="server"></asp:Label>
        </div>
        <div class="mb-3 row">
            <label class="col-sm-2 col-form-label">Confirmar Contraseña</label>
            <asp:TextBox ID="txt_alta_password_confirm" Class="form-control w-25" TextMode="Password" runat="server" />
        </div>
        <div class="mb-3 row">
            <label class="col-sm-2 col-form-label">Permiso</label>
            <div class="col-sm-10">
                <asp:DropDownList id="lstpermisoscrear" runat="server">
                </asp:DropDownList>
            </div>
            <div class="col-sm-10">
                <asp:Button Text="Crear" Class="btn btn-outline-dark m-2 w-auto" runat="server" OnClick="crear_Click" />
            </div>
        </div>
    </div>
    <div class="p-2 m-2 border border-dark">
        <h2>Borrar/Editar Usuario</h2>
        <div class="mb-3 row">
            <label class="col-sm-2 col-form-label">Buscar Usuario</label>
            <asp:TextBox ID="txt_buscar_usuario" Class="form-control w-25" runat="server" />
            <asp:Button Text="Buscar" Class="btn btn-outline-dark ms-1 w-auto" runat="server" OnClick="buscar_click" />
        </div>
        <div class="mt-2">
            <h3>Usuario seleccionado</h3>
            <div class="mb-3 row">
                <label class="col-sm-2 col-form-label">Usuario</label>
                <asp:TextBox ID="text_usuario" Class="form-control-plaintext w-25" ReadOnly="true" runat="server" />
            </div>

            <div class="mb-3 row">
                <label class="col-sm-2 col-form-label">Permiso</label>
                <asp:TextBox ID="txt_permiso" Class="form-control-plaintext w-25" ReadOnly="true" runat="server" />
            </div>
            <div>
                <div>
                    <h3>Borrar usuario</h3>
                    <asp:Button Text="Borrar" Class="btn btn-outline-dark m-2 w-auto" runat="server" OnClick="borrar_Click" />
                    <asp:Label ID="label_error_borrar" class="text-danger" Text="" runat="server" />
                </div>

                <div>
                    <h3>Editar permiso</h3>
                    <div class="mb-3 row">
                        <label class="col-sm-2 col-form-label">Nuevo Permiso</label>
                        <div class="col-sm-10">
                            <asp:DropDownList ID="lstpermisosactualizar" runat="server">
                            </asp:DropDownList>
                        </div>
                        <div class="col-sm-10">
                            <asp:Button Text="Editar" Class="btn btn-outline-dark m-2 w-auto" runat="server" OnClick="editar_Click" />
                            <asp:Label ID="label_error_edit" class="text-danger" Text="" runat="server" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
