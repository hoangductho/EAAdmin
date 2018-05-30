using System;
using System.Web;
using System.Web.Security;
namespace IdentityManagement.Authorization
{
    public class FanxiAuthenticationModule : IHttpModule
    {
        public void Init(HttpApplication httpapp)
        {
            httpapp.AuthenticateRequest += new EventHandler(OnAuthenticate);
        }
        void OnAuthenticate(object sender, EventArgs e)
        {
            HttpApplication app = (HttpApplication)sender;
            if (app.Context.User != null && app.Context.User.Identity.IsAuthenticated)
            {
                HttpCookie authenCookie = app.Context.Request.Cookies.Get(FormsAuthentication.FormsCookieName);
                if (authenCookie == null) return ;
                FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(authenCookie.Value);
                UserIdentity Id = new UserIdentity(ticket);
                FanxiPrincipal _principal = new FanxiPrincipal(Id);
                app.Context.User = _principal;
            }
        }

        public void Dispose()
        {
            // Nothing
        }
    }
}