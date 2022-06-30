Imports MRACommon
Imports MRACommon.CommonUtil
Imports System.Drawing
Imports System.Globalization
Public Class MRA_FE_0051
    Inherits MRA_FW.BasePL

    Public BL As New Table_BL
    Private Sub MRA_FE_0051_Unload(sender As Object, e As EventArgs) Handles Me.Unload
        BL.Dispose()
    End Sub
    '''' <summary>
    '''' initload
    '''' </summary>
    '''' <param name="sender"></param>
    '''' <param name="e"></param>
    Private Sub MRA_FE_0051_InitLoad(sender As Object, e As EventArgs) Handles Me.InitLoad
        Dim dt As DataTable
        Try
            ClearMessages()
            ClearError(form1)
            AddMessage("MSG_1000_01")
            dt = BL.Load()
            If dt.Rows.Count > 0 Then
                GRD_DATA.DataSource = dt
                GRD_DATA.DataBind()
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
        strSearch = Trim(TXT_SEARCH.Value)
        Dim strDateFrom As String
        Dim strDateTo As String
        strDateFrom = Trim(DATE_FROM.Value)
        strDateTo = Trim(DATE_TO.Value)
        Dim chkFlag As Boolean = False
        Dim lstError As New List(Of String)
        Dim dt As DataTable
        Try
            ClearMessages()
            ClearError(form1)
            If strSearch <> "" Or strDateFrom <> "" Or strDateTo <> "" Then
                If strDateFrom <> "" And strDateTo <> "" Then
                    If (DateTime.Parse(strDateFrom, CultureInfo.InvariantCulture) > DateTime.Parse(strDateTo, CultureInfo.InvariantCulture)) Then
                        lstError.Add(DATE_FROM.ClientID)
                        lstError.Add(DATE_TO.ClientID)
                        AddMessage("MSG_0041_02", {"FromDate", "ToDate"})
                        chkFlag = True
                    End If
                End If
                If chkFlag Then
                    If lstError.Count > 0 Then
                        SetError(Me.form1, lstError)
                    End If
                    Exit Sub
                End If
                dt = BL.Search(strSearch, strDateFrom, strDateTo)
                If dt.Rows.Count > 0 Then
                    GRD_DATA.DataSource = dt
                    GRD_DATA.DataBind()
                    GRD_DATA.PageIndex = 0
                Else
                    AddMessage("MSG_0001_04")
                    GRD_DATA.DataSource = New List(Of String)
                    GRD_DATA.DataBind()
                    Exit Sub
                End If
            End If
            If GRD_DATA.Rows.Count > 0 Then
                BTN_ADD_ROW.Visible = True
            Else
                BTN_ADD_ROW.Visible = False
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
    '--------update delete edit cancel row start---------
    Protected Sub OnRowEditing(sender As Object, e As GridViewEditEventArgs)
        GRD_DATA.EditIndex = e.NewEditIndex
        showData()
    End Sub
    Protected Sub OnUpdate(sender As Object, e As EventArgs)
        Dim row As GridViewRow = TryCast(TryCast(sender, LinkButton).NamingContainer, GridViewRow)
        Dim dicData As Dictionary(Of String, String)
        ClearMessages()

        If IsPostBack Then
            Try
                dicData = New Dictionary(Of String, String)
                Dim table_id As String = row.Cells(1).Text
                Dim nameVN As String = TryCast(row.Cells(2).Controls(0), TextBox).Text
                Dim nameJP As String = TryCast(row.Cells(3).Controls(0), TextBox).Text
                Dim nameEN As String = TryCast(row.Cells(4).Controls(0), TextBox).Text
                Dim capacity As String = TryCast(row.Cells(5).Controls(0), TextBox).Text
                Dim description As String = TryCast(row.Cells(7).Controls(0), TextBox).Text
                dicData.Add("table_id", table_id)
                dicData.Add("nameVN", nameVN)
                dicData.Add("nameJP", nameJP)
                dicData.Add("nameEN", nameEN)
                dicData.Add("capacity", capacity)
                dicData.Add("description", description)
                If nameVN = "" Or nameJP = "" Or nameEN = "" Or capacity = "" Then
                    AddMessage("MSG_1000_05", {"Name and Capacity"})
                    Exit Sub
                End If
                Dim numExp As New Regex("^[1-9-]*$")
                If Not numExp.Match(capacity).Success Then
                    AddMessage("MSG_1000_06", {"Capacity"})
                    Exit Sub
                End If
                GRD_DATA.EditIndex = -1
                BL.updTable(dicData)
                AddMessage("MSG_0001_02", {"Table master"})
                showData()
            Catch ex As Exception
                AddMessage("MSG_9000_01", {ex.Message})
            End Try
        End If
    End Sub

    Protected Sub OnRowDeleting(sender As Object, e As GridViewDeleteEventArgs)
        Try
            ClearMessages()
            Dim table_id As String = e.Keys("table_id").ToString()
            Dim iCnt As Integer = BL.getCntOrdersByTable(table_id)
            If iCnt > 0 Then
                AddMessage("MSG_0051_01", {iCnt})
                Exit Sub
            Else
                If BL.fncDelTable(table_id) Then
                    'Setting the EditIndex property to -1 to cancel the Edit mode in Gridview
                    showData()
                Else
                    AddMessage("MSG_0001_09", {"Table"})
                    Exit Sub
                End If
            End If
        Catch ex As Exception
            AddMessage("MSG_9000_01", {ex.Message})
        End Try
    End Sub
    Protected Sub OnCancel(sender As Object, e As EventArgs)
        GRD_DATA.EditIndex = -1
        showData()
    End Sub

    ''' <summary>
    ''' change background for status column
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Protected Sub OnRowDataBound(sender As Object, e As GridViewRowEventArgs)
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim cell As TableCell = e.Row.Cells(6)
            Dim strTblstatus As String = cell.Text.Trim()

            Select Case strTblstatus
                Case "Booking"
                    cell.BackColor = Color.Blue
                Case "Ordering"
                    cell.BackColor = Color.Red
                Case "Serving"
                    cell.BackColor = Color.Green
            End Select
        End If
    End Sub

    '--------update delete edit cancel row end---------
    '--------add row start---------
    Private Sub BTN_ADD_ROW_ServerClick(sender As Object, e As EventArgs) Handles BTN_ADD_ROW.ServerClick
        ScriptManager.RegisterStartupScript(Me.Page, Page.GetType(), "MyScript", "$('#addModalDates').modal('show');", True)
    End Sub
    Private Sub BTN_SAVE_Click(sender As Object, e As EventArgs) Handles BTN_SAVE.Click
        Dim CommonDB As CommonDB = New CommonDB
        CommonDB.BeginTransaction()

        If TXT_VN.Text = "" Or TXT_JP.Text = "" Or TXT_EN.Text = "" Or TXT_CAP.Text = "" Then
            AddMessage("MSG_1000_05", {"Menu Name"})
            Exit Sub
        End If
        Dim numExp As New Regex("^[1-9-]*$")
        If Not numExp.Match(TXT_CAP.Text).Success Then
            AddMessage("MSG_1000_06", {"Capacity"})
            Exit Sub
        End If
        Try
            Dim sql As String = ""
            sql = ""
            sql &= "INSERT INTO m_table_list"
            sql &= "("
            sql &= "table_nm_vn"
            sql &= ",table_nm_en"
            sql &= ",table_nm_jp"
            sql &= ",sort_no"
            sql &= ",table_ava"
            sql &= ",capacity"
            sql &= ",description"
            sql &= ",crt_dt"
            sql &= ",crt_user_id"
            sql &= ",crt_pgm_id"
            sql &= ",upd_dt"
            sql &= ",upd_user_id"
            sql &= ",upd_pgm_id"
            sql &= ",del_fg"
            sql &= ")"
            sql &= "VALUES"
            sql &= " (" & CommonDB.EncloseVal(TXT_VN.Text)
            sql &= " ," & CommonDB.EncloseVal(TXT_EN.Text)
            sql &= " ," & CommonDB.EncloseVal(TXT_JP.Text)
            sql &= " ,''"
            sql &= " ,'table1.png'"
            sql &= " ," & CommonDB.EncloseVal(TXT_CAP.Text)
            sql &= " ," & CommonDB.EncloseVal(TXT_NOTE.Text)
            sql &= " ," & CommonDB.EncloseVal(System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"))
            sql &= " ,'admin'"
            sql &= " ,'MRA-FE-0051'"
            sql &= " ,''"
            sql &= " ,''"
            sql &= " ,''"
            sql &= " ,'0'"
            sql &= ")"
            If Not CommonDB.ExecuteNonQuery(sql) = 1 Then
                CommonDB.Rollback()
            End If
            CommonDB.Commit()
            AddMessage("MSG_0001_01", {"Table"})
            Response.Redirect("~/MRA-FE-0051.aspx", False)
        Catch ex As Exception
            AddMessage("MSG_9000_01", {"Insert table fail:" & ex.Message})
        End Try
    End Sub
    '--------add row end---------
    'ShowData method for Displaying Data in Gridview
    Private Sub showData()
        Dim strSearch As String
        strSearch = Trim(TXT_SEARCH.Value)
        Dim strDateFrom As String
        Dim strDateTo As String
        strDateFrom = Trim(DATE_FROM.Value)
        strDateTo = Trim(DATE_TO.Value)
        Dim dtl As DataTable
        Dim dts As DataTable
        Try
            ClearMessages()
            dtl = BL.Load()
            dts = BL.Search(strSearch, strDateFrom, strDateTo)
            If strSearch <> "" Or strDateFrom <> "" Or strDateTo <> "" Then
                If dts.Rows.Count > 0 Then
                    GRD_DATA.DataSource = BL.Search(strSearch, strDateFrom, strDateTo)
                    GRD_DATA.DataBind()
                End If
            Else
                If dtl.Rows.Count > 0 Then
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
