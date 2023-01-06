Imports AutoMapper
Imports ManShop.Common.ManShop.Common
Imports ManShop.Model.ManShop.Model.Models
Imports ManShop.Service.ManShop.Service
Imports ManShop.Web.ManShop.Web.Infrastructure.Core
Imports ManShop.Web.ManShop.Web.Infrastructure.Extensions
Imports ManShop.Web.ManShop.Web.Models
Imports OfficeOpenXml
Imports System.IO
Imports System.Net
Imports System.Net.Http
Imports System.Threading.Tasks
Imports System.Web.Http
Imports System.Web.Script.Serialization

Namespace TeduShop.Web.Api
    <RoutePrefix("api/product")>
    <Authorize>
    Public Class ProductController
        Inherits ApiControllerBase

        Private _productService As IProductService

        Public Sub New(ByVal errorService As IErrorService, ByVal productService As IProductService)
            MyBase.New(errorService)
            Me._productService = productService
        End Sub

        <Route("getallparents")>
        <HttpGet>
        Public Function GetAll(ByVal request As HttpRequestMessage) As HttpResponseMessage
            Dim func As Func(Of HttpResponseMessage) = Function()
                                                           Dim model = _productService.GetAll()
                                                           Dim responseData = Mapper.Map(Of IEnumerable(Of Product), IEnumerable(Of ProductViewModel))(model)
                                                           Dim response = request.CreateResponse(HttpStatusCode.OK, responseData)
                                                           Return response
                                                       End Function

            Return CreateHttpResponse(request, func)
        End Function

        <Route("getbyid/{id:int}")>
        <HttpGet>
        Public Function GetById(ByVal request As HttpRequestMessage, ByVal id As Integer) As HttpResponseMessage
            Return CreateHttpResponse(request, Function()
                                                   Dim model = _productService.GetById(id)
                                                   Dim responseData = Mapper.Map(Of Product, ProductViewModel)(model)
                                                   Dim response = request.CreateResponse(HttpStatusCode.OK, responseData)
                                                   Return response
                                               End Function)
        End Function

        <Route("getall")>
        <HttpGet>
        Public Function GetAll(ByVal request As HttpRequestMessage, ByVal keyword As String, ByVal page As Integer, ByVal Optional pageSize As Integer = 20) As HttpResponseMessage
            Return CreateHttpResponse(request, Function()
                                                   Dim totalRow As Integer = 0
                                                   Dim model = _productService.GetAll(keyword)
                                                   totalRow = model.Count()
                                                   Dim query = model.OrderByDescending(Function(x) x.CreatedDate).Skip(page * pageSize).Take(pageSize)
                                                   Dim responseData = Mapper.Map(Of IEnumerable(Of Product), IEnumerable(Of ProductViewModel))(query.AsEnumerable())
                                                   Dim paginationSet = New PaginationSet(Of ProductViewModel)() With {
                                                       .Items = responseData,
                                                       .Page = page,
                                                       .TotalCount = totalRow,
                                                       .TotalPages = CInt(Math.Ceiling(CDec(totalRow) / pageSize))
                                                   }
                                                   Dim response = request.CreateResponse(HttpStatusCode.OK, paginationSet)
                                                   Return response
                                               End Function)
        End Function

        <Route("create")>
        <HttpPost>
        Public Function Create(ByVal request As HttpRequestMessage, ByVal productCategoryVm As ProductViewModel) As HttpResponseMessage
            Return CreateHttpResponse(request, Function()
                                                   Dim response As HttpResponseMessage = Nothing

                                                   If Not ModelState.IsValid Then
                                                       response = request.CreateResponse(HttpStatusCode.BadRequest, ModelState)
                                                   Else
                                                       Dim newProduct = New Product()
                                                       newProduct.UpdateProduct(productCategoryVm)
                                                       newProduct.CreatedDate = DateTime.Now
                                                       newProduct.CreatedBy = User.Identity.Name
                                                       _productService.Add(newProduct)
                                                       _productService.Save()
                                                       Dim responseData = Mapper.Map(Of Product, ProductViewModel)(newProduct)
                                                       response = request.CreateResponse(HttpStatusCode.Created, responseData)
                                                   End If

                                                   Return response
                                               End Function)
        End Function

        <Route("update")>
        <HttpPut>
        Public Function Update(ByVal request As HttpRequestMessage, ByVal productVm As ProductViewModel) As HttpResponseMessage
            Return CreateHttpResponse(request, Function()
                                                   Dim response As HttpResponseMessage = Nothing

                                                   If Not ModelState.IsValid Then
                                                       response = request.CreateResponse(HttpStatusCode.BadRequest, ModelState)
                                                   Else
                                                       Dim dbProduct = _productService.GetById(productVm.ID)
                                                       dbProduct.UpdateProduct(productVm)
                                                       dbProduct.UpdatedDate = DateTime.Now
                                                       dbProduct.UpdatedBy = User.Identity.Name
                                                       _productService.Update(dbProduct)
                                                       _productService.Save()
                                                       Dim responseData = Mapper.Map(Of Product, ProductViewModel)(dbProduct)
                                                       response = request.CreateResponse(HttpStatusCode.Created, responseData)
                                                   End If

                                                   Return response
                                               End Function)
        End Function

        <Route("delete")>
        <HttpDelete>
        Public Function Delete(ByVal request As HttpRequestMessage, ByVal id As Integer) As HttpResponseMessage
            Return CreateHttpResponse(request, Function()
                                                   Dim response As HttpResponseMessage = Nothing

                                                   If Not ModelState.IsValid Then
                                                       response = request.CreateResponse(HttpStatusCode.BadRequest, ModelState)
                                                   Else
                                                       Dim oldProductCategory = _productService.Delete(id)
                                                       _productService.Save()
                                                       Dim responseData = Mapper.Map(Of Product, ProductViewModel)(oldProductCategory)
                                                       response = request.CreateResponse(HttpStatusCode.Created, responseData)
                                                   End If

                                                   Return response
                                               End Function)
        End Function

        <Route("deletemulti")>
        <HttpDelete>
        Public Function DeleteMulti(ByVal request As HttpRequestMessage, ByVal checkedProducts As String) As HttpResponseMessage
            Return CreateHttpResponse(request, Function()
                                                   Dim response As HttpResponseMessage = Nothing

                                                   If Not ModelState.IsValid Then
                                                       response = request.CreateResponse(HttpStatusCode.BadRequest, ModelState)
                                                   Else
                                                       Dim listProductCategory = New JavaScriptSerializer().Deserialize(Of List(Of Integer))(checkedProducts)

                                                       For Each item In listProductCategory
                                                           _productService.Delete(item)
                                                       Next

                                                       _productService.Save()
                                                       response = request.CreateResponse(HttpStatusCode.OK, listProductCategory.Count)
                                                   End If

                                                   Return response
                                               End Function)
        End Function

        <Route("import")>
        <HttpPost>
        Public Async Function Import() As Task(Of HttpResponseMessage)
            If Not Request.Content.IsMimeMultipartContent() Then
                Request.CreateErrorResponse(HttpStatusCode.UnsupportedMediaType, "Định dạng không được server hỗ trợ")
            End If

            Dim root = HttpContext.Current.Server.MapPath("~/UploadedFiles/Excels")

            If Not Directory.Exists(root) Then
                Directory.CreateDirectory(root)
            End If

            Dim provider = New MultipartFormDataStreamProvider(root)
            Dim result = Await Request.Content.ReadAsMultipartAsync(provider)

            If result.FormData("categoryId") Is Nothing Then
                Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Bạn chưa chọn danh mục sản phẩm.")
            End If

            Dim addedCount As Integer = 0
            Dim categoryId As Integer = 0
            Integer.TryParse(result.FormData("categoryId"), categoryId)

            For Each fileData As MultipartFileData In result.FileData

                If String.IsNullOrEmpty(fileData.Headers.ContentDisposition.FileName) Then
                    Return Request.CreateResponse(HttpStatusCode.NotAcceptable, "Yêu cầu không đúng định dạng")
                End If

                Dim fileName As String = fileData.Headers.ContentDisposition.FileName

                If fileName.StartsWith("""") AndAlso fileName.EndsWith("""") Then
                    fileName = fileName.Trim(""""c)
                End If

                If fileName.Contains("/") OrElse fileName.Contains("\") Then
                    fileName = Path.GetFileName(fileName)
                End If

                Dim fullPath = Path.Combine(root, fileName)
                File.Copy(fileData.LocalFileName, fullPath, True)
                Dim listProduct = Me.ReadProductFromExcel(fullPath, categoryId)

                If listProduct.Count > 0 Then

                    For Each product In listProduct
                        _productService.Add(product)
                        addedCount += 1
                    Next

                    _productService.Save()
                End If
            Next

            Return Request.CreateResponse(HttpStatusCode.OK, "Đã nhập thành công " & addedCount & " sản phẩm thành công.")
        End Function

        <HttpGet>
        <Route("ExportXls")>
        Public Async Function ExportXls(ByVal request As HttpRequestMessage, ByVal Optional filter As String = Nothing) As Task(Of HttpResponseMessage)
            Dim fileName As String = String.Concat("Product_" & DateTime.Now.ToString("yyyyMMddhhmmsss") & ".xlsx")
            Dim folderReport = ConfigHelper.GetByKey("ReportFolder")
            Dim filePath As String = HttpContext.Current.Server.MapPath(folderReport)

            If Not Directory.Exists(filePath) Then
                Directory.CreateDirectory(filePath)
            End If

            Dim fullPath As String = Path.Combine(filePath, fileName)

            Try
                Dim data = _productService.GetListProduct(filter).ToList()
                Await ReportHelper.GenerateXls(data, fullPath)
                Return request.CreateErrorResponse(HttpStatusCode.OK, Path.Combine(folderReport, fileName))
            Catch ex As Exception
                Return request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message)
            End Try
        End Function

        <HttpGet>
        <Route("ExportPdf")>
        Public Async Function ExportPdf(ByVal request As HttpRequestMessage, ByVal id As Integer) As Task(Of HttpResponseMessage)
            Dim fileName As String = String.Concat("Product" & DateTime.Now.ToString("yyyyMMddhhmmssfff") & ".pdf")
            Dim folderReport = ConfigHelper.GetByKey("ReportFolder")
            Dim filePath As String = HttpContext.Current.Server.MapPath(folderReport)

            If Not Directory.Exists(filePath) Then
                Directory.CreateDirectory(filePath)
            End If

            Dim fullPath As String = Path.Combine(filePath, fileName)

            Try
                Dim template = File.ReadAllText(HttpContext.Current.Server.MapPath("/Assets/admin/templates/product-detail.html"))
                Dim replaces = New Dictionary(Of String, String)()
                Dim product = _productService.GetById(id)
                replaces.Add("{{ProductName}}", product.Name)
                replaces.Add("{{Price}}", product.Price.ToString("N0"))
                replaces.Add("{{Description}}", product.Description)
                replaces.Add("{{Warranty}}", product.Warranty & " tháng")
                template = template.Parse(replaces)
                Await ReportHelper.GeneratePdf(template, fullPath)
                Return request.CreateErrorResponse(HttpStatusCode.OK, Path.Combine(folderReport, fileName))
            Catch ex As Exception
                Return request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message)
            End Try
        End Function

        Private Function ReadProductFromExcel(ByVal fullPath As String, ByVal categoryId As Integer) As List(Of Product)
            Using package = New ExcelPackage(New FileInfo(fullPath))
                Dim workSheet As ExcelWorksheet = package.Workbook.Worksheets(1)
                Dim listProduct As List(Of Product) = New List(Of Product)()
                Dim productViewModel As ProductViewModel
                Dim product As Product
                Dim originalPrice As Decimal = 0
                Dim price As Decimal = 0
                Dim promotionPrice As Decimal
                Dim status As Boolean = False
                Dim showHome As Boolean = False
                Dim isHot As Boolean = False
                Dim warranty As Integer

                For i As Integer = workSheet.Dimension.Start.Row + 1 To workSheet.Dimension.[End].Row
                    productViewModel = New ProductViewModel()
                    product = New Product()
                    productViewModel.Name = workSheet.Cells(i, 1).Value.ToString()
                    productViewModel.[Alias] = StringHelper.ToUnsignString(productViewModel.Name)
                    productViewModel.Description = workSheet.Cells(i, 2).Value.ToString()

                    If Integer.TryParse(workSheet.Cells(i, 3).Value.ToString(), warranty) Then
                        productViewModel.Warranty = warranty
                    End If

                    Decimal.TryParse(workSheet.Cells(i, 4).Value.ToString().Replace(",", ""), originalPrice)
                    productViewModel.OriginalPrice = originalPrice
                    Decimal.TryParse(workSheet.Cells(i, 5).Value.ToString().Replace(",", ""), price)
                    productViewModel.Price = price

                    If Decimal.TryParse(workSheet.Cells(i, 6).Value.ToString(), promotionPrice) Then
                        productViewModel.PromotionPrice = promotionPrice
                    End If

                    productViewModel.Content = workSheet.Cells(i, 7).Value.ToString()
                    productViewModel.MetaKeyword = workSheet.Cells(i, 8).Value.ToString()
                    productViewModel.MetaDescription = workSheet.Cells(i, 9).Value.ToString()
                    productViewModel.CategoryID = categoryId
                    Boolean.TryParse(workSheet.Cells(i, 10).Value.ToString(), status)
                    productViewModel.Status = status
                    Boolean.TryParse(workSheet.Cells(i, 11).Value.ToString(), showHome)
                    productViewModel.HomeFlag = showHome
                    Boolean.TryParse(workSheet.Cells(i, 12).Value.ToString(), isHot)
                    productViewModel.HotFlag = isHot
                    product.UpdateProduct(productViewModel)
                    listProduct.Add(product)
                Next

                Return listProduct
            End Using
        End Function
    End Class
End Namespace
