﻿
Imports MRACommon
Imports MRACommon.CommonUtil
Imports MRA_FW
Imports MRA_FW.BasePL
Imports System.IO

Public Class MRA_FE_0022
    Inherits System.Web.UI.Page

    Public BL As New MenuDetail_BL
    Private Sub MRA_FE_0022_Unload(sender As Object, e As EventArgs) Handles Me.Unload
        BL.Dispose()
    End Sub
    ''' <summary>
    ''' 初期表示
    ''' </summary>
    Private Sub MRA_FE_0022_InitLoad(sender As Object, e As EventArgs) Handles Me.Load
        Dim strMenuId As String = Request.QueryString("menu_id")
        'Data info menu
        Dim dt As New DataTable
        'Data list product
        Dim dtlist As New DataTable
        'Data list chosen product
        Dim dtlistc As New DataTable
        If Not Page.IsPostBack Then
            Try

                If strMenuId <> "" Then
                    'mode update
                    'get data
                    LBL_MENU_ID.Text = strMenuId
                    dt = BL.GetMenuInfo(strMenuId)
                    Dim row As DataRow = dt.Rows(0)
                    TXT_NAME_VN.Text = row("menu_nm_vn")
                    TXT_NAME_JP.Text = row("menu_nm_jp")
                    TXT_NOTE.Text = row("note")

                    'get image
                    If row("menu_img").ToString <> "" Then
                        IMG_ID.ImageUrl = "\image\" & row("menu_img")
                        LBL_FILEPATH.InnerText = row("menu_img")
                        TXT_PATH.Value = row("menu_img")
                    Else
                    End If
                    'set state update
                    BTN_ADD.Visible = False
                    BTN_UPDATE.Visible = True
                    BTN_DELETE.Visible = True
                    BTN_UPDATE.Text = LIST_BUTTON_NAME.UPDATE

                Else
                        'set state insert
                        BTN_ADD.Visible = True
                    BTN_UPDATE.Visible = False
                    BTN_DELETE.Visible = False
                    BTN_ADD.Text = LIST_BUTTON_NAME.INSERT
                End If
                '2listbox
                'TXT_PRODUCT_LIST.DataSource = dtlist
                'TXT_PRODUCT_LIST.DataValueField = "product_nm_vn"
                'TXT_PRODUCT_LIST.DataBind()
                Dim lstItem As ListItem
                dtlist = BL.GetListProduct()
                Dim strProductNm As String = ""

                If dtlist.Rows.Count > 0 Then
                    Dim row_list As DataRow = dtlist.Rows(0)
                    For Each row_list In dtlist.Rows
                        If strProductNm <> row_list("product_nm_vn").ToString() Then
                            lstItem = New ListItem(row_list("product_nm_vn").ToString(), row_list("product_id").ToString())
                            SELECT_SEND_PRODUCT_LEFT.Items.Add(lstItem)
                        End If
                        If strMenuId <> "" Then
                            dtlistc = BL.GetListChosenProduct(strMenuId)
                            If dtlistc.Rows.Count > 0 Then
                                Dim row_listc As DataRow
                                row_listc = dtlistc.Rows(0)
                                For Each row_listc In dtlistc.Rows
                                    If row_listc("product_nm_vn").ToString() = row_list("product_nm_vn").ToString() Then
                                        lstItem.Selected = True
                                    End If
                                Next
                            Else
                            End If
                        End If
                        strProductNm = row_list("product_nm_vn").ToString()
                    Next
                End If
            Catch ex As Exception
                Console.WriteLine("exp error", ex.Message)
            End Try
        Else
        End If
    End Sub

    '''' <summary>
    '''' Upload image
    '''' </summary>
    Protected Sub UPLOAD_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim uploadObj As New HtmlInputFile
        Dim uploadFolder As String
        Dim uploadFileName As String
        Dim uploadFilePath As String
        uploadFolder = Server.MapPath("\image\")
        Try
            uploadObj = TXT_FILEPATH
            If uploadObj.PostedFile.ContentLength > 0 Then
                uploadFileName = uploadObj.PostedFile.FileName
                If Not Directory.Exists(uploadFolder) Then
                    Directory.CreateDirectory(uploadFolder)
                End If
                uploadFilePath = Path.Combine(uploadFolder, uploadFileName)
                uploadObj.PostedFile.SaveAs(uploadFilePath)
                IMG_ID.ImageUrl = "\image\" & uploadFileName
                LBL_FILEPATH.InnerText = uploadFileName
                'set value to insert update for DB
                TXT_PATH.Value = uploadFileName
            End If

        Catch ex As Exception
            Response.Write("ERROR: " & ex.Message.ToString())
        End Try
    End Sub

    ''' <summary>
    ''' Add
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub BTN_ADD_Click(sender As Object, e As EventArgs) Handles BTN_ADD.Click

        Dim selListItem As New List(Of String)
        Dim CommonDB As CommonDB = New CommonDB
        CommonDB.BeginTransaction()
        Try
            Dim sql As String = ""
            sql = ""
            sql &= "INSERT INTO m_menu"
            sql &= "("
            'sql &= " menu_id"
            sql &= "menu_nm_vn"
            sql &= ",menu_nm_jp"
            sql &= ",menu_nm_en"
            sql &= ",menu_img"
            sql &= ",note"
            sql &= ",crt_dt"
            sql &= ",crt_user_id"
            sql &= ",crt_pgm_id"
            sql &= ",upd_dt"
            sql &= ",upd_user_id"
            sql &= ",upd_pgm_id"
            sql &= ",del_fg"
            sql &= ")"
            sql &= "VALUES"
            'sql &= " (" & CommonDB.EncloseVal(pd.GetItem("LBL_MENU_ID").ToString())
            sql &= " (" & CommonDB.EncloseVal(TXT_NAME_VN.Text)
            sql &= " ," & CommonDB.EncloseVal(TXT_NAME_JP.Text)
            sql &= " ,''"
            sql &= " ," & CommonDB.EncloseVal(TXT_PATH.Value)
            sql &= " ," & CommonDB.EncloseVal(TXT_NOTE.Text)
            sql &= " ," & CommonDB.EncloseVal(System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"))
            sql &= " ,'admin'"
            sql &= " ,'MRA-FE-0022'"
            sql &= " ,''"
            sql &= " ,''"
            sql &= " ,''"
            sql &= " ,'0'"
            sql &= ")"
            If Not CommonDB.ExecuteNonQuery(sql) = 1 Then
                CommonDB.Rollback()
            End If
            If SELECT_SEND_PRODUCT_LEFT.Items.Count > 0 Then
                Dim sql1 As String
                For Each productId As ListItem In SELECT_SEND_PRODUCT_LEFT.Items
                    If productId.Selected = True Then
                        sql1 = ""
                        sql1 &= "UPDATE m_product"
                        sql1 &= "   SET menu_id = " & BL.getSeq("m_menu")
                        sql1 &= "      ,upd_dt = " & CommonDB.EncloseVal(System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"))
                        sql1 &= "      ,upd_user_id  = 'admin'"
                        sql1 &= "      ,upd_pgm_id   = 'MRA-FE-0022'"
                        sql1 &= " WHERE product_id = " & productId.Value
                        sql1 &= " ; "
                        If Not CommonDB.ExecuteNonQuery(sql1) = 1 Then
                            CommonDB.Rollback()
                        End If
                    End If
                Next
            End If
            CommonDB.Commit()
            MsgBox("Insert OK")
            Response.Redirect("~/MRA-FE-0021.aspx", False)
        Catch ex As Exception
            MsgBox("Insert except", ex.Message)
        End Try
    End Sub

    ''' <summary>
    ''' Update
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub BTN_Update_Click(sender As Object, e As EventArgs) Handles BTN_UPDATE.Click
        Dim selListItem As New List(Of String)
        Dim CommonDB As CommonDB = New CommonDB
        CommonDB.BeginTransaction()
        Try

            Dim sql As String
            sql = ""
            sql &= "UPDATE m_menu"
            sql &= "   SET menu_nm_vn = " & CommonDB.EncloseVal(TXT_NAME_VN.Text)
            sql &= "      ,menu_nm_jp = " & CommonDB.EncloseVal(TXT_NAME_JP.Text)
            sql &= "      ,menu_img = " & CommonDB.EncloseVal(TXT_PATH.Value)
            sql &= "      ,note = " & CommonDB.EncloseVal(TXT_NOTE.Text)
            sql &= "      ,upd_dt = " & CommonDB.EncloseVal(System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"))
            sql &= "      ,upd_user_id  = 'admin'"
            sql &= "      ,upd_pgm_id   = 'MRA-FE-0022'"
            sql &= " WHERE menu_id = " & LBL_MENU_ID.Text
            If Not CommonDB.ExecuteNonQuery(sql) = 1 Then
                CommonDB.Rollback()
            End If

            If SELECT_SEND_PRODUCT_LEFT.Items.Count > 0 Then
                For Each productId As ListItem In SELECT_SEND_PRODUCT_LEFT.Items
                    Dim sql1 As String
                    If productId.Selected = True Then
                        sql1 = ""
                        sql1 &= "UPDATE m_product"
                        sql1 &= "   SET menu_id = " & Convert.ToInt32(LBL_MENU_ID.Text)
                        sql1 &= "      ,upd_dt = " & CommonDB.EncloseVal(System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"))
                        sql1 &= "      ,upd_user_id  = 'admin'"
                        sql1 &= "      ,upd_pgm_id   = 'MRA-FE-0022'"
                        sql1 &= " WHERE product_id = " & Convert.ToInt32(productId.Value)
                        sql1 &= " ; "
                        If Not CommonDB.ExecuteNonQuery(sql1) = 1 Then
                            CommonDB.Rollback()
                        End If
                    End If
                Next
            End If
            CommonDB.Commit()
            MsgBox("Update OK")
            Response.Redirect("~/MRA-FE-0021.aspx", False)

        Catch ex As Exception
            MsgBox("Update except", ex.Message)
        End Try
    End Sub
    ''' <summary>
    ''' Update
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub BTN_DELETE_Click(sender As Object, e As EventArgs) Handles BTN_DELETE.Click
        Dim strMenuId As String = Request.QueryString("menu_id")
        Dim CommonDB As CommonDB = New CommonDB
        CommonDB.BeginTransaction()
        Try
            'Dim bCheck As Boolean = True
            'If PageData.GetItem(STATE_MODE) = State.Init Then
            '    PageData.SetItem(STATE_MODE, State.Enter)
            '    SetStateInfo()
            'ElseIf PageData.GetItem(STATE_MODE) = State.Enter Then

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
            End If
            CommonDB.Commit()
            'If BL.BtnConfirm(PageData) Then
            'PageData.SetItem(STATE_MODE, State.Check)
            'SetStateInfo()
            MsgBox("Delete OK")
            Response.Redirect("~/MRA-FE-0021.aspx", False)
            'End If
        Catch ex As Exception
            MsgBox("Delete except", ex.Message)
        End Try
    End Sub
End Class
