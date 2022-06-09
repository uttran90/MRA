
Public Class MRA_FE_0031
    Inherits System.Web.UI.Page
    Public BL As New Product_BL

    Private Sub MRA_FE_0031_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            If BL.Load().Rows.Count > 0 Then
                GRD_DATA.DataSource = BL.Load()
                GRD_DATA.DataBind()
            Else
                MsgBox("No data")
                Exit Sub
            End If
        Catch ex As Exception
            MsgBox("system err")
        End Try
    End Sub

    Private Sub BTN_SEARCH_ServerClick(sender As Object, e As EventArgs) Handles BTN_SEARCH.ServerClick
        Dim strSearch As String
        strSearch = Trim(TXT_SEARCH.Value)
        Try
            If strSearch <> "" Then
                If BL.Search(strSearch).Rows.Count > 0 Then
                    GRD_DATA.DataSource = BL.Search(strSearch)
                    GRD_DATA.DataBind()
                Else
                    MsgBox("No data")
                    Exit Sub
                End If
            End If
        Catch ex As Exception
            MsgBox("system err")
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
