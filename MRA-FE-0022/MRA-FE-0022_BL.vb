Imports MRACommon
Imports MRACommon.CommonUtil
Public Class MenuDetail_BL
    Public CommonDB As CommonDB
    Public Sub New()
        MyBase.New
        CommonDB = New CommonDB
    End Sub
    Public Sub Dispose()
        CommonDB.Dispose()
    End Sub
    ''' <summary>
    ''' GetListProduct
    ''' </summary>
    ''' <returns>DataTable</returns>
    Public Function GetMenuInfo(ByRef strMenuId As String) As DataTable
        Dim dt As New DataTable
        Dim sql As String
        Try
            sql = ""
            sql &= " select mm.menu_nm_vn"
            sql &= "       ,mm.menu_nm_jp"
            sql &= "       ,mm.menu_nm_en"
            sql &= "       ,mm.note"
            sql &= "       ,mm.menu_img"
            sql &= " from   m_menu mm"
            sql &= " where  mm.del_fg <> '1' "
            If String.IsNullOrEmpty(strMenuId) = False OrElse String.IsNullOrWhiteSpace(strMenuId) = False Then
                sql &= "    and mm.menu_id = " & strMenuId
            End If
            sql &= " order  by mm.menu_id asc"
            dt = CommonDB.ExecuteFill(sql)
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    ''' <summary>
    ''' GetListProduct
    ''' </summary>
    ''' <returns>DataTable</returns>
    Public Function GetListProduct() As DataTable
        Dim dt As New DataTable
        Dim sql As String
        Try
            sql = ""
            sql &= " select md.product_id"
            sql &= "        ,md.product_nm_vn"
            sql &= " from   m_product md"
            sql &= " where  md.del_fg <> '1'"
            sql &= "   and  md.product_stt_id <> '1'"
            sql &= " order  by md.product_id asc"
            dt = CommonDB.ExecuteFill(sql)
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    ''' <summary>
    ''' GetListChosenProduct
    ''' </summary>
    ''' <returns>DataTable</returns>
    Public Function GetListChosenProduct(ByRef strMenuId As String) As DataTable
        Dim dt As New DataTable
        Dim sql As String
        Try
            sql = ""
            sql &= " select md.product_id    as product_id"
            sql &= "       ,md.product_nm_vn as product_nm_vn"
            sql &= " from   m_menu mm"
            sql &= "       ,m_product md"
            sql &= " where  mm.del_fg <> '1' "
            sql &= "    and md.del_fg <> '1'"
            sql &= "    and mm.menu_id = md.menu_id"
            If String.IsNullOrEmpty(strMenuId) = False OrElse String.IsNullOrWhiteSpace(strMenuId) = False Then
                sql &= "    and mm.menu_id = " & strMenuId
            End If
            sql &= "    and  md.product_stt_id <> '1'"
            sql &= " order  by mm.menu_id asc"
            dt = CommonDB.ExecuteFill(sql)
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    ''' <summary>
    ''' GetListIdProduct
    ''' </summary>
    ''' <returns>DataTable</returns>
    Public Function GetListIdProduct(ByRef strProductNm As String) As DataTable
        Dim dt As New DataTable
        Dim sql As String
        Try
            sql = ""
            sql &= " select md.product_id    as product_id"
            sql &= " from   m_product md"
            sql &= " where  md.del_fg <> '1'"
            If String.IsNullOrEmpty(strProductNm) = False OrElse String.IsNullOrWhiteSpace(strProductNm) = False Then
                sql &= "    and md.product_nm = '" & strProductNm & "'"
            End If
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

    Public Function fncDelete(ByVal strMenuId As String) As Boolean
        Dim CommonDB As CommonDB = New CommonDB
        Try
            CommonDB.BeginTransaction()
            Dim sql As String
            sql = ""
            sql &= "UPDATE m_menu"
            sql &= "   SET del_fg = '1'"
            sql &= "      ,upd_dt = " & CommonDB.EncloseVal(System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"))
            sql &= "      ,upd_user_id  = 'admin'"
            sql &= "      ,upd_pgm_id   = 'MRA-FE-0022'"
            sql &= " WHERE menu_id = " & CommonDB.EncloseVal(strMenuId)
            If Not CommonDB.ExecuteNonQuery(sql) = 1 Then
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
End Class

