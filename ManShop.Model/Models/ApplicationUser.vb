Imports Microsoft.AspNet.Identity
Imports Microsoft.AspNet.Identity.EntityFramework
Imports System.Collections.Generic
Imports System.ComponentModel.DataAnnotations
Imports System.Security.Claims
Imports System.Threading.Tasks

Namespace ManShop.Model.Models
    Public Class ApplicationUser
        Inherits IdentityUser
        <MaxLength(256)>
        Public Property FullName As String

        <MaxLength(256)>
        Public Property Address As String

        Public Property BirthDay As Date?

        Public Async Function GenerateUserIdentityAsync(ByVal manager As UserManager(Of ApplicationUser), ByVal authenticationType As String) As Task(Of ClaimsIdentity)
            ' Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            Dim userIdentity = Await manager.CreateIdentityAsync(Me, authenticationType)
            ' Add custom user claims here
            Return userIdentity
        End Function

        Public Overridable Property Orders As IEnumerable(Of Order)
    End Class
End Namespace
