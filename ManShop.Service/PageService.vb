Imports ManShop.Data.ManShop.Data.Infrastructure
Imports ManShop.Data.ManShop.Data.Repositories
Imports ManShop.Model.ManShop.Model.Models

Namespace ManShop.Service
    Public Interface IPageService
        Function GetByAlias(ByVal [alias] As String) As Page
    End Interface
    Public Class PageService
        Implements IPageService
        Private _pageRepository As IPageRepository
        Private _unitOfWork As IUnitOfWork
        Public Sub New(ByVal pageRepository As IPageRepository, ByVal unitOfWork As IUnitOfWork)
            _pageRepository = pageRepository
            _unitOfWork = unitOfWork
        End Sub
        Public Function GetByAlias(ByVal [alias] As String) As Page Implements IPageService.GetByAlias
            Return _pageRepository.GetSingleByCondition(Function(x) x.Alias Is [alias])
        End Function
    End Class
End Namespace
