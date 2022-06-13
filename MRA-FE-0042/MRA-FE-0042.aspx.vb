Imports MRACommon

Public Class MRA_FE_0042
    Inherits System.Web.UI.Page
    Public BL As New OrderDetail_BL

    Private Sub MRA_FE_0042_Unload(sender As Object, e As EventArgs) Handles Me.Unload
        BL.Dispose()
    End Sub
    '''' <summary>
    '''' initload
    '''' </summary>
    '''' <param name="sender"></param>
    '''' <param name="e"></param>
    Private Sub MRA_FE_0042_InitLoad(sender As Object, e As EventArgs) Handles Me.Load
        Dim strOrderId As String = Request.QueryString("table_info_id")
        'Data info order
        Dim dt As DataTable
        Try
            If Not Page.IsPostBack Then
                If strOrderId <> "" Then
                    'get data
                    dt = BL.GetOrderInfo(strOrderId)
                    GRD_DATA.DataSource = dt
                    Dim row As DataRow = dt.Rows(0)
                    GRD_DATA.DataBind()
                    'SumTotalGRD_DATA_(BL.GetOrderInfo(strOrderId))
                    TXT_SEARCH.Text = strOrderId
                    TXT_DATE.Text = row("serve_date")
                Else
                    TXT_SEARCH.Text = ""
                End If
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
        Dim strOrderId As String
        strOrderId = Trim(TXT_SEARCH.Text)
        Dim dt As DataTable
        Try
            If strOrderId <> "" Then
                dt = BL.GetOrderInfo(strOrderId)
                If dt.Rows.Count > 0 Then
                    GRD_DATA.DataSource = dt
                    GRD_DATA.DataBind()
                    'SumTotalGRD_DATA_(BL.GetOrderInfo(strOrderId))
                Else
                    MsgBox("No data")
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
    'Sum bill
    Private Sub SumTotalGRD_DATA_(dt As DataTable)
        Dim total As Decimal = dt.AsEnumerable().Sum(Function(row) row.Field(Of Decimal)("Price"))
        'Set sum label in footer row
        GRD_DATA.FooterRow.Cells(1).Text = "Sum"
        GRD_DATA.FooterRow.Cells(1).Font.Bold = True
        GRD_DATA.FooterRow.Cells(1).HorizontalAlign = HorizontalAlign.Right
        GRD_DATA.FooterRow.Cells(2).Text = total.ToString("N2")
    End Sub

    Protected Sub OnRowEditing(sender As Object, e As GridViewEditEventArgs)
        GRD_DATA.EditIndex = e.NewEditIndex
        showData()
    End Sub

    Protected Sub Update(sender As Object, e As EventArgs)
        Dim row As GridViewRow = TryCast(TryCast(sender, LinkButton).NamingContainer, GridViewRow)
        Dim CommonDB As CommonDB = New CommonDB
        CommonDB.BeginTransaction()
        Dim strOrderId As String
        strOrderId = Trim(TXT_SEARCH.Text)
        If IsPostBack Then
            Try
                Dim table_order_id As String = row.Cells(1).Text
                Dim product_id As String = TryCast(row.Cells(2).Controls(0), TextBox).Text
                Dim count_p As String = TryCast(row.Cells(4).Controls(0), TextBox).Text
                Dim opt_id As String = TryCast(row.Cells(6).Controls(0), TextBox).Text
                Dim count_o As String = TryCast(row.Cells(8).Controls(0), TextBox).Text
                Dim note_tx As String = TryCast(row.Cells(11).Controls(0), TextBox).Text
                If ((product_id <> "" Or product_id = "0") And count_p = "") _
                    Or (product_id = "" And (count_p <> "" Or count_p = "0")) _
                    Or ((opt_id <> "" Or opt_id = "0") And count_o = "") Or (opt_id = "" And (count_o <> "" Or count_o = "0")) Then
                    MsgBox("ID and count must update together!")
                    Exit Sub
                End If
                GRD_DATA.EditIndex = -1
                Dim sql As String
                sql = ""
                sql &= "UPDATE t_table_order"
                sql &= " SET   product_id     = " & product_id
                sql &= "      ,count          = " & CommonDB.EncloseVal(count_p)
                If opt_id <> "" Then
                    sql &= " ,product_opt_id = " & opt_id
                End If
                If count_o <> "" Then
                    sql &= "      ,opt_count      = " & CommonDB.EncloseVal(count_o)
                End If
                sql &= "      ,note_tx        = " & CommonDB.EncloseVal(note_tx)
                sql &= "      ,upd_dt         = " & CommonDB.EncloseVal(System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"))
                sql &= "      ,upd_user_id    = 'admin'"
                sql &= "      ,upd_pgm_id     = 'MRA-FE-0042'"
                sql &= " WHERE table_info_id   = " & strOrderId
                sql &= "   and table_order_id  = " & table_order_id
                If Not CommonDB.ExecuteNonQuery(sql) = 1 Then
                    CommonDB.Rollback()
                End If
                CommonDB.Commit()
                showData()
            Catch ex As Exception
                MsgBox("system err")
            End Try
        End If
    End Sub
    Protected Sub OnRowDeleting(sender As Object, e As GridViewDeleteEventArgs)
        Dim CommonDB As CommonDB = New CommonDB
        CommonDB.BeginTransaction()
        Dim strOrderId As String
        strOrderId = Trim(TXT_SEARCH.Text)
        Try
            Dim table_order_id As String = e.Keys("table_order_id").ToString()
            Dim sql As String
            sql = ""
            sql &= "UPDATE t_table_order"
            sql &= "   SET del_fg = '1'"
            sql &= "      ,upd_dt = " & CommonDB.EncloseVal(System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"))
            sql &= "      ,upd_user_id  = 'admin'"
            sql &= "      ,upd_pgm_id   = 'MRA-FE-0042'"
            sql &= " WHERE table_info_id = '" & strOrderId & "'"
            sql &= "    and product_id       = " & table_order_id
            If Not CommonDB.ExecuteNonQuery(sql) = 1 Then
                CommonDB.Rollback()
            End If
            CommonDB.Commit()
            showData()
        Catch ex As Exception
            MsgBox("system err")
        End Try
    End Sub
    'ShowData method for Displaying Data in Gridview
    Private Sub showData()
        Dim strSearch As String
        strSearch = Trim(TXT_SEARCH.Text)
        Dim dt As DataTable
        Try
            dt = BL.GetOrderInfo(strSearch)
            If dt.Rows.Count > 0 Then
                GRD_DATA.DataSource = dt
                GRD_DATA.DataBind()
            Else
                MsgBox("No data")
            End If
        Catch ex As Exception
            MsgBox("system err")
        End Try
    End Sub
    Protected Sub Cancel(sender As Object, e As EventArgs)
        GRD_DATA.EditIndex = -1
        showData()
    End Sub
    'set text box date
    Private Sub TXT_SEARCH_CHANGE(sender As Object, e As EventArgs) Handles TXT_SEARCH.TextChanged

        Dim strOrderId As String = TXT_SEARCH.Text
        If strOrderId <> "" Then
            If BL.GetOrderInfo(strOrderId).Rows.Count > 0 Then
                Dim row As DataRow = BL.GetOrderInfo(strOrderId).Rows(0)
                TXT_DATE.Text = row("serve_date")
            Else
                MsgBox("No data!")
            End If
        Else
                MsgBox("Input key search!")
        End If
    End Sub
End Class