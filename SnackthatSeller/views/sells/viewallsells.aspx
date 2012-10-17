<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="viewallsells.aspx.cs" Inherits="viewallsells" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <h1 class="center">Ventas Realizadas</h1>
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

    <asp:GridView ID="GridView1" runat="server" CssClass="table table-striped table-condensed table-bordered" 
        onrowdatabound="GridView1_RowDataBound" >
    <Columns>
        <asp:CommandField ShowDeleteButton="true" ShowEditButton="true" ShowSelectButton="true" Visible="false"/>

        <asp:TemplateField HeaderText="Acciones">
            <ItemTemplate>
                <a class="btn btn-mini btn-success" href="<%= webURL %>views/sells/viewsell.aspx?id=<%# Eval("ID") %>"><i class="icon-white icon-eye-open"> </i></a>
                <a class="btn btn-mini btn-danger" href="<%= webURL %>views/sells/viewallsells.aspx?action=delete&id=<%# Eval("ID") %>" onclick="return confirm('¿Deseas eliminar el registro?');"><i class="icon-white icon-trash"> </i></a>
            </ItemTemplate>       
        </asp:TemplateField>
    </Columns>
    </asp:GridView>
    <a class="btn btn-large btn-info" href="<%= webURL %>Default.aspx"><i class="icon-white icon-home"></i> Inicio</a>
</asp:Content>

