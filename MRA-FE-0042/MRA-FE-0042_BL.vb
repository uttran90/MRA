Imports MRACommon

Public Class OrderDetail_BL
    Public CommonDB As CommonDB
    Public Sub New()
        MyBase.New
        CommonDB = New CommonDB
    End Sub
    ''' <summary>
    ''' GetListProduct
    ''' </summary>
    ''' <returns>DataTable</returns>
    Public Function GetOrderInfo(ByRef strOrderId As String) As DataTable
        Dim dt As New DataTable
        Dim sql As String
        Try
            sql = ""
            sql &= " select ROW_NUMBER() OVER (ORDER BY tto.product_id) AS num"
            sql &= "     ,md.product_id                              as product_id"
            sql &= "     ,md.product_nm_vn                           as product_nm_vn"
            sql &= "     ,cast(tto.count  as Unsigned)               as product_count"
            sql &= "     ,md.price                                   as price"
            sql &= "     ,''                                         as product_opt_nm"
            sql &= "     ,''                                         as product_opt_count"
            sql &= "     ,''                                         as product_opt_price"
            sql &= "     ,DATE_FORMAT(tti.serve_date,'%Y/%m/%d')     as serve_date" 'DATE_FORMAT(SYSDATE(), '%Y%m%d%H%i%s')
            sql &= "     ,tbl.table_nm_vn      as table_nm_vn"
            sql &= "      ,case when tti.is_end = '1' then 'Paid'"
            sql &= "            else 'Serving'"
            sql &= "        end as table_stt"
            sql &= "     ,tto.note_tx          as note_tx"
            sql &= " from   t_table_info      tti"
            sql &= "       ,t_table_order     tto"
            sql &= "       ,m_table_list      tbl"
            sql &= "       ,m_product         md"
            sql &= " where  tti.del_fg     <> '1' "
            sql &= "   and  tto.del_fg     <> '1' "
            sql &= "   and  tbl.del_fg     <> '1' "
            sql &= "   and  md.del_fg      <> '1' "
            sql &= "    and tti.table_info_id    = tto.table_info_id"
            sql &= "    and tti.table_id         = tbl.table_id"
            sql &= "    and tto.product_id       = md.product_id"
            If String.IsNullOrEmpty(strOrderId) = False OrElse String.IsNullOrWhiteSpace(strOrderId) = False Then
                sql &= "    and tti.table_info_id = '" & strOrderId & "'"
            End If
            sql &= " order  by md.product_id asc"
            dt = CommonDB.ExecuteFill(sql)
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function

End Class

