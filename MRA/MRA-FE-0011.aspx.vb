Imports System.Data
Imports System.Configuration
Imports MySql.Data.MySqlClient
Imports System.Threading.Tasks
Imports System.Web.UI.HtmlControls.HtmlGenericControl
Imports System.Security.Cryptography

Public Class MRA_FE_0011
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Me.IsPostBack Then
            Dim constr As String = ConfigurationManager.ConnectionStrings("MRconstr").ConnectionString
            Using con As New MySqlConnection(constr)
                'Using cmd As New MySqlCommand("SELECT USER_NM,CRT_DT,CRT_USER_ID FROM M_USER")
                ' Using sda As New MySqlDataAdapter()
                ' cmd.Connection = con
                'sda.SelectCommand = cmd
                'Using dt As New DataTable()
                'sda.Fill(dt)
                '      GridView1.DataSource = dt
                'GridView1.DataBind()
                ' End Using
                'End Using
                'End Using
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Button Login 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub BTN_LOGIN_ServerClick(sender As Object, e As EventArgs) Handles BTN_LOGIN.ServerClick
        Dim strUser As String = String.Empty
        Dim strPass As String = String.Empty
        strUser = TXT_ID.Value
        strPass = TXT_PW.Value
        If Trim(strUser) = "" Or Trim(strPass) = "" Then
            MsgBox("Please input admin!")
        End If
        If (Trim(strUser) = "admin" And Trim(strPass) = "admin") Then
            Response.Redirect("~/MRA-FE-0021.aspx")
        End If

    End Sub
    ''' <summary>
    ''' Check null
    ''' </summary>
    ''' <returns></returns>
    Private Function CheckIsInputEmpty() As Boolean
        Dim bCheck As Boolean = True
        Dim errLst As List(Of String) = New List(Of String)
        Try
            'Check null
            If TXT_ID.ToString().Trim() = "" OrElse TXT_PW.ToString().Trim() = "" Then

                MsgBox("User or password is wrong!")
                bCheck = False
            End If
        Catch ex As Exception

            MsgBox("Exception Err!")
        End Try
        Return bCheck
    End Function
End Class
