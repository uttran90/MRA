Imports MRACommon

Public Class Menu_BL
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
            sql &= " select ROW_NUMBER() OVER (ORDER BY menu_id) AS num"
            sql &= "    ,mm.menu_id"
            sql &= "    ,mm.menu_nm_vn"
            sql &= "    ,mm.menu_nm_jp"
            sql &= "    ,mm.note"
            sql &= " from   m_menu mm"
            sql &= " where  mm.del_fg <> '1' "
            If String.IsNullOrEmpty(strSearch) = False OrElse String.IsNullOrWhiteSpace(strSearch) = False Then
                sql &= "    and mm.menu_nm_vn like '%" & strSearch & "%'"
            End If
            If String.IsNullOrEmpty(strSearch) = False OrElse String.IsNullOrWhiteSpace(strSearch) = False Then
                sql &= "    or mm.menu_nm_jp like '%" & strSearch & "%'"
            End If
            If String.IsNullOrEmpty(strSearch) = False OrElse String.IsNullOrWhiteSpace(strSearch) = False Then
                sql &= "    or mm.note like '%" & strSearch & "%'"
            End If
            sql &= " order  by mm.menu_id asc"
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
            sql &= " select ROW_NUMBER() OVER (ORDER BY menu_id) AS num"
            sql &= "    ,mm.menu_id"
            sql &= "    ,mm.menu_nm_vn"
            sql &= "    ,mm.menu_nm_jp"
            sql &= "    ,mm.note"
            sql &= " from m_menu mm"
            sql &= " where mm.del_fg <> '1' "
            sql &= " order by mm.menu_id asc"
            dt = CommonDB.ExecuteFill(sql)
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function

End Class

