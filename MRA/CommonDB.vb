Imports MySql.Data.MySqlClient
Public Class CommonDB
    ''' <summary>
    ''' DBクラスはプレゼンテーション（aspx.vb）からの呼び出しはしないでください
    ''' </summary>
    Private connection As MySqlConnection

    Public Sub New()
        Dim constr As String = ConfigurationManager.ConnectionStrings("MRconstr").ConnectionString
        Me.connection = New MySqlConnection(constr)
        Try
            Me.connection.Open()
            Trace.Write("DB Connection Succeeded. ")
        Catch ex As Exception
            Console.WriteLine("DB Connect Open Failed : " & ex.Message)
            Throw ex
        End Try
    End Sub

    Public Function ExecuteFill(ByVal query As String) As DataTable
        Dim cmd As MySqlCommand = New MySqlCommand()
        Dim da As MySqlDataAdapter = New MySqlDataAdapter
        Dim dt As DataTable = New DataTable
        Try
            cmd.Connection = Me.connection
            cmd.CommandType = CommandType.Text
            cmd.CommandText = query
            da.SelectCommand = cmd
            da.Fill(dt)
            Console.WriteLine("ExecuteFill Return Rows " & dt.Rows.Count & " : " & query)
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
        Return dt
    End Function
End Class
