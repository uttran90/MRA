Imports MySql.Data.MySqlClient
Imports MRA.CommonUtil
Imports MRA.PageData
Public Class MenuDetail_BL
    Public CommonDB As CommonDB
    Public CommonUtil As CommonUtil
    Public Sub New()
        MyBase.New
        CommonDB = New CommonDB
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
            sql &= "       ,mm.note"
            sql &= "       ,mm.menu_img"
            sql &= " from   m_menu mm"
            sql &= " where  mm.del_fg <> '1' "
            If String.IsNullOrEmpty(strMenuId) = False OrElse String.IsNullOrWhiteSpace(strMenuId) = False Then
                sql &= "    and mm.menu_id = '" & strMenuId & "'"
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
            sql &= " select md.product_id"
            sql &= "       ,md.product_nm_vn"
            sql &= " from   m_menu mm"
            sql &= "       ,m_product md"
            sql &= " where  mm.del_fg <> '1' "
            sql &= "    and md.del_fg <> '1'"
            sql &= "    and mm.menu_id = md.menu_id"
            If String.IsNullOrEmpty(strMenuId) = False OrElse String.IsNullOrWhiteSpace(strMenuId) = False Then
                sql &= "    and mm.menu_id = '" & strMenuId & "'"
            End If
            sql &= "    and  md.product_stt_id <> '1'"
            sql &= " order  by mm.menu_id asc"
            dt = CommonDB.ExecuteFill(sql)
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    '''' <summary>
    '''' BtnConfirmInUp
    '''' </summary>
    '''' <returns>Boolean</returns>
    'Public Function BtnConfirm(ByRef pd As PageData) As Boolean

    '    Dim sql As String = ""

    '    CommonDB.BeginTransaction()
    '    Try
    '        Dim strShoriMode As String = pd.GetItem(CommonUtil.SHORI_MODE)
    '        Select Case strShoriMode
    '            Case CommonUtil.LIST_MODE.INSERT
    '                sql = ""
    '                sql &= "INSERT INTO m_menu"
    '                sql &= "("
    '                'sql &= " menu_id"
    '                sql &= ",menu_nm_vn"
    '                sql &= ",menu_nm_jp"
    '                sql &= ",menu_nm_en"
    '                sql &= ",menu_img"
    '                sql &= ",note"
    '                sql &= ",crt_dt"
    '                sql &= ",crt_user_id"
    '                sql &= ",crt_pgm_id"
    '                sql &= ")"
    '                sql &= "VALUES"
    '                'sql &= " (" & CommonDB.EncloseVal(pd.GetItem("LBL_MENU_ID").ToString())
    '                sql &= " ," & CommonDB.EncloseVal(pd.GetItem("TXT_NAME_VN").ToString())
    '                sql &= " ," & CommonDB.EncloseVal(pd.GetItem("TXT_NAME_JP").ToString())
    '                sql &= " ,''"
    '                sql &= " ," & CommonDB.EncloseVal(pd.GetItem("LBL_FILE").ToString())
    '                sql &= " ," & CommonDB.EncloseVal(pd.GetItem("TXT_NOTE").ToString())
    '                sql &= " ," & CommonDB.EncloseVal(System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"))
    '                sql &= " ,'admin'"
    '                sql &= " ,'MRA-FE-0022'"
    '                sql &= " ,''"
    '                sql &= " ,''"
    '                sql &= " ,''"
    '                sql &= " ,'0'"
    '                sql &= ")"
    '                If Not CommonDB.ExecuteNonQuery(sql) = 1 Then
    '                    CommonDB.Rollback()
    '                    Return False
    '                End If
    '            Case CommonUtil.LIST_MODE.UPDATE
    '                sql = ""
    '                sql &= "UPDATE m_menu"
    '                sql &= "   SET menu_nm_vn = " & CommonDB.EncloseVal(pd.GetItem("TXT_NAME_VN").ToString())
    '                sql &= "      ,menu_nm_jp = " & CommonDB.EncloseVal(pd.GetItem("TXT_NAME_JP").ToString())
    '                sql &= "      ,menu_img = " & CommonDB.EncloseVal(pd.GetItem("LBL_FILE").ToString())
    '                sql &= "      ,note = " & CommonDB.EncloseVal(pd.GetItem("TXT_NOTE").ToString())
    '                sql &= "      ,upd_dt = " & CommonDB.EncloseVal(System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"))
    '                sql &= "      ,upd_user_id  = 'admin'"
    '                sql &= "      ,upd_pgm_id   = 'MRA-FE-0022'"
    '                sql &= " WHERE menu_id = " & CommonDB.EncloseVal(pd.GetItem("LBL_MENU_ID").ToString())
    '                If Not CommonDB.ExecuteNonQuery(sql) = 1 Then
    '                    CommonDB.Rollback()
    '                    Return False
    '                End If
    '            Case CommonUtil.LIST_MODE.DELETE
    '                sql = ""
    '                sql &= "UPDATE m_menu"
    '                sql &= "   SET del_fg = '1'"
    '                sql &= "      ,upd_dt = " & CommonDB.EncloseVal(System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"))
    '                sql &= "      ,upd_user_id  = 'admin'"
    '                sql &= "      ,upd_pgm_id   = 'MRA-FE-0022'"
    '                sql &= " WHERE menu_id = " & CommonDB.EncloseVal(pd.GetItem("LBL_MENU_ID").ToString())
    '                If Not CommonDB.ExecuteNonQuery(sql) = 1 Then
    '                    CommonDB.Rollback()
    '                    Return False
    '                End If
    '        End Select
    '        CommonDB.Commit()
    '        Return True
    '    Catch ex As Exception
    '        CommonDB.Rollback()
    '        Throw ex
    '    End Try
    'End Function
    '''' <summary>
    '''' seq_m_menu
    '''' </summary>
    '''' <returns>seq_m_menu.NEXTVAL</returns>
    'Public Function GetMenuIdFromSeq() As String
    '    Dim sql As String
    '    Try
    '        sql = ""
    '        sql &= " SELECT SELECT count(LAST_INSERT_ID() ) + 1 as menu_id"
    '        sql &= " FROM   m_menu"
    '        Return CommonDB.ExecuteScalar(sql)
    '    Catch ex As Exception
    '        Throw ex
    '    End Try
    'End Function
End Class

