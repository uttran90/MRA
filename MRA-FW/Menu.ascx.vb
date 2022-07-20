Public Class Menu
    Inherits System.Web.UI.UserControl

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim strRole As String = String.Empty

        Dim sessionId As String = HttpContext.Current.Session.SessionID
        strRole = HttpContext.Current.Session("role_id")

        ViewState("ROLE_ID") = strRole
    End Sub

End Class