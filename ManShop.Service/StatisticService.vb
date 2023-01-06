Imports System.Collections.Generic
Imports ManShop.Common.ManShop.Common.ViewModels
Imports ManShop.Data.ManShop.Data.Repositories

Namespace ManShop.Service
    Public Interface IStatisticService
        Function GetRevenueStatistic(ByVal fromDate As String, ByVal toDate As String) As IEnumerable(Of RevenueStatisticViewModel)

    End Interface
    Public Class StatisticService
        Implements IStatisticService
        Private _orderRepository As IOrderRepository
        Public Sub New(ByVal orderRepository As IOrderRepository)
            _orderRepository = orderRepository
        End Sub
        Public Function GetRevenueStatistic(ByVal fromDate As String, ByVal toDate As String) As IEnumerable(Of RevenueStatisticViewModel) Implements IStatisticService.GetRevenueStatistic
            Return _orderRepository.GetRevenueStatistic(fromDate, toDate)
        End Function
    End Class
End Namespace
