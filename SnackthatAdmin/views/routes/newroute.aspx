<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="newroute.aspx.cs" Inherits="newroute" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <h1 class="center">Crear Ruta</h1>
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
		    <label for="TextBox1" class="required bold">Nombre <span class="required">*</span></label>
            <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
        </div>
        <br /> 
        
        <% if (!hideGrid)
           {
        %>
            <div class="row">
                <div class="span12">
                    <label class="bold" for="GridView1">Clientes</label>
                    <asp:GridView ID="GridView1" runat="server" CssClass="table table-striped table-condensed table-bordered">
                        <Columns>
                            <asp:TemplateField HeaderText="Sel.">
                                <ItemTemplate>
                                    <input type="checkbox" id="Direcciones" name="Direcciones" value="<%# Eval("ID Direccion") %>" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>        
        <% } %>

        <div class="row buttons">
            <asp:Button ID="Button1" runat="server" CssClass="btn btn-large btn-primary"  
                Text="Guardar" onclick="Button1_Click" />
            <a class="btn btn-large btn-info" href="<%= webURL %>views/routes/viewallroutes.aspx"><i class="icon-white icon-arrow-left"></i> Regresar</a>
        </div>
    </div>
</asp:Content>

