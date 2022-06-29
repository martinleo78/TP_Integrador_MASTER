<%@ Page Title="CRUD Privilegios" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CRUDPrivilegios.aspx.cs" Inherits="TP_Integrador_Master.CRUDPrivilegios" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="mt-4">
        <h4>Busqueda de Privilegios</h4>

    </div>

    <label>Nombre de Privilegio</label>
    <asp:TextBox id="txtb_buscarPrivilegio" runat="server" />
    <asp:Button Text="Buscar" class="btn btn-primary" runat="server" OnClick="buscarPrivilegio_click" />

    <asp:TextBox Text="Nuevo Privilegio" type="button" class="btn btn-secondary" data-bs-toggle="modal" data-bs-target="#myModal" runat="server" />
    <!-- The Modal -->
    <div class="modal fade" id="myModal">
        <div class="modal-dialog">
            <div class="modal-content">

                <!-- Modal Header -->
                <div class="modal-header">
                    <h4 class="modal-title">Nuevo Privilegio</h4>
                    <button type="button" class="btn-close" data-bs-dismiss="modal"></button>
                </div>

                <!-- Modal body -->
                <div class="modal-body">
                    <label>Nombre de Privilegio</label>
                    <asp:TextBox id="txtb_nuevoPrivilegio" runat="server" />
                    <asp:Button Text="Guardar" class="btn btn-success" runat="server" OnClick="crearPrivilegio_click" />

                </div>

                <!-- Modal footer -->
                <div class="modal-footer">
                    <button type="button" class="btn btn-danger" data-bs-dismiss="modal">Cerrar</button>
                </div>

            </div>
        </div>
    </div>


    <div class="mt-6">
        <asp:label class="badge bg-success" id="label_resultado" runat="server" Text=""></asp:label>
    </div>

    <div class="mt-6">
        <asp:Panel ID="tablaPrivilegios" runat="server">
        </asp:Panel>
    </div>
</asp:Content>
