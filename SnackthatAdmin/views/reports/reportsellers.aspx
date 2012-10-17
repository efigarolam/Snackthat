<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="reportsellers.aspx.cs" Inherits="views_reports_reportsellers" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="container">
        <div class="row">
            <div class="span8">
                <CR:CrystalReportViewer ID="crvSeller" runat="server" AutoDataBind="true" 
                    Height="520px" ToolPanelView="None" Width="1120px" 
                    BestFitPage="False" PageZoomFactor="100">
                </CR:CrystalReportViewer>
            </div>                        
        </div>
    </div>    
</asp:Content>

