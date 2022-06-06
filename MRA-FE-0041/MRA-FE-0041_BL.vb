Imports System.Globalization
Imports MRACommon

Public Class Order_BL
    Public CommonDB As CommonDB
    Public Sub New()
        MyBase.New
        CommonDB = New CommonDB
    End Sub
    Public Sub Dispose()
        CommonDB.Dispose()
    End Sub
    Private Enum DayOfMonth
        FIRST_DAY = 1
        LAST_DAY = 2
    End Enum
    'Public Function Search(ByRef strSearch As String, ByRef strDateFrom As String, ByRef strDateTo As String, ByRef strTimeFrom As String, ByRef strTimeTo As String) As DataTable
    Public Function Search(ByRef strSearch As String) As DataTable
        Dim dt As New DataTable
        Dim sql As String
        Try
            sql = ""
            sql &= " select ROW_NUMBER() OVER (ORDER BY tti.table_info_id) AS num"
            sql &= "     ,tti.table_info_id    as table_info_id"
            sql &= "     ,tti.guess_nm         as guess_nm"
            sql &= "     ,tti.guess_count      as guess_count"
            sql &= "     ,tti.guess_phone      as guess_phone"
            sql &= "     ,t_total.count        as product_count"
            sql &= "     ,t_total.total        as total"
            sql &= "     ,DATE_FORMAT(tti.serve_date,'%Y/%m/%d')     as serve_date" 'DATE_FORMAT(SYSDATE(), '%Y%m%d%H%i%s')
            sql &= "     ,TIME_FORMAT(tti.serve_time,'%H:%i:%s')     as serve_time"
            sql &= "     ,tbl.table_nm_vn      as table_nm_vn"
            sql &= "     ,tti.note_tx          as note_tx"
            sql &= " from   t_table_info  tti"
            sql &= "       ,t_table_order tto"
            sql &= "       ,m_table_list  tbl"
            sql &= "       ,m_product     md"
            sql &= "       ,( select  tti2.table_info_id"
            sql &= "                 ,sum(cast(tto2.count as Unsigned)) as count"
            sql &= "                 ,sum(cast(tto2.count as Unsigned) * cast(md1.price as Unsigned)) as total"
            sql &= "            from  m_product md1"
            sql &= "                 ,t_table_order tto2"
            sql &= "                 ,t_table_info  tti2"
            sql &= "           where  md1.product_id = tto2.product_id"
            sql &= "              and tto2.table_info_id = tti2.table_info_id"
            sql &= "         group by tto2.table_info_id"
            sql &= "          ) t_total"
            sql &= " where  tti.del_fg <> '1' "
            sql &= "   and  tto.del_fg <> '1' "
            sql &= "   and  tbl.del_fg <> '1' "
            sql &= "   and  md.del_fg  <> '1' "
            sql &= "    and tti.table_info_id     = tto.table_info_id"
            sql &= "    and tti.table_id          = tbl.table_id"
            sql &= "    and tto.product_id        = md.product_id"
            sql &= "    and t_total.table_info_id = tti.table_info_id"
            If String.IsNullOrEmpty(strSearch) = False OrElse String.IsNullOrWhiteSpace(strSearch) = False Then
                sql &= "    and (tti.guess_nm like '%" & strSearch & "%'"
            End If
            If String.IsNullOrEmpty(strSearch) = False OrElse String.IsNullOrWhiteSpace(strSearch) = False Then
                sql &= "    or tbl.table_nm_vn like '%" & strSearch & "%'"
            End If
            If String.IsNullOrEmpty(strSearch) = False OrElse String.IsNullOrWhiteSpace(strSearch) = False Then
                sql &= "    or tti.note_tx like '%" & strSearch & "%'"
            End If
            If String.IsNullOrEmpty(strSearch) = False OrElse String.IsNullOrWhiteSpace(strSearch) = False Then
                sql &= "    or DATE_FORMAT(tti.serve_date,'%Y/%m/%d') like '%" & strSearch & "%'"
            End If
            If String.IsNullOrEmpty(strSearch) = False OrElse String.IsNullOrWhiteSpace(strSearch) = False Then
                sql &= "    or TIME_FORMAT(tti.serve_time,'%H:%i:%s')  like '%" & strSearch & "%')"
            End If
            ''only from date
            'If strDateFrom <> "" Then
            '    sql &= " and DATE_FORMAT(tti.serve_date,'%Y/%m/%d')  >= TIME_FORMAT(" & CommonDB.EncloseVal(GetDayOfMonth(strDateFrom, DayOfMonth.FIRST_DAY)) & ",'%Y/%m/%d')"
            'End If
            ''only to date
            'If strDateTo <> "" Then
            '    sql &= " and DATE_FORMAT(tti.serve_date,'%Y/%m/%d')  <= TOTIME_FORMAT_DATE(" & CommonDB.EncloseVal(GetDayOfMonth(strDateTo, DayOfMonth.LAST_DAY)) & ",'%Y/%m/%d')"
            'End If
            ''only from hour
            'If strTimeFrom <> "" Then
            '    sql &= " and TIME_FORMAT(tti.serve_time,'%H:%i:%s') >= TIME_FORMAT(" & CommonDB.EncloseVal(GetDayOfMonth(strTimeFrom, DayOfMonth.FIRST_DAY)) & ",'%H:%i:%s')"
            'End If
            ''only to hour
            'If strTimeTo <> "" Then
            '    sql &= " and TIME_FORMAT(tti.serve_time,'%H:%i:%s') <= TIME_FORMAT(" & CommonDB.EncloseVal(GetDayOfMonth(strTimeTo, DayOfMonth.LAST_DAY)) & ",'%H:%i:%s')"
            'End If
            sql &= " group by t_total.table_info_id"
            sql &= " order  by tti.table_info_id asc"
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
            sql &= " select ROW_NUMBER() OVER (ORDER BY tti.table_info_id) AS num"
            sql &= "     ,tti.table_info_id    as table_info_id"
            sql &= "     ,tti.guess_nm         as guess_nm"
            sql &= "     ,tti.guess_count      as guess_count"
            sql &= "     ,tti.guess_phone      as guess_phone"
            sql &= "     ,t_total.count        as product_count"
            sql &= "     ,t_total.total        as total"
            sql &= "     ,DATE_FORMAT(tti.serve_date,'%Y/%m/%d')     as serve_date" 'DATE_FORMAT(SYSDATE(), '%Y%m%d%H%i%s')
            sql &= "     ,TIME_FORMAT(tti.serve_time,'%H:%i:%s')     as serve_time"
            sql &= "     ,tbl.table_nm_vn      as table_nm_vn"
            sql &= "     ,tti.note_tx          as note_tx"
            sql &= " from   t_table_info  tti"
            sql &= "       ,t_table_order tto"
            sql &= "       ,m_table_list  tbl"
            sql &= "       ,m_product     md"
            sql &= "       ,( select  tti2.table_info_id"
            sql &= "                 ,sum(cast(tto2.count as Unsigned)) as count"
            sql &= "                 ,sum(cast(tto2.count as Unsigned) * cast(md1.price as Unsigned)) as total"
            sql &= "            from  m_product md1"
            sql &= "                 ,t_table_order tto2"
            sql &= "                 ,t_table_info tti2"
            sql &= "           where  md1.product_id = tto2.product_id"
            sql &= "              and tto2.table_info_id = tti2.table_info_id"
            sql &= "         group by tto2.table_info_id"
            sql &= "          ) t_total"
            sql &= " where  tti.del_fg <> '1' "
            sql &= "   and  tto.del_fg <> '1' "
            sql &= "   and  tbl.del_fg <> '1' "
            sql &= "   and  md.del_fg  <> '1' "
            sql &= "    and tti.table_info_id = tto.table_info_id"
            sql &= "    and tti.table_id      = tbl.table_id"
            sql &= "    and t_total.table_info_id = tti.table_info_id"
            sql &= " group by t_total.table_info_id"
            sql &= " order  by tti.table_info_id asc"
            dt = CommonDB.ExecuteFill(sql)
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    'get day of month function
    Private Function GetDayOfMonth(ByVal strDate As String, ByVal indexDay As Integer) As String
        Dim finalDate As String = ""
        Dim dtmDate As DateTime
        Try
            dtmDate = DateTime.ParseExact(strDate, "yyyy/MM/dd", CultureInfo.InvariantCulture)
            Select Case indexDay
                Case DayOfMonth.FIRST_DAY
                    finalDate = New DateTime(dtmDate.Year, dtmDate.Month, 1).ToString("yyyy/MM/dd")
                Case DayOfMonth.LAST_DAY
                    dtmDate = New DateTime(dtmDate.Year, dtmDate.Month, 1)
                    finalDate = dtmDate.AddMonths(1).AddDays(-1).ToString("yyyy/MM/dd")
            End Select
            Return finalDate
        Catch ex As Exception
            Throw ex
        End Try
    End Function
End Class

