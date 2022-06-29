<%@ Page Title="Crea o Modificar Usuario" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CreaUsuario.aspx.cs" Inherits="TP_Integrador_Master.CreaUsuario" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="p-2 m-2">
        <h2>Mantenimiento de usuarios</h2>
        <div class="mb-3 row">
            <label class="col-sm-2 col-form-label">Buscar Usuario</label>
            <asp:TextBox ID="txt_buscar_usuario" Class="form-control w-25" runat="server" />
            <asp:Button Text="Buscar" Class="btn btn-outline-dark ms-1 w-auto" runat="server" OnClick="buscar_click" />
        </div>
        <div class="mt-3">
            <h3>Usuario seleccionado</h3>
            <div class="mb-3 row">
                <label class="col-sm-2 col-form-label">Usuario</label>
                <asp:TextBox ID="text_usuario" Class="form-control-plaintext w-25" ReadOnly="true" runat="server" />
            </div>
            <div class="mb-3 row">
                <label class="col-sm-2 col-form-label">Permiso</label>
                <asp:TextBox ID="txt_permiso" Class="form-control-plaintext w-25" ReadOnly="true" runat="server" />
            </div>
            <div class="container mt-3">
                <div class="row align-items-start">
                    <div class="col">
                        <h3>Nuevo Usuario</h3>
                        <asp:TextBox Text="Nuevo Usuario" type="button" class="btn btn-outline-dark" data-bs-toggle="modal" data-bs-target="#myModal" runat="server" />
                        <asp:Label ID="label_nuevo_usuario" runat="server" />
                        <div class="modal fade" id="myModal">
                            <div class="modal-dialog">
                                <div class="modal-content">
                                    <div class="modal-header">
                                        <h4 class="modal-title">Nuevo Usuario</h4>
                                        <button type="button" class="btn-close" data-bs-dismiss="modal"></button>
                                    </div>
                                    <div class="modal-body">
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
                                                <asp:DropDownList ID="lstpermisoscrear" runat="server">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="modal-footer">
                                        <asp:button text="Crear" type="button" class="btn btn-outline-dark" runat="server" OnClick="nuevo_Click" />
                                        <button type="button" class="btn btn-danger" data-bs-dismiss="modal">Cerrar</button>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col">
                        <h3>Editar permiso</h3>
                        <div class="mb-3 row">
                            <div class="col-sm-10">
                                <asp:DropDownList ID="lstpermisosactualizar" runat="server">
                                </asp:DropDownList>
                                <asp:Button Text="Editar" Class="btn btn-outline-dark m-2 w-auto" runat="server" OnClick="editar_Click" />
                                <asp:Label ID="label_error_edit" class="text-danger" Text="" runat="server" />
                            </div>
                        </div>
                    </div>
                    <div class="col">
                        <h3>Borrar usuario</h3>
                        <asp:Button Text="Borrar" Class="btn btn-outline-dark m-2 w-auto" runat="server" OnClick="borrar_Click" />
                        <asp:Label ID="label_error_borrar" class="text-danger" Text="" runat="server" />
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>