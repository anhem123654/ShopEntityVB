Imports System.Collections.Generic
Imports System.Data.SqlClient
Imports ManShop.Common.ManShop.Common.ViewModels
Imports ManShop.Data.ManShop.Data.Infrastructure
Imports ManShop.Model.ManShop.Model.Models

Namespace ManShop.Data.Repositories
    Public Interface IOrderRepository
        Inherits IRepository(Of Order)
        Function GetRevenueStatistic(ByVal fromDate As String, ByVal toDate As String) As IEnumerable(Of RevenueStatisticViewModel)
    End Interface

    Public Class OrderRepository
        Inherits RepositoryBase(Of Order)
        Implements IOrderRepository
        Public Sub New(ByVal dbFactory As IDbFactory)
            MyBase.New(dbFactory)
        End Sub

        Public Function GetRevenueStatistic(ByVal fromDate As String, ByVal toDate As String) As IEnumerable(Of RevenueStatisticViewModel) Implements IOrderRepository.GetRevenueStatistic
            Dim parameters = New SqlParameter() {New SqlParameter("@fromDate", fromDate), New SqlParameter("@toDate", toDate)}
            Return DbContext.Database.SqlQuery(Of RevenueStatisticViewModel)("GetRevenueStatistic @fromDate,@toDate", parameters)
        End Function
    End Class
End Namespace
