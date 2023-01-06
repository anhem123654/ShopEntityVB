

Imports ManShop.Common.ManShop.Common
Imports ManShop.Data.ManShop.Data.Infrastructure
Imports ManShop.Data.ManShop.Data.Repositories
Imports ManShop.Model.ManShop.Model.Models

Namespace ManShop.Service
    Public Interface ICommonService
        Function GetFooter() As Footer
        Function GetSlides() As IEnumerable(Of Slide)
        Function GetSystemConfig(ByVal code As String) As SystemConfig
    End Interface

    Public Class CommonService
        Implements ICommonService

        Private _footerRepository As IFooterRepository
        Private _systemConfigRepository As ISystemConfigRepository
        Private _unitOfWork As IUnitOfWork
        Private _slideRepository As ISlideRepository

        Public Sub New(ByVal footerRepository As IFooterRepository, ByVal systemConfigRepository As ISystemConfigRepository, ByVal unitOfWork As IUnitOfWork, ByVal slideRepository As ISlideRepository)
            _footerRepository = footerRepository
            _unitOfWork = unitOfWork
            _systemConfigRepository = systemConfigRepository
            _slideRepository = slideRepository
        End Sub

        Public Function GetFooter() As Footer Implements ICommonService.GetFooter
            Return _footerRepository.GetSingleByCondition(Function(x) x.ID = CommonConstants.DefaultFooterId)
        End Function

        Public Function GetSlides() As IEnumerable(Of Slide) Implements ICommonService.GetSlides
            Return _slideRepository.GetMulti(Function(x) x.Status)
        End Function

        Public Function GetSystemConfig(code As String) As SystemConfig Implements ICommonService.GetSystemConfig
            Return _systemConfigRepository.GetSingleByCondition(Function(x) x.Code = code)
        End Function
    End Class
End Namespace
