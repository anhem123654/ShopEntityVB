Imports System
Imports System.Data.Entity.Infrastructure
Imports System.Data.Entity.Validation
Imports System.Diagnostics
Imports System.Net
Imports System.Net.Http
Imports System.Web.Http
Imports ManShop.Model.ManShop.Model.Models
Imports ManShop.Service.ManShop.Service

Namespace ManShop.Web.Infrastructure.Core
    Public Class ApiControllerBase
        Inherits ApiController
        Private _errorService As IErrorService

        Public Sub New(ByVal errorService As IErrorService)
            _errorService = errorService
        End Sub

        Protected Function CreateHttpResponse(ByVal requestMessage As HttpRequestMessage, ByVal [function] As Func(Of HttpResponseMessage)) As HttpResponseMessage
            Dim response As HttpResponseMessage = Nothing
            Try
                response = [function].Invoke()
            Catch ex As DbEntityValidationException
                For Each eve In ex.EntityValidationErrors
                    Call Trace.WriteLine($"Entity of type ""{eve.Entry.Entity.[GetType]().Name}"" in state ""{eve.Entry.State}"" has the following validation error.")
                    For Each ve In eve.ValidationErrors
                        Trace.WriteLine($"- Property: ""{ve.PropertyName}"", Error: ""{ve.ErrorMessage}""")
                    Next
                Next
                LogError(ex)
                response = requestMessage.CreateResponse(HttpStatusCode.BadRequest, ex.InnerException.Message)
            Catch dbEx As DbUpdateException
                LogError(dbEx)
                response = requestMessage.CreateResponse(HttpStatusCode.BadRequest, dbEx.InnerException.Message)
            Catch ex As Exception
                LogError(ex)
                response = requestMessage.CreateResponse(HttpStatusCode.BadRequest, ex.Message)
            End Try
            Return response
        End Function

        Private Sub LogError(ByVal ex As Exception)
            Try
                Dim [error] As [Error] = New [Error]()
                [error].CreatedDate = Date.Now
                [error].Message = ex.Message
                [error].StackTrace = ex.StackTrace
                _errorService.Create([error])
                _errorService.Save()
            Catch
            End Try
        End Sub
    End Class
End Namespace
