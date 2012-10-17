<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="setexpiredproducts.aspx.cs" Inherits="setexpiredproducts" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <h1 class="center">Dar de Baja Productos Caducados</h1>
    <hr/>

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
            <div class="span4 offset4">
                <asp:Button ID="Button1" runat="server" Text="Dar de Baja Productos Caducados"  
                    CssClass="btn btn-warning btn-large" onclick="Button1_Click" />
            </div>
        </div>
    </div>
    

</asp:Content>

