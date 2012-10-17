<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="reports.aspx.cs" Inherits="views_reports_reports" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <h1 class="center">Generar Reportes</h1>
    <hr />
    <%
    if (this.notification[0] != "nothing")
    { %>
        <div class="alert alert-<%= this.notification[0] %>">
            <a class="close" data-dismiss="alert">×</a>
            <h4 class="alert-heading"><%= this.notification[1] %></h4>
            <%= this.notification[2] %>
        </div>
    <% } %>

    <div class="form center">
        <div class="row">
            <div class="span4">
                <div class="row">
		            <label class="bold" for="DropDownList1">¡Selecciona una ruta!</label>
                    <asp:DropDownList ID="DropDownList1" runat="server">
                    </asp:DropDownList>
                    
                    <br />
                    <asp:Button class="btn btn-large btn-primary" ID="Button1" runat="server" 
                        Text="Generar Reporte de Ruta" onclick="Button1_Click" />
                </div>
                <br />
                <div class="row">
		            <label class="bold" for="DropDownList2">¡Selecciona un producto!</label>
                    <asp:DropDownList ID="DropDownList2" runat="server">
                    </asp:DropDownList>
                    
                    <br />
                    <asp:Button class="btn btn-large btn-info" ID="Button2" runat="server" 
                        Text="Generar Reporte de de Producto" onclick="Button2_Click" />
                </div>
            </div>
            <div class="span4">
                <div class="row">
		            <label class="bold" for="Button5">¡Generar Reporte de Productos Caducados!</label>
                    <asp:Button class="btn btn-large btn-danger" ID="Button5" runat="server" 
                        Text="Generar" onclick="Button5_Click" />
                </div>
            </div>
            <div class="span4">
                <div class="row">
		            <label class="bold" for="DropDownList3">¡Selecciona un vendedor!</label>
                    <asp:DropDownList ID="DropDownList3" runat="server">
                    </asp:DropDownList>
                    
                    <br />
                    <asp:Button class="btn btn-large btn-warning" ID="Button3" runat="server" 
                        Text="Generar Reporte de Vendedores" onclick="Button3_Click" />
                </div>
                <br />
                <div class="row">
		            <label class="bold" for="DropDownList4">¡Selecciona un mes!</label>
                    <asp:DropDownList ID="DropDownList4" runat="server">
                    </asp:DropDownList>
                    
                    <br />
                    <asp:Button class="btn btn-large btn-inverse" ID="Button4" runat="server" 
                        Text="Generar Reporte por Mes" onclick="Button4_Click" />
                </div>                
            </div>
        </div>
    </div>
</asp:Content>

