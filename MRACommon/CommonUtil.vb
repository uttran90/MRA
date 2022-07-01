Imports System
Imports System.Threading.Tasks
Imports System.Security.Cryptography

Public Module CommonUtil
    Public Const PREFIX_PASS = "MRAWEB"
    Public Const SHORI_MODE = "SHORI_MODE"
    Public Const STATE_MODE = "STATE_MODE"
    ''' <summary>
    ''' Mode
    ''' </summary>
    Public Structure LIST_MODE
        Const UPDATE = "UPDATE"
        Const INSERT = "INSERT"
        Const DELETE = "DELETE"
    End Structure

    ''' <summary>
    ''' Role
    ''' </summary>
    Public Structure ROLE_ID
        Const ADMIN = "A"
        Const EMPLOYEE = "E"
        Const CUSTOMER = "C"
    End Structure

    ''' <summary>
    ''' Role
    ''' </summary>
    Public Structure SESSION_LIST_NAME
        Const USER_ID = "USER_ID"
        Const USER_NM = "USER_NM"
        Const ROLE_ID = "ROLE_ID"
    End Structure

    Public Structure LIST_BUTTON_NAME
        Const INSERT = "Add"
        Const DELETE = "Delete"
        Const CONFIRM = "Confirm"
        Const UPDATE = "Update"
    End Structure

    Sub ClearMessages()
        HttpContext.Current.Session("HEADER_MESSAGES") = Nothing
    End Sub

    Sub AddMessage(ByVal msgId As String)
        AddMessage(msgId, {})
    End Sub

    Sub AddMessage(ByVal msgId As String, ByVal params As String())
        Dim msgDic As Dictionary(Of String, String()) = HttpContext.Current.Application("G_MESSAGES")
        Dim msgStr As String
        Dim addMsg As String()
        Dim i As Integer = 1

        If msgDic.ContainsKey(msgId) Then
            msgStr = msgDic(msgId)(1)
            For Each param As String In params
                'msgStr.Replace("%" & i, param)
                msgStr = msgStr.Replace("%" & i, param)
                i += 1
            Next
            addMsg = {msgDic(msgId)(0), msgStr}
        Else
            addMsg = {"E", "[" & msgId & "] : not found messages.xml"}
        End If

        If HttpContext.Current.Session("HEADER_MESSAGES") Is Nothing Then
            Dim lst As New List(Of Object)
            lst.Add(addMsg)
            HttpContext.Current.Session("HEADER_MESSAGES") = lst
        Else
            CType(HttpContext.Current.Session("HEADER_MESSAGES"), List(Of Object)).Add(addMsg)
        End If
    End Sub

    ''' <summary>
    ''' GetMessageById
    ''' </summary>
    ''' <param name="msgId"></param>
    ''' <returns></returns>
    Function GetMessage(ByVal msgId As String) As String
        Return GetMessage(msgId, {})
    End Function

    ''' <summary>
    ''' GetMessageById param
    ''' </summary>
    ''' <param name="msgId"></param>
    ''' <param name="params"></param>
    ''' <returns></returns>
    Function GetMessage(ByVal msgId As String, ByVal params As String()) As String
        Dim msgDic As Dictionary(Of String, String()) = HttpContext.Current.Application("G_MESSAGES")
        Dim msgStr As String
        Dim i As Integer = 1

        If msgDic.ContainsKey(msgId) Then
            msgStr = msgDic(msgId)(1)
            For Each param As String In params
                msgStr = msgStr.Replace("%" & i, param)
                i += 1
            Next
        Else
            msgStr = "[" & msgId & "] : not found messages.xml"
        End If
        Return msgStr
    End Function

    Public Function fncMD5hashPass(ByVal strPass As String) As String
        Dim strData As String = PREFIX_PASS & strPass
        Dim md5 As MD5
        Dim hashData As Byte()
        Dim strDatabuilder As New StringBuilder
        Try
            md5 = MD5.Create()
            hashData = md5.ComputeHash(Encoding.Default.GetBytes(strData))
            For i As Integer = 0 To hashData.Length - 1
                strDatabuilder.Append(hashData(i).ToString())
            Next
            Return strDatabuilder.ToString()
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' Check pass
    ''' </summary>
    ''' <param name="inputData"></param>
    ''' <param name="storedHashData"></param>
    ''' <returns></returns>
    Public Function ValidateMD5HashData(ByVal inputData As String, ByVal storedHashData As String) As Boolean
        Dim strHashInputdata As String = ""
        Try
            strHashInputdata = fncMD5hashPass(inputData)
            If String.Compare(strHashInputdata, storedHashData) = 0 Then
                Return True
            End If
            Return False
        Catch ex As Exception
            Throw ex
        End Try
    End Function
End Module
