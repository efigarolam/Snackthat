<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <h1 class="center">¡Bienvenido, <%= this.user.Name + " " + this.user.LastName %>!</h1>
    <br />
    <div class="row">
        <ul class="thumbnails">
            <li class="span6">
                <div class="thumbnail">
                    <h3 class="center">Últimos Usuarios</h3>
                    <hr />
            <% if (!this.notUsers)
                { %>
                    <asp:GridView ID="GridView1" runat="server" CssClass="table table-bordered table-striped">
                    </asp:GridView>
                <% if (this.user.idPrivilege == 1)
                   { %>
                    <p class="right">
                        <a class="btn btn-medium btn-info" href="<%= webURL + "views/users/viewallusers.aspx" %>" ><i class="icon-white icon-eye-open"></i> Ver más</a>
                    </p>
                <% } %>
            <% } 
               else 
               { %>
                    <h4 class="center">¡Al parecer no hay usuarios!</h4>
                    <br />
                <% if (this.user.idPrivilege == 1)
                   { %>
                    <p class="right">
                        <a class="btn btn-medium btn-success" href="<%= webURL + "views/users/newuser.aspx" %>" ><i class="icon-white icon-user"></i> Agregar Usuario</a>
                    </p>
                <% } %>
            <% } %>
                </div>    
            </li>
            <li class="span6">
                <div class="thumbnail">
                    <h3 class="center">Últimos Clientes</h3>
                    <hr />
            <% if (!this.notCustomers)
                { %>
                    <asp:GridView ID="GridView2" runat="server" CssClass="table table-bordered table-striped">
                    </asp:GridView>
                <% if (this.user.idPrivilege == 1 || this.user.idPrivilege == 2)
                   { %>
                    <p class="right">
                        <a class="btn btn-medium btn-inverse" href="<%= webURL + "views/customers/viewallcustomers.aspx" %>" ><i class="icon-white icon-eye-open"></i> Ver más</a>
                    </p>
                <% } %>
            <% } 
               else 
               { %>
                    <h4 class="center">¡Al parecer no hay clientes registrados!</h4>
                    <br />
                <% if (this.user.idPrivilege == 1 || this.user.idPrivilege == 2)
                   { %>
                    <p class="right">
                        <a class="btn btn-medium btn-success" href="<%= webURL + "views/customers/newcustomer.aspx" %>" ><i class="icon-white icon-plus"></i> Agregar Cliente</a>
                    </p>
                <% } %>
            <% } %>
                </div> 
            </li>
            <li class="span6">
                <div class="thumbnail">
                    <h3 class="center">Últimas Rutas</h3>
                    <hr />
            <% if (!this.notRoutes)
                { %>
                    <asp:GridView ID="GridView3" runat="server" CssClass="table table-bordered table-striped">
                    </asp:GridView>
                <% if (this.user.idPrivilege == 1 || this.user.idPrivilege == 2)
                   { %>
                    <p class="right">
                        <a class="btn btn-medium btn-danger" href="<%= webURL + "views/routes/viewallroutes.aspx" %>" ><i class="icon-white icon-eye-open"></i> Ver más</a>
                    </p>
                <% } %>
            <% } 
               else 
               { %>
                    <h4 class="center">¡Al parecer no hay rutas registradas!</h4>
                    <br />
                <% if (this.user.idPrivilege == 1 || this.user.idPrivilege == 2)
                   { %>
                    <p class="right">
                        <a class="btn btn-medium btn-success" href="<%= webURL + "views/routes/newroute.aspx" %>" ><i class="icon-white icon-plus"></i> Agregar Ruta</a>
                    </p>
                <% } %>
            <% } %>
                </div> 
            </li>
            <li class="span6">
                <div class="thumbnail">
                    <h3 class="center">Últimos Productos</h3>
                    <hr />
                <% if (!this.notProducts)
                { %>
                    <asp:GridView ID="GridView4" runat="server" CssClass="table table-bordered table-striped">
                    </asp:GridView>
                <% if (this.user.idPrivilege == 1 || this.user.idPrivilege == 3)
                   { %>
                    <p class="right">
                        <a class="btn btn-medium btn-success" href="<%= webURL + "views/products/viewallproducts.aspx" %>" ><i class="icon-white icon-eye-open"></i> Ver más</a>
                    </p>
                <% } %>
            <% } 
               else 
               { %>
                    <h4 class="center">¡Al parecer no hay productos registradas!</h4>
                    <br />
                <% if (this.user.idPrivilege == 1 || this.user.idPrivilege == 3)
                   { %>
                    <p class="right">
                        <a class="btn btn-medium btn-success" href="<%= webURL + "views/products/newproduct.aspx" %>" ><i class="icon-white icon-plus"></i> Agregar Producto</a>
                    </p>
                <% } %>
            <% } %>
                </div> 
            </li>
        </ul>
    </div>
</asp:Content>

