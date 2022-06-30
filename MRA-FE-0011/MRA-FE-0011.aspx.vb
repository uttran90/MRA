Imports MRACommon.CommonUtil

Public Class MRA_FE_0011
    Inherits System.Web.UI.Page
    Public BL As New Login_BL

    Private Sub MRA_FE_0011_Unload(sender As Object, e As EventArgs) Handles Me.Unload
        BL.Dispose()
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Me.IsPostBack Then
            TXT_ID.Value = ""
            TXT_PW.Value = ""
        End If
    End Sub
    ''' <summary>
    ''' Button Login 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub BTN_LOGIN_ServerClick(sender As Object, e As EventArgs) Handles BTN_LOGIN.ServerClick
        Dim strUser As String
        Dim strPass As String
        Dim dt As DataTable
        strUser = TXT_ID.Value
        strPass = TXT_PW.Value
        'warning when user, pass is null
        If Trim(strUser) = "" Then
            AddMessage("MSG_0011_02", {"User"})
        End If
        If Trim(strPass) = "" Then
            AddMessage("MSG_0011_02", {"Password"})
        End If
        If Trim(strUser) <> "" And Trim(strPass) <> "" Then
            dt = BL.GetLogin(strUser, strPass)
            'warning when user, pass is not exist
            If dt.Rows(0)("count") = 0 Then
                AddMessage("MSG_0011_03")
            Else
                Response.Redirect("MRA-FE-0021.aspx")
                Session("user_id") = Trim(strUser)
                HttpContext.Current.Session.Add("user_id", strUser)

            End If
        End If
    End Sub
End Class
