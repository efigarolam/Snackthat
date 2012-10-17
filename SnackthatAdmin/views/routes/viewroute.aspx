<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="viewroute.aspx.cs" Inherits="viewroute" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <h1 class="center">Detalles de: <%= name %></h1>
    <hr />
    <asp:DetailsView ID="DetailsView1" runat="server" CssClass="table table-striped table-condensed table-bordered">
    </asp:DetailsView>
    
    <% if (showCustomers)
       {
    %>
        <h2>Clientes de la ruta:</h2>
        <asp:GridView ID="GridView1" runat="server" CssClass="table table-striped table-condensed table-bordered">
        </asp:GridView>
    <% } %>
    <a class="btn btn-large btn-info" href="<%= webURL %>views/routes/viewallroutes.aspx"><i class="icon-white icon-arrow-left"></i> Regresar</a>
    <a class="btn btn-large btn-primary" href="<%= webURL %>views/routes/editroute.aspx?id=<%= routeid %>"><i class="icon-white icon-refresh"></i> Editar</a>
    <a class="btn btn-large btn-danger" href="<%= webURL %>views/routes/viewallroutes.aspx?action=delete&id=<%= routeid %>" onclick="return confirm('¿Deseas eliminar el registro?');"><i class="icon-white icon-trash"></i> Eliminar</a>
</asp:Content>

