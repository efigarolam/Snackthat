<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="assignroutes.aspx.cs" Inherits="assignroutes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <h1 class="center">Asignar Rutas a Vendedores</h1>
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

    <% if (hideForm == false)
       { 
    %>
        <div class="form center">
            <div class="row">
                <div class="span6">
                    <label for="DropDownList1" class="required bold">Ruta <span class="required">*</span></label>
                    <asp:DropDownList ID="DropDownList1" runat="server">
                    </asp:DropDownList>
                </div>

                <div class="span6">
                    <label for="DropDownList2" class="required bold">Vendedor <span class="required">*</span></label>
                    <asp:DropDownList ID="DropDownList2" runat="server">
                    </asp:DropDownList>
                </div>
            </div>
        
            <div class="row buttons">
                <asp:Button ID="Button1" runat="server" CssClass="btn btn-large btn-primary"  
                    Text="   Asignar   " onclick="Button1_Click" />
            </div>

        </div>
        <br />
        <div class="row">
            <div class="span12">
                <h2>Rutas asignadas:</h2>
                <br />
                <asp:GridView ID="GridView1" runat="server" CssClass="table table-striped table-condensed table-bordered">
                </asp:GridView>
            </div>
        </div>
    <% } %>
</asp:Content>

