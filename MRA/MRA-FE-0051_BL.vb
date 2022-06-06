Imports MySql.Data.MySqlClient

Public Class Table_BL
    Public CommonDB As CommonDB
    Public Sub New()
        MyBase.New
        CommonDB = New CommonDB
    End Sub
    ''' <summary>
    ''' GET LIST
    ''' </summary>
    ''' <param name="strSearch"></param>
    ''' <returns></returns>
    Public Function Search(ByRef strSearch As String) As DataTable
        Dim dt As New DataTable
        Dim sql As String
        Try
            sql = ""
            sql &= " select ROW_NUMBER() OVER (ORDER BY mtl.table_id) AS num"
            sql &= "        ,mtl.table_id"
            sql &= "        ,mtl.table_nm_vn"
            sql &= "        ,mtl.table_nm_jp"
            sql &= "        ,mtl.capacity"
            sql &= "        ,mtl.description"
            sql &= "        ,case when tti.is_end = '1' or mh.attr1_tx is null then 'Emptying'"
            sql &= "        else mh.attr1_tx"
            sql &= "        end as table_stt"
            sql &= " from      m_table_list mtl"
            sql &= " left join t_table_info tti"
            sql &= "        on mtl.table_id = tti.table_id"
            sql &= "       And tti.del_fg <> '1' "
            sql &= " left join m_hanyo mh"
            sql &= "        on tti.table_stt_id = mh.hanyo_id"
            sql &= "    and mh.del_fg <> '1' "
            sql &= " where  mtl.del_fg <> '1' "
            If String.IsNullOrEmpty(strSearch) = False OrElse String.IsNullOrWhiteSpace(strSearch) = False Then
                sql &= "    and mtl.table_nm_vn like '%" & strSearch & "%'"
            End If
            If String.IsNullOrEmpty(strSearch) = False OrElse String.IsNullOrWhiteSpace(strSearch) = False Then
                sql &= "    or mtl.table_nm_jp like '%" & strSearch & "%'"
            End If
            If String.IsNullOrEmpty(strSearch) = False OrElse String.IsNullOrWhiteSpace(strSearch) = False Then
                sql &= "    or mtl.description like '%" & strSearch & "%'"
            End If
            sql &= " order  by mtl.table_id asc"
            dt = CommonDB.ExecuteFill(sql)
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function Load() As DataTable
        Dim dt = New DataTable
        Dim sql As String
        Try
            sql = ""
            sql &= " select ROW_NUMBER() OVER (ORDER BY mtl.table_id) AS num"
            sql &= "        ,mtl.table_id"
            sql &= "        ,mtl.table_nm_vn"
            sql &= "        ,mtl.table_nm_jp"
            sql &= "        ,mtl.capacity"
            sql &= "        ,mtl.description"
            sql &= "        ,case when tti.is_end = '1' or mh.attr1_tx is null then 'Emptying'"
            sql &= "        else mh.attr1_tx"
            sql &= "        end as table_stt"
            sql &= " from      m_table_list mtl"
            sql &= " left join t_table_info tti"
            sql &= "        on mtl.table_id = tti.table_id"
            sql &= "       And tti.del_fg <> '1' "
            sql &= " left join m_hanyo mh"
            sql &= "        on tti.table_stt_id = mh.hanyo_id"
            sql &= "    and mh.del_fg <> '1' "
            sql &= " where  mtl.del_fg <> '1' "
            sql &= " order  by mtl.table_id asc"
            dt = CommonDB.ExecuteFill(sql)
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function getSeq(tbl As String) As Integer
        Dim sqll As String
        Try
            sqll = ""
            sqll &= " SELECT AUTO_INCREMENT -1 "
            sqll &= " FROM   information_schema.tables"
            sqll &= " WHERE table_name   = " & CommonDB.EncloseVal(tbl)
            sqll &= "   and table_schema = 'meo09965_MR'"
            Return CommonDB.ExecuteScalar(sqll)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
End Class

