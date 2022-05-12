Imports System.Data
Imports System.Configuration
Imports MySql.Data.MySqlClient

Public Class MRA_FE_0011
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Me.IsPostBack Then
            Dim constr As String = ConfigurationManager.ConnectionStrings("MRconstr").ConnectionString
            Using con As New MySqlConnection(constr)
                Using cmd As New MySqlCommand("SELECT USER_NM,CRT_DT,CRT_USER_ID FROM M_USER")
                    Using sda As New MySqlDataAdapter()
                        cmd.Connection = con
                        sda.SelectCommand = cmd
                        Using dt As New DataTable()
                            sda.Fill(dt)
                            GridView1.DataSource = dt
                            GridView1.DataBind()
                        End Using
                    End Using
                End Using
            End Using
        End If
    End Sub

End Class