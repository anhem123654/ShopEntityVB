Imports AutoMapper
Imports ManShop.Common.ManShop.Common
Imports ManShop.Model.ManShop.Model.Models
Imports ManShop.Service.ManShop.Service
Imports ManShop.Service.TeduShop.Service
Imports ManShop.Web.ManShop.Web.Models
Imports System.Web.Mvc

Namespace ManShop.Web.Controllers
    Public Class HomeController
        Inherits Controller
        Private _productCategoryService As IProductCategoryService
        Private _productService As IProductService
        Private _commonService As ICommonService

        Public Sub New(ByVal productCategoryService As IProductCategoryService, ByVal productService As IProductService, ByVal commonService As ICommonService)
            _productCategoryService = productCategoryService
            _commonService = commonService
            _productService = productService
        End Sub

        <OutputCache(Duration:=60, Location:=System.Web.UI.OutputCacheLocation.Client)>
        Public Function Index() As ActionResult
            Dim slideModel = _commonService.GetSlides()
            Dim slideView = Mapper.Map(Of IEnumerable(Of Slide), IEnumerable(Of SlideViewModel))(slideModel)
            Dim homeViewModel = New HomeViewModel()
            homeViewModel.Slides = slideView

            Dim lastestProductModel = _productService.GetLastest(3)
            Dim topSaleProductModel = _productService.GetHotProduct(3)
            Dim lastestProductViewModel = Mapper.Map(Of IEnumerable(Of Product), IEnumerable(Of ProductViewModel))(lastestProductModel)
            Dim topSaleProductViewModel = Mapper.Map(Of IEnumerable(Of Product), IEnumerable(Of ProductViewModel))(topSaleProductModel)
            homeViewModel.LastestProducts = lastestProductViewModel
            homeViewModel.TopSaleProducts = topSaleProductViewModel

            Try
                homeViewModel.Title = _commonService.GetSystemConfig(CommonConstants.HomeTitle).ValueString
                homeViewModel.MetaKeyword = _commonService.GetSystemConfig(CommonConstants.HomeMetaKeyword).ValueString
                homeViewModel.MetaDescription = _commonService.GetSystemConfig(CommonConstants.HomeMetaDescription).ValueString
            Catch

            End Try

            Return View(homeViewModel)
        End Function


        <ChildActionOnly>
        <OutputCache(Duration:=3600)>
        Public Function Footer() As ActionResult
            Dim footerModel = _commonService.GetFooter()
            Dim footerViewModel = Mapper.Map(Of Footer, FooterViewModel)(footerModel)
            Return PartialView(footerViewModel)
        End Function

        <ChildActionOnly>
        Public Function Header() As ActionResult
            Return PartialView()
        End Function

        <ChildActionOnly>
        Public Function Category() As ActionResult
            Dim model = _productCategoryService.GetAll()
            Dim listProductCategoryViewModel = Mapper.Map(Of IEnumerable(Of ProductCategory), IEnumerable(Of ProductCategoryViewModel))(model)
            Return PartialView(listProductCategoryViewModel)
        End Function
    End Class
End Namespace
