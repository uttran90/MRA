Imports MRACommon.CommonUtil
Public Class MRA_FE_0031
    Inherits MRA_FW.BasePL

    Public BL As New Product_BL

    Private Sub MRA_FE_0031_Load(sender As Object, e As EventArgs) Handles Me.InitLoad
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

    Private Sub GRD_DATA_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles GRD_DATA.PageIndexChanging
        GRD_DATA.PageIndex = e.NewPageIndex
        showData() 'bindgridview will Get the data source And bind it again
    End Sub

    Private Sub showData()
        Dim strSearch As String
        strSearch = Trim(TXT_SEARCH.Value)
        Try
            ClearMessages()
            If strSearch <> "" Then
                GRD_DATA.DataSource = BL.Search(strSearch)
                GRD_DATA.DataBind()
            Else
                GRD_DATA.DataSource = BL.Load()
                GRD_DATA.DataBind()
            End If
            AddMessage("MSG_1000_01")
        Catch ex As Exception
            AddMessage("MSG_9000_01", {ex.Message})
        End Try
    End Sub
End Class
