Imports System.Collections.Generic

Namespace ManShop.Web.Models
    Public Class HomeViewModel
        Public Property Slides As IEnumerable(Of SlideViewModel)
        Public Property LastestProducts As IEnumerable(Of ProductViewModel)
        Public Property TopSaleProducts As IEnumerable(Of ProductViewModel)

        Public Property Title As String
        Public Property MetaKeyword As String
        Public Property MetaDescription As String
    End Class
End Namespace
