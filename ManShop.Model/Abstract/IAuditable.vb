Namespace ManShop.Model.Abstract
    Public Interface IAuditable
        Property CreatedDate As Date?
        Property CreatedBy As String
        Property UpdatedDate As Date?
        Property UpdatedBy As String
        Property MetaKeyword As String
        Property MetaDescription As String
        Property Status As Boolean
    End Interface
End Namespace
