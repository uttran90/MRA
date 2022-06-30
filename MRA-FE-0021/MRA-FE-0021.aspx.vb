Imports MRACommon.CommonUtil

Public Class MRA_FE_0021
    Inherits MRA_FW.BasePL

    Public BL As New Menu_BL
    Private Sub MRA_FE_0021_Unload(sender As Object, e As EventArgs) Handles Me.Unload
        BL.Dispose()
    End Sub
    '''' <summary>
    '''' initload
    '''' </summary>
    '''' <param name="sender"></param>
    '''' <param name="e"></param>
    Private Sub MRA_FE_0021_InitLoad(sender As Object, e As EventArgs) Handles Me.InitLoad

        Try
            ClearMessages()
            AddMessage("MSG_1000_01")
            If BL.Load().Rows.Count > 0 Then
                GRD_DATA.DataSource = BL.Load()
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
        Try
            ClearMessages()
            If strSearch <> "" Then
                If BL.Search(strSearch).Rows.Count > 0 Then
                    GRD_DATA.DataSource = BL.Search(strSearch)
                    GRD_DATA.DataBind()
                    AddMessage("MSG_1000_01")
                Else
                    AddMessage("MSG_0001_04")
                    GRD_DATA.DataSource = New List(Of String)
                    GRD_DATA.DataBind()
                    Exit Sub
                End If
            End If
        Catch ex As Exception
            AddMessage("MSG_9000_01", {ex.Message})
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
            AddMessage("MSG_9000_01", {ex.Message})
        End Try
    End Sub
End Class

