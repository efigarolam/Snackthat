<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="editproduct.aspx.cs" Inherits="editproduct" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <h1 class="center">Editar Producto</h1>
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
            <div class="span4">
                <asp:HiddenField ID="HiddenField1" runat="server" />

                <div class="row">
		            <label for="TextBox1" class="required bold">Nombre <span class="required bold">*</span></label>
                    <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                </div>

                <div class="row">
		            <label for="TextBox2" class="required bold">Cantidad <span class="required bold">*</span></label>
                    <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                </div>
            </div>
            
            <div class="span4">
                <div class="row">
		            <label for="TextBox3" class="required bold">Precio <span class="required bold">*</span></label>
                    <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
                </div>

                <div class="row">
		            <label for="TextBox4" class="required bold">Descripción <span class="required bold">*</span></label>
                    <asp:TextBox ID="TextBox4" runat="server" TextMode="MultiLine"></asp:TextBox>
                </div>
            </div>

            <div class="span4">
                <div class="row">
		            <label for="DropDownList1" class="required bold">Presentación <span class="required bold">*</span></label>
                    <asp:DropDownList ID="DropDownList1" runat="server">
                        <asp:ListItem Text="80 Gramos" Value="1" />
                        <asp:ListItem Text="150 Gramos" Value="2" />
                        <asp:ListItem Text="250 Gramos" Value="3" />
                    </asp:DropDownList>
                </div>

                <div class="row">
		            <label for="TextBox5" class="required bold">Fecha de Caducidad <span class="required bold">*</span></label>
                    <asp:TextBox ID="TextBox5" runat="server"></asp:TextBox>
                </div>
            </div>
        </div>
        <br />
        <div class="row buttons">
            <asp:Button ID="Button1" runat="server" CssClass="btn btn-large btn-primary"  
                Text="Editar" onclick="Button1_Click" />
            <a class="btn btn-large btn-info" href="<%= webURL %>views/products/viewallproducts.aspx"><i class="icon-white icon-arrow-left"></i> Regresar</a>
        </div>
    </div>
</asp:Content>

