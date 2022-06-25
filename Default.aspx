<%@ Page Title="Login" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="TP_Integrador_Master._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row justify-content-center">
        <div class="col-auto">
            <table class="table table-borderless table-hover table-light table-lg table-responsive">
                <tr>
                    <th scope="col"></th>
                    <th scope="col">LOGIN</th>
                    <th scope="col"></th>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label2" runat="server" Text="UserId:"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label3" runat="server" Text="Password :"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="TextBox2" runat="server" TextMode="Password"></asp:TextBox>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                    <td style="text-align: center">
                        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Log In" />
                    </td>
                    <td></td>
                </tr>
            </table>
            <table class="table table-sm table-responsive">
                <tr>
                    <td>
                        <asp:Label ID="Label4" class="alert-danger" runat="server"></asp:Label>
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
