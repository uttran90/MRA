Imports MySql.Data.MySqlClient
Public Class CommonDB
    ''' <summary>
    ''' DBクラスはプレゼンテーション（aspx.vb）からの呼び出しはしないでください
    ''' </summary>
    Private connection As MySqlConnection
    Private transaction As MySqlTransaction

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
    Public Sub Dispose()
        Me.connection.Close()
        Console.WriteLine("DB Connection Closed")
        Me.connection.Dispose()
    End Sub
    Public Sub BeginTransaction()
        Me.transaction = Me.connection.BeginTransaction()
        Console.WriteLine("DB Transaction Begined")
    End Sub
    Public Sub Commit()
        Me.transaction.Commit()
        Console.WriteLine("DB Transaction Commited")
    End Sub
    Public Sub Rollback()
        Me.transaction.Rollback()
        Console.WriteLine("DB Transaction Rollbacked")
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
    Public Function ExecuteScalar(ByVal query As String) As Object
        Dim cmd As MySqlCommand = New MySqlCommand()
        Dim rtn As Object
        Try
            cmd.Connection = Me.connection
            cmd.CommandType = CommandType.Text
            cmd.CommandText = query
            rtn = cmd.ExecuteScalar()

            If rtn Is Nothing Then
                Console.WriteLine("ExecuteScalar Return Nothing : " & query)
            Else
                Console.WriteLine("ExecuteScalar Return " & rtn.ToString & " : " & query)
            End If
        Catch ex As Exception
            Console.WriteLine(ex.Message & " : " & query)
            Throw ex
        Finally
            If Not cmd Is Nothing Then
                cmd.Dispose()
            End If
        End Try
        Return rtn
    End Function
    Public Function ExecuteNonQuery(ByVal query As String) As Integer
        Dim cmd As MySqlCommand = New MySqlCommand
        Dim rtn As Integer = -1
        Try
            cmd.Connection = Me.connection
            cmd.Transaction = Me.transaction
            cmd.CommandType = CommandType.Text
            cmd.CommandText = query
            rtn = cmd.ExecuteNonQuery()

            Console.WriteLine("ExecuteNonQuery Return " & rtn & " : " & query)
        Catch ex As Exception
            Console.WriteLine(ex.Message & " : " & query)
            Throw ex
        Finally
            If Not cmd Is Nothing Then
                cmd.Dispose()
            End If
        End Try
        Return rtn
    End Function

    ''' <summary>
    ''' 文字列をシングルクォーテーションで囲う
    ''' 文字列にシングルクォーテーションがすでに存在する場合はシングルクォーテーション２つに置換する
    ''' </summary>
    ''' <param name="val"></param>
    ''' <returns></returns>
    Public Function EncloseVal(ByVal val As String) As String
        Return "'" & Replace(val, "'", "''") & "'"
    End Function

End Class

