Imports System.ComponentModel.DataAnnotations

Namespace ManShop.Model.Abstract
    Public MustInherit Class Auditable
        Implements IAuditable
        Public Property CreatedDate As Date? Implements IAuditable.CreatedDate
        <MaxLength(256)>
        Public Property CreatedBy As String Implements IAuditable.CreatedBy
        Public Property UpdatedDate As Date? Implements IAuditable.UpdatedDate
        <MaxLength(256)>
        Public Property UpdatedBy As String Implements IAuditable.UpdatedBy
        <MaxLength(256)>
        Public Property MetaKeyword As String Implements IAuditable.MetaKeyword
        <MaxLength(256)>
        Public Property MetaDescription As String Implements IAuditable.MetaDescription
        Public Property Status As Boolean Implements IAuditable.Status
    End Class
End Namespace
