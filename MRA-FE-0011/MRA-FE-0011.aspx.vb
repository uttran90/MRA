Imports MRACommon.CommonUtil
Imports MRA_FW
Public Class MRA_FE_0011
    Inherits BasePL
    Public BL As New Login_BL

    Private Sub MRA_FE_0011_Unload(sender As Object, e As EventArgs) Handles Me.Unload
        BL.Dispose()
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.InitLoad
        ClearMessages()
        ClearError(form1)
        If Not IsPostBack Then
            AddMessage("MSG_0011_01")
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
        ClearMessages()
        ClearError(form1)
        Dim strUser As String
        Dim strPass As String        
        Dim dt As DataTable

        Dim strR As String
        Dim dtRole As DataTable
        
        strUser = TXT_ID.Value
        strPass = TXT_PW.Value
        Try
            'warning when user, pass is null
            If Not ChkEmptyInput() Then
                If Trim(strUser) <> "" And Trim(strPass) <> "" Then
                    Dim strHaspass As String = MRACommon.CommonUtil.fncMD5hashPass(strPass)
                    dt = BL.GetLogin(strUser, strHaspass)
                    dtRole = BL.GetRoleLogin(strUser)
                    'warning when user, pass is not exist
                    If dt.Rows(0)("count") = 0 Then
                        AddMessage("MSG_0011_03")
                    Else
                        If dtRole.Rows.Count > 0 Then
                            Dim row As DataRow = dtRole.Rows(0)
                            strR = row("role_id")
                            Session("role_id") = Trim(strR)
                            HttpContext.Current.Session.Add("role_id", strR)
                        End If
                        Response.Redirect("MRA-FE-0021.aspx")
                        Session("user_id") = Trim(strUser)
                        HttpContext.Current.Session.Add("user_id", strUser)
                    End If
                End If
            End If
        Catch ex As Exception
            AddMessage("MSG_9000_01", {ex.Message})
        End Try
    End Sub

    Private Function ChkEmptyInput() As Boolean
        Dim lstError As New List(Of String)
        Try
            If TXT_ID.Value = "" Then
                AddMessage("MSG_9000_03", {"User name"})
                lstError.Add(TXT_ID.ClientID)
            End If
            If TXT_PW.Value = "" Then
                AddMessage("MSG_9000_03", {"Password"})
                lstError.Add(TXT_PW.ClientID)
            End If

            If lstError.Count > 0 Then
                SetError(Me.form1, lstError)
                Return True
            End If
            Return False
        Catch ex As Exception
            Throw ex
        End Try
    End Function
End Class
