<%@ Application Language="C#" %>
<%@ Import Namespace="System.Security.Principal" %>
<%@ Import Namespace="System.Web.Security" %>

<script runat="server">
    
    protected void Application_AuthenticateRequest(Object sender, EventArgs e)
    {
        if (!(HttpContext.Current.User == null))
        {
            if (HttpContext.Current.User.Identity.IsAuthenticated)
            {
                if (HttpContext.Current.User.Identity.GetType() == typeof(FormsIdentity))
                {
                    FormsIdentity fi = (FormsIdentity)HttpContext.Current.User.Identity;
                    FormsAuthenticationTicket fat = fi.Ticket;

                    String[] astrRoles = fat.UserData.Split('|');
                    HttpContext.Current.User = new GenericPrincipal(fi, astrRoles);
                }
            }
        }
    }
    
    void Application_Start(object sender, EventArgs e) 
    {
        // Código que se ejecuta al iniciarse la aplicación

    }
    
    void Application_End(object sender, EventArgs e) 
    {
        //  Código que se ejecuta cuando se cierra la aplicación

    }
        
    void Application_Error(object sender, EventArgs e) 
    { 
        // Código que se ejecuta al producirse un error no controlado

    }

    void Session_Start(object sender, EventArgs e) 
    {
        // Código que se ejecuta cuando se inicia una nueva sesión

    }

    void Session_End(object sender, EventArgs e) 
    {
        // Código que se ejecuta cuando finaliza una sesión. 
        // Nota: El evento Session_End se desencadena sólo con el modo sessionstate
        // se establece como InProc en el archivo Web.config. Si el modo de sesión se establece como StateServer 
        // o SQLServer, el evento no se genera.

    }
       
</script>
