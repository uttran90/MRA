Imports MRACommon

Public Class Table_BL
    Public CommonDB As CommonDB
    Public Sub New()
        MyBase.New
        CommonDB = New CommonDB
    End Sub
    Public Sub Dispose()
        CommonDB.Dispose()
    End Sub
    ''' <summary>
    ''' GET LIST
    ''' </summary>
    ''' <param name="strSearch"></param>
    ''' <returns></returns>
    Public Function Search(ByRef strSearch As String, ByRef strDateFrom As String, ByRef strDateTo As String) As DataTable
        Dim dt As New DataTable
        Dim sql As String
        Try
            sql = ""
            sql &= " select ROW_NUMBER() OVER (ORDER BY mtl.table_id desc) AS num"
            sql &= "        ,mtl.table_id     as table_id"
            sql &= "        ,mtl.table_nm_vn  as table_nm_vn"
            sql &= "        ,mtl.table_nm_en  as table_nm_en"
            sql &= "        ,mtl.table_nm_jp  as table_nm_jp"
            sql &= "        ,mtl.capacity     as capacity"
            sql &= "        ,mtl.description  as description"
            sql &= "        ,case when tti.is_end = '1' or mh.attr1_tx is null then 'Emptying'"
            sql &= "        else mh.attr1_tx"
            sql &= "        end as table_stt"
            sql &= " from      m_table_list mtl"
            sql &= " left join t_table_info tti"
            sql &= "        on mtl.table_id = tti.table_id"
            sql &= "       And tti.del_fg <> '1' "
            sql &= " left join m_hanyo mh"
            sql &= "        on tti.table_stt_id = mh.hanyo_id and mh.class_id ='S001'"
            sql &= "    and mh.del_fg <> '1' "
            sql &= " where  mtl.del_fg <> '1' "
            If String.IsNullOrEmpty(strSearch) = False OrElse String.IsNullOrWhiteSpace(strSearch) = False Then
                sql &= "    and (mtl.table_nm_vn like '%" & strSearch & "%'"
                sql &= "    or mtl.table_nm_en like '%" & strSearch & "%'"
                sql &= "    or mtl.table_nm_jp like '%" & strSearch & "%'"
                sql &= "    or mh.attr1_tx like '%" & strSearch & "%'"
                sql &= "    or mtl.capacity like '%" & strSearch & "%'"
                sql &= "    or mtl.description like '%" & strSearch & "%')"
            End If
            'only from date
            If strDateFrom <> "" Then
                sql &= " and DATE_FORMAT(tti.serve_date,'%Y-%m-%d')  >= DATE_FORMAT(" & CommonDB.EncloseVal(strDateFrom) & ",'%Y-%m-%d')"
            End If
            'only to date
            If strDateTo <> "" Then
                sql &= " and DATE_FORMAT(tti.serve_date,'%Y-%m-%d')  <= DATE_FORMAT(" & CommonDB.EncloseVal(strDateTo) & ",'%Y-%m-%d')"
            End If
            sql &= " order  by mtl.table_id desc"
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
            sql &= " select ROW_NUMBER() OVER (ORDER BY mtl.table_id desc) AS num"
            sql &= "        ,mtl.table_id     as table_id"
            sql &= "        ,mtl.table_nm_vn  as table_nm_vn"
            sql &= "        ,mtl.table_nm_en  as table_nm_en"
            sql &= "        ,mtl.table_nm_jp  as table_nm_jp"
            sql &= "        ,mtl.capacity     as capacity"
            sql &= "        ,mtl.description  as description"
            sql &= "        ,case when tti.is_end = '1' or mh.attr1_tx is null then 'Emptying'"
            sql &= "        else mh.attr1_tx"
            sql &= "        end as table_stt"
            sql &= " from      m_table_list mtl"
            sql &= " left join t_table_info tti"
            sql &= "        on mtl.table_id = tti.table_id"
            sql &= "       And tti.del_fg <> '1' "
            sql &= " left join m_hanyo mh"
            sql &= "        on tti.table_stt_id = mh.hanyo_id and mh.class_id ='S001'"
            sql &= "    and mh.del_fg <> '1' "
            sql &= " where  mtl.del_fg <> '1' "
            sql &= " order  by mtl.table_id desc"
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

    Public Function updTable(ByVal dicData As Dictionary(Of String, String)) As Boolean
        Dim CommonDB As CommonDB = New CommonDB
        CommonDB.BeginTransaction()
        Try
            Dim sql As String
            sql = ""
            sql &= "UPDATE m_table_list"
            sql &= "   SET table_nm_vn = " & CommonDB.EncloseVal(dicData("nameVN").ToString())
            sql &= "      ,table_nm_jp = " & CommonDB.EncloseVal(dicData("nameJP").ToString())
            sql &= "      ,table_nm_en = " & CommonDB.EncloseVal(dicData("nameEN").ToString())
            sql &= "      ,capacity = " & CommonDB.EncloseVal(dicData("capacity").ToString())
            sql &= "      ,description = " & CommonDB.EncloseVal(dicData("description").ToString())
            sql &= "      ,upd_dt = " & CommonDB.EncloseVal(System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"))
            sql &= "      ,upd_user_id  = 'admin'"
            sql &= "      ,upd_pgm_id   = 'MRA-FE-0051'"
            sql &= " WHERE table_id = " & dicData("table_id")
            If Not CommonDB.ExecuteNonQuery(sql) = 1 Then
                CommonDB.Rollback()
                Return False
            End If
            CommonDB.Commit()
        Catch ex As Exception
            CommonDB.Rollback()
            Throw ex
        Finally
            If Not CommonDB Is Nothing Then
                CommonDB.Dispose()
            End If
        End Try
        Return True
    End Function

    ''' <summary>
    ''' Delete master table
    ''' </summary>
    ''' <param name="table_id"></param>
    ''' <returns></returns>
    Public Function fncDelTable(ByVal table_id As String) As Boolean
        Dim CommonDB As CommonDB = New CommonDB
        CommonDB.BeginTransaction()
        Try
            Dim sql As String
            sql = ""
            sql &= "UPDATE m_table_list"
            sql &= "   SET del_fg = '1'"
            sql &= "      ,upd_dt = " & CommonDB.EncloseVal(System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"))
            sql &= "      ,upd_user_id  = 'admin'"
            sql &= "      ,upd_pgm_id   = 'MRA-FE-0051'"
            sql &= " WHERE table_id = " & table_id
            If Not CommonDB.ExecuteNonQuery(sql) = 1 Then
                CommonDB.Rollback()
                Return False
            End If
            CommonDB.Commit()
        Catch ex As Exception
            Throw ex
        End Try
        Return True
    End Function

    ''' <summary>
    ''' get exist order of  table
    ''' </summary>
    ''' <returns></returns>
    Public Function getCntOrdersByTable(ByVal strTableId As Integer) As Integer
        Dim iCount As Integer = 0
        Try
            Dim sql As String
            sql = ""
            sql &= " select  count(*)"
            sql &= " from      m_table_list mtl"
            sql &= " inner join t_table_info tti"
            sql &= "        on mtl.table_id = tti.table_id"
            sql &= "       And tti.del_fg <> '1' "
            sql &= " inner join t_table_order tto"
            sql &= "        on tto.table_info_id = tti.table_info_id"
            sql &= "    and tto.del_fg <> '1' "
            sql &= " where  mtl.del_fg <> '1' "
            sql &= " and   mtl.table_id =" & strTableId
            iCount = CommonDB.ExecuteScalar(sql)
        Catch ex As Exception
            Throw ex
        End Try
        Return iCount
    End Function
End Class

