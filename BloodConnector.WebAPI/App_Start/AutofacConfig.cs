using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using BloodConnector.WebAPI.Interface;
using BloodConnector.WebAPI.Models;
using BloodConnector.WebAPI.Repository;
using BloodConnector.WebAPI.Services;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.DataProtection;
using Microsoft.Owin.Security.OAuth;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace BloodConnector.WebAPI.App_Start
{
    /// <summary>
    /// Configure the IoC container - in this class Autofac
    /// </summary>
    public class AutofacConfig
    {
        public static void ConfigureContainer()
        {
            var builder = new ContainerBuilder();


            // register all controller classes in this class
            builder.RegisterControllers(Assembly.GetExecutingAssembly());
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            // authentication
            builder.RegisterType<ApplicationDbContext>().AsSelf().InstancePerRequest();
            builder.RegisterType<UserStore<User>>().As<IUserStore<User>>().InstancePerRequest();
            builder.RegisterType<ApplicationUserManager>().AsSelf().InstancePerRequest();
            builder.RegisterType<ApplicationSignInManager>().AsSelf().InstancePerRequest();
            builder.RegisterType<UserServices>().AsSelf().InstancePerRequest();
            builder.Register<ApplicationUserManager>(c => HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>());
            builder.Register<IAuthenticationManager>(c => HttpContext.Current.GetOwinContext().Authentication);
            builder.Register<OAuthAuthorizationServerOptions>(c => Startup.OAuthOptions);

            builder.RegisterType<SmtpEmailService>().As<IIdentityMessageService>().InstancePerLifetimeScope();
            builder.RegisterType<BloodGroupRepository>().As<IBloodGroupRepository>();
            builder.RegisterType<CountryRepository>().As<ICountryRepository>();

            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}