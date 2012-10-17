<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="viewallproducts.aspx.cs" Inherits="viewallproducts" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <h1 class="center">Consulta de Productos</h1>
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

    <asp:GridView ID="GridView1" runat="server" CssClass="table table-striped table-condensed table-bordered" >
    <Columns>
        <asp:CommandField ShowDeleteButton="true" ShowEditButton="true" ShowSelectButton="true" Visible="false"/>

        <asp:TemplateField HeaderText="Acciones">
            <ItemTemplate>
                <a class="btn btn-mini btn-success" href="<%= webURL %>views/products/viewproduct.aspx?id=<%# Eval("ID") %>"><i class="icon-white icon-eye-open"></i></a>
                <a class="btn btn-mini btn-info" href="<%= webURL %>views/products/editproduct.aspx?id=<%# Eval("ID") %>"><i class="icon-white icon-refresh"></i></a>
                <a class="btn btn-mini btn-danger" href="<%= webURL %>views/products/viewallproducts.aspx?action=delete&id=<%# Eval("ID") %>" onclick="return confirm('¿Deseas eliminar el registro?');"><i class="icon-white icon-trash"></i></a>
            </ItemTemplate>       
        </asp:TemplateField>
    </Columns>
    </asp:GridView>
    <a class="btn btn-large btn-success" href="<%= webURL %>views/products/newproduct.aspx"><i class="icon-white icon-plus"></i> Agregar Producto</a>
    <a class="btn btn-large btn-info" href="<%= webURL %>views/products/viewexpiredproducts.aspx"><i class="icon-white icon-zoom-in"></i> Ver Caducados</a>
</asp:Content>

