Imports System.Threading.Tasks
Imports System.Web.UI.HtmlControls.HtmlGenericControl
Public Class MRA_FE_0041
    Inherits System.Web.UI.Page
    Public BL As New Order_BL
    Private Sub MRA_FE_0041_Unload(sender As Object, e As EventArgs) Handles Me.Unload
        BL.Dispose()
    End Sub
    '''' <summary>
    '''' initload
    '''' </summary>
    '''' <param name="sender"></param>
    '''' <param name="e"></param>
    Private Sub MRA_FE_0041_InitLoad(sender As Object, e As EventArgs) Handles Me.Load
        Try
            If BL.Load().Rows.Count > 0 Then
                GRD_DATA.DataSource = BL.Load()
                GRD_DATA.DataBind()
                SumTotalGRD_DATA(BL.Load())
            Else
                MsgBox("No data")
                Exit Sub
            End If
        Catch ex As Exception
            MsgBox("system err")
        End Try
    End Sub
    ''' <summary>
    ''' Search
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub BTN_SEARCH_ServerClick(sender As Object, e As EventArgs) Handles BTN_SEARCH.ServerClick
        Dim strSearch As String
        'Dim strDateFrom As String
        'Dim strDateTo As String
        'Dim strTimeFrom As String
        'Dim strTimeTo As String

        strSearch = Trim(TXT_SEARCH.Value)
        'strDateFrom = Convert.ToDateTime("DATE_FROM").ToString("yyyy-MM-dd")
        'strDateTo = Convert.ToDateTime("DATE_TO").ToString("yyyy-MM-dd")
        'strTimeFrom = Convert.ToDateTime("TIME_FROM").ToString()
        'strTimeTo = Convert.ToDateTime("TIME_TO").ToString()

        Try
            If strSearch <> "" Then
                'GRD_DATA.DataSource = BL.Search(strSearch, strDateFrom, strDateTo, strTimeFrom, strTimeTo)
                If BL.Search(strSearch).Rows.Count > 0 Then
                    GRD_DATA.DataSource = BL.Search(strSearch)
                    GRD_DATA.DataBind()
                    SumTotalGRD_DATA(BL.Search(strSearch))
                Else
                    MsgBox("No data")
                    Exit Sub
                End If
            End If
        Catch ex As Exception
            MsgBox("system err")
        End Try
    End Sub
    'Change page in datagrid
    Private Sub GRD_DATA_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles GRD_DATA.PageIndexChanging
        GRD_DATA.PageIndex = e.NewPageIndex
        showData() 'bindgridview will Get the data source And bind it again
    End Sub
    Private Sub SumTotalGRD_DATA(dt As DataTable)
        Dim total As Decimal = dt.AsEnumerable().Sum(Function(row) row.Field(Of Decimal)("Total"))
        'Set sum label in footer row
        GRD_DATA.FooterRow.Cells(5).Text = "Sum"
        GRD_DATA.FooterRow.Cells(5).Font.Bold = True
        GRD_DATA.FooterRow.Cells(5).HorizontalAlign = HorizontalAlign.Right
        GRD_DATA.FooterRow.Cells(6).Text = total.ToString("N2")
    End Sub
    ''' <summary>
    ''' Search
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub BTN_CSV_ServerClick(sender As Object, e As EventArgs) Handles BTN_CSV.ServerClick
        Dim strSearch As String
        strSearch = Trim(TXT_SEARCH.Value)
        Try
            Response.Clear()
            Response.Buffer = True
            Response.AddHeader("content-disposition", "attachment;filename=Export.csv")
            Response.Charset = ""
            Response.ContentType = "application/text"
            GRD_DATA.DataSource = BL.Search(strSearch)
            GRD_DATA.DataBind()
            Dim sBuilder As StringBuilder = New System.Text.StringBuilder()
            For a As Integer = 0 To GRD_DATA.Columns.Count - 1
                sBuilder.Append(GRD_DATA.Columns(a).HeaderText + ","c)
            Next
            sBuilder.Append(vbCr & vbLf)

            For i As Integer = 0 To GRD_DATA.Rows.Count - 1
                For k As Integer = 0 To GRD_DATA.HeaderRow.Cells.Count - 1
                    sBuilder.Append(GRD_DATA.Rows(i).Cells(k).Text.Replace(",", "") + ",")
                Next
                sBuilder.Append(vbCr & vbLf)
            Next
            Response.Output.Write(sBuilder.Replace("&nbsp;", ""))
            Response.Flush()
            Response.[End]()
            MsgBox("Export OK")
        Catch ex As Exception
            'MsgBox("system err", ex.Message)
        End Try
    End Sub
    'ShowData method for Displaying Data in Gridview
    Private Sub showData()
        Dim strSearch As String
        strSearch = Trim(TXT_SEARCH.Value)
        Try
            If strSearch <> "" Then
                GRD_DATA.DataSource = BL.Search(strSearch)
                GRD_DATA.DataBind()

            End If
        Catch ex As Exception
            MsgBox("system err")
        End Try
    End Sub
End Class