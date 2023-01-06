Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks
Imports ManShop.Data.ManShop.Data.Infrastructure
Imports ManShop.Data.ManShop.Data.Repositories
Imports ManShop.Model.ManShop.Model.Models

Namespace ManShop.Service
    Public Interface IErrorService
        Function Create(ByVal [error] As [Error]) As [Error]
        Sub Save()
    End Interface

    Public Class ErrorService
        Implements IErrorService

        Private _errorRepository As IErrorRepository
        Private _unitOfWork As IUnitOfWork

        Public Sub New(ByVal errorRepository As IErrorRepository, ByVal unitOfWork As IUnitOfWork)
            Me._errorRepository = errorRepository
            Me._unitOfWork = unitOfWork
        End Sub

        Public Function Create(ByVal [error] As [Error]) As [Error] Implements IErrorService.Create
            Return _errorRepository.Add([error])
        End Function

        Public Sub Save() Implements IErrorService.Save
            _unitOfWork.Commit()
        End Sub
    End Class
End Namespace
