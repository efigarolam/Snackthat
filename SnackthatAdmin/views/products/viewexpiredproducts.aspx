<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="viewexpiredproducts.aspx.cs" Inherits="viewexpiredproducts" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <h1 class="center">Productos Caducados</h1>
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
    </asp:GridView>
    <a class="btn btn-large btn-info" href="<%= webURL %>views/products/viewallproducts.aspx"><i class="icon-white icon-arrow-left"></i> Ver productos</a>
</asp:Content>

