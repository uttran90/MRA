Imports System.Globalization
Imports System.Threading.Tasks
Imports System.Web.UI.HtmlControls.HtmlGenericControl
Imports MRACommon
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
        Dim dt As DataTable
        Try
            dt = BL.Load()
            If dt.Rows.Count > 0 Then
                GRD_DATA.DataSource = dt
                GRD_DATA.DataBind()
                SumTotalGRD_DATA(dt)
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
        Dim strDateFrom As String
        Dim strDateTo As String
        Dim strTimeFrom As String
        Dim strTimeTo As String

        strSearch = Trim(TXT_SEARCH.Value)
        strDateFrom = Trim(DATE_FROM.Value)
        strDateTo = Trim(DATE_TO.Value)
        strTimeFrom = Trim(TIME_FROM.Value)
        strTimeTo = Trim(TIME_TO.Value)
        Dim dt As DataTable
        Try
            If strSearch <> "" Or strDateFrom <> "" Or strDateTo <> "" Or strTimeFrom <> "" Or strTimeTo <> "" Then
                If strDateFrom <> "" And strDateTo <> "" Then
                    If (DateTime.Parse(strDateFrom, CultureInfo.InvariantCulture) >= DateTime.Parse(strDateTo, CultureInfo.InvariantCulture)) Then
                        MsgBox("From must be less than To")
                        Exit Sub
                    End If
                End If
                If strTimeFrom <> "" And strTimeTo <> "" Then
                    If (DateTime.ParseExact(strTimeFrom, "HH:mm:ss", CultureInfo.InvariantCulture) >= DateTime.ParseExact(strTimeTo, "HH:mm:ss", CultureInfo.InvariantCulture)) Then
                        MsgBox("From must be less than To")
                        Exit Sub
                    End If
                End If
                dt = BL.Search(strSearch, strDateFrom, strDateTo, strTimeFrom, strTimeTo)
                If dt.Rows.Count > 0 Then
                    GRD_DATA.DataSource = dt
                    GRD_DATA.DataBind()
                    SumTotalGRD_DATA(dt)
                Else
                    MsgBox("No data")
                    GRD_DATA.DataBind()
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

        Dim strDateFrom As String
        Dim strDateTo As String
        Dim strTimeFrom As String
        Dim strTimeTo As String
        strDateFrom = Trim(DATE_FROM.Value)
        strDateTo = Trim(DATE_TO.Value)
        strTimeFrom = Trim(TIME_FROM.Value)
        strTimeTo = Trim(TIME_TO.Value)
        If strDateFrom > strDateTo Or strTimeFrom > strTimeTo Then
            MsgBox("From must be less than To")
            Exit Sub
        End If
        Dim dtExport As DataTable
        Response.Clear()
        Response.Buffer = True
        Response.AddHeader("content-disposition", "attachment;filename=Export.csv")
        Response.Charset = ""
        Response.ContentType = "application/text"
        dtExport = BL.Search(strSearch, strDateFrom, strDateTo, strTimeFrom, strTimeTo)
        'read column
        If dtExport.Rows.Count > 0 Then
            Dim sBuilder As StringBuilder = New System.Text.StringBuilder()
            For a As Integer = 0 To GRD_DATA.Columns.Count - 1
                sBuilder.Append(GRD_DATA.Columns(a).HeaderText + ","c)
            Next
            sBuilder.Append(vbCr & vbLf)
            'read row
            For i As Integer = 0 To dtExport.Rows.Count - 1
                For k As Integer = 0 To dtExport.Columns.Count - 1 'read cell
                    sBuilder.Append(dtExport.Rows(i)(k).ToString().Replace(",", "") + ",")
                Next
                sBuilder.Append(vbCr & vbLf)
            Next
            Response.Output.Write(sBuilder.Replace("&nbsp;", ""))
            Response.Flush()
            Response.End()
            MsgBox("Export OK")
        Else
            MsgBox("No data")
            Exit Sub
        End If
    End Sub
    '--------update delete edit cancel row start---------
    Protected Sub OnRowEditing(sender As Object, e As GridViewEditEventArgs)
        GRD_DATA.EditIndex = e.NewEditIndex
        GRD_DATA.DataBind()
    End Sub
    Protected Sub OnUpdate(sender As Object, e As EventArgs)
        Dim row As GridViewRow = TryCast(TryCast(sender, LinkButton).NamingContainer, GridViewRow)
        Dim CommonDB As CommonDB = New CommonDB
        CommonDB.BeginTransaction()

        Try
            Dim table_info_id As String = TryCast(row.Cells(1).Controls(0), HyperLink).Text
            Dim cus_nm As String = TryCast(row.Cells(2).Controls(0), TextBox).Text
            Dim cus_cnt As String = TryCast(row.Cells(3).Controls(0), TextBox).Text
            Dim cus_phone As String = TryCast(row.Cells(4).Controls(0), TextBox).Text
            Dim date_o As String = TryCast(row.Cells(7).Controls(0), TextBox).Text
            Dim time_o As String = TryCast(row.Cells(8).Controls(0), TextBox).Text
            Dim note As String = TryCast(row.Cells(10).Controls(0), TextBox).Text

            Dim dt As DataTable = New DataTable
            Dim sql As String
            sql = ""
            sql &= "UPDATE t_table_info"
            sql &= "   SET guess_nm = " & CommonDB.EncloseVal(cus_nm)
            sql &= "      ,guess_count = " & CommonDB.EncloseVal(cus_cnt)
            sql &= "      ,guess_phone = " & CommonDB.EncloseVal(cus_phone)
            sql &= "      ,serve_date = DATE_FORMAT(" & CommonDB.EncloseVal(date_o) & ",'%Y-%m-%d')"
            sql &= "      ,serve_time = TIME_FORMAT(" & CommonDB.EncloseVal(time_o) & ",'%H:%i:%s')"
            sql &= "      ,note_tx = " & CommonDB.EncloseVal(note)
            sql &= "      ,upd_dt = " & CommonDB.EncloseVal(System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"))
            sql &= "      ,upd_user_id  = 'admin'"
            sql &= "      ,upd_pgm_id   = 'MRA-FE-0041'"
            sql &= " WHERE table_info_id = " & table_info_id
            If Not CommonDB.ExecuteNonQuery(sql) = 1 Then
                CommonDB.Rollback()
            End If
            CommonDB.Commit()
            GRD_DATA.EditIndex = -1
            showData()
            CommonDB.Dispose()
        Catch ex As Exception
            MsgBox("system err")
        End Try
    End Sub
    Protected Sub OnCancel(sender As Object, e As EventArgs)
        GRD_DATA.EditIndex = -1
        showData()
    End Sub
    Protected Sub OnRowDeleting(sender As Object, e As GridViewDeleteEventArgs)
        Dim CommonDB As CommonDB = New CommonDB
        CommonDB.BeginTransaction()
        Try
            Dim table_info_id As String = e.Keys("table_info_id").ToString()
            Dim dt As DataTable = New DataTable
            Dim sql As String
            sql = ""
            sql &= "UPDATE t_table_info"
            sql &= "   SET del_fg = '1'"
            sql &= "      ,upd_dt = " & CommonDB.EncloseVal(System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"))
            sql &= "      ,upd_user_id  = 'admin'"
            sql &= "      ,upd_pgm_id   = 'MRA-FE-0041'"
            sql &= " WHERE table_info_id = " & table_info_id
            If Not CommonDB.ExecuteNonQuery(sql) = 1 Then
                CommonDB.Rollback()
            End If
            CommonDB.Commit()
            showData()
            CommonDB.Dispose()
        Catch ex As Exception
            MsgBox("system err")
        End Try
    End Sub
    '--------update delete edit cancel row end---------
    'ShowData method for Displaying Data in Gridview
    Private Sub showData()
        Dim strSearch As String
        strSearch = Trim(TXT_SEARCH.Value)

        Dim strDateFrom As String
        Dim strDateTo As String
        Dim strTimeFrom As String
        Dim strTimeTo As String
        strDateFrom = Trim(DATE_FROM.Value)
        strDateTo = Trim(DATE_TO.Value)
        strTimeFrom = Trim(TIME_FROM.Value)
        strTimeTo = Trim(TIME_TO.Value)
        If strDateFrom > strDateTo Or strTimeFrom > strTimeTo Then
            MsgBox("From must be less than To")
            Exit Sub
        End If
        Dim dt As DataTable = BL.Search(strSearch, strDateFrom, strDateTo, strTimeFrom, strTimeTo)
        Dim dtl As DataTable = BL.Load()
        Try
            If strSearch <> "" Then
                If dt.Rows.Count > 0 Then
                    GRD_DATA.DataSource = dt
                    GRD_DATA.DataBind()
                End If
            Else
                If dt.Rows.Count > 0 Then
                    GRD_DATA.DataSource = dtl
                    GRD_DATA.DataBind()
                End If
            End If
        Catch ex As Exception
            MsgBox("system err")
        End Try
    End Sub
End Class