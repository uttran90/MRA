
Imports MRACommon
Public Class Product_BL
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
            sql &= " select ROW_NUMBER() OVER (ORDER BY product_id) AS num"
            sql &= "    ,mpro.product_id"
            sql &= "    ,mpro.product_nm_jp"
            sql &= "    ,mpro.price_show"
            sql &= "    ,case when mpro.product_stt_id ='0' then 'ON' ELSE 'OFF' END as product_stt"
            sql &= "    ,mpro.product_note"
            sql &= "    ,mm.menu_nm_jp"
            sql &= " from m_product mpro"
            sql &= " left join m_menu mm on mm.menu_id = mpro.menu_id"
            sql &= " where mpro.del_fg <> '1' "
            sql &= " and   mm.del_fg <> '1' "
            If String.IsNullOrEmpty(strSearch) = False OrElse String.IsNullOrWhiteSpace(strSearch) = False Then
                sql &= "    and mpro.product_nm_vn like '%" & strSearch & "%'"
            End If
            If String.IsNullOrEmpty(strSearch) = False OrElse String.IsNullOrWhiteSpace(strSearch) = False Then
                sql &= "    and mpro.product_nm_en like '%" & strSearch & "%'"
            End If
            If String.IsNullOrEmpty(strSearch) = False OrElse String.IsNullOrWhiteSpace(strSearch) = False Then
                sql &= "    and mpro.product_nm_jp like '%" & strSearch & "%'"
            End If
            If String.IsNullOrEmpty(strSearch) = False OrElse String.IsNullOrWhiteSpace(strSearch) = False Then
                sql &= "    and mpro.product_note like '%" & strSearch & "%'"
            End If
            If String.IsNullOrEmpty(strSearch) = False OrElse String.IsNullOrWhiteSpace(strSearch) = False Then
                sql &= "    and mm.menu_nm_vn like '%" & strSearch & "%'"
            End If
            If String.IsNullOrEmpty(strSearch) = False OrElse String.IsNullOrWhiteSpace(strSearch) = False Then
                sql &= "    or mm.menu_nm_jp like '%" & strSearch & "%'"
            End If
            sql &= " order by mpro.product_id asc"
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
            sql &= " select ROW_NUMBER() OVER (ORDER BY product_id) AS num"
            sql &= "    ,mpro.product_id"
            sql &= "    ,mpro.product_nm_jp"
            sql &= "    ,mpro.price_show"
            sql &= "    ,case when mpro.product_stt_id ='0' then 'ON' ELSE 'OFF' END as product_stt"
            sql &= "    ,mpro.product_note"
            sql &= "    ,mm.menu_nm_jp"
            sql &= " from m_product mpro"
            sql &= " left join m_menu mm on mm.menu_id = mpro.menu_id"
            sql &= " where mpro.del_fg <> '1' "
            sql &= " and   mm.del_fg <> '1' "
            sql &= " order by mpro.product_id asc"
            dt = CommonDB.ExecuteFill(sql)
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function
End Class
