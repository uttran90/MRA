Imports System.Threading.Tasks
Imports System.Web.UI.HtmlControls.HtmlGenericControl
Public Class MRA_FE_0021
    Inherits System.Web.UI.Page

    Private BL As New BL
    Public PageData As PageData
    Public Sub New()
        MyBase.New
        PageData = New PageData
    End Sub

    '''' <summary>
    '''' 
    '''' </summary>
    '''' <param name="sender"></param>
    '''' <param name="e"></param>
    Private Sub MRA_FE_0021_InitLoad(sender As Object, e As EventArgs) Handles Me.Load
        Try
            'State control
            If BL.Load(PageData) Then
                MsgBox("Data map")
            Else
                MsgBox("No data map")

            End If

        Catch ex As Exception

        End Try
    End Sub
    ''' <summary>
    ''' Search
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub BTN_SEARCH_ServerClick(sender As Object, e As EventArgs) Handles BTN_SEARCH.ServerClick
        Try
            If BL.Search(PageData) Then
                MsgBox("No data map")
            Else
                MsgBox("No data map")

            End If
        Catch ex As Exception
            MsgBox("system err")
        End Try
    End Sub

End Class

