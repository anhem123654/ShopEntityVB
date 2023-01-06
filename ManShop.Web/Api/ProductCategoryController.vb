Imports AutoMapper
Imports System
Imports System.Net
Imports System.Net.Http
Imports System.Web.Http
Imports System.Web.Script.Serialization
Imports System.Data.Entity.Validation
Imports ManShop.Web.ManShop.Web.Infrastructure.Core
Imports ManShop.Model.ManShop.Model.Models
Imports ManShop.Web.ManShop.Web.Models
Imports ManShop.Web.ManShop.Web.Infrastructure.Extensions
Imports ManShop.Service.ManShop.Service
Imports ManShop.Service.TeduShop.Service

Namespace ManShop.Web.Api
    <RoutePrefix("api/productcategory")>
    <Authorize>
    Public Class ProductCategoryController
        Inherits ApiControllerBase
#Region "Initialize"
        Private _productCategoryService As IProductCategoryService

        Public Sub New(ByVal errorService As IErrorService, ByVal productCategoryService As IProductCategoryService)
            MyBase.New(errorService)
            _productCategoryService = productCategoryService
        End Sub

#End Region

        <Route("getallparents")>
        <HttpGet>
        Public Function GetAll(ByVal request As HttpRequestMessage) As HttpResponseMessage
            Return CreateHttpResponse(request, Function()
                                                   Dim model = _productCategoryService.GetAll()

                                                   Dim responseData = Mapper.Map(Of IEnumerable(Of ProductCategory), IEnumerable(Of ProductCategoryViewModel))(model)

                                                   Dim response = request.CreateResponse(HttpStatusCode.OK, responseData)
                                                   Return response
                                               End Function)
        End Function
        <Route("getbyid/{id:int}")>
        <HttpGet>
        Public Function GetById(ByVal request As HttpRequestMessage, ByVal id As Integer) As HttpResponseMessage
            Return CreateHttpResponse(request, Function()
                                                   Dim model = _productCategoryService.GetById(id)

                                                   Dim responseData = Mapper.Map(Of ProductCategory, ProductCategoryViewModel)(model)

                                                   Dim response = request.CreateResponse(HttpStatusCode.OK, responseData)

                                                   Return response
                                               End Function)
        End Function

        <Route("getall")>
        <HttpGet>
        Public Function GetAll(ByVal request As HttpRequestMessage, ByVal keyword As String, ByVal page As Integer, ByVal Optional pageSize As Integer = 20) As HttpResponseMessage
            Return CreateHttpResponse(request, Function()
                                                   Dim totalRow = 0
                                                   Dim model = _productCategoryService.GetAll(keyword)

                                                   totalRow = model.Count()
                                                   Dim query = model.OrderByDescending(Function(x) x.CreatedDate).Skip(page * pageSize).Take(pageSize)

                                                   Dim responseData = Mapper.Map(Of IEnumerable(Of ProductCategory), IEnumerable(Of ProductCategoryViewModel))(query)

                                                   Dim paginationSet = New PaginationSet(Of ProductCategoryViewModel)() With {
                                       .Items = responseData,
                                       .Page = page,
                                       .TotalCount = totalRow,
                                       .TotalPages = Math.Ceiling(CDec(totalRow) / pageSize)
                                   }
                                                   Dim response = request.CreateResponse(HttpStatusCode.OK, paginationSet)
                                                   Return response
                                               End Function)
        End Function


        <Route("create")>
        <HttpPost>
        <AllowAnonymous>
        Public Function Create(ByVal request As HttpRequestMessage, ByVal productCategoryVm As ProductCategoryViewModel) As HttpResponseMessage
            Return CreateHttpResponse(request, Function()
                                                   Dim response As HttpResponseMessage = Nothing
                                                   If Not ModelState.IsValid Then
                                                       response = request.CreateResponse(HttpStatusCode.BadRequest, ModelState)
                                                   Else
                                                       Dim newProductCategory = New ProductCategory()
                                                       newProductCategory.UpdateProductCategory(productCategoryVm)
                                                       newProductCategory.CreatedDate = Date.Now
                                                       _productCategoryService.Add(newProductCategory)
                                                       _productCategoryService.Save()

                                                       Dim responseData = Mapper.Map(Of ProductCategory, ProductCategoryViewModel)(newProductCategory)
                                                       response = request.CreateResponse(HttpStatusCode.Created, responseData)
                                                   End If

                                                   Return response
                                               End Function)
        End Function

        <Route("update")>
        <HttpPut>
        <AllowAnonymous>
        Public Function Update(ByVal request As HttpRequestMessage, ByVal productCategoryVm As ProductCategoryViewModel) As HttpResponseMessage
            Return CreateHttpResponse(request, Function()
                                                   Dim response As HttpResponseMessage = Nothing
                                                   If Not ModelState.IsValid Then
                                                       response = request.CreateResponse(HttpStatusCode.BadRequest, ModelState)
                                                   Else
                                                       Dim dbProductCategory = _productCategoryService.GetById(productCategoryVm.ID)

                                                       dbProductCategory.UpdateProductCategory(productCategoryVm)
                                                       dbProductCategory.UpdatedDate = Date.Now

                                                       _productCategoryService.Update(dbProductCategory)
                                                       Try
                                                           _productCategoryService.Save()
                                                       Catch e As DbEntityValidationException
                                                           For Each eve In e.EntityValidationErrors
                                                               Console.WriteLine("Entity of type ""{0}"" in state ""{1}"" has the following validation errors:", eve.Entry.Entity.[GetType]().Name, eve.Entry.State)
                                                               For Each ve In eve.ValidationErrors
                                                                   Console.WriteLine("- Property: ""{0}"", Error: ""{1}""", ve.PropertyName, ve.ErrorMessage)
                                                               Next
                                                           Next
                                                           Throw
                                                       End Try


                                                       Dim responseData = Mapper.Map(Of ProductCategory, ProductCategoryViewModel)(dbProductCategory)
                                                       response = request.CreateResponse(HttpStatusCode.Created, responseData)
                                                   End If

                                                   Return response
                                               End Function)
        End Function

        <Route("delete")>
        <HttpDelete>
        <AllowAnonymous>
        Public Function Delete(ByVal request As HttpRequestMessage, ByVal id As Integer) As HttpResponseMessage
            Return CreateHttpResponse(request, Function()
                                                   Dim response As HttpResponseMessage = Nothing
                                                   If Not ModelState.IsValid Then
                                                       response = request.CreateResponse(HttpStatusCode.BadRequest, ModelState)
                                                   Else
                                                       Dim oldProductCategory = _productCategoryService.Delete(id)
                                                       _productCategoryService.Save()

                                                       Dim responseData = Mapper.Map(Of ProductCategory, ProductCategoryViewModel)(oldProductCategory)
                                                       response = request.CreateResponse(HttpStatusCode.Created, responseData)
                                                   End If

                                                   Return response
                                               End Function)
        End Function
        <Route("deletemulti")>
        <HttpDelete>
        <AllowAnonymous>
        Public Function DeleteMulti(ByVal request As HttpRequestMessage, ByVal checkedProductCategories As String) As HttpResponseMessage
            Return CreateHttpResponse(request, Function()
                                                   Dim response As HttpResponseMessage = Nothing
                                                   If Not ModelState.IsValid Then
                                                       response = request.CreateResponse(HttpStatusCode.BadRequest, ModelState)
                                                   Else
                                                       Dim listProductCategory = New JavaScriptSerializer().Deserialize(Of List(Of Integer))(checkedProductCategories)
                                                       For Each item In listProductCategory
                                                           _productCategoryService.Delete(item)
                                                       Next

                                                       _productCategoryService.Save()

                                                       response = request.CreateResponse(HttpStatusCode.OK, listProductCategory.Count)
                                                   End If

                                                   Return response
                                               End Function)
        End Function
    End Class
End Namespace
