using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using AnimalsShelter.Web.WebUtils.Contracts;
using System.Security.Principal;
using System.Web;

namespace AnimalsShelter.Web.WebUtils
{
    public class HttpContextProvider : IHttpContextProvider
    {
        public HttpContext CurrentHttpContext
        {
            get
            {
                return HttpContext.Current;
            }
        }

        public IOwinContext CurrentOwinContext
        {
            get
            {
                return HttpContext.Current.GetOwinContext();
            }
        }

        public IIdentity CurrentIdentity
        {
            get
            {
                return HttpContext.Current.User.Identity;
            }
        }

        public TManager GetUserManager<TManager>()
        {
            return HttpContext.Current.GetOwinContext().GetUserManager<TManager>();
        }
    }
}