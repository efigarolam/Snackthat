<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="viewproduct.aspx.cs" Inherits="viewproduct" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <h1 class="center">Detalles de: <%= name %></h1>
    <hr />
    <asp:DetailsView ID="DetailsView1" runat="server" CssClass="table table-striped table-condensed table-bordered">
    </asp:DetailsView>
    <a class="btn btn-large btn-info" href="<%= webURL %>views/products/viewallproducts.aspx"><i class="icon-white icon-arrow-left"></i> Regresar</a>
    <a class="btn btn-large btn-primary" href="<%= webURL %>views/products/editproduct.aspx?id=<%= productid %>"><i class="icon-white icon-refresh"></i> Editar</a>
    <a class="btn btn-large btn-danger" href="<%= webURL %>views/products/viewallproducts.aspx?action=delete&id=<%= productid %>" onclick="return confirm('¿Deseas eliminar el registro?');"><i class="icon-white icon-trash"></i> Eliminar</a>
</asp:Content>  

