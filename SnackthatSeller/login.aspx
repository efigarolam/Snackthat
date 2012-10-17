<%@ Page Title="" Language="C#" MasterPageFile="~/Login.master" AutoEventWireup="true" CodeFile="login.aspx.cs" Inherits="login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="span4 offset4">
        <asp:Login ID="Login1" runat="server" onauthenticate="Login1_Authenticate">
        </asp:Login>
    </div>
    
</asp:Content>

