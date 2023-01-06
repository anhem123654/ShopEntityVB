Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema
Imports ManShop.Model.ManShop.Model.Models

Namespace ManShop.Model.Models
    <Table("OrderDetails")>
    Public Class OrderDetail
        <Key>
        <Column(Order:=1)>
        Public Property OrderID As Integer

        <Key>
        <Column(Order:=2)>
        Public Property ProductID As Integer

        Public Property Quantity As Integer

        Public Property Price As Decimal

        <ForeignKey("OrderID")>
        Public Overridable Property Order As Order

        <ForeignKey("ProductID")>
        Public Overridable Property Product As Product
    End Class
End Namespace
