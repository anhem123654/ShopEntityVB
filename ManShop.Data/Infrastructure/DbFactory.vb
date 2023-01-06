Namespace ManShop.Data.Infrastructure
    Public Class DbFactory
        Inherits Disposable
        Implements IDbFactory
        Private dbContext As ManShopDbContext
        Public Function Init() As ManShopDbContext Implements IDbFactory.Init
            Return If(dbContext, Function()
                                     dbContext = New ManShopDbContext()
                                     Return dbContext
                                 End Function())
        End Function
        Protected Overrides Sub DisposeCore()
            If dbContext IsNot Nothing Then dbContext.Dispose()
        End Sub
    End Class
End Namespace
