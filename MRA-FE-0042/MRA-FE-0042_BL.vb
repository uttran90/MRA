Imports MRACommon

Public Class OrderDetail_BL
    Public CommonDB As CommonDB
    Public Sub New()
        MyBase.New
        CommonDB = New CommonDB
    End Sub
    Public Sub Dispose()
        CommonDB.Dispose()
    End Sub
    ''' <summary>
    ''' GetOrderInfo
    ''' </summary>
    ''' <returns>DataTable</returns>
    Public Function GetOrderInfo(ByRef strOrderId As String) As DataTable
        Dim dt As New DataTable
        Dim sql As String
        Try
            sql = ""
            sql &= " select ROW_NUMBER() OVER (ORDER BY tto.product_id) AS num"
            sql &= "     ,tto.table_order_id                         as table_order_id"
            sql &= "     ,md.product_id                              as product_id"
            sql &= "     ,md.product_nm_vn                           as product_nm_vn"
            sql &= "     ,cast(tto.count  as Unsigned)               as product_count"
            sql &= "     ,cast(md.price as Unsigned)                 as price"
            sql &= "     ,temp.product_opt_id                        as product_opt_id"
            sql &= "     ,temp.product_opt_nm                        as product_opt_nm"
            sql &= "     ,cast(temp.opt_count  as Unsigned)          as product_opt_count"
            sql &= "     ,cast(temp.product_opt_price as Unsigned)   as product_opt_price"
            sql &= "     ,DATE_FORMAT(tti.serve_date,'%Y-%m-%d')     as serve_date" 'DATE_FORMAT(SYSDATE(), '%Y%m%d%H%i%s')
            sql &= "     ,tbl.table_nm_vn      as table_nm_vn"
            sql &= "     ,tto.note_tx          as note_tx"
            sql &= " from   t_table_info      tti"
            sql &= "       ,t_table_order     tto"
            sql &= "       ,m_table_list      tbl"
            sql &= "       ,m_product         md"
            sql &= "       ,( select    tto1.table_order_id"
            sql &= "                    ,tto1.opt_count"
            sql &= "                    ,tto1.product_opt_id"
            sql &= "                    ,mpo.product_opt_nm"
            sql &= "                    ,mpo.product_opt_price"
            sql &= "           from     t_table_order     tto1"
            sql &= "          left join m_product_opt     mpo "
            sql &= "                on  tto1.product_opt_id = mpo.product_opt_id "
            sql &= "                and  mpo.del_fg <> '1' ) temp"
            sql &= " where  tti.del_fg     <> '1' "
            sql &= "   and  tto.del_fg     <> '1' "
            sql &= "   and  tbl.del_fg     <> '1' "
            sql &= "   and  md.del_fg      <> '1' "
            sql &= "   and  md.del_fg      <> '1' "
            sql &= "   and tti.table_info_id    = tto.table_info_id"
            sql &= "   and tti.table_id         = tbl.table_id"
            sql &= "   and tto.product_id       = md.product_id"
            sql &= "   and tto.table_order_id       = temp.table_order_id"
            If String.IsNullOrEmpty(strOrderId) = False OrElse String.IsNullOrWhiteSpace(strOrderId) = False Then
                sql &= "    and tti.table_info_id = " & strOrderId
            End If
            sql &= " order  by md.product_id asc"
            dt = CommonDB.ExecuteFill(sql)
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    ''' <summary>
    ''' GetFullProduct
    ''' </summary>
    ''' <returns>DataTable</returns>
    Public Function GetListProduct() As DataTable
        Dim dt As New DataTable
        Dim sql As String
        Try
            sql = ""
            sql &= " select mp.product_id     as product_id"
            sql &= "       ,mp.product_nm_vn  as product_nm_vn"
            sql &= " from  m_product         mp"
            sql &= " where    md.del_fg      <> '1' "
            sql &= " order  by mp.product_id asc"
            dt = CommonDB.ExecuteFill(sql)
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    ''' <summary>
    ''' GetFullOPt
    ''' </summary>
    ''' <returns>DataTable</returns>
    Public Function GetListOPt() As DataTable
        Dim dt As New DataTable
        Dim sql As String
        Try
            sql = ""
            sql &= " select mpo.product_opt_id as product_opt_id"
            sql &= "       ,mpo.product_opt_nm as product_opt_nm"
            sql &= " from  m_product_opt         mpo"
            sql &= " where    mpo.del_fg      <> '1' "
            sql &= " order  by mpo.product_id asc"
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
    Public Function GetProduct(strProductID As String) As DataTable
        Dim dt As New DataTable
        Dim sql As String
        Try
            sql = ""
            sql &= " select count(*) as count"
            sql &= " from  m_product         mp"
            sql &= " where    mp.product_id  = " & strProductID
            sql &= "   and    mp.del_fg      <> '1' "
            dt = CommonDB.ExecuteFill(sql)
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function GetProductOpt(strProductOptID As String) As DataTable
        Dim dt As New DataTable
        Dim sql As String
        Try
            sql = ""
            sql &= " select count(*) as count"
            sql &= " from  m_product         mp"
            sql &= " where    mp.product_id  = " & strProductOptID
            sql &= "   and    mp.del_fg      <> '1' "
            dt = CommonDB.ExecuteFill(sql)
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function
End Class

