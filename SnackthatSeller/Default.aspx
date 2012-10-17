<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <h1 class="center">¡Bienvenido, <%= this.user.Name + " " + this.user.LastName %>!</h1>
    <br />
    <div class="row">
        <ul class="thumbnails">
            <li class="span4">
                <div class="thumbnail">
                    <h3 class="center">Tus Ventas</h3>
                    <hr />
                <% if (!this.notSells)
                   { %>
                    <asp:GridView ID="GridView1" runat="server" CssClass="table table-bordered table-striped">
                    </asp:GridView>
                    <p class="right">
                        <a class="btn btn-medium btn-info" href="<%= webURL + "views/sells/viewallsells.aspx" %>" ><i class="icon-white icon-eye-open"></i> Ver más</a>
                    </p>
                <% }
                   else
                   { %>
                    <h4 class="center">¡No tienes ventas registradas aún!</h4>
                    <br />
                    <p class="right">
                        <a class="btn btn-medium btn-success" href="<%= webURL + "views/sells/sell.aspx" %>" ><i class="icon-white icon-shopping-cart"></i> Registrar venta</a>
                    </p>
                <% } %>
                </div>
            </li>
            <li class="span4">
                <div class="thumbnail">
                    <h3 class="center">Tu Ruta</h3>
                    <hr />
                <% if (!this.notRoute)
                   { %>
                    <asp:DetailsView ID="DetailsView1" runat="server" CssClass="table table-bordered table-striped">
                    </asp:DetailsView>
                    <p class="right">
                        <a class="btn btn-medium btn-info" href="<%= webURL + "views/routes/viewroute.aspx" %>" ><i class="icon-white icon-eye-open"></i> Ver más</a>
                    </p>
                <% }
                   else
                   { %>
                    <h4 class="center">¡No tienes ruta asignada todavía!</h4>
                    <br />
                    <p class="right">
                        <a class="btn btn-medium btn-danger" href="<%= webURL + "views/sync/sync.aspx" %>" ><i class="icon-white icon-refresh"></i> Sincronizar</a>
                    </p>
                <% } %>
                </div>
            </li>
            <li class="span4">
                <div class="thumbnail">         
                    <h3 class="center">Tus Clientes</h3>
                    <hr />
                <% if (!this.notCustomers)
                   { %>
                    <asp:GridView ID="GridView2" runat="server" CssClass="table table-bordered table-striped">
                    </asp:GridView>
                    <p class="right">
                        <a class="btn btn-medium btn-info" href="<%= webURL + "views/customers/viewallcustomers.aspx" %>" ><i class="icon-white icon-eye-open"></i> Ver más</a>
                    </p>
                <% }
                   else
                   { %>
                    <h4 class="center">¡No tienes clientes asignados!</h4>
                    <br />
                    <p class="right">
                        <a class="btn btn-medium btn-danger" href="<%= webURL + "views/sync/sync.aspx" %>" ><i class="icon-white icon-refresh"></i> Sincronizar</a>
                        <a class="btn btn-medium btn-success" href="<%= webURL + "views/customers/newcustomer.aspx" %>" ><i class="icon-white icon-plus"></i> Agregar Cliente</a>
                    </p>
                <% } %>
                </div>
            </li>
        </ul>
    </div>
</asp:Content>

