Imports System.Net
Imports System.Net.Http
Imports System.Web.Http
Imports ManShop.Service.ManShop.Service
Imports ManShop.Web.ManShop.Web.Infrastructure.Core

Namespace ManShop.Web.Api

    <RoutePrefix("api/statistic")>
    Public Class StatisticController
        Inherits ApiControllerBase
        Private _statisticService As IStatisticService
        Public Sub New(ByVal errorService As IErrorService, ByVal statisticService As IStatisticService)
            MyBase.New(errorService)
            _statisticService = statisticService
        End Sub

        <Route("getrevenue")>
        <HttpGet>
        Public Function GetRevenueStatistic(ByVal request As HttpRequestMessage, ByVal fromDate As String, ByVal toDate As String) As HttpResponseMessage

            Return CreateHttpResponse(request, Function()
                                                   Dim model = _statisticService.GetRevenueStatistic(fromDate, toDate)
                                                   Dim response As HttpResponseMessage = request.CreateResponse(HttpStatusCode.OK, model)
                                                   Return response
                                               End Function)
        End Function

    End Class
End Namespace
