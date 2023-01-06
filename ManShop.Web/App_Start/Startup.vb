Imports Microsoft.Owin
Imports Owin
Imports System.Reflection
Imports Microsoft.Owin.Security.DataProtection
Imports Autofac
Imports ManShop.Data.ManShop.Data.Infrastructure
Imports Microsoft.AspNet.Identity
Imports ManShop.Model.ManShop.Model.Models
Imports ManShop.Data.ManShop.Data
Imports Autofac.Integration.Mvc
Imports ManShop.Data.ManShop.Data.Repositories
Imports ManShop.Service.ManShop.Service
Imports Autofac.Integration.WebApi
Imports System.Web.Http
Imports System.Web.Mvc

<Assembly: OwinStartup(GetType(ManShop.Web.App_Start.Startup))>
Namespace ManShop.Web.App_Start
    Partial Public Class Startup
        Public Sub Configuration(ByVal app As IAppBuilder)
            ConfigAutofac(app)
            ConfigureAuth(app)
        End Sub

        Private Sub ConfigAutofac(ByVal app As IAppBuilder)
            Dim builder = New ContainerBuilder()
            builder.RegisterControllers(Assembly.GetExecutingAssembly())
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly())
            builder.RegisterType(Of UnitOfWork)().[As](Of IUnitOfWork)().InstancePerRequest()
            builder.RegisterType(Of DbFactory)().[As](Of IDbFactory)().InstancePerRequest()
            builder.RegisterType(Of ManShopDbContext)().AsSelf().InstancePerRequest()
            builder.RegisterType(Of ApplicationUserStore)().[As](Of IUserStore(Of ApplicationUser))().InstancePerRequest()
            builder.RegisterType(Of ApplicationUserManager)().AsSelf().InstancePerRequest()
            builder.RegisterType(Of ApplicationSignInManager)().AsSelf().InstancePerRequest()
            builder.Register(Function(c) HttpContext.Current.GetOwinContext().Authentication).InstancePerRequest()
            builder.Register(Function(c) app.GetDataProtectionProvider()).InstancePerRequest()
            builder.RegisterAssemblyTypes(GetType(PostCategoryRepository).Assembly).Where(Function(t) t.Name.EndsWith("Repository")).AsImplementedInterfaces().InstancePerRequest()
            builder.RegisterAssemblyTypes(GetType(PostCategoryService).Assembly).Where(Function(t) t.Name.EndsWith("Service")).AsImplementedInterfaces().InstancePerRequest()
            Dim container As Autofac.IContainer = builder.Build()
            DependencyResolver.SetResolver(New AutofacDependencyResolver(container))
            GlobalConfiguration.Configuration.DependencyResolver = New AutofacWebApiDependencyResolver(CType(container, IContainer))
        End Sub
    End Class
End Namespace
