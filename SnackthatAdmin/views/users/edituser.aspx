<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="edituser.aspx.cs" Inherits="edituser" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <h1 class="center">Editar Usuarios</h1>
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
		            <label for="TextBox2" class="required bold">Apellidos <span class="required bold">*</span></label>
                    <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                </div>

                <div class="row">
		            <label for="TextBox3" class="required bold">Usuario <span class="required bold">*</span></label>
                    <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
                </div>
            </div>
            
            <div class="span4">
                <div class="row">
		            <label for="DropDownList1" class="required bold">Privilegio <span class="required bold">*</span></label>
                    <asp:DropDownList ID="DropDownList1" runat="server">
                        <asp:ListItem Text="Gerente" Value="1" />
                        <asp:ListItem Text="Jefe" Value="2" />
                        <asp:ListItem Text="Inventario" Value="3" />
                        <asp:ListItem Text="Vendedor" Value="4" />
                    </asp:DropDownList>
                </div>

                <div class="row">
		            <label for="TextBox5" class="required bold">Email <span class="required bold">*</span></label>
                    <asp:TextBox ID="TextBox5" runat="server"></asp:TextBox>
                </div>

                <div class="row">
		            <label for="TextBox8" class="required bold">Teléfono <span class="required bold">*</span></label>
                    <asp:TextBox ID="TextBox8" runat="server"></asp:TextBox>
                </div>
            </div>
            
            <div class="span4">
                <div class="row">
		            <label for="TextBox7" class="required bold">Dirección <span class="required bold">*</span></label>
                    <asp:TextBox ID="TextBox7" runat="server"></asp:TextBox>
                </div>

                <div class="row">
		            <label for="TextBox6" class="required bold">RFC <span class="required bold">*</span></label>
                    <asp:TextBox ID="TextBox6" runat="server"></asp:TextBox>
                </div>
            </div>
        </div>
        <br />
        <div class="row buttons">
            <asp:Button ID="Button1" runat="server" CssClass="btn btn-large btn-primary"  
                Text="Editar" onclick="Button1_Click" />
            <a class="btn btn-large btn-info" href="<%= webURL %>views/users/viewallusers.aspx"><i class="icon-white icon-arrow-left"></i> Regresar</a>
        </div>
        
    </div>
</asp:Content>

