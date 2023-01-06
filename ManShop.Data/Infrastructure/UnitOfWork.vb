
Namespace ManShop.Data.Infrastructure
    Public Class UnitOfWork
        Implements IUnitOfWork
        Private ReadOnly dbFactory As IDbFactory
        Private dbContextField As ManShopDbContext

        Public Sub New(ByVal dbFactory As IDbFactory)
            Me.dbFactory = dbFactory
        End Sub

        Public ReadOnly Property DbContext As ManShopDbContext
            Get
                Return If(dbContextField, Function()
                                              dbContextField = dbFactory.Init()
                                              Return dbContextField
                                          End Function())
            End Get
        End Property

        Public Sub Commit() Implements IUnitOfWork.Commit
            DbContext.SaveChanges()
        End Sub
    End Class
End Namespace
