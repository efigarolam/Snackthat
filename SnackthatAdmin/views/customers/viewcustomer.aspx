<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="viewcustomer.aspx.cs" Inherits="viewcustomer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <h1 class="center">Detalles de: <%= name %></h1>
    <hr />
    <asp:DetailsView ID="DetailsView1" runat="server" CssClass="table table-condensed table-striped table-bordered">
    </asp:DetailsView>

    <h2>Dirección(es) del cliente:</h2>
    <asp:GridView ID="GridView1" runat="server" CssClass="table table-condensed table-striped table-bordered">
    </asp:GridView>
        
    <a class="btn btn-large btn-info" href="<%= webURL %>views/customers/viewallcustomers.aspx"><i class="icon-white icon-arrow-left"></i> Regresar</a>
    <a class="btn btn-large btn-primary" href="<%= webURL %>views/customers/editcustomer.aspx?id=<%= customerid %>"><i class="icon-white icon-refresh"></i> Editar</a>
    <a class="btn btn-large btn-danger" href="<%= webURL %>views/customers/viewallcustomers.aspx?action=delete&id=<%= customerid %>" onclick="return confirm('¿Deseas eliminar el registro?');"><i class="icon-white icon-trash"></i> Eliminar</a>
</asp:Content>

