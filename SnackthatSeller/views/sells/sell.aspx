<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="sell.aspx.cs" Inherits="sell" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <h1 class="center">Registrar Venta</h1>
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
                    <div id="showTotal" class="alert alert-info">
                        <a class="close" data-dismiss="alert">×</a>
                        <h3 class="alert-heading">El total de venta es:</h3>
                        <p class="totalPrice"></p>
                    </div>
                </div>                
            </div>

            <div class="row">
                <div class="span4 offset4">
                    <label for="DropDownList1" class="required bold">Cliente <span class="required">*</span></label>
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
                                        <input type="hidden" id="Precio<%# Eval("ID") %>" value="<%# Eval("Precio") %>" />
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
                <input type="hidden" id="Total" name="Total" value="0" />
                <a id="Calculate" class="btn btn-large btn-info" href="#"><i class="icon-white icon-shopping-cart"></i> Calcular</a>
                <asp:Button ID="Button1" runat="server" CssClass="btn btn-large btn-primary"  
                    Text="   Registrar Venta   " onclick="Button1_Click" />               
            </div>

        </div>
        <br />
        
    <% } %>

    <script type="text/javascript">
        $(document).ready(function () {
            $('#showTotal').hide();
            $('.quantity').attr('disabled', true);
            $('#ContentPlaceHolder1_Button1').attr('disabled', true);

            $('input:checkbox').click(function () {
                if ($(this).is(':checked')) {
                    var id = $(this).attr("value");
                    var selector = ".id" + id;
                    $(selector).attr("disabled", false);
                } else {
                    var id = $(this).attr("value");
                    var selector = ".id" + id;
                    var selector2 = ".id" + id + " option[value=0]";
                    $(selector).attr("disabled", true);
                    $(selector2).attr("selected", true);
                }
            });

            var idsChecked = [];
            var total = 0;
            $('#Calculate').click(function () {
                total = 0;
                idsChecked = [];
                $('.totalPrice').text("");
                $('#showTotal').hide();
                $('#ContentPlaceHolder1_Button1').attr('disabled', true);

                $('input[type=checkbox]:checked').each(function () {
                    idsChecked.push($(this).val());
                });

                for (i = 0; i < idsChecked.length; i++) {
                    var selector = "#Precio" + idsChecked[i].toString();
                    var selector2 = ".id" + idsChecked[i].toString();
                    var price = $(selector).val();
                    var amount = $(selector2).val();
                    total += (price * amount);
                }

                $('#Total').val(total);

                if (total > 0) {
                    $('.totalPrice').text("$" + total + " pesos");
                    $('#showTotal').show();
                    $('#ContentPlaceHolder1_Button1').attr('disabled', false);
                }
            });
        });
    </script>
</asp:Content>

