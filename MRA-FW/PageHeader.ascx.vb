Public Class PageHeader
    Inherits System.Web.UI.UserControl
    Public _messageInfo As New List(Of String)
    Public _messageWar As New List(Of String)
    Public _messageErr As New List(Of String)
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Private Sub PageHeader_PreRender(sender As Object, e As EventArgs) Handles Me.PreRender
        If Not HttpContext.Current.Session("HEADER_MESSAGES") Is Nothing Then
            Dim headerMessages As List(Of Object) = HttpContext.Current.Session("HEADER_MESSAGES")

            For Each msg As String() In headerMessages
                Select Case msg(0)
                    Case "I"
                        _messageInfo.Add(msg(1))
                    Case "E"
                        _messageErr.Add(msg(1))
                    Case "W"
                        _messageWar.Add(msg(1))
                End Select
            Next
        End If
    End Sub
End Class