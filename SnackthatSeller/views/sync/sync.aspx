<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="sync.aspx.cs" Inherits="sync" %>

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

    <div id="successAlert" class="alert alert-block alert-success">
          <a class="close" data-dismiss="alert" href="#">×</a>
          <h4 class="alert-heading">¡Éxito!</h4>
          Los datos se han sincronizado correctamente...
    </div>
    <div id="errorAlert1" class="alert alert-block alert-error">
          <a class="close" data-dismiss="alert" href="#">×</a>
          <h4 class="alert-heading">¡Error!</h4>
          Imposible conectar con el Web Service, razón: no estás conectado a la red local...
    </div>
    <div id="errorAlert2" class="alert alert-block alert-error">
          <a class="close" data-dismiss="alert" href="#">×</a>
          <h4 class="alert-heading">¡Error!</h4>
          No hay datos, probablemente ya hayas sincronizado antes...
    </div>
    <div id="errorAlert3" class="alert alert-block alert-error">
          <a class="close" data-dismiss="alert" href="#">×</a>
          <h4 class="alert-heading">¡Error!</h4>
          Algo salió mal...
    </div>

    <div class="form center">
        <div class="row">
            <div class="span12">
                <div id="progresscontainer" class="progress progress-striped progress-danger">
                  <div id="progress" class="bar" style="width: 0%;">
                  </div>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="span2 offset5">
                <a class="btn btn-large btn-success" id="receiveData" href="#">Obtener Datos</a>
                <br /><br />
                <a class="btn btn-large btn-danger" id="sendData" href="#">Enviar Datos</a>
            </div>            
        </div>
    </div>

    <script src="<%= webURL %>js/jquery.signalR-0.5.0.min.js" type="text/javascript"></script>
    <script src="<%= webURL %>signalr/hubs" type="text/javascript"></script>
    <script type="text/javascript">
        $(function myfunction() {
            $('#progresscontainer').hide();
            $('#successAlert').hide();
            $('#errorAlert1').hide();
            $('#errorAlert2').hide();
            $('#errorAlert3').hide();

            var sync = $.connection.sync;

            sync.addMessage = function (message) {
                $('#progress').each(function () {
                    if(message < 100) {
                        $(this).css('width', (message) + '%');
                    } else if(message == 100) {
                        $(this).css('width', (message) + '%');                      
                        setTimeout("$('#successAlert').show(200)", 500);  
                        setTimeout("$('#progresscontainer').hide()", 700);                      
                    } else if(message == 1000) {
                        setTimeout("$('#errorAlert1').show(200)", 500);  
                        setTimeout("$('#progresscontainer').hide()", 700);                    
                    } else if(message == 2000) {
                        setTimeout("$('#errorAlert2').show(200)", 500); 
                        setTimeout("$('#progresscontainer').hide()", 700);
                    } else if(message == 3000) {
                        setTimeout("$('#errorAlert3').show(200)", 500); 
                        setTimeout("$('#progresscontainer').hide()", 700);
                    }      
                });
            }

            $('#receiveData').click(function () {
                if(confirm('¿Realmente deseas recibir los datos? Todos tus datos serán actualizados al completarse la transferencia'))
                {
                    $('#progresscontainer').hide();
                    $('#successAlert').hide();
                    $('#errorAlert1').hide();
                    $('#errorAlert2').hide();
                    $('#errorAlert3').hide();
                    $('#progress').css("width", '0%');
                    $('#progresscontainer').addClass('active');
                    $('#progresscontainer').show();
                    sync.receiveData(<%= idSeller %>);
                }
            });

            $('#sendData').click(function () {
                if(confirm('¿Realmente deseas enviar los datos? Todos tus datos serán eliminados al completarse la transferencia'))
                {
                    $('#progresscontainer').hide();
                    $('#successAlert').hide();
                    $('#errorAlert1').hide();
                    $('#errorAlert2').hide();
                    $('#errorAlert3').hide();
                    $('#progress').css("width", '0%');
                    $('#progresscontainer').addClass('active');
                    $('#progresscontainer').show();
                    sync.sendData();
                }                
            });

            $.connection.hub.start();
        });

    </script>
</asp:Content>

