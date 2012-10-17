<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="viewsell.aspx.cs" Inherits="viewsell" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <h1 class="center">Detalles de Venta</h1>
    <hr />

    <asp:DetailsView ID="DetailsView1" runat="server" CssClass="table table-striped table-condensed table-bordered">
    </asp:DetailsView>

    <h2>Productos vendidos:</h2>
    <asp:GridView ID="GridView1" runat="server" CssClass="table table-striped table-condensed table-bordered">
    </asp:GridView>
        
    <a class="btn btn-large btn-info" href="<%= webURL %>views/sells/viewallsells.aspx"><i class="icon-white icon-arrow-left"></i> Regresar</a>
    <a class="btn btn-large btn-danger" href="<%= webURL %>views/sells/viewallsells.aspx?action=delete&id=<%= sellid %>" onclick="return confirm('¿Deseas eliminar el registro?');"><i class="icon-white icon-trash"></i> Eliminar</a>

</asp:Content>

