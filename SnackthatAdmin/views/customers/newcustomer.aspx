<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="newcustomer.aspx.cs" Inherits="newcustomer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <h1 class="center">Crear Clientes</h1>
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
            <div class="span6">
                <div class="row">
		            <label for="TextBox1" class="required bold">Nombre <span class="required bold">*</span></label>
                    <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                </div>

                <div class="row">
		            <label for="TextBox2" class="required bold">Apellidos <span class="required bold">*</span></label>
                    <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                </div>
            </div>

            <div class="span6">
                <div class="row">
		            <label for="TextBox3" class="required bold">Email <span class="required bold">*</span></label>
                    <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
                </div>

                <div class="row">
		            <label for="TextBox4" class="required bold">RFC <span class="required bold">*</span></label>
                    <asp:TextBox ID="TextBox4" runat="server"></asp:TextBox>
                </div>
            </div>
        </div>

        <br />
        <div class="row">
            <h4>Dirección(es):</h4>
            <a id="addAddress" class="btn btn-mini btn-success" href="#newaddress"><i class="icon-white icon-plus"></i> Agregar</a>
            <br />
            <div class="span6">
                <label for="addr1">Dirección 1</label>
                <input type="text" name="Address1" id="addr1" />
                <input type="hidden" name="nAddresses" value="1" />
                <a name="newaddress"></a>
            </div>

            <div id="Phones" class="span6">
                <label for="phn1">Teléfono 1</label>
                <input type="text" name="Phone1" id="phn1" />
            </div>
        </div>
        <br />
        <div class="row buttons">
            <asp:Button ID="Button1" runat="server" CssClass="btn btn-large btn-primary"  
                Text="Guardar" onclick="Button1_Click" />
            <a class="btn btn-large btn-info" href="<%= webURL %>views/customers/viewallcustomers.aspx"><i class="icon-white icon-arrow-left"></i> Regresar</a>
        </div>        
    </div>
    
    <script type="text/javascript" src="<%= webURL + "js/jsNewCustomerForm.js" %>"></script>
    <script type="text/javascript">
        $(function init() {
            addNewAddresses();
        });
    </script>
</asp:Content>

