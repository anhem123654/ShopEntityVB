Imports System.Security.Cryptography
Imports System.IO
Imports System.Xml
Imports System.Net

Namespace ManShop.Web.Infrastructure.NganLuongAPI
    Public Class APICheckoutV3

        Public Function GetUrlCheckout(ByVal requestContent As RequestInfo, ByVal Optional payment_method As String = "NL") As ResponseInfo

            requestContent.Payment_method = payment_method
            Dim requestinfo = GetParamPost(requestContent)
            Dim result = HttpPost(requestinfo)
            result = result.Replace("&", "&amp;")
            Dim dom As XmlDocument = New XmlDocument()
            dom.LoadXml(result)
            Dim root = dom.DocumentElement.ChildNodes

            Dim resResult As ResponseInfo = New ResponseInfo()
            resResult.Checkout_url = root.Item(4).InnerText
            resResult.Description = root.Item(2).InnerText
            resResult.Error_code = root.Item(0).InnerText
            resResult.Token = root.Item(1).InnerText

            Return resResult
        End Function

        Public Function GetTransactionDetail(ByVal info As RequestCheckOrder) As ResponseCheckOrder


            Dim request = ""
            request += "function=" & info.Funtion
            request += "&version=" & info.Version
            request += "&merchant_id=" & info.Merchant_id
            request += "&merchant_password=" & CreateMD5Hash(info.Merchant_password)
            request += "&token=" & info.Token
            Dim result = HttpPost(request)
            result = result.Replace("&", "&amp;")
            Dim dom As XmlDocument = New XmlDocument()
            dom.LoadXml(result)
            Dim root = dom.DocumentElement.ChildNodes

            Dim objResult As ResponseCheckOrder = New ResponseCheckOrder()


            objResult.errorCode = root.Item(0).InnerText
            objResult.token = root.Item(1).InnerText
            objResult.description = root.Item(2).InnerText
            objResult.transactionStatus = root.Item(3).InnerText
            'objResult.receiver_email = root.Item(4).InnerText; //receiver_email
            objResult.order_code = root.Item(5).InnerText
            objResult.paymentAmount = root.Item(6).InnerText 'total_amount
            'objResult.payment_method = root.Item(7).InnerText; //payment_method
            'objResult.bank_code = root.Item(8).InnerText; //bank_code
            'objResult.payment_type = root.Item(9).InnerText; //payment_type
            'objResult.description = root.Item(10).InnerText; //order_description
            'objResult.tax_amount = root.Item(11).InnerText; //tax_amount
            'objResult.discount_amount = root.Item(12).InnerText; //discount_amount
            'objResult.fee_shipping = root.Item(13).InnerText; //fee_shipping
            'objResult.return_url = root.Item(14).InnerText; //return_url
            ' objResult.cancel_url = root.Item(15).InnerText; //cancel_url
            objResult.payerName = root.Item(16).InnerText 'buyer_fullname
            objResult.payerEmail = root.Item(17).InnerText 'buyer_email
            objResult.payerMobile = root.Item(18).InnerText 'buyer_mobile
            'objResult.buyer_address = root.Item(19).InnerText; //buyer_address
            'objResult.affiliate_code = root.Item(20).InnerText; //affiliate_code
            objResult.transactionId = root.Item(21).InnerText


            objResult.errorCode = root.Item(0).InnerText
            objResult.token = root.Item(1).InnerText
            objResult.description = root.Item(2).InnerText
            objResult.transactionStatus = root.Item(3).InnerText
            objResult.order_code = root.Item(5).InnerText
            objResult.paymentAmount = root.Item(6).InnerText
            objResult.transactionId = root.Item(21).InnerText

            ' 
            ' String error_code =root.Item(0).InnerText;
            ' String token =root.Item(1).InnerText;
            ' String description =root.Item(2).InnerText;
            ' String transaction_status =root.Item(3).InnerText;
            ' String receiver_email =root.Item(4).InnerText;
            ' String order_code =root.Item(5).InnerText;
            ' String total_amount =root.Item(6).InnerText;
            ' String payment_method =root.Item(7).InnerText;
            ' String bank_code =root.Item(8).InnerText;
            ' String payment_type =root.Item(9).InnerText;
            ' String order_description =root.Item(10).InnerText;
            ' String tax_amount =root.Item(11).InnerText;
            ' String discount_amount =root.Item(12).InnerText;
            ' String fee_shipping =root.Item(13).InnerText;
            ' String return_url =root.Item(14).InnerText;
            ' String cancel_url =root.Item(15).InnerText;
            ' String buyer_fullname =root.Item(16).InnerText;            
            ' String buyer_email =root.Item(17).InnerText;
            ' String buyer_mobile =root.Item(18).InnerText;
            ' String buyer_address =root.Item(19).InnerText;
            ' String affiliate_code =root.Item(20).InnerText;
            ' String transaction_id =root.Item(21).InnerText;
            ' 
            Return objResult
        End Function

        Private Shared Function GetParamPost(ByVal info As RequestInfo) As String

            Dim request = ""

            request += "function=" & info.Funtion
            request += "&cur_code=" & info.cur_code
            request += "&version=" & info.Version
            request += "&merchant_id=" & info.Merchant_id
            request += "&receiver_email=" & info.Receiver_email
            request += "&merchant_password=" & CreateMD5Hash(info.Merchant_password)
            request += "&order_code=" & info.Order_code
            request += "&total_amount=" & info.Total_amount
            request += "&payment_method=" & info.Payment_method
            request += "&bank_code=" & info.bank_code
            request += "&payment_type=" & info.Payment_type
            request += "&order_description=" & info.order_description
            request += "&tax_amount=" & info.tax_amount
            request += "&fee_shipping=" & info.fee_shipping
            request += "&discount_amount=" & info.Discount_amount
            request += "&return_url=" & info.return_url
            request += "&cancel_url=" & info.cancel_url
            request += "&buyer_fullname=" & info.Buyer_fullname
            request += "&buyer_email=" & info.Buyer_email
            request += "&buyer_mobile=" & info.Buyer_mobile

            Return request
        End Function


        Private Shared Function HttpPost(ByVal postData As String) As String

            Dim encoding As ASCIIEncoding = New ASCIIEncoding()
            Dim data = encoding.GetBytes(postData)

            ' Prepare web request...
            Dim myRequest As HttpWebRequest = CType(WebRequest.Create("https://www.nganluong.vn/checkout.api.nganluong.post.php"), HttpWebRequest)
            myRequest.Method = "POST"
            myRequest.ContentType = "application/x-www-form-urlencoded"
            myRequest.ContentLength = data.Length
            Dim newStream As Stream = myRequest.GetRequestStream()
            ' Send the data.

            newStream.Write(data, 0, data.Length)
            newStream.Close()

            Dim response As HttpWebResponse = CType(myRequest.GetResponse(), HttpWebResponse)
            Dim reader As StreamReader = New StreamReader(response.GetResponseStream())
            Dim output As String = reader.ReadToEnd()
            response.Close()
            Return output
        End Function

        Private Shared Function CreateMD5Hash(ByVal input As String) As String
            ' Use input string to calculate MD5 hash
            Dim md5 As MD5 = MD5.Create()
            Dim inputBytes = Encoding.ASCII.GetBytes(input)
            Dim hashBytes = md5.ComputeHash(inputBytes)

            ' Convert the byte array to hexadecimal string
            Dim sb As StringBuilder = New StringBuilder()
            For i = 0 To hashBytes.Length - 1
                sb.Append(hashBytes(i).ToString("x2"))
            Next
            Return sb.ToString()
        End Function

        Private Shared Function GetErrorMessage(ByVal _ErrorCode As String) As String
            Dim _Message = ""
            Select Case _ErrorCode
                Case "00"
                    _Message = "Giao dịch thành công"
                Case "01"
                    _Message = "Lỗi, địa chỉ IP truy cập API của NgânLượng.vn bị từ chối"
                Case "02"
                    _Message = "Lỗi, tham số gửi từ merchant tới NgânLượng.vn chưa chính xác."
                Case "03"
                    _Message = "Lỗi, mã merchant không tồn tại hoặc merchant đang bị khóa kết nối tới NgânLượng.vn"
                Case "04"
                    _Message = "Lỗi, mã checksum không chính xác"
                Case "05"
                    _Message = "Tài khoản nhận tiền nạp của merchant không tồn tại"
                Case "06"
                    _Message = "Tài khoản nhận tiền nạp của  merchant đang bị khóa hoặc bị phong tỏa, không thể thực hiện được giao dịch nạp tiền"
                Case "07"
                    _Message = "Thẻ đã được sử dụng"
                Case "08"
                    _Message = "Thẻ bị khóa"
                Case "09"
                    _Message = "Thẻ hết hạn sử dụng"
                Case "10"
                    _Message = "Thẻ chưa được kích hoạt hoặc không tồn tại"
                Case "11"
                    _Message = "Mã thẻ sai định dạng"
                Case "12"
                    _Message = "Sai số serial của thẻ"
                Case "13"
                    _Message = "Mã thẻ và số serial không khớp"
                Case "14"
                    _Message = "Thẻ không tồn tại"
                Case "15"
                    _Message = "Thẻ không sử dụng được"
                Case "16"
                    _Message = "Số lần tưử của thẻ vượt quá giới hạn cho phép"
                Case "17"
                    _Message = "Hệ thống Telco bị lỗi hoặc quá tải, thẻ chưa bị trừ"
                Case "18"
                    _Message = "Hệ thống Telco  bị lỗi hoặc quá tải, thẻ có thể bị trừ, cần phối hợp với nhà mạng để đối soát"
                Case "19"
                    _Message = "Kết nối NgânLượng với Telco bị lỗi, thẻ chưa bị trừ."
                Case "20"
                    _Message = "Kết nối tới Telco thành công, thẻ bị trừ nhưng chưa cộng tiền trên NgânLượng.vn"
                Case "99"
                    _Message = "Lỗi tuy nhiên lỗi chưa được định nghĩa hoặc chưa xác định được nguyên nhân"
            End Select
            Return _Message
        End Function
    End Class

#Region "entity RequestCheckOrder"
    Public Class RequestCheckOrder
        Private _Funtion As String = "GetTransactionDetail"

        Public ReadOnly Property Funtion As String
            Get
                Return _Funtion
            End Get
        End Property

        Private _Version As String = "3.1"

        Public ReadOnly Property Version As String
            Get
                Return _Version
            End Get
        End Property
        Private _Merchant_id As String = String.Empty
        Public Property Merchant_id As String
            Get
                Return _Merchant_id
            End Get
            Set(ByVal value As String)
                _Merchant_id = value
            End Set
        End Property
        Private _Merchant_password As String = String.Empty

        Public Property Merchant_password As String
            Get
                Return _Merchant_password
            End Get
            Set(ByVal value As String)
                _Merchant_password = value
            End Set
        End Property
        Private _token As String = String.Empty

        Public Property Token As String
            Get
                Return _token
            End Get
            Set(ByVal value As String)
                _token = value
            End Set
        End Property
    End Class


    Public Class ResponseCheckOrder
        Private error_code As String = String.Empty

        Public Property errorCode As String
            Get
                Return error_code
            End Get
            Set(ByVal value As String)
                error_code = value
            End Set
        End Property
        Private error_description As String = String.Empty

        Public Property description As String
            Get
                Return error_description
            End Get
            Set(ByVal value As String)
                error_description = value
            End Set
        End Property
        Private time_limit As String = String.Empty

        Public Property timeLimit As String
            Get
                Return time_limit
            End Get
            Set(ByVal value As String)
                time_limit = value
            End Set
        End Property
        Private _token As String = String.Empty

        Public Property token As String
            Get
                Return _token
            End Get
            Set(ByVal value As String)
                _token = value
            End Set
        End Property
        Private transaction_id As String = String.Empty

        Public Property transactionId As String
            Get
                Return transaction_id
            End Get
            Set(ByVal value As String)
                transaction_id = value
            End Set
        End Property
        Private amount As String = String.Empty

        Public Property paymentAmount As String
            Get
                Return amount
            End Get
            Set(ByVal value As String)
                amount = value
            End Set
        End Property
        Private _order_code As String = String.Empty

        Public Property order_code As String
            Get
                Return _order_code
            End Get
            Set(ByVal value As String)
                _order_code = value
            End Set
        End Property
        Private transaction_type As String = String.Empty

        Public Property transactionType As String
            Get
                Return transaction_type
            End Get
            Set(ByVal value As String)
                transaction_type = value
            End Set
        End Property
        Private transaction_status As String = String.Empty

        Public Property transactionStatus As String
            Get
                Return transaction_status
            End Get
            Set(ByVal value As String)
                transaction_status = value
            End Set
        End Property
        Private payer_name As String = String.Empty

        Public Property payerName As String
            Get
                Return payer_name
            End Get
            Set(ByVal value As String)
                payer_name = value
            End Set
        End Property
        Private payer_email As String = String.Empty

        Public Property payerEmail As String
            Get
                Return payer_email
            End Get
            Set(ByVal value As String)
                payer_email = value
            End Set
        End Property
        Private payer_mobile As String = String.Empty

        Public Property payerMobile As String
            Get
                Return payer_mobile
            End Get
            Set(ByVal value As String)
                payer_mobile = value
            End Set
        End Property
        Private receiver_name As String = String.Empty

        Public Property merchantName As String
            Get
                Return receiver_name
            End Get
            Set(ByVal value As String)
                receiver_name = value
            End Set
        End Property
        Private receiver_address As String = String.Empty

        Public Property merchantAddress As String
            Get
                Return receiver_address
            End Get
            Set(ByVal value As String)
                receiver_address = value
            End Set
        End Property
        Private receiver_mobile As String = String.Empty

        Public Property merchantMobile As String
            Get
                Return receiver_mobile
            End Get
            Set(ByVal value As String)
                receiver_mobile = value
            End Set
        End Property
        Private payment_method As String = String.Empty

        Public Property paymentMethod As String
            Get
                Return payment_method
            End Get
            Set(ByVal value As String)
                payment_method = value
            End Set
        End Property
    End Class

#End Region

#Region "Entity RequestInfo"
    Public Class RequestInfo
        Private _Funtion As String = "SetExpressCheckout"

        Public ReadOnly Property Funtion As String
            Get
                Return _Funtion
            End Get
        End Property

        Private _Version As String = "3.1"

        Public ReadOnly Property Version As String
            Get
                Return _Version
            End Get
        End Property

        Private _cur_code As String = String.Empty
        Public Property cur_code As String
            Get
                Return _cur_code
            End Get
            Set(ByVal value As String)
                _cur_code = value
            End Set
        End Property

        Private _discount_amount As String = String.Empty
        Public Property Discount_amount As String
            Get
                Return _discount_amount
            End Get
            Set(ByVal value As String)
                _discount_amount = value
            End Set
        End Property

        Private _Merchant_id As String = String.Empty
        Public Property Merchant_id As String
            Get
                Return _Merchant_id
            End Get
            Set(ByVal value As String)
                _Merchant_id = value
            End Set
        End Property
        Private _Receiver_email As String = String.Empty

        Public Property Receiver_email As String
            Get
                Return _Receiver_email
            End Get
            Set(ByVal value As String)
                _Receiver_email = value
            End Set
        End Property
        Private _Merchant_password As String = String.Empty

        Public Property Merchant_password As String
            Get
                Return _Merchant_password
            End Get
            Set(ByVal value As String)
                _Merchant_password = value
            End Set
        End Property
        Private _Order_code As String = String.Empty

        Public Property Order_code As String
            Get
                Return _Order_code
            End Get
            Set(ByVal value As String)
                _Order_code = value
            End Set
        End Property
        Private _Total_amount As String = String.Empty

        Public Property Total_amount As String
            Get
                Return _Total_amount
            End Get
            Set(ByVal value As String)
                _Total_amount = value
            End Set
        End Property
        Private _Payment_method As String = String.Empty

        Public Property Payment_method As String
            Get
                Return _Payment_method
            End Get
            Set(ByVal value As String)
                _Payment_method = value
            End Set
        End Property
        Private _Payment_type As String = String.Empty

        Public Property Payment_type As String
            Get
                Return _Payment_type
            End Get
            Set(ByVal value As String)
                _Payment_type = value
            End Set
        End Property
        Private _bank_code As String = String.Empty

        Public Property bank_code As String
            Get
                Return _bank_code
            End Get
            Set(ByVal value As String)
                _bank_code = value
            End Set
        End Property
        Private _order_description As String = String.Empty

        Public Property order_description As String
            Get
                Return _order_description
            End Get
            Set(ByVal value As String)
                _order_description = value
            End Set
        End Property
        Private _fee_shipping As String = String.Empty

        Public Property fee_shipping As String
            Get
                Return _fee_shipping
            End Get
            Set(ByVal value As String)
                _fee_shipping = value
            End Set
        End Property
        Private _tax_amount As String = String.Empty

        Public Property tax_amount As String
            Get
                Return _tax_amount
            End Get
            Set(ByVal value As String)
                _tax_amount = value
            End Set
        End Property

        Private _return_url As String = String.Empty

        Public Property return_url As String
            Get
                Return _return_url
            End Get
            Set(ByVal value As String)
                _return_url = value
            End Set
        End Property
        Private _cancel_url As String = String.Empty

        Public Property cancel_url As String
            Get
                Return _cancel_url
            End Get
            Set(ByVal value As String)
                _cancel_url = value
            End Set
        End Property
        Private _time_limit As String = String.Empty

        Public Property time_limit As String
            Get
                Return _time_limit
            End Get
            Set(ByVal value As String)
                _time_limit = value
            End Set
        End Property
        Private _Buyer_fullname As String = String.Empty

        Public Property Buyer_fullname As String
            Get
                Return _Buyer_fullname
            End Get
            Set(ByVal value As String)
                _Buyer_fullname = value
            End Set
        End Property
        Private _Buyer_email As String = String.Empty

        Public Property Buyer_email As String
            Get
                Return _Buyer_email
            End Get
            Set(ByVal value As String)
                _Buyer_email = value
            End Set
        End Property
        Private _Buyer_mobile As String = String.Empty

        Public Property Buyer_mobile As String
            Get
                Return _Buyer_mobile
            End Get
            Set(ByVal value As String)
                _Buyer_mobile = value
            End Set
        End Property
    End Class
    Public Class ResponseInfo
        Private _error_code As String = String.Empty

        Public Property Error_code As String
            Get
                Return _error_code
            End Get
            Set(ByVal value As String)
                _error_code = value
            End Set
        End Property
        Private _checkout_url As String = String.Empty

        Public Property Checkout_url As String
            Get
                Return _checkout_url
            End Get
            Set(ByVal value As String)
                _checkout_url = value
            End Set
        End Property
        Private _Token As String = String.Empty

        Public Property Token As String
            Get
                Return _Token
            End Get
            Set(ByVal value As String)
                _Token = value
            End Set
        End Property

        Private _description As String = String.Empty

        Public Property Description As String
            Get
                Return _description
            End Get
            Set(ByVal value As String)
                _description = value
            End Set
        End Property
    End Class
#End Region
End Namespace
