Imports System
Imports System.Collections.Generic
Imports ManShop.Data.ManShop.Data.Infrastructure
Imports ManShop.Data.ManShop.Data.Repositories
Imports ManShop.Model.ManShop.Model.Models

Namespace ManShop.Service
    Public Interface IOrderService
        Function Create(ByRef order As Order, ByVal orderDetails As List(Of OrderDetail)) As Order
        Sub UpdateStatus(ByVal orderId As Integer)
        Sub Save()
    End Interface
    Public Class OrderService
        Implements IOrderService
        Private _orderRepository As IOrderRepository
        Private _orderDetailRepository As IOrderDetailRepository
        Private _unitOfWork As IUnitOfWork

        Public Sub New(ByVal orderRepository As IOrderRepository, ByVal orderDetailRepository As IOrderDetailRepository, ByVal unitOfWork As IUnitOfWork)
            _orderRepository = orderRepository
            _orderDetailRepository = orderDetailRepository
            _unitOfWork = unitOfWork
        End Sub
        Public Function Create(ByRef order As Order, ByVal orderDetails As List(Of OrderDetail)) As Order Implements IOrderService.Create
            Try
                _orderRepository.Add(order)
                _unitOfWork.Commit()

                For Each orderDetail In orderDetails
                    orderDetail.OrderID = order.ID
                    _orderDetailRepository.Add(orderDetail)
                Next
                Return order
            Catch ex As Exception
                Throw
            End Try
        End Function

        Public Sub UpdateStatus(ByVal orderId As Integer) Implements IOrderService.UpdateStatus
            Dim order = _orderRepository.GetSingleById(orderId)
            order.Status = True
            _orderRepository.Update(order)
        End Sub

        Public Sub Save() Implements IOrderService.Save
            _unitOfWork.Commit()
        End Sub
    End Class
End Namespace
