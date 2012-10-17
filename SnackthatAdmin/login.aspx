<%@ Page Title="" Language="C#" MasterPageFile="~/Login.master" AutoEventWireup="true" CodeFile="login.aspx.cs" Inherits="login" %>

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
    <div class="span4 offset4">
        <asp:Login ID="Login1" runat="server" onauthenticate="Login1_Authenticate" TextLayout="TextOnLeft" Font-Bold="True" Font-Size="Larger">
        </asp:Login>
    </div>
</asp:Content>

