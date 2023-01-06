Imports System.Collections.Generic
Imports System.IO
Imports System.Threading.Tasks
Imports OfficeOpenXml
Imports OfficeOpenXml.Table

Namespace ManShop.Common
    Public Module ReportHelper
        Public Async Function GeneratePdf(ByVal html As String, ByVal filePath As String) As Task
            Await Task.Run(Sub()
                               Using ms As FileStream = New FileStream(filePath, FileMode.Create)
                                   Dim pdf = TheArtOfDev.HtmlRenderer.PdfSharp.PdfGenerator.GeneratePdf(html, PdfSharp.PageSize.A4)
                                   pdf.Save(ms)
                               End Using
                           End Sub)
        End Function
        Public Function GenerateXls(Of T)(ByVal datasource As List(Of T), ByVal filePath As String) As Task
            Return Task.Run(Sub()
                                Using pck As ExcelPackage = New ExcelPackage(New FileInfo(filePath))
                                    'Create the worksheet
                                    Dim ws As ExcelWorksheet = pck.Workbook.Worksheets.Add(NameOf(T))
                                    ws.Cells(CStr("A1")).LoadFromCollection(Of T)(datasource, True, TableStyles.Light1)
                                    ws.Cells.AutoFitColumns()
                                    pck.Save()
                                End Using
                            End Sub)
        End Function
    End Module
End Namespace
