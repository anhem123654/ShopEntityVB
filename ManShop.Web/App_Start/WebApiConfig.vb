Imports Microsoft.Owin.Security.OAuth
Imports Newtonsoft.Json.Serialization
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Web.Http

Namespace ManShop.Web
    Module WebApiConfig
        Sub Register(ByVal config As HttpConfiguration)
            config.MapHttpAttributeRoutes()
            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = New DefaultContractResolver With {
                .IgnoreSerializableAttribute = True
            }
            config.SuppressDefaultHostAuthentication()
            config.Filters.Add(New HostAuthenticationFilter(OAuthDefaults.AuthenticationType))
            config.Routes.MapHttpRoute(name:="DefaultApi", routeTemplate:="api/{controller}/{id}", defaults:=New With {Key .id = RouteParameter.[Optional]
            })
        End Sub
    End Module
End Namespace
