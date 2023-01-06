Imports AutoMapper
Imports ManShop.Common.ManShop.Common
Imports ManShop.Model.ManShop.Model.Models
Imports ManShop.Service.ManShop.Service
Imports ManShop.Web.ManShop.Web.App_Start
Imports ManShop.Web.ManShop.Web.Infrastructure.Extensions
Imports ManShop.Web.ManShop.Web.Infrastructure.NganLuongAPI
Imports ManShop.Web.ManShop.Web.Models
Imports Microsoft.AspNet.Identity
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Web.Mvc
Imports System.Web.Script.Serialization

Namespace ManShop.Web.Controllers
    Public Class ShoppingCartController
        Inherits Controller
        Private _productService As IProductService
        Private _orderService As IOrderService
        Private _userManager As ApplicationUserManager

        Private merchantId As String = ConfigHelper.GetByKey("MerchantId")
        Private merchantPassword As String = ConfigHelper.GetByKey("MerchantPassword")
        Private merchantEmail As String = ConfigHelper.GetByKey("MerchantEmail")

        Public Sub New(ByVal orderService As IOrderService, ByVal productService As IProductService, ByVal userManager As ApplicationUserManager)
            _productService = productService
            _userManager = userManager
            _orderService = orderService
        End Sub
        ' GET: ShoppingCart
        Public Function Index() As ActionResult
            If Session(CommonConstants.SessionCart) Is Nothing Then Session(CommonConstants.SessionCart) = New List(Of ShoppingCartViewModel)()
            Return View()
        End Function

        Public Function CheckOut() As ActionResult
            If Session(CommonConstants.SessionCart) Is Nothing Then
                Return Redirect("/gio-hang.html")
            End If
            Return View()
        End Function
        Public Function GetUser() As JsonResult
            If Request.IsAuthenticated Then
                Dim userId = User.Identity.GetUserId()
                Dim userr = _userManager.FindById(userId)
                Return Json(New With {
                .data = user,
                .status = True
            })
            End If
            Return Json(New With {
                .status = False
            })
        End Function
        Public Function CreateOrder(ByVal orderViewModel As String) As ActionResult
            Dim order = New JavaScriptSerializer().Deserialize(Of OrderViewModel)(orderViewModel)

            Dim orderNew = New Order()

            orderNew.UpdateOrder(order)

            If Request.IsAuthenticated Then
                orderNew.CustomerId = User.Identity.GetUserId()
                orderNew.CreatedBy = User.Identity.GetUserName()
            End If

            Dim cart = CType(Session(CommonConstants.SessionCart), List(Of ShoppingCartViewModel))
            Dim orderDetails As List(Of OrderDetail) = New List(Of OrderDetail)()
            Dim isEnough = True
            For Each item In cart
                Dim detail = New OrderDetail()
                detail.ProductID = item.ProductId
                detail.Quantity = item.Quantity
                detail.Price = item.Product.Price
                orderDetails.Add(detail)

                isEnough = _productService.SellProduct(item.ProductId, item.Quantity)
                Exit For
            Next
            If isEnough Then
                Dim orderReturn = _orderService.Create(orderNew, orderDetails)
                _productService.Save()

                If order.PaymentMethod Is "CASH" Then
                    Return Json(New With {
                        .status = True
                    })
                Else

                    Dim currentLink = ConfigHelper.GetByKey("CurrentLink")
                    Dim info As RequestInfo = New RequestInfo()
                    info.Merchant_id = merchantId
                    info.Merchant_password = merchantPassword
                    info.Receiver_email = merchantEmail



                    info.cur_code = "vnd"
                    info.bank_code = order.BankCode

                    info.Order_code = orderReturn.ID.ToString()
                    info.Total_amount = Enumerable.Sum(Of OrderDetail)(orderDetails, CType(Function(x) x.Quantity * x.Price, Func(Of OrderDetail, Integer))).ToString()
                    info.fee_shipping = "0"
                    info.Discount_amount = "0"
                    info.order_description = "Thanh toán đơn hàng tại TeduShop"
                    info.return_url = currentLink & "xac-nhan-don-hang.html"
                    info.cancel_url = currentLink & "huy-don-hang.html"

                    info.Buyer_fullname = order.CustomerName
                    info.Buyer_email = order.CustomerEmail
                    info.Buyer_mobile = order.CustomerMobile

                    Dim objNLChecout As APICheckoutV3 = New APICheckoutV3()
                    Dim result As ResponseInfo = objNLChecout.GetUrlCheckout(info, order.PaymentMethod)
                    If result.Error_code Is "00" Then
                        Return Json(New With {
                            .status = True,
                            .urlCheckout = result.Checkout_url,
                            .message = result.Description
                        })
                    Else
                        Return Json(New With {
                            .status = False,
                            .message = result.Description
                        })
                    End If
                End If
            Else
                Return Json(New With {
                    .status = False,
                    .message = "Không đủ hàng."
                })
            End If

        End Function
        Public Function GetAll() As JsonResult
            If Session(CommonConstants.SessionCart) Is Nothing Then Session(CommonConstants.SessionCart) = New List(Of ShoppingCartViewModel)()
            Dim cart = CType(Session(CommonConstants.SessionCart), List(Of ShoppingCartViewModel))
            Return Json(New With {
                .data = cart,
                .status = True
            }, JsonRequestBehavior.AllowGet)
        End Function
        <HttpPost>
        Public Function Add(ByVal productId As Integer) As JsonResult
            Dim cart = CType(Session(CommonConstants.SessionCart), List(Of ShoppingCartViewModel))
            Dim product = _productService.GetById(productId)
            If cart Is Nothing Then
                cart = New List(Of ShoppingCartViewModel)()
            End If
            If product.Quantity = 0 Then
                Return Json(New With {
                    .status = False,
                    .message = "Sản phẩm này hiện đang hết hàng"
                })
            End If
            If cart.Any(Function(x) x.ProductId = productId) Then
                For Each item In cart
                    If item.ProductId = productId Then
                        item.Quantity += 1
                    End If
                Next
            Else
                Dim newItem As ShoppingCartViewModel = New ShoppingCartViewModel()
                newItem.ProductId = productId
                newItem.Product = Mapper.Map(Of Product, ProductViewModel)(product)
                newItem.Quantity = 1
                cart.Add(newItem)
            End If

            Session(CommonConstants.SessionCart) = cart
            Return Json(New With {
                .status = True
            })
        End Function

        <HttpPost>
        Public Function DeleteItem(ByVal productId As Integer) As JsonResult
            Dim cartSession = CType(Session(CommonConstants.SessionCart), List(Of ShoppingCartViewModel))
            If cartSession IsNot Nothing Then
                cartSession.RemoveAll(Function(x) x.ProductId = productId)
                Session(CommonConstants.SessionCart) = cartSession
                Return Json(New With {
                    .status = True
                })
            End If
            Return Json(New With {
                .status = False
            })
        End Function

        <HttpPost>
        Public Function Update(ByVal cartData As String) As JsonResult
            Dim cartViewModel = New JavaScriptSerializer().Deserialize(Of List(Of ShoppingCartViewModel))(cartData)

            Dim cartSession = CType(Session(CommonConstants.SessionCart), List(Of ShoppingCartViewModel))
            For Each item In cartSession
                For Each jitem In cartViewModel
                    If item.ProductId = jitem.ProductId Then
                        item.Quantity = jitem.Quantity
                    End If
                Next
            Next

            Session(CommonConstants.SessionCart) = cartSession
            Return Json(New With {
                .status = True
            })
        End Function

        <HttpPost>
        Public Function DeleteAll() As JsonResult
            Session(CommonConstants.SessionCart) = New List(Of ShoppingCartViewModel)()
            Return Json(New With {
                .status = True
            })
        End Function

        Public Function ConfirmOrder() As ActionResult
            Dim token As String = Request("token")
            Dim info As RequestCheckOrder = New RequestCheckOrder()
            info.Merchant_id = merchantId
            info.Merchant_password = merchantPassword
            info.Token = token
            Dim objNLChecout As APICheckoutV3 = New APICheckoutV3()
            Dim result As ResponseCheckOrder = objNLChecout.GetTransactionDetail(info)
            If result.errorCode Is "00" Then
                'update status order
                _orderService.UpdateStatus(Integer.Parse(result.order_code))
                _orderService.Save()
                ViewBag.IsSuccess = True
                ViewBag.Result = "Thanh toán thành công. Chúng tôi sẽ liên hệ lại sớm nhất."
            Else
                ViewBag.IsSuccess = True
                ViewBag.Result = "Có lỗi xảy ra. Vui lòng liên hệ admin."
            End If
            Return View()
        End Function
        Public Function CancelOrder() As ActionResult
            Return View()
        End Function
    End Class
End Namespace
