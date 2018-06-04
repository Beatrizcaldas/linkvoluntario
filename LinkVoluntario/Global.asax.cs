using LinkVoluntario.Domain.Interfaces;
using LinkVoluntario.Domain.Services;
using LinkVoluntario.Infra.Data;
using LinkVoluntario.Infra.Data.Repository;
using SimpleInjector;
using SimpleInjector.Integration.Web;
using SimpleInjector.Integration.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace LinkVoluntario
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {

            var container = new Container();
            container.Options.DefaultScopedLifestyle = new WebRequestLifestyle();

            container.Register<IInstitutionService, InstitutionService>(Lifestyle.Transient);
            container.Register<IInstitutionRepository, InstitutionRepository>(Lifestyle.Transient);
            container.Register<ILoginService, LoginService>(Lifestyle.Transient);
            container.Register<IEmailService, EmailService>(Lifestyle.Transient);
            container.Register<IEmailRepository, EmailRepository>(Lifestyle.Transient);
            //container.Register<DbContext, LinkVoluntarioContext>(Lifestyle.Transient);

            container.RegisterMvcControllers(Assembly.GetExecutingAssembly());

            container.Verify();

            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
