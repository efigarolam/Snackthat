<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="viewroute.aspx.cs" Inherits="viewroute" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <%
    if (this.notification[0] != "nothing")
    { %>
        <div class="alert alert-<%= this.notification[0] %>">
            <a class="close" data-dismiss="alert">×</a>
            <h4 class="alert-heading"><%= this.notification[1] %></h4>
            <%= this.notification[2] %>
        </div>
    <% } %>

    <% if (showData)
       {
    %>
        <h1 class="center">Tu ruta asignada es: <%= name %></h1>
        <hr />
        <asp:DetailsView ID="DetailsView1" runat="server" CssClass="table table-striped table-condensed table-bordered">
        </asp:DetailsView>
    
   
        <h2>Tus clientes de ruta son:</h2>
        <asp:GridView ID="GridView1" runat="server" CssClass="table table-striped table-condensed table-bordered">
        </asp:GridView>
    <% } %>
    <a class="btn btn-large btn-info" href="<%= webURL %>Default.aspx"><i class="icon-white icon-home"></i> Inicio</a>
</asp:Content>

