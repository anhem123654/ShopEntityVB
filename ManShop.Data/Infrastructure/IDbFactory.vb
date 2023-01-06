Imports System
Imports ManShop.Data.ManShop.Data

Namespace ManShop.Data.Infrastructure
    Public Interface IDbFactory
        Inherits IDisposable
        Function Init() As ManShopDbContext
    End Interface
End Namespace
