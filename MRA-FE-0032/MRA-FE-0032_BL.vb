Imports MRACommon
Public Class ProductDetail_BL
    Public CommonDB As CommonDB
    Public Sub New()
        MyBase.New
        CommonDB = New CommonDB
    End Sub
    Public Sub Dispose()
        CommonDB.Dispose()
    End Sub
    ''' <summary>
    ''' GetProductInfo
    ''' </summary>
    ''' <returns>DataTable</returns>
    Public Function GetProductInfo(ByRef strProductId As String) As DataTable
        Dim dt As New DataTable
        Dim sql As String
        Try
            sql = ""
            sql &= " select mp.product_id       as product_id"
            sql &= "       ,temp.menu_id        as menu_id"
            sql &= "       ,temp.menu_nm_jp     as menu_nm_jp"
            sql &= "       ,mp.product_nm_vn    as product_nm_vn"
            sql &= "       ,mp.product_nm_jp    as product_nm_jp"
            sql &= "       ,mp.product_nm_en    as product_nm_en"
            sql &= "       ,mp.product_nm_off   as product_nm_off"
            sql &= "       ,mp.price            as price"
            sql &= "       ,mp.price_show       as price_show"
            sql &= "       ,mp.discount_p       as discount_p"
            sql &= "       ,mp.product_stt_id   as product_stt"
            sql &= "       ,mp.description      as description"
            sql &= "       ,mp.product_avatar   as product_avatar"
            sql &= "       ,mp.product_note     as note"
            sql &= " from m_product mp"
            sql &= " left join ( select    mm.menu_nm_jp, mp1.product_id, mp1.menu_id"
            sql &= "             from      m_product mp1"
            sql &= "             left join m_menu mm "
            sql &= "             on        mm.menu_id = mp1.menu_id "
            sql &= "             and       mm.del_fg <> '1' ) temp"
            sql &= " on    temp.product_id = mp.product_id"
            sql &= " where mp.del_fg <> '1' "
            If String.IsNullOrEmpty(strProductId) = False OrElse String.IsNullOrWhiteSpace(strProductId) = False Then
                sql &= "    and mp.product_id = " & strProductId
            End If
            dt = CommonDB.ExecuteFill(sql)
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    ''' <summary>
    ''' GetProductOptChosen
    ''' </summary>
    ''' <returns>DataTable</returns>
    Public Function GetProductOptChosen(ByRef strProductId As String) As DataTable
        Dim dt As New DataTable
        Dim sql As String
        Try
            sql = ""
            sql &= " select ROW_NUMBER() OVER (ORDER BY mpo.product_opt_id) AS num"
            sql &= "       ,mpo.product_opt_id       as product_opt_id"
            sql &= "       ,mpo.product_opt_nm       as product_opt_nm"
            sql &= "       ,mpo.product_opt_price    as product_opt_price"
            sql &= "       ,mpo.note                 as note"
            sql &= "       ,mp.product_id            as product_id"
            sql &= " from   m_product mp"
            sql &= "       ,m_product_opt mpo"
            sql &= " where  mp.del_fg  <> '1' "
            sql &= "    and mpo.del_fg <> '1'"
            sql &= "    and mp.product_id = mpo.product_id"
            If String.IsNullOrEmpty(strProductId) = False OrElse String.IsNullOrWhiteSpace(strProductId) = False Then
                sql &= "    and mp.product_id = " & strProductId
            End If
            sql &= " order by mpo.product_opt_id asc"
            dt = CommonDB.ExecuteFill(sql)
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    ''' <summary>
    ''' GetListProductOpt
    ''' </summary>
    ''' <returns>DataTable</returns>
    Public Function GetProductOpt() As DataTable
        Dim dt As New DataTable
        Dim sql As String
        Try
            sql = ""
            sql &= " select mpo.product_opt_id    as product_opt_id"
            sql &= "       ,mpo.product_opt_nm    as product_opt_nm"
            sql &= "       ,mpo.product_opt_price as product_opt_price"
            sql &= "       ,mpo.note              as note"
            sql &= "  from  m_product_opt mpo"
            sql &= " where  mpo.del_fg <> '1'"
            sql &= " order by mpo.product_opt_id asc"
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

    Public Function fncDelete(ByVal strProductId As String) As Boolean
        Dim CommonDB As CommonDB = New CommonDB
        Try
            CommonDB.BeginTransaction()
            Dim sql As String
            sql = ""
            sql &= "UPDATE m_product"
            sql &= "   SET del_fg = '1'"
            sql &= "      ,upd_dt = " & CommonDB.EncloseVal(System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"))
            sql &= "      ,upd_user_id  = 'admin'"
            sql &= "      ,upd_pgm_id   = 'MRA-FE-0032'"
            sql &= " WHERE product_id = " & CommonDB.EncloseVal(strProductId)
            If Not CommonDB.ExecuteNonQuery(sql) = 1 Then
                CommonDB.Rollback()
            End If
            'delete product option
            If Not fncDeleteOpt(strProductId, CommonDB) Then
                CommonDB.Rollback()
                Return False
            End If
            CommonDB.Commit()
            Return True
        Catch ex As Exception
            CommonDB.Rollback()
            Throw ex
        Finally
            If Not CommonDB Is Nothing Then
                CommonDB.Dispose()
            End If
        End Try
    End Function

    Private Function fncDeleteOpt(ByVal strProductId As String, ByRef CommonDB As CommonDB) As Boolean
        Try
            Dim sql As String
            sql = ""
            sql &= "UPDATE m_product_opt"
            sql &= "   SET del_fg = '1'"
            sql &= "      ,upd_dt = " & CommonDB.EncloseVal(System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"))
            sql &= "      ,upd_user_id  = 'admin'"
            sql &= "      ,upd_pgm_id   = 'MRA-FE-0022'"
            sql &= " WHERE product_opt_id in (select proopt.product_opt_id from m_product_opt proopt where proopt.product_id = " & CommonDB.EncloseVal(strProductId) & ")"
            If Not CommonDB.ExecuteNonQuery(sql) = 1 Then
                Return False
            End If
            Return True
        Catch ex As Exception
            Throw ex
        Finally
            If Not CommonDB Is Nothing Then
                CommonDB.Dispose()
            End If
        End Try
    End Function
End Class

