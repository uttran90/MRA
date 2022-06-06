Imports System.Web.UI.HtmlControls.HtmlGenericControl

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
    Private Sub BindGrid()
        GRD_DATA.DataSource = TryCast(ViewState("dt"), DataTable)
        GRD_DATA.DataBind()
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
                Dim table_id As String = TryCast(row.Cells(1).Controls(0), TextBox).Text
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
        Dim rowIndex As Integer = Convert.ToInt32(TryCast(TryCast(sender, LinkButton).NamingContainer, GridViewRow).RowIndex)
        Dim row As GridViewRow = GRD_DATA.Rows(rowIndex)

        lblNo.Text = TryCast(row.FindControl("lblstudent_Id"), Label).Text
        lblmonth.Text = TryCast(row.FindControl("lblMonth_Name"), Label).Text


        txtAmount.Text = TryCast(row.FindControl("lblAmount"), Label).Text
        ClientScript.RegisterStartupScript(Me.[GetType](), "Pop", "openModal();", True)
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
