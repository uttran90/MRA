Imports MySql.Data.MySqlClient
Public Class CommonDB
    ''' <summary>
    ''' DBクラスはプレゼンテーション（aspx.vb）からの呼び出しはしないでください
    ''' </summary>
    Private connection As MySqlConnection

    Public Sub New()
        Dim constr As String = ConfigurationManager.ConnectionStrings("MRconstr").ConnectionString
        Dim con As New MySqlConnection(constr)
        Try
            con.Open()
            Trace.Write("DB Connection Succeeded. ")
        Catch ex As Exception
            Console.WriteLine("DB Connect Open Failed : " & ex.Message)
            Throw ex
        End Try
    End Sub

    Public Function ExecuteFill(ByVal query As String) As DataTable
        Dim cmd As MySqlCommand
        Dim da As MySqlDataAdapter
        Dim rtn As DataTable
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
        Try
            cmd = New MySqlCommand()
            da = New MySqlDataAdapter
            rtn = New DataTable
            cmd.Connection = Me.connection
            cmd.CommandType = CommandType.Text
            cmd.CommandText = query
            da.SelectCommand = cmd
            da.Fill(rtn)

            Console.WriteLine("ExecuteFill Return Rows " & rtn.Rows.Count & " : " & query)
        Catch ex As Exception
            Console.WriteLine(ex.Message & " : " & query)
            Throw ex
            Finally
                If Not da Is Nothing Then
                    da.Dispose()
                End If
                If Not cmd Is Nothing Then
                    cmd.Dispose()
                End If
            End Try
            Return rtn
        End Function

    Public Function ExecuteFill(ByVal query As String, ByVal maxRow As Integer) As DataTable
        Dim wk As String

        wk = ""
        wk &= "SELECT *"
        wk &= "  FROM ("
        wk &= query
        wk &= "       )"
        wk &= "FETCH FIRST " & maxRow.ToString & " ROWS ONLY"

        Return ExecuteFill(wk)
    End Function
End Class
