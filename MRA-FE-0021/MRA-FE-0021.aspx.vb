Imports System.Threading.Tasks
Imports System.Web.UI.HtmlControls.HtmlGenericControl
Public Class MRA_FE_0021
    Inherits System.Web.UI.Page

    Public BL As New Menu_BL
    Private Sub MRA_FE_0021_Unload(sender As Object, e As EventArgs) Handles Me.Unload
        BL.Dispose()
    End Sub
    '''' <summary>
    '''' initload
    '''' </summary>
    '''' <param name="sender"></param>
    '''' <param name="e"></param>
    Private Sub MRA_FE_0021_InitLoad(sender As Object, e As EventArgs) Handles Me.Load

        Try
            GRD_DATA.DataSource = BL.Load()
            GRD_DATA.DataBind()
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

