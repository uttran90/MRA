Imports System.Threading.Tasks
Imports System.Web.UI.HtmlControls.HtmlGenericControl
Imports MRA.CommonUtil
Imports MRA.PageData
Public Class MRA_FE_0022
    Inherits System.Web.UI.Page

    Public BL As New MenuDetail_BL
    Public PageData As New PageData

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
                    'menu
                    'get data
                    LBL_MENU_ID.Text = strMenuId
                    dt = BL.GetMenuInfo(strMenuId)
                    Dim row As DataRow = dt.Rows(0)
                    TXT_NAME_VN.Text = row("menu_nm_vn")
                    TXT_NAME_JP.Text = row("menu_nm_jp")
                    TXT_NOTE.Text = row("note")

                    'product
                    dtlistc = BL.GetListChosenProduct(strMenuId)
                    Dim lb() As Byte = Encoding.ASCII.GetBytes(row("menu_img"))
                    Dim ms As New System.IO.MemoryStream(lb)
                    IMG_ID.ImageUrl = "\image\" & row("menu_img")
                    LBL_FILE.Text = row("menu_img")

                    'set state update
                    BTN_ADD.Visible = False
                    BTN_UPDATE.Visible = True
                    BTN_DELETE.Visible = True
                    BTN_UPDATE.Text = LIST_BUTTON_NAME.UPDATE


                Else
                    ' LBL_MENU_ID.Text = BL.GetMenuIdFromSeq()
                    'set state insert
                    BTN_ADD.Visible = True
                    BTN_UPDATE.Visible = False
                    BTN_DELETE.Visible = False
                    BTN_ADD.Text = LIST_BUTTON_NAME.INSERT
                End If
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
        End If
    End Sub

    '''' <summary>
    '''' Create random id in 10 character
    '''' </summary>
    'Private Function Random_ID()

    '    Dim alphabets As String = "ABCDEFGHIJKLMNOPQRSTUVWXYZ"
    '    Dim small_alphabets As String = "abcdefghijklmnopqrstuvwxyz"
    '    Dim numbers As String = "1234567890"

    '    Dim characters As String = numbers
    '    characters += Convert.ToString(alphabets & small_alphabets) & numbers
    '    'length of ID = 10
    '    Dim length As Integer = 10
    '    Dim id As String = String.Empty
    '    For i As Integer = 0 To length - 1
    '        Dim c As String = String.Empty
    '        Do
    '            Dim index As Integer = New Random().Next(0, characters.Length)
    '            c = characters.ToCharArray()(index).ToString()
    '        Loop While ID.IndexOf(c) <> -1
    '        id += c
    '    Next
    '    Return id
    'End Function
    '''' <summary>
    '''' Upload image
    '''' </summary>
    Protected Sub UPLOAD_Click(ByVal sender As Object,
     ByVal e As System.EventArgs)
        If FILE_PATH.HasFiles Then
            Try
                FILE_PATH.SaveAs(Server.MapPath("\image\" + FILE_PATH.FileName))
                IMG_ID.ImageUrl = "\image\" & FILE_PATH.FileName
            Catch ex As Exception
                Response.Write("ERROR: " & ex.Message.ToString())
            End Try
        End If
    End Sub

    ''' <summary>
    ''' Add
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub BTN_ADD_Click(sender As Object, e As EventArgs) Handles BTN_ADD.Click

        Dim CommonDB As CommonDB = New CommonDB
        CommonDB.BeginTransaction()
        Try
            'If PageData.GetItem(STATE_MODE) = State.Init Then
            '    PageData.SetItem(STATE_MODE, State.Enter)
            '    SetStateInfo()
            'ElseIf PageData.GetItem(STATE_MODE) = State.Enter Then
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
            sql &= " ," & CommonDB.EncloseVal(LBL_FILE.Text)
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
            CommonDB.Commit()
            'If BL.BtnConfirm(PageData) Then
            MsgBox("Insert OK")
            Response.Redirect("~/MRA-FE-0021.aspx", False)
            'PageData.SetItem(STATE_MODE, State.Check)
            'SetStateInfo()
            'End If
            'End If
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
        Dim CommonDB As CommonDB = New CommonDB
        CommonDB.BeginTransaction()
        Try
            'Dim bCheck As Boolean = True
            'If PageData.GetItem(STATE_MODE) = State.Init Then
            '    PageData.SetItem(STATE_MODE, State.Enter)
            '    SetStateInfo()
            'ElseIf PageData.GetItem(STATE_MODE) = State.Enter Then
            Dim sql As String = ""
            sql = ""
            sql &= "UPDATE m_menu"
            sql &= "   SET menu_nm_vn = " & CommonDB.EncloseVal(TXT_NAME_VN.Text)
            sql &= "      ,menu_nm_jp = " & CommonDB.EncloseVal(TXT_NAME_JP.Text)
            sql &= "      ,menu_img = " & CommonDB.EncloseVal(LBL_FILE.Text)
            sql &= "      ,note = " & CommonDB.EncloseVal(TXT_NOTE.Text)
            sql &= "      ,upd_dt = " & CommonDB.EncloseVal(System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"))
            sql &= "      ,upd_user_id  = 'admin'"
            sql &= "      ,upd_pgm_id   = 'MRA-FE-0022'"
            sql &= " WHERE menu_id = " & CommonDB.EncloseVal(LBL_MENU_ID.Text)
            If Not CommonDB.ExecuteNonQuery(sql) = 1 Then
                CommonDB.Rollback()
            End If
            CommonDB.Commit()
            MsgBox("Update OK")
            Response.Redirect("~/MRA-FE-0021.aspx", False)
            'If BL.BtnConfirm(PageData) Then
            'PageData.SetItem(STATE_MODE, State.Check)
            'SetStateInfo()
            'End If
            'End If
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
            Dim sql As String = ""
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

    '''' <summary>
    '''' 状態設定
    '''' </summary>
    'Private Sub SetStateInfo()
    '    Select Case PageData.GetItem(SHORI_MODE)
    '        Case LIST_MODE.INSERT
    '            Select Case PageData.GetItem(STATE_MODE)
    '            'メニューからの呼び出し時
    '                Case State.Init
    '                    BTN_ADD.Visible = True
    '                    BTN_UPDATE.Visible = False
    '                    BTN_DELETE.Visible = False
    '                    BTN_ADD.Text = LIST_BUTTON_NAME.INSERT
    '                Case State.Enter
    '                    TXT_NAME_VN.Enabled = False
    '                    TXT_NAME_JP.Enabled = False
    '                    TXT_NOTE.Enabled = False
    '                    FILE_PATH.Enabled = False
    '                    BTN_UPLOAD.Enabled = False
    '                    SELECT_SEND_PRODUCT_LEFT.Enabled = False

    '                    BTN_ADD.Visible = True
    '                    BTN_UPDATE.Visible = False
    '                    BTN_DELETE.Visible = False
    '                    BTN_ADD.Text = LIST_BUTTON_NAME.CONFIRM
    '                Case State.Check
    '                    'clear 
    '                    LBL_MENU_ID.Text = ""
    '                    TXT_NAME_VN.Text = ""
    '                    TXT_NAME_JP.Text = ""
    '                    TXT_NOTE.Text = ""
    '                    LBL_FILE.Text = ""
    '                    SELECT_SEND_PRODUCT_LEFT.Text = ""

    '                    BTN_ADD.Visible = False
    '                    BTN_UPDATE.Visible = False
    '                    BTN_DELETE.Visible = False
    '                    BTN_ADD.Text = LIST_BUTTON_NAME.INSERT
    '            End Select
    '        Case LIST_MODE.UPDATE
    '            Select Case PageData.GetItem(STATE_MODE)
    '                Case State.Init
    '                    BTN_ADD.Visible = False
    '                    BTN_UPDATE.Visible = True
    '                    BTN_DELETE.Visible = True
    '                    BTN_UPDATE.Text = LIST_BUTTON_NAME.UPDATE
    '                Case State.Enter
    '                    TXT_NAME_VN.Enabled = False
    '                    TXT_NAME_JP.Enabled = False
    '                    TXT_NOTE.Enabled = False
    '                    FILE_PATH.Enabled = False
    '                    BTN_UPLOAD.Enabled = False
    '                    SELECT_SEND_PRODUCT_LEFT.Enabled = False

    '                    BTN_ADD.Visible = False
    '                    BTN_UPDATE.Visible = True
    '                    BTN_DELETE.Visible = True
    '                    BTN_UPDATE.Text = LIST_BUTTON_NAME.CONFIRM
    '                Case State.Check
    '                    'clear 
    '                    LBL_MENU_ID.Text = ""
    '                    TXT_NAME_VN.Text = ""
    '                    TXT_NAME_JP.Text = ""
    '                    TXT_NOTE.Text = ""
    '                    LBL_FILE.Text = ""
    '                    SELECT_SEND_PRODUCT_LEFT.Text = ""

    '                    BTN_ADD.Visible = False
    '                    BTN_UPDATE.Visible = False
    '                    BTN_DELETE.Visible = False
    '                    BTN_ADD.Text = LIST_BUTTON_NAME.UPDATE
    '            End Select
    '        Case LIST_MODE.DELETE
    '            Select Case PageData.GetItem(STATE_MODE)
    '                Case State.Init
    '                    TXT_NAME_VN.Enabled = True
    '                    TXT_NAME_JP.Enabled = True
    '                    TXT_NOTE.Enabled = True
    '                    FILE_PATH.Enabled = True
    '                    BTN_UPLOAD.Enabled = True
    '                    SELECT_SEND_PRODUCT_LEFT.Enabled = True

    '                    BTN_ADD.Visible = False
    '                    BTN_UPDATE.Visible = True
    '                    BTN_DELETE.Visible = True
    '                    BTN_DELETE.Text = LIST_BUTTON_NAME.DELETE
    '                Case State.Enter
    '                    TXT_NAME_VN.Enabled = False
    '                    TXT_NAME_JP.Enabled = False
    '                    TXT_NOTE.Enabled = False
    '                    FILE_PATH.Enabled = False
    '                    BTN_UPLOAD.Enabled = False
    '                    SELECT_SEND_PRODUCT_LEFT.Enabled = False

    '                    BTN_ADD.Visible = False
    '                    BTN_UPDATE.Visible = True
    '                    BTN_DELETE.Visible = True
    '                    BTN_DELETE.Text = LIST_BUTTON_NAME.CONFIRM
    '                Case State.Check
    '                    'clear 
    '                    LBL_MENU_ID.Text = ""
    '                    TXT_NAME_VN.Text = ""
    '                    TXT_NAME_JP.Text = ""
    '                    TXT_NOTE.Text = ""
    '                    LBL_FILE.Text = ""
    '                    SELECT_SEND_PRODUCT_LEFT.Text = ""

    '                    BTN_ADD.Visible = False
    '                    BTN_UPDATE.Visible = False
    '                    BTN_DELETE.Visible = False
    '                    BTN_ADD.Text = LIST_BUTTON_NAME.DELETE
    '            End Select
    '    End Select
    'End Sub
End Class
