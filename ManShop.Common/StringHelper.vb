Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Text.RegularExpressions
Imports System.Threading.Tasks
Imports System.Runtime.CompilerServices

Namespace ManShop.Common
    Public Module StringHelper
        Function ToUnsignString(ByVal input As String) As String
            input = input.Trim()

            For i As Integer = &H20 To &H30 - 1
                input = input.Replace((ChrW(i)).ToString(), " ")
            Next

            input = input.Replace(".", "-")
            input = input.Replace(" ", "-")
            input = input.Replace(",", "-")
            input = input.Replace(";", "-")
            input = input.Replace(":", "-")
            input = input.Replace("  ", "-")
            Dim regex As Regex = New Regex("\p{IsCombiningDiacriticalMarks}+")
            Dim str As String = input.Normalize(NormalizationForm.FormD)
            Dim str2 As String = regex.Replace(str, String.Empty).Replace("đ"c, "d"c).Replace("Đ"c, "D"c)

            While str2.IndexOf("?") >= 0
                str2 = str2.Remove(str2.IndexOf("?"), 1)
            End While

            While str2.Contains("--")
                str2 = str2.Replace("--", "-").ToLower()
            End While

            Return str2
        End Function

        <Extension()>
        Function Parse(ByVal template As String, ByVal replacements As Dictionary(Of String, String)) As String
            If replacements.Count > 0 Then
                template = replacements.Keys.Aggregate(template, Function(current, key) current.Replace(key, replacements(key)))
            End If

            Return template
        End Function
    End Module
End Namespace
