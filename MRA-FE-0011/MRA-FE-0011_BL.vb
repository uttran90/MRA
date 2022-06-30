Imports MRACommon

Public Class Login_BL
    Public CommonDB As CommonDB
    Public Sub New()
        MyBase.New
        CommonDB = New CommonDB
    End Sub
    Public Sub Dispose()
        CommonDB.Dispose()
    End Sub

    Public Function GetLogin(ByVal strUserId As String, ByVal strPass As String) As DataTable
        Dim userId As String = String.Empty
        Dim sql As String = String.Empty
        Dim hashPw As String = String.Empty
        Dim dtLogin As DataTable = Nothing

        userId = strUserId
        hashPw = strPass
        Try
            sql = ""
            sql &= "select count(*) as count"
            sql &= "  from m_user mu"
            sql &= " where  mu.user_id = " & CommonDB.EncloseVal(userId)
            sql &= "   and  mu.password = " & CommonDB.EncloseVal(hashPw)
            sql &= "   and  mu.del_fg <> '1' "
            dtLogin = CommonDB.ExecuteFill(sql)

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        Return dtLogin
    End Function
End Class
