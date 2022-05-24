Imports MySql.Data.MySqlClient
Public Class BL
    Public CommonDB As CommonDB
    Public Sub New()
        MyBase.New
        CommonDB = New CommonDB
    End Sub

    Public Property GRD_DATA As DataGrid

    ''' <summary>
    ''' GET LIST
    ''' </summary>
    ''' <param name="pd"></param>
    ''' <returns></returns>
    Public Function Search(ByRef pd As PageData) As Boolean
        Dim tbl As New DataTable
        Dim sql As String
        Dim strSearch As String = String.Empty
        Dim flag As Boolean
        'init column
        tbl.Columns.Add("menu_id")
        tbl.Columns.Add("menu_nm_vn")
        tbl.Columns.Add("note")

        strSearch = pd.GetItem("TXT_SEARCH").ToString()
        Try
            sql = ""
            sql &= " select mm.menu_id "
            sql &= "    ,mm.menu_nm_vn  "
            sql &= "    ,mm.note    "
            sql &= " from   m_menu mm"
            sql &= " where  mm.del_fg <> '1' "
            If String.IsNullOrEmpty(strSearch) = False OrElse String.IsNullOrWhiteSpace(strSearch) = False Then
                sql &= "    AND mm.menu_nm_vn like '%" & strSearch & "%'"
            End If
            If String.IsNullOrEmpty(strSearch) = False OrElse String.IsNullOrWhiteSpace(strSearch) = False Then
                sql &= "    or mm.note like '%" & strSearch & "%'"
            End If
            sql &= " order  by mm.menu_id asc"
            'tbl = CommonDB.ExecuteFill(sql)
            'If tbl.Rows.Count > 0 Then
            '    flag = True
            'Else
            '    flag = False
            'End If
            'pd.SetItem("GRD_DATA", tbl)
            Return flag
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function Load(ByRef pd As PageData) As Boolean
        Dim tbl As New DataTable
        Dim sql As String
        Dim flag As Boolean
        'init column
        Dim cmd As MySqlCommand
        Dim da As MySqlDataAdapter
        Dim constr As String = ConfigurationManager.ConnectionStrings("MRconstr").ConnectionString
        Dim connection As New MySqlConnection(constr)
        Try
            sql = ""
            sql &= " select mm.menu_id "
            sql &= "    ,mm.menu_nm_vn  "
            sql &= "    ,mm.note    "
            sql &= " from   m_menu mm"
            sql &= " where  mm.del_fg <> '1' "
            sql &= " order  by mm.menu_id asc"
            '
            cmd = New MySqlCommand()
                da = New MySqlDataAdapter
            tbl = New DataTable
            cmd.Connection = connection
                cmd.CommandType = CommandType.Text
            cmd.CommandText = sql
            da.SelectCommand = cmd
            da.Fill(tbl)
            'tbl = CommonDB.ExecuteFill(sql)
            If tbl.Rows.Count > 0 Then
                flag = True
            Else
                flag = False
            End If
            GRD_DATA.DataSource = tbl
                GRD_DATA.DataBind()
                'pd.SetItem("GRD_DATA", tbl)
                Return flag
            Catch ex As Exception
                Throw ex
            End Try
    End Function
End Class

