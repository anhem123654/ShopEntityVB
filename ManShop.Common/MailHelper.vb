Imports System
Imports System.Net.Mail
Imports System.Text

Namespace ManShop.Common
    Public Class MailHelper
        Public Shared Function SendMail(ByVal toEmail As String, ByVal subject As String, ByVal content As String) As Boolean
            Try
                Dim host = ConfigHelper.GetByKey("SMTPHost")
                Dim port = Integer.Parse(ConfigHelper.GetByKey("SMTPPort"))
                Dim fromEmail = ConfigHelper.GetByKey("FromEmailAddress")
                Dim password = ConfigHelper.GetByKey("FromEmailPassword")
                Dim fromName = ConfigHelper.GetByKey("FromName")

                Dim smtpClient = New SmtpClient(host, port) With {
    .UseDefaultCredentials = False,
    .Credentials = New Net.NetworkCredential(fromEmail, password),
    .DeliveryMethod = SmtpDeliveryMethod.Network,
    .EnableSsl = True,
    .Timeout = 100000
}

                Dim mail = New MailMessage With {
    .Body = content,
    .Subject = subject,
    .From = New MailAddress(fromEmail, fromName)
}

                mail.[To].Add(New MailAddress(toEmail))
                mail.BodyEncoding = Encoding.UTF8
                mail.IsBodyHtml = True
                mail.Priority = MailPriority.High

                smtpClient.Send(mail)

                Return True
            Catch smex As SmtpException

                Return False
            End Try
        End Function
    End Class
End Namespace
