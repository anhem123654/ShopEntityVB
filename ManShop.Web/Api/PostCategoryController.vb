Imports AutoMapper
Imports ManShop.Model.ManShop.Model.Models
Imports ManShop.Service.ManShop.Service
Imports ManShop.Web.ManShop.Web.Infrastructure.Core
Imports ManShop.Web.ManShop.Web.Infrastructure.Extensions
Imports ManShop.Web.ManShop.Web.Models
Imports System.Collections.Generic
Imports System.Net
Imports System.Net.Http
Imports System.Web.Http

Namespace ManShop.Web.Api
    <RoutePrefix("api/postcategory")>
    <Authorize>
    Public Class PostCategoryController
        Inherits ApiControllerBase

        Private _postCategoryService As IPostCategoryService

        Public Sub New(ByVal errorService As IErrorService, ByVal postCategoryService As IPostCategoryService)
            MyBase.New(errorService)
            Me._postCategoryService = postCategoryService
        End Sub

        <Route("getall")>
        Public Function [Get](ByVal request As HttpRequestMessage) As HttpResponseMessage
            Return CreateHttpResponse(request, Function()
                                                   Dim listCategory = _postCategoryService.GetAll()
                                                   Dim listPostCategoryVm = Mapper.Map(Of List(Of PostCategoryViewModel))(listCategory)
                                                   Dim response As HttpResponseMessage = request.CreateResponse(HttpStatusCode.OK, listPostCategoryVm)
                                                   Return response
                                               End Function)
        End Function

        <Route("add")>
        Public Function Post(ByVal request As HttpRequestMessage, ByVal postCategoryVm As PostCategoryViewModel) As HttpResponseMessage
            Return CreateHttpResponse(request, Function()
                                                   Dim response As HttpResponseMessage = Nothing

                                                   If Not ModelState.IsValid Then
                                                       request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState)
                                                   Else
                                                       Dim newPostCategory As PostCategory = New PostCategory()
                                                       newPostCategory.UpdatePostCategory(postCategoryVm)
                                                       Dim category = _postCategoryService.Add(newPostCategory)
                                                       _postCategoryService.Save()
                                                       response = request.CreateResponse(HttpStatusCode.Created, category)
                                                   End If

                                                   Return response
                                               End Function)
        End Function

        <Route("update")>
        Public Function Put(ByVal request As HttpRequestMessage, ByVal postCategoryVm As PostCategoryViewModel) As HttpResponseMessage
            Return CreateHttpResponse(request, Function()
                                                   Dim response As HttpResponseMessage = Nothing

                                                   If Not ModelState.IsValid Then
                                                       request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState)
                                                   Else
                                                       Dim postCategoryDb = _postCategoryService.GetById(postCategoryVm.ID)
                                                       postCategoryDb.UpdatePostCategory(postCategoryVm)
                                                       _postCategoryService.Update(postCategoryDb)
                                                       _postCategoryService.Save()
                                                       response = request.CreateResponse(HttpStatusCode.OK)
                                                   End If

                                                   Return response
                                               End Function)
        End Function

        Public Function Delete(ByVal request As HttpRequestMessage, ByVal id As Integer) As HttpResponseMessage
            Return CreateHttpResponse(request, Function()
                                                   Dim response As HttpResponseMessage = Nothing

                                                   If Not ModelState.IsValid Then
                                                       request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState)
                                                   Else
                                                       _postCategoryService.Delete(id)
                                                       _postCategoryService.Save()
                                                       response = request.CreateResponse(HttpStatusCode.OK)
                                                   End If

                                                   Return response
                                               End Function)
        End Function
    End Class
End Namespace
