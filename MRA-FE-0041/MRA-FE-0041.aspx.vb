Imports System.Globalization
Imports System.Threading.Tasks
Imports System.Web.UI.HtmlControls.HtmlGenericControl
Imports MRACommon
Imports MRACommon.CommonUtil

Public Class MRA_FE_0041
    Inherits MRA_FW.BasePL

    Public BL As New Order_BL
    Private Sub MRA_FE_0041_Unload(sender As Object, e As EventArgs) Handles Me.Unload
        BL.Dispose()
    End Sub
    '''' <summary>
    '''' initload
    '''' </summary>
    '''' <param name="sender"></param>
    '''' <param name="e"></param>
    Private Sub MRA_FE_0041_InitLoad(sender As Object, e As EventArgs) Handles Me.InitLoad
        Dim dt As DataTable
        Try
            ClearMessages()
            AddMessage("MSG_1000_01")
            dt = BL.Load()
            If dt.Rows.Count > 0 Then
                GRD_DATA.DataSource = dt
                GRD_DATA.DataBind()
                SumTotalGRD_DATA(dt)
            Else
                AddMessage("MSG_0001_04")
                GRD_DATA.DataSource = New List(Of String)
                GRD_DATA.DataBind()
                Exit Sub
            End If
        Catch ex As Exception
            AddMessage("MSG_9000_01", {ex.Message})
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
        Dim chkFlag As Boolean = False
        Dim lstError As New List(Of String)
        Dim dt As DataTable
        Try
            ClearMessages()
            ClearError(form1)
            If strSearch <> "" Or strDateFrom <> "" Or strDateTo <> "" Or strTimeFrom <> "" Or strTimeTo <> "" Then
                If strDateFrom <> "" And strDateTo <> "" Then
                    If (DateTime.Parse(strDateFrom, CultureInfo.InvariantCulture) > DateTime.Parse(strDateTo, CultureInfo.InvariantCulture)) Then
                        lstError.Add(DATE_FROM.ClientID)
                        lstError.Add(DATE_TO.ClientID)
                        AddMessage("MSG_0041_02", {"FromDate", "ToDate"})
                        chkFlag = True
                    End If
                End If
                If strTimeFrom <> "" And strTimeTo <> "" Then
                    If (DateTime.Parse(strDateFrom, CultureInfo.InvariantCulture) = DateTime.Parse(strDateTo, CultureInfo.InvariantCulture)) And
                       (DateTime.ParseExact(strTimeFrom, "HH:mm:ss", CultureInfo.InvariantCulture) > DateTime.ParseExact(strTimeTo, "HH:mm:ss", CultureInfo.InvariantCulture)) Then
                        lstError.Add(TIME_FROM.ClientID)
                        lstError.Add(TIME_TO.ClientID)
                        AddMessage("MSG_0041_02", {"FromTime", "ToTime"})
                        chkFlag = True
                    End If
                End If
                If chkFlag Then
                    If lstError.Count > 0 Then
                        SetError(Me.form1, lstError)
                    End If
                    Exit Sub
                End If
                dt = BL.Search(strSearch, strDateFrom, strDateTo, strTimeFrom, strTimeTo)
                If dt.Rows.Count > 0 Then
                    GRD_DATA.DataSource = dt
                    GRD_DATA.DataBind()
                    SumTotalGRD_DATA(dt)
                Else
                    AddMessage("MSG_0001_04")
                    GRD_DATA.DataSource = New List(Of String)
                    GRD_DATA.DataBind()
                    Exit Sub
                End If
            End If
            AddMessage("MSG_1000_01")
        Catch ex As Exception
            AddMessage("MSG_9000_01", {ex.Message})
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
        GRD_DATA.FooterRow.Cells(5).Attributes.Add("style", "Background:#65add7")
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
        Dim chkFlag As Boolean = False
        Dim lstError As New List(Of String)
        ClearMessages()
        ClearError(form1)
        If strDateFrom <> "" And strDateTo <> "" Then
            If (DateTime.Parse(strDateFrom, CultureInfo.InvariantCulture) > DateTime.Parse(strDateTo, CultureInfo.InvariantCulture)) Then
                lstError.Add(DATE_FROM.ClientID)
                lstError.Add(DATE_TO.ClientID)
                AddMessage("MSG_0041_02", {"FromDate", "ToDate"})
                chkFlag = True
            End If
        End If
        If strTimeFrom <> "" And strTimeTo <> "" Then
            If (DateTime.Parse(strDateFrom, CultureInfo.InvariantCulture) = DateTime.Parse(strDateTo, CultureInfo.InvariantCulture)) And
                       (DateTime.ParseExact(strTimeFrom, "HH:mm:ss", CultureInfo.InvariantCulture) > DateTime.ParseExact(strTimeTo, "HH:mm:ss", CultureInfo.InvariantCulture)) Then
                lstError.Add(TIME_FROM.ClientID)
                lstError.Add(TIME_TO.ClientID)
                AddMessage("MSG_0041_02", {"FromTime", "ToTime"})
                chkFlag = True
            End If
        End If
        If chkFlag Then
            If lstError.Count > 0 Then
                SetError(Me.form1, lstError)
            End If
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
            AddMessage("MSG_0041_01")
        Else
            AddMessage("MSG_0001_04")
            Exit Sub
        End If
    End Sub
    '--------update delete edit cancel row start---------
    Protected Sub OnRowEditing(sender As Object, e As GridViewEditEventArgs)
        GRD_DATA.EditIndex = e.NewEditIndex
        showData()
    End Sub
    Protected Sub OnUpdate(sender As Object, e As EventArgs)
        Dim row As GridViewRow = TryCast(TryCast(sender, LinkButton).NamingContainer, GridViewRow)
        Dim dtCap
        Dim dicData As Dictionary(Of String, String)
        ClearMessages()
        Dim CommonDB As CommonDB = New CommonDB
        CommonDB.BeginTransaction()

        Try
            dicData = New Dictionary(Of String, String)
            Dim table_info_id As String = TryCast(row.Cells(1).Controls(0), HyperLink).Text
            Dim cus_nm As String = TryCast(row.Cells(2).Controls(0), TextBox).Text
            Dim cus_cnt As String = TryCast(row.Cells(3).Controls(0), TextBox).Text
            Dim cus_phone As String = TryCast(row.Cells(4).Controls(0), TextBox).Text
            Dim date_o As String = TryCast(row.Cells(7).Controls(0), TextBox).Text
            Dim time_o As String = TryCast(row.Cells(8).Controls(0), TextBox).Text
            Dim note As String = TryCast(row.Cells(10).Controls(0), TextBox).Text
            dicData.Add("table_info_id", table_info_id)
            dicData.Add("cus_nm", cus_nm)
            dicData.Add("cus_cnt", cus_cnt)
            dicData.Add("cus_phone", cus_phone)
            dicData.Add("date_o", date_o)
            dicData.Add("time_o", time_o)
            dicData.Add("note", note)
            Dim numExp As New Regex("^[0-9-]*$")
            If Not numExp.Match(cus_cnt).Success Then
                AddMessage("MSG_1000_04", {"Count"})
                Exit Sub
            End If
            If Not numExp.Match(cus_phone).Success Then
                AddMessage("MSG_1000_04", {"Phone"})
                Exit Sub
            End If
            dtCap = BL.GetCapacityTableOrder(table_info_id)
            If Not String.IsNullOrEmpty(cus_cnt) AndAlso Not String.IsNullOrEmpty(dtCap.Rows(0)("capacity").ToString()) AndAlso
                Convert.ToInt32(cus_cnt) > Convert.ToInt32(dtCap.Rows(0)("capacity")) Then
                AddMessage("MSG_0041_03", {dtCap.Rows(0)("capacity").ToString()})
            End If

            If Not BL.fncUpdTableInfo(dicData, CommonDB) Then
                CommonDB.Rollback()
                AddMessage("MSG_0001_08", {"Order"})
                Exit Sub
            End If
            CommonDB.Commit()
            GRD_DATA.EditIndex = -1
            AddMessage("MSG_0001_02", {"Table Info"})
            showData()
        Catch ex As Exception
            AddMessage("MSG_9000_01", {ex.Message})
        Finally
            If Not CommonDB Is Nothing Then
                CommonDB.Dispose()
            End If
        End Try
    End Sub

    Protected Sub OnCancel(sender As Object, e As EventArgs)
        GRD_DATA.EditIndex = -1
        showData()
    End Sub

    Protected Sub OnRowDeleting(sender As Object, e As GridViewDeleteEventArgs)
        ClearMessages()
        Dim CommonDB As CommonDB = New CommonDB
        CommonDB.BeginTransaction()
        Try
            Dim table_info_id As String = e.Keys("table_info_id").ToString()
            If Not BL.fncDelTableInfo(table_info_id, CommonDB) Then  'delete table info
                CommonDB.Rollback()
                AddMessage("MSG_0001_09", {"Table info"})
                Exit Sub
            ElseIf Not BL.fncDelTableOrder(table_info_id, CommonDB) Then 'delete table order
                CommonDB.Rollback()
                AddMessage("MSG_0001_09", {"Table order"})
                Exit Sub
            ElseIf Not BL.fncDelTableReceipt(table_info_id, CommonDB) Then 'delete t_table_reciept
                CommonDB.Rollback()
                AddMessage("MSG_0001_09", {"Receipt"})
                Exit Sub
            End If
            CommonDB.Commit()
            showData()
        Catch ex As Exception
            AddMessage("MSG_9000_01", {ex.Message})
        Finally
            If Not CommonDB Is Nothing Then
                CommonDB.Dispose()
            End If
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
        ClearMessages()
        Dim dt As DataTable = BL.Search(strSearch, strDateFrom, strDateTo, strTimeFrom, strTimeTo)
        Dim dtl As DataTable = BL.Load()
        Try
            If strSearch <> "" Or strDateFrom <> "" Or strDateTo <> "" Or strTimeFrom <> "" Or strTimeTo <> "" Then
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
            AddMessage("MSG_1000_01")
        Catch ex As Exception
            AddMessage("MSG_9000_01", {ex.Message})
        End Try
    End Sub
End Class