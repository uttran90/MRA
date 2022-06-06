Imports System
Imports System.Threading.Tasks
Imports System.Security.Cryptography

Public Class CommonUtil

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

    Public _PageData As Dictionary(Of String, Object)
    Public SelectedData As Dictionary(Of String, Object)
    Public Sub New()
        _PageData = New Dictionary(Of String, Object)
    End Sub
End Class
