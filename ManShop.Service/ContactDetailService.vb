Imports ManShop.Data.ManShop.Data.Infrastructure
Imports ManShop.Data.ManShop.Data.Repositories
Imports ManShop.Model.ManShop.Model.Models

Namespace ManShop.Service
    Public Interface IContactDetailService
        Function GetDefaultContact() As ContactDetail
    End Interface
    Public Class ContactDetailService
        Implements IContactDetailService
        Private _contactDetailRepository As IContactDetailRepository
        Private _unitOfWork As IUnitOfWork
        Public Sub New(ByVal contactDetailRepository As IContactDetailRepository, ByVal unitOfWork As IUnitOfWork)
            Me._contactDetailRepository = contactDetailRepository
            Me._unitOfWork = unitOfWork
        End Sub
        Public Function GetDefaultContact() As ContactDetail Implements IContactDetailService.GetDefaultContact
            Return _contactDetailRepository.GetSingleByCondition(Function(x) x.Status)
        End Function
    End Class
End Namespace
