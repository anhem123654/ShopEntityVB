Imports ManShop.Data.ManShop.Data.Infrastructure
Imports ManShop.Data.ManShop.Data.Repositories
Imports ManShop.Model.ManShop.Model.Models

Namespace TeduShop.Service
    Public Interface IFeedbackService
        Function Create(ByVal feedback As Feedback) As Feedback
        Sub Save()
    End Interface

    Public Class FeedbackService
        Implements IFeedbackService
        Private _feedbackRepository As IFeedbackRepository
        Private _unitOfWork As IUnitOfWork

        Public Sub New(ByVal feedbackRepository As IFeedbackRepository, ByVal unitOfWork As IUnitOfWork)
            _feedbackRepository = feedbackRepository
            _unitOfWork = unitOfWork
        End Sub

        Public Function Create(ByVal feedback As Feedback) As Feedback Implements IFeedbackService.Create
            Return _feedbackRepository.Add(feedback)
        End Function

        Public Sub Save() Implements IFeedbackService.Save
            _unitOfWork.Commit()
        End Sub
    End Class
End Namespace
