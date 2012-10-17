<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="assignproducts.aspx.cs" Inherits="assignproducts" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <h1 class="center">Registrar salida de Productos</h1>
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
                <div class="span12">
                    <label for="DropDownList1" class="required bold">Vendedor <span class="required">*</span></label>
                    <asp:DropDownList ID="DropDownList1" runat="server">
                    </asp:DropDownList>
                </div>
            </div>
            <br />
            <div class="row">
            <div class="span12">
                <label for="GridView1" class="required bold">Productos <span class="required">*</span></label>
                <asp:GridView ID="GridView1" runat="server" CssClass="table table-striped table-condensed table-bordered" 
                    onrowdatabound="GridView1_RowDataBound">
                     <Columns>
                            <asp:TemplateField HeaderText="Sel.">
                                <ItemTemplate>
                                    <input type="checkbox" id="Productos" name="Productos" value="<%# Eval("ID") %>" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Cant.">
                                <ItemTemplate>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                </asp:GridView>
            </div>
        </div>
        
        <div class="row buttons">
            <asp:Button ID="Button1" runat="server" CssClass="btn btn-large btn-primary"  
                Text="   Asignar   " onclick="Button1_Click" />
        </div>

        </div>
        <br />
        
    <% } %>

    <script type="text/javascript" src="<%= webURL + "js/jsAssignProductsForm.js" %>"></script>
    <script type="text/javascript">
        $(function init() {
            validateForm();
        });
    </script>
</asp:Content>

