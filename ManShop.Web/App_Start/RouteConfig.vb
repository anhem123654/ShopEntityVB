Imports System.Web.Mvc
Imports System.Web.Routing

Namespace ManShop.Web
    Public Class RouteConfig
        Public Shared Sub RegisterRoutes(ByVal routes As RouteCollection)
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}")
            ' BotDetect requests must not be routed
            routes.IgnoreRoute("{*botdetect}", New With {
                .botdetect = "(.*)BotDetectCaptcha\.ashx"
            })

            routes.MapRoute(name:="Confirm Order", url:="xac-nhan-don-hang.html", defaults:=New With {
.controller = "ShoppingCart",
.action = "ConfirmOrder",
.id = UrlParameter.Optional
}, namespaces:=New String() {"ManShop.Web.Controllers"})
            routes.MapRoute(name:="Cancel Order", url:="huy-don-hang.html", defaults:=New With {
.controller = "ShoppingCart",
.action = "CancelOrder",
.id = UrlParameter.Optional
}, namespaces:=New String() {"ManShop.Web.Controllers"})

            routes.MapRoute(name:="Contact", url:="lien-he.html", defaults:=New With {
.controller = "Contact",
.action = "Index",
.id = UrlParameter.Optional
}, namespaces:=New String() {"ManShop.Web.Controllers"})
            routes.MapRoute(name:="Search", url:="tim-kiem.html", defaults:=New With {
.controller = "Product",
.action = "Search",
.id = UrlParameter.Optional
}, namespaces:=New String() {"ManShop.Web.Controllers"})
            routes.MapRoute(name:="Login", url:="dang-nhap.html", defaults:=New With {
.controller = "Account",
.action = "Login",
.id = UrlParameter.Optional
}, namespaces:=New String() {"ManShop.Web.Controllers"})
            routes.MapRoute(name:="Register", url:="dang-ky.html", defaults:=New With {
.controller = "Account",
.action = "Register",
.id = UrlParameter.Optional
}, namespaces:=New String() {"ManShop.Web.Controllers"})
            routes.MapRoute(name:="Cart", url:="gio-hang.html", defaults:=New With {
.controller = "ShoppingCart",
.action = "Index",
.id = UrlParameter.Optional
}, namespaces:=New String() {"ManShop.Web.Controllers"})
            routes.MapRoute(name:="Checkout", url:="thanh-toan.html", defaults:=New With {
.controller = "ShoppingCart",
.action = "Index",
.id = UrlParameter.Optional
}, namespaces:=New String() {"ManShop.Web.Controllers"})
            routes.MapRoute(name:="Page", url:="trang/{alias}.html", defaults:=New With {
.controller = "Page",
.action = "Checkout",
.[alias] = UrlParameter.Optional
}, namespaces:=New String() {"ManShop.Web.Controllers"})

            routes.MapRoute(name:="Product Category", url:="{alias}.pc-{id}.html", defaults:=New With {
.controller = "Product",
.action = "Category",
.id = UrlParameter.Optional
}, namespaces:=New String() {"ManShop.Web.Controllers"})

            routes.MapRoute(name:="Product", url:="{alias}.p-{productId}.html", defaults:=New With {
.controller = "Product",
.action = "Detail",
.productId = UrlParameter.Optional
}, namespaces:=New String() {"ManShop.Web.Controllers"})
            routes.MapRoute(name:="TagList", url:="tag/{tagId}.html", defaults:=New With {
.controller = "Product",
.action = "ListByTag",
.tagId = UrlParameter.Optional
}, namespaces:=New String() {"ManShop.Web.Controllers"})
            routes.MapRoute(name:="Default", url:="{controller}/{action}/{id}", defaults:=New With {
.controller = "Home",
.action = "Index",
.id = UrlParameter.Optional
}, namespaces:=New String() {"ManShop.Web.Controllers"})
        End Sub
    End Class
End Namespace
