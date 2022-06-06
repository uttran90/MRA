Imports System.Web.UI.HtmlControls.HtmlGenericControl
Imports MRACommon
Public Class MRA_FE_0051
    Inherits System.Web.UI.Page
    Public BL As New Table_BL

    '''' <summary>
    '''' initload
    '''' </summary>
    '''' <param name="sender"></param>
    '''' <param name="e"></param>
    Private Sub MRA_FE_0051_InitLoad(sender As Object, e As EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                GRD_DATA.DataSource = BL.Load()
                GRD_DATA.DataBind()
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
        Dim CommonDB As CommonDB = New CommonDB
        CommonDB.BeginTransaction()
        If IsPostBack Then
            Try
                Dim table_id As String = row.Cells(1).Text
                Dim nameVN As String = TryCast(row.Cells(2).Controls(0), TextBox).Text
                Dim nameJP As String = TryCast(row.Cells(3).Controls(0), TextBox).Text
                Dim capacity As String = TryCast(row.Cells(4).Controls(0), TextBox).Text
                Dim description As String = TryCast(row.Cells(6).Controls(0), TextBox).Text
                GRD_DATA.EditIndex = -1
                Dim dt As DataTable = New DataTable
                Dim sql As String
                sql = ""
                sql &= "UPDATE m_table_list"
                sql &= "   SET table_nm_vn = " & CommonDB.EncloseVal(nameVN)
                sql &= "      ,table_nm_jp = " & CommonDB.EncloseVal(nameJP)
                sql &= "      ,capacity = " & CommonDB.EncloseVal(capacity)
                sql &= "      ,description = " & CommonDB.EncloseVal(description)
                sql &= "      ,upd_dt = " & CommonDB.EncloseVal(System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"))
                sql &= "      ,upd_user_id  = 'admin'"
                sql &= "      ,upd_pgm_id   = 'MRA-FE-0051'"
                sql &= " WHERE table_id = " & table_id
                If Not CommonDB.ExecuteNonQuery(sql) = 1 Then
                    CommonDB.Rollback()
                End If
                CommonDB.Commit()
                'GRD_DATA.DataBind()
                'Setting the EditIndex property to -1 to cancel the Edit mode in Gridview
                showData()
            Catch ex As Exception
                MsgBox("system err")
            End Try
        End If
    End Sub
    Protected Sub OnRowDeleting(sender As Object, e As GridViewDeleteEventArgs)
        Dim CommonDB As CommonDB = New CommonDB
        CommonDB.BeginTransaction()
        Try
            Dim table_id As String = e.Keys("table_id").ToString()
            Dim dt As DataTable = New DataTable
            Dim sql As String
            sql = ""
            sql &= "UPDATE m_table_list"
            sql &= "   SET del_fg = '1'"
            sql &= "      ,upd_dt = " & CommonDB.EncloseVal(System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"))
            sql &= "      ,upd_user_id  = 'admin'"
            sql &= "      ,upd_pgm_id   = 'MRA-FE-0051'"
            sql &= " WHERE table_id = " & table_id
            If Not CommonDB.ExecuteNonQuery(sql) = 1 Then
                CommonDB.Rollback()
            End If
            CommonDB.Commit()
            'Setting the EditIndex property to -1 to cancel the Edit mode in Gridview
            showData()
        Catch ex As Exception
            MsgBox("system err")
        End Try
    End Sub
    Protected Sub OnCancel(sender As Object, e As EventArgs)
        GRD_DATA.EditIndex = -1
        showData()
    End Sub
    '--------update delete edit cancel row end---------
    '--------add row start---------
    Private Sub BTN_ADD_ROW_ServerClick(sender As Object, e As EventArgs) Handles BTN_ADD_ROW.ServerClick
        ScriptManager.RegisterStartupScript(Me.Page, Page.GetType(), "MyScript", "$('#addModalDates').modal('show');", True)
    End Sub
    Private Sub BTN_SAVE_Click(sender As Object, e As EventArgs) Handles BTN_SAVE.Click
        Dim CommonDB As CommonDB = New CommonDB
        CommonDB.BeginTransaction()
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
            sql &= " ,''"
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
            MsgBox("Insert OK")
            Response.Redirect("~/MRA-FE-0051.aspx", False)
        Catch ex As Exception
            MsgBox("Insert except", ex.Message)
        End Try
    End Sub
    '--------add row end---------
    'ShowData method for Displaying Data in Gridview
    Private Sub showData()
        Dim strSearch As String
        strSearch = Trim(TXT_SEARCH.Value)
        Try
            If strSearch <> "" Then
                GRD_DATA.DataSource = BL.Search(strSearch)
                GRD_DATA.DataBind()
            Else
                GRD_DATA.DataSource = BL.Load()
                GRD_DATA.DataBind()
            End If
        Catch ex As Exception
            MsgBox("system err")
        End Try
    End Sub
End Class
