Imports System

Namespace ManShop.Web.Models
    <Serializable>
    Public Class ShoppingCartViewModel
        Public Property ProductId As Integer
        Public Property Product As ProductViewModel
        Public Property Quantity As Integer
    End Class
End Namespace
