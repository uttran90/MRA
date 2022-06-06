Imports System.Threading.Tasks
Imports System.Web.UI.HtmlControls.HtmlGenericControl

Public Class MRA_FE_0042
    Inherits System.Web.UI.Page
    Public BL As New OrderDetail_BL

    '''' <summary>
    '''' initload
    '''' </summary>
    '''' <param name="sender"></param>
    '''' <param name="e"></param>
    Private Sub MRA_FE_0042_InitLoad(sender As Object, e As EventArgs) Handles Me.Load
        Dim strOrderId As String = Request.QueryString("table_info_id")
        'Data info order
        Try
            If Not Page.IsPostBack Then
                If strOrderId <> "" Then
                    'get data
                    GRD_DATA.DataSource = BL.GetOrderInfo(strOrderId)
                    GRD_DATA.DataBind()
                    'SumTotalGRD_DATA_(BL.GetOrderInfo(strOrderId))
                    TXT_SEARCH.Text = strOrderId

                    'set state update
                    TXT_SEARCH.readonly = vbReadOnly
                    BTN_SEARCH.Visible = False

                Else
                    TXT_SEARCH.Text = ""
                    BTN_SEARCH.Visible = True
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
        Try
            If strOrderId <> "" Then
                GRD_DATA.DataSource = BL.GetOrderInfo(strOrderId)
                GRD_DATA.DataBind()
                'SumTotalGRD_DATA_(BL.GetOrderInfo(strOrderId))
            End If
        Catch ex As Exception
            MsgBox("system err")
        End Try
    End Sub
    'Change page in datagrid
    Private Sub GRD_DATA_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles GRD_DATA.PageIndexChanging
        GRD_DATA.PageIndex = e.NewPageIndex
        GRD_DATA.DataBind() 'bindgridview will Get the data source And bind it again
    End Sub
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
        If IsPostBack Then
            Try
                Dim product_id As String = TryCast(row.Cells(1).Controls(0), TextBox).Text
                Dim price As String = TryCast(row.Cells(4).Controls(0), TextBox).Text
                GRD_DATA.EditIndex = -1
                Dim dt As DataTable = New DataTable
                Dim sql As String
                sql = ""
                sql &= "UPDATE m_product"
                sql &= "   SET price = " & CommonDB.EncloseVal(price)
                sql &= "      ,upd_dt = " & CommonDB.EncloseVal(System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"))
                sql &= "      ,upd_user_id  = 'admin'"
                sql &= "      ,upd_pgm_id   = 'MRA-FE-0042'"
                sql &= " WHERE product_id = " & product_id
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
    'ShowData method for Displaying Data in Gridview
    Private Sub showData()
        Dim strSearch As String
        strSearch = Trim(TXT_SEARCH.Text)
        Try
            GRD_DATA.DataSource = BL.GetOrderInfo(strSearch)
            GRD_DATA.DataBind()
        Catch ex As Exception
            MsgBox("system err")
        End Try
    End Sub
    Protected Sub Cancel(sender As Object, e As EventArgs)
        GRD_DATA.EditIndex = -1
        showData()
    End Sub
End Class