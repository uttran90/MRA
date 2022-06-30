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
    Public Function Search(ByRef strSearch As String, ByRef strDateFrom As String, ByRef strDateTo As String, ByRef strTimeFrom As String, ByRef strTimeTo As String) As DataTable
        Dim dt As New DataTable
        Dim sql As String
        Try
            sql = ""
            sql &= " select a.num"
            sql &= " ,a.table_info_id"
            sql &= " ,a.guess_nm"
            sql &= " ,a.guess_count"
            sql &= " ,a.guess_phone"
            sql &= " ,a.product_count"
            sql &= " ,case when ttr.VAT is null then a.total"
            sql &= "  else a.total + a.total * (ttr.VAT/100) "
            sql &= "  end as total"
            sql &= " ,a.serve_date"
            sql &= " ,a.serve_time"
            sql &= " ,ttr.VAT as tax"
            sql &= " ,a.table_nm_vn"
            sql &= " ,a.note_tx"
            sql &= " from (select ROW_NUMBER() OVER (ORDER BY tti.table_info_id) AS num"
            sql &= "         ,tti.table_info_id    as table_info_id"
            sql &= "         ,tti.guess_nm         as guess_nm"
            sql &= "         ,tti.guess_count      as guess_count"
            sql &= "         ,tti.guess_phone      as guess_phone"
            sql &= "         ,t_total.count        as product_count"
            sql &= "         ,t_total.total        as total"
            sql &= "         ,DATE_FORMAT(tti.serve_date,'%Y-%m-%d')     as serve_date" 'DATE_FORMAT(SYSDATE(), '%Y%m%d%H%i%s')
            sql &= "         ,TIME_FORMAT(tti.serve_time,'%H:%i:%s')     as serve_time"
            sql &= "         ,tbl.table_nm_vn      as table_nm_vn"
            sql &= "         ,tti.note_tx          as note_tx"
            sql &= "         from   t_table_info  tti"
            sql &= "               ,t_table_order tto"
            sql &= "               ,m_table_list  tbl"
            sql &= "               ,m_product     md"
            sql &= "               ,( select  tti2.table_info_id"
            sql &= "                         ,sum(cast(tto2.count as Unsigned)) as count"
            sql &= "                         ,sum(cast(tto2.count as Unsigned) * cast(md1.price_show as Unsigned)) as total_p"
            sql &= "                         ,sum(cast(temp.opt_count as Unsigned) * cast(temp.product_opt_price as Unsigned)) as total_o"
            sql &= "                         ,case when temp.opt_count is null or temp.product_opt_price is null then"
            sql &= "                               sum(cast(tto2.count as Unsigned) * cast(md1.price as Unsigned))"
            sql &= "                          else sum(cast(tto2.count as Unsigned) * cast(md1.price as Unsigned)) + sum(cast(temp.opt_count as Unsigned) * cast(temp.product_opt_price as Unsigned)) end as total"
            sql &= "                    from  m_product md1"
            sql &= "                         ,t_table_order tto2"
            sql &= "                         ,t_table_info  tti2"
            sql &= "                         ,( select    tto3.table_order_id"
            sql &= "                                      ,tto3.opt_count"
            sql &= "                                      ,tto3.product_opt_id"
            sql &= "                                      ,mpo.product_opt_nm"
            sql &= "                                      ,mpo.product_opt_price"
            sql &= "                             from     t_table_order     tto3"
            sql &= "                            left join m_product_opt     mpo "
            sql &= "                                  on  tto3.product_opt_id  = mpo.product_opt_id "
            sql &= "                                  and  mpo.del_fg <> '1' ) temp"
            sql &= "                   where  md1.product_id       = tto2.product_id"
            sql &= "                      and tto2.table_info_id   = tti2.table_info_id"
            sql &= "                      and  temp.table_order_id = tto2.table_order_id "
            sql &= "                   group by tto2.table_info_id"
            sql &= "                  ) t_total"
            sql &= "         where  tti.del_fg <> '1' "
            sql &= "           and  tto.del_fg <> '1' "
            sql &= "           and  tbl.del_fg <> '1' "
            sql &= "           and  md.del_fg  <> '1' "
            sql &= "           and tti.table_info_id     = tto.table_info_id"
            sql &= "           and tti.table_id          = tbl.table_id"
            sql &= "           and tto.product_id        = md.product_id"
            sql &= "           and t_total.table_info_id = tti.table_info_id"
            If String.IsNullOrEmpty(strSearch) = False OrElse String.IsNullOrWhiteSpace(strSearch) = False Then
                sql &= "    and (tti.guess_nm like '%" & strSearch & "%'"
            End If
            'table name vn
            If String.IsNullOrEmpty(strSearch) = False OrElse String.IsNullOrWhiteSpace(strSearch) = False Then
                sql &= "    or tbl.table_nm_vn like '%" & strSearch & "%'"
            End If
            'table name jp
            If String.IsNullOrEmpty(strSearch) = False OrElse String.IsNullOrWhiteSpace(strSearch) = False Then
                sql &= "    or tbl.table_nm_jp like '%" & strSearch & "%'"
            End If
            'table name en
            If String.IsNullOrEmpty(strSearch) = False OrElse String.IsNullOrWhiteSpace(strSearch) = False Then
                sql &= "    or tbl.table_nm_en like '%" & strSearch & "%'"
            End If
            'table name vn
            If String.IsNullOrEmpty(strSearch) = False OrElse String.IsNullOrWhiteSpace(strSearch) = False Then
                sql &= "    or md.product_nm_vn like '%" & strSearch & "%'"
            End If
            'table name en
            If String.IsNullOrEmpty(strSearch) = False OrElse String.IsNullOrWhiteSpace(strSearch) = False Then
                sql &= "    or md.product_nm_en like '%" & strSearch & "%'"
            End If
            'table name jp
            If String.IsNullOrEmpty(strSearch) = False OrElse String.IsNullOrWhiteSpace(strSearch) = False Then
                sql &= "    or md.product_nm_jp like '%" & strSearch & "%'"
            End If
            If String.IsNullOrEmpty(strSearch) = False OrElse String.IsNullOrWhiteSpace(strSearch) = False Then
                sql &= "    or tti.note_tx like '%" & strSearch & "%')"
            End If

            'only from date
            If strDateFrom <> "" Then
                sql &= " and DATE_FORMAT(tti.serve_date,'%Y-%m-%d')  >= DATE_FORMAT(" & CommonDB.EncloseVal(strDateFrom) & ",'%Y-%m-%d')"
            End If
            'only to date
            If strDateTo <> "" Then
                sql &= " and DATE_FORMAT(tti.serve_date,'%Y-%m-%d')  <= DATE_FORMAT(" & CommonDB.EncloseVal(strDateTo) & ",'%Y-%m-%d')"
            End If
            'only from hour
            If strTimeFrom <> "" Then
                sql &= " and TIME_FORMAT(tti.serve_time,'%H:%i:%s') >= TIME_FORMAT(" & CommonDB.EncloseVal(strTimeFrom) & ",'%H:%i:%s')"
            End If
            'only to hour
            If strTimeTo <> "" Then
                sql &= " and TIME_FORMAT(tti.serve_time,'%H:%i:%s') <= TIME_FORMAT(" & CommonDB.EncloseVal(strTimeTo) & ",'%H:%i:%s')"
            End If
            sql &= " group by t_total.table_info_id"
            sql &= " order by tti.table_info_id asc ) a"
            sql &= " left join t_table_receipt ttr"
            sql &= "        on ttr.table_info_id = a.table_info_id "
            sql &= "       and ttr.del_fg  <> '1' "
            sql &= " group by a.num"
            sql &= " order by a.num asc"
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
            sql &= " select a.num"
            sql &= " ,a.table_info_id"
            sql &= " ,a.guess_nm"
            sql &= " ,a.guess_count"
            sql &= " ,a.guess_phone"
            sql &= " ,a.product_count"
            sql &= " ,case when ttr.VAT is null then a.total"
            sql &= "  else a.total + a.total * (ttr.VAT/100) "
            sql &= "  end as total"
            sql &= " ,a.serve_date"
            sql &= " ,a.serve_time"
            sql &= " ,ttr.VAT as tax"
            sql &= " ,a.table_nm_vn"
            sql &= " ,a.note_tx"
            sql &= " from (select ROW_NUMBER() OVER (ORDER BY tti.table_info_id) AS num"
            sql &= "     ,tti.table_info_id    as table_info_id"
            sql &= "     ,tti.guess_nm         as guess_nm"
            sql &= "     ,tti.guess_count      as guess_count"
            sql &= "     ,tti.guess_phone      as guess_phone"
            sql &= "     ,t_total.count        as product_count"
            sql &= "     ,t_total.total        as total"
            sql &= "     ,DATE_FORMAT(tti.serve_date,'%Y-%m-%d')     as serve_date" 'DATE_FORMAT(SYSDATE(), '%Y%m%d%H%i%s')
            sql &= "     ,TIME_FORMAT(tti.serve_time,'%H:%i:%s')     as serve_time"
            sql &= "     ,tbl.table_nm_vn      as table_nm_vn"
            sql &= "     ,tti.note_tx          as note_tx"
            sql &= " from   t_table_info  tti"
            sql &= "       ,t_table_order tto"
            sql &= "       ,m_table_list  tbl"
            sql &= "       ,m_product     md"
            sql &= "       ,( select  tti2.table_info_id"
            sql &= "                 ,sum(cast(tto2.count as Unsigned)) as count"
            sql &= "                 ,sum(cast(tto2.count as Unsigned) * cast(md1.price as Unsigned)) as total_p"
            sql &= "                 ,sum(cast(temp.opt_count as Unsigned) * cast(temp.product_opt_price as Unsigned)) as total_o"
            sql &= "                 ,case when temp.opt_count is null or temp.product_opt_price is null then"
            sql &= "                       sum(cast(tto2.count as Unsigned) * cast(md1.price as Unsigned))"
            sql &= "                  else sum(cast(tto2.count as Unsigned) * cast(md1.price as Unsigned)) + sum(cast(temp.opt_count as Unsigned) * cast(temp.product_opt_price as Unsigned)) end as total"
            sql &= "            from  m_product md1"
            sql &= "                 ,t_table_order tto2"
            sql &= "                 ,t_table_info  tti2"
            sql &= "                 ,( select    tto3.table_order_id"
            sql &= "                              ,tto3.opt_count"
            sql &= "                              ,tto3.product_opt_id"
            sql &= "                              ,mpo.product_opt_nm"
            sql &= "                              ,mpo.product_opt_price"
            sql &= "                     from     t_table_order     tto3"
            sql &= "                    left join m_product_opt     mpo "
            sql &= "                          on  tto3.product_opt_id  =  mpo.product_opt_id "
            sql &= "                          and  mpo.del_fg          <> '1' ) temp"
            sql &= "           where  md1.product_id       = tto2.product_id"
            sql &= "             and  tto2.table_info_id   = tti2.table_info_id"
            sql &= "             and  temp.table_order_id  = tto2.table_order_id "
            sql &= "           group by tto2.table_info_id"
            sql &= "          ) t_total"
            sql &= " where  tti.del_fg <> '1' "
            sql &= "   and  tto.del_fg <> '1' "
            sql &= "   and  tbl.del_fg <> '1' "
            sql &= "   and  md.del_fg  <> '1' "
            sql &= "   and tti.table_info_id     = tto.table_info_id"
            sql &= "   and tti.table_id          = tbl.table_id"
            sql &= "   and t_total.table_info_id = tti.table_info_id"
            sql &= " group by t_total.table_info_id"
            sql &= " order by tti.table_info_id asc ) a"
            sql &= " left join t_table_receipt ttr"
            sql &= "        on ttr.table_info_id = a.table_info_id "
            sql &= "       and ttr.del_fg  <> '1' "
            sql &= " group by a.num"
            sql &= " order by a.num asc"
            dt = CommonDB.ExecuteFill(sql)
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Function GetCapacityTableOrder(ByRef strOrderID As String) As DataTable
        Dim dt = New DataTable
        Dim sql As String
        Try
            sql = ""
            sql &= " select mtl.capacity as capacity"
            sql &= "   from t_table_info tti"
            sql &= "       ,m_table_list mtl"
            sql &= " where mtl.table_id = tti.table_id"
            sql &= "   and tti.table_info_id =" & strOrderID

            dt = CommonDB.ExecuteFill(sql)
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function fncUpdTableInfo(ByVal dicData As Dictionary(Of String, String), ByRef CommonDB As CommonDB) As Boolean
        Try
            Dim sql As String
            sql = ""
            sql &= "UPDATE t_table_info"
            sql &= "   SET guess_nm = " & CommonDB.EncloseVal(dicData("cus_nm").ToString())
            If Not String.IsNullOrEmpty(dicData("cus_cnt").ToString()) AndAlso Convert.ToInt32(dicData("cus_cnt").ToString()) > 0 Then
                sql &= "      ,guess_count = " & CommonDB.EncloseVal(dicData("cus_cnt").ToString())
            Else
                sql &= "      ,guess_count = ''"
            End If
            sql &= "      ,guess_phone = " & CommonDB.EncloseVal(dicData("cus_phone").ToString())
            sql &= "      ,serve_date = DATE_FORMAT(" & CommonDB.EncloseVal(dicData("date_o").ToString()) & ",'%Y-%m-%d')"
            sql &= "      ,serve_time = TIME_FORMAT(" & CommonDB.EncloseVal(dicData("time_o").ToString()) & ",'%H:%i:%s')"
            sql &= "      ,note_tx = " & CommonDB.EncloseVal(dicData("note").ToString())
            sql &= "      ,upd_dt = " & CommonDB.EncloseVal(System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"))
            sql &= "      ,upd_user_id  = 'admin'"
            sql &= "      ,upd_pgm_id   = 'MRA-FE-0041'"
            sql &= " WHERE table_info_id = " & dicData("strTableInfoId").ToString()
            If Not CommonDB.ExecuteNonQuery(sql) = 1 Then
                Return False
            End If
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function fncDelTableInfo(ByVal strTableInfoId As String, ByRef CommonDB As CommonDB) As Boolean
        Try
            Dim sql As String
            sql = ""
            sql &= "UPDATE t_table_info"
            sql &= "   SET del_fg = '1'"
            sql &= "      ,upd_dt = " & CommonDB.EncloseVal(System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"))
            sql &= "      ,upd_user_id  = 'admin'"
            sql &= "      ,upd_pgm_id   = 'MRA-FE-0041'"
            sql &= " WHERE table_info_id = " & strTableInfoId
            If Not CommonDB.ExecuteNonQuery(sql) = 1 Then
                Return False
            End If
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function fncDelTableOrder(ByVal strTableInfoId As String, ByRef CommonDB As CommonDB) As Boolean
        Try
            Dim strOrderId As String = getKeyOrder(strTableInfoId, CommonDB)
            Dim sql As String
            sql = ""
            sql &= " UPDATE t_table_order"
            sql &= "   Set del_fg = '1'"
            sql &= "      ,upd_dt = " & CommonDB.EncloseVal(System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"))
            sql &= "      ,upd_user_id  = 'admin'"
            sql &= "      ,upd_pgm_id   = 'MRA-FE-0041'"
            sql &= " WHERE table_order_id in (" & strOrderId & ")"
            If Not CommonDB.ExecuteNonQuery(sql) = 1 Then
                Return False
            End If
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function fncDelTableReceipt(ByVal strTableInfoId As String, ByRef CommonDB As CommonDB) As Boolean
        Try
            Dim strReceiptId As String = getKeyReceipt(strTableInfoId, CommonDB)
            Dim sql As String
            sql = ""
            sql &= "UPDATE t_table_receipt"
            sql &= "   SET del_fg = '1'"
            sql &= "      ,upd_dt = " & CommonDB.EncloseVal(System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"))
            sql &= "      ,upd_user_id  = 'admin'"
            sql &= "      ,upd_pgm_id   = 'MRA-FE-0041'"
            sql &= " WHERE table_receipt_id in (" & strReceiptId & ")"
            If Not CommonDB.ExecuteNonQuery(sql) = 1 Then
                Return False
            End If
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Function getKeyOrder(ByVal strTableInfoId As String, ByRef commonDB As CommonDB) As String
        Try
            Dim strOrderId As String = ""
            Dim dtkey As DataTable = New DataTable
            Dim sql As String
            sql = ""
            sql &= " select table_order_id from t_table_order where table_info_id = " & strTableInfoId
            dtkey = commonDB.ExecuteFill(sql)
            For Each row As DataRow In dtkey.Rows
                If strOrderId = "" Then
                    strOrderId = row("table_order_id").ToString()
                Else
                    strOrderId = "," & row("table_order_id").ToString()
                End If
            Next
            Return strOrderId
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Function getKeyReceipt(ByVal strTableInfoId As String, ByRef commonDB As CommonDB) As String
        Try
            Dim strReceiptId As String = ""
            Dim dtkey As DataTable = New DataTable
            Dim sql As String
            sql = ""
            sql &= " select table_receipt_id from t_table_receipt where table_info_id =" & strTableInfoId
            dtkey = commonDB.ExecuteFill(sql)
            For Each row As DataRow In dtkey.Rows
                If strReceiptId = "" Then
                    strReceiptId = row("table_receipt_id").ToString()
                Else
                    strReceiptId = "," & row("table_receipt_id").ToString()
                End If
            Next
            Return strReceiptId
        Catch ex As Exception
            Throw ex
        End Try
    End Function
End Class

