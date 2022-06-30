Imports System.IO
Imports MRACommon
Imports MRACommon.CommonUtil

Public Class MRA_FE_0032
    Inherits MRA_FW.BasePL

    Public BL As New ProductDetail_BL
    Private Sub MRA_FE_0032_Unload(sender As Object, e As EventArgs) Handles Me.Unload
        BL.Dispose()
    End Sub
    ''' <summary>
    ''' 初期表示
    ''' </summary>
    Private Sub MRA_FE_0032_InitLoad(sender As Object, e As EventArgs) Handles Me.InitLoad
        Dim strProductId As String = Request.QueryString("product_id")
        ClearMessages()
        Try
            If strProductId <> "" Then
                'mode update
                'get data
                If Not Page.IsPostBack Then
                    LBL_ID.Text = strProductId
                    MRA_FE_0032_InitLoad(strProductId)
                End If
                'set state update
                BTN_ADD_ROW.Visible = True
                BTN_ADD.Visible = False
                BTN_UPDATE.Visible = True
                BTN_DELETE.Visible = True
                BTN_UPDATE.Text = CommonUtil.LIST_BUTTON_NAME.UPDATE
                AddMessage("MSG_0032_02")
            Else
                'set state insert
                BTN_ADD_ROW.Visible = False
                BTN_ADD.Visible = True
                BTN_UPDATE.Visible = False
                BTN_DELETE.Visible = False
                BTN_ADD.Text = CommonUtil.LIST_BUTTON_NAME.INSERT
                AddMessage("MSG_0032_01")
            End If
            TXT_MENU_VALUE.Enabled = False
        Catch ex As Exception
            AddMessage("MSG_9000_01", {ex.Message})
        End Try
    End Sub
    Private Sub MRA_FE_0032_InitLoad(ByVal strProductId As String)
        'Data info product
        Dim dt As New DataTable
        Try
            dt = BL.GetProductInfo(strProductId)
            If dt.Rows.Count > 0 Then
                Dim row As DataRow = dt.Rows(0)
                If row("menu_nm_jp").ToString() <> "" Then
                    TXT_MENU_VALUE.Text = row("menu_nm_jp")
                End If
                TXT_NM_VN_ON.Text = row("product_nm_vn")
                TXT_NM_JP_ON.Text = row("product_nm_jp")
                TXT_NM_EN_ON.Text = row("product_nm_en")
                TXT_NM_OFF.Text = row("product_nm_off")
                If row("description").ToString() <> "" Then
                    TXT_DES.Text = row("description")
                End If
                If row("note").ToString() <> "" Then
                    TXT_NOTE.Text = row("note")
                End If
                TXT_PRICE.Text = row("price")
                TXT_PRICE_SHOW.Text = row("price_show")
                'get stt product
                DDL_STS.SelectedIndex = row("product_stt")

                'get image
                If row("product_avatar").ToString <> "" Then
                    IMG_ID.ImageUrl = "\image\" & row("product_avatar")
                    LBL_FILEPATH.InnerText = row("product_avatar")
                    TXT_PATH.Value = row("product_avatar")
                End If


                'get list opt
                Dim dtProductChoose As DataTable = BL.GetProductOptChosen(strProductId)
                If dtProductChoose.Rows.Count > 0 Then
                    GRD_OPT.DataSource = dtProductChoose
                    GRD_OPT.DataBind()
                Else
                    GRD_OPT.DataSource = New List(Of String)
                    GRD_OPT.DataBind()
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
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
                AddMessage("MSG_0001_05")
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

        ClearMessages()
        ClearError(form1)
        Dim CommonDB As CommonDB = New CommonDB
        CommonDB.BeginTransaction()

        Try
            If ChkEmptyValidateInput() Then
                Dim sql As String = ""
                sql = ""
                sql &= "INSERT INTO m_product"
                sql &= "("
                sql &= "product_nm_vn"
                sql &= ",product_nm_jp"
                sql &= ",product_nm_en"
                sql &= ",product_nm_off"
                sql &= ",price"
                sql &= ",price_show"
                sql &= ",discount_p"
                sql &= ",product_stt_id"
                sql &= ",description   "
                sql &= ",product_avatar"
                sql &= ",product_note  "
                sql &= ",crt_dt"
                sql &= ",crt_user_id"
                sql &= ",crt_pgm_id"
                sql &= ",upd_dt"
                sql &= ",upd_user_id"
                sql &= ",upd_pgm_id"
                sql &= ",del_fg"
                sql &= ")"
                sql &= "VALUES"
                sql &= " (" & CommonDB.EncloseVal(TXT_NM_VN_ON.Text)
                sql &= " ," & CommonDB.EncloseVal(TXT_NM_JP_ON.Text)
                sql &= " ," & CommonDB.EncloseVal(TXT_NM_EN_ON.Text)
                sql &= " ," & CommonDB.EncloseVal(TXT_NM_OFF.Text)
                sql &= " ," & CommonDB.EncloseVal(TXT_PRICE.Text)
                sql &= " ," & CommonDB.EncloseVal(TXT_PRICE_SHOW.Text)
                sql &= " ,''"
                sql &= " ," & CommonDB.EncloseVal(DDL_STS.SelectedIndex)
                sql &= " ," & CommonDB.EncloseVal(TXT_DES.Text)
                sql &= " ," & CommonDB.EncloseVal(TXT_PATH.Value)
                sql &= " ," & CommonDB.EncloseVal(TXT_NOTE.Text)
                sql &= " ," & CommonDB.EncloseVal(System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"))
                sql &= " ,'admin'"
                sql &= " ,'MRA-FE-0032'"
                sql &= " ,''"
                sql &= " ,''"
                sql &= " ,''"
                sql &= " ,'0'"
                sql &= ")"
                If Not CommonDB.ExecuteNonQuery(sql) = 1 Then
                    CommonDB.Rollback()
                End If
                CommonDB.Commit()
                AddMessage("MSG_0001_01", {"Product"})
                CleaAllTextBox(form1)
            End If
        Catch ex As Exception
            AddMessage("MSG_9000_01", {"Insert" & ex.Message})
        End Try
    End Sub

    ''' <summary>
    ''' Update
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub BTN_Update_Click(sender As Object, e As EventArgs) Handles BTN_UPDATE.Click
        ClearMessages()
        ClearError(form1)
        Dim CommonDB As CommonDB = New CommonDB
        CommonDB.BeginTransaction()

        Try
            If ChkEmptyValidateInput() Then
                Dim sql As String
                sql = ""
                sql &= "UPDATE m_product"
                sql &= "   SET product_nm_vn  = " & CommonDB.EncloseVal(TXT_NM_VN_ON.Text)
                sql &= "      ,product_nm_jp  = " & CommonDB.EncloseVal(TXT_NM_JP_ON.Text)
                sql &= "      ,product_nm_en  = " & CommonDB.EncloseVal(TXT_NM_EN_ON.Text)
                sql &= "      ,product_nm_off = " & CommonDB.EncloseVal(TXT_NM_OFF.Text)
                sql &= "      ,price          = " & CommonDB.EncloseVal(TXT_PRICE.Text)
                sql &= "      ,price_show     = " & CommonDB.EncloseVal(TXT_PRICE_SHOW.Text)
                sql &= "      ,product_stt_id = " & CommonDB.EncloseVal(DDL_STS.SelectedIndex)
                sql &= "      ,description    = " & CommonDB.EncloseVal(TXT_DES.Text)
                sql &= "      ,product_avatar = " & CommonDB.EncloseVal(TXT_PATH.Value)
                sql &= "      ,product_note   = " & CommonDB.EncloseVal(TXT_NOTE.Text)
                sql &= "      ,upd_dt         = " & CommonDB.EncloseVal(System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"))
                sql &= "      ,upd_user_id  = 'admin'"
                sql &= "      ,upd_pgm_id   = 'MRA-FE-0032'"
                sql &= "      ,upd_user_id  = 'admin'"
                sql &= "      ,upd_pgm_id   = 'MRA-FE-0032'"
                sql &= " WHERE product_id = " & LBL_ID.Text
                If Not CommonDB.ExecuteNonQuery(sql) = 1 Then
                    CommonDB.Rollback()
                End If
                CommonDB.Commit()
                AddMessage("MSG_0001_02", {"Product"})
                BTN_UPDATE.Enabled = False
            End If
        Catch ex As Exception
            AddMessage("MSG_9000_01", {"Update" & ex.Message})
        End Try
    End Sub

    ''' <summary>
    ''' Back to product list
    ''' </summary>
    Private Sub BTN_BACK_Click() Handles BTN_BACK.Click
        Response.Redirect("~/MRA-FE-0031.aspx", False)
    End Sub

    ''' <summary>
    ''' Update
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub BTN_DELETE_Click(sender As Object, e As EventArgs) Handles BTN_DELETE.Click
        Dim strProductId As String = Request.QueryString("product_id")
        ClearMessages()
        ClearError(form1)
        Dim CommonDB As CommonDB = New CommonDB
        CommonDB.BeginTransaction()
        Try
            Dim confirmValue As String = Request.Form("confirm_value")
            If confirmValue = "Yes" Then
                If BL.fncDelete(strProductId) Then
                    AddMessage("MSG_0001_03", {"Product"})
                    BTN_UPDATE.Enabled = False
                    BTN_DELETE.Enabled = False
                    BTN_UPLOAD.Enabled = False
                    BTN_ADD_ROW.Disabled = True
                Else
                    AddMessage("MSG_0001_09", {"Product"})
                End If
            Else
                AddMessage("MSG_0032_02")
            End If

        Catch ex As Exception
            AddMessage("MSG_9000_01", {"Delete" & ex.Message})
        End Try
    End Sub
    '=========Option Grid Start
    'Change page in datagrid
    Private Sub GRD_OPT_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles GRD_OPT.PageIndexChanging
        GRD_OPT.PageIndex = e.NewPageIndex
        showData() 'bindgridview will Get the data source And bind it again
    End Sub
    '--------update delete edit cancel row start---------
    Protected Sub OnRowEditing(sender As Object, e As GridViewEditEventArgs)
        GRD_OPT.EditIndex = e.NewEditIndex
        showData()
    End Sub
    Protected Sub OnUpdate(sender As Object, e As EventArgs)
        Dim row As GridViewRow = TryCast(TryCast(sender, LinkButton).NamingContainer, GridViewRow)
        Dim CommonDB As CommonDB = New CommonDB
        ClearMessages()
        CommonDB.BeginTransaction()
        If IsPostBack Then
            Try
                Dim opt_id As String = row.Cells(1).Text
                Dim name As String = TryCast(row.Cells(2).Controls(0), TextBox).Text
                Dim price As String = TryCast(row.Cells(3).Controls(0), TextBox).Text
                Dim note As String = TryCast(row.Cells(4).Controls(0), TextBox).Text
                Dim numExp As New Regex("^[0-9-]*$")
                If name = "" Or price = "" Then
                    AddMessage("MSG_1000_05", {"Product name and Price"})
                    Exit Sub
                End If
                If Not numExp.Match(price).Success Then
                    AddMessage("MSG_1000_04", {"Price"})
                    Exit Sub
                End If
                If Convert.ToInt32(price) <= 0 Then
                    AddMessage("MSG_1000_06", {"Price"})
                    Exit Sub
                End If
                GRD_OPT.EditIndex = -1
                Dim dt As DataTable = New DataTable
                Dim sql As String
                sql = ""
                sql &= "UPDATE m_product_opt"
                sql &= "   SET product_opt_nm    = " & CommonDB.EncloseVal(name)
                sql &= "      ,product_opt_price = " & CommonDB.EncloseVal(price)
                sql &= "      ,note           = " & CommonDB.EncloseVal(note)
                sql &= "      ,upd_dt         = " & CommonDB.EncloseVal(System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"))
                sql &= "      ,upd_user_id    = 'admin'"
                sql &= "      ,upd_pgm_id     = 'MRA-FE-0032'"
                sql &= " WHERE product_opt_id = " & opt_id
                If Not CommonDB.ExecuteNonQuery(sql) = 1 Then
                    CommonDB.Rollback()
                End If
                CommonDB.Commit()
                showData()
                AddMessage("MSG_0001_02", {"Product Option"})
            Catch ex As Exception
                AddMessage("MSG_9000_01", {ex.Message})
            End Try
        End If
    End Sub
    Protected Sub OnRowDeleting(sender As Object, e As GridViewDeleteEventArgs)
        Dim CommonDB As CommonDB = New CommonDB
        ClearMessages()
        CommonDB.BeginTransaction()
        Try
            Dim opt_id As String = e.Keys("product_opt_id").ToString()
            Dim dt As DataTable = New DataTable
            Dim sql As String
            sql = ""
            sql &= "UPDATE m_product_opt"
            sql &= "   SET del_fg = '1'"
            sql &= "      ,upd_dt = " & CommonDB.EncloseVal(System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"))
            sql &= "      ,upd_user_id  = 'admin'"
            sql &= "      ,upd_pgm_id   = 'MRA-FE-0032'"
            sql &= " WHERE product_opt_id = " & opt_id
            If Not CommonDB.ExecuteNonQuery(sql) = 1 Then
                CommonDB.Rollback()
            End If
            CommonDB.Commit()
            showData()
            AddMessage("MSG_0001_03", {"Product Option"})
        Catch ex As Exception
            AddMessage("MSG_9000_01", {ex.Message})
        End Try
    End Sub
    Protected Sub OnCancel(sender As Object, e As EventArgs)
        GRD_OPT.EditIndex = -1
        showData()
    End Sub
    '--------update delete edit cancel row end---------
    '--------add row start---------
    Private Sub BTN_ADD_ROW_ServerClick(sender As Object, e As EventArgs) Handles BTN_ADD_ROW.ServerClick
        ScriptManager.RegisterStartupScript(Me.Page, Page.GetType(), "MyScript", "$('#addModal').modal('show');", True)
    End Sub
    Private Sub BTN_SAVE_Click(sender As Object, e As EventArgs) Handles BTN_SAVE.Click
        Dim CommonDB As CommonDB = New CommonDB
        ClearMessages()
        CommonDB.BeginTransaction()
        Dim strProductId As String = Request.QueryString("product_id")
        Dim name As String = TXT_OPT_NM.Text
        Dim price As String = TXT_OPT_PRICE.Text
        Dim numExp As New Regex("^[0-9-]*$")
        If name = "" Or price = "" Then
            AddMessage("MSG_1000_05", {"Name and Price"})
            Exit Sub
        End If
        If Not numExp.Match(price).Success Then
            AddMessage("MSG_1000_04", {"Price"})
            Exit Sub
        End If
        If Convert.ToInt32(price) <= 0 Then
            AddMessage("MSG_1000_06", {"Price"})
            Exit Sub
        End If
        Try
            Dim sql As String = ""
            sql = ""
            sql &= "INSERT INTO m_product_opt"
            sql &= "("
            sql &= "product_id"
            sql &= ",product_opt_nm"
            sql &= ",product_opt_price"
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
            sql &= " (" & Convert.ToInt32(strProductId)
            sql &= " ," & CommonDB.EncloseVal(name)
            sql &= " ," & CommonDB.EncloseVal(price)
            sql &= " ," & CommonDB.EncloseVal(TXT_OPT_NOTE.Text)
            sql &= " ," & CommonDB.EncloseVal(System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"))
            sql &= " ,'admin'"
            sql &= " ,'MRA-FE-0032'"
            sql &= " ,''"
            sql &= " ,''"
            sql &= " ,''"
            sql &= " ,'0'"
            sql &= ")"
            If Not CommonDB.ExecuteNonQuery(sql) = 1 Then
                CommonDB.Rollback()
            End If
            CommonDB.Commit()
            AddMessage("MSG_0001_01", {"Product option"})
            showData()
        Catch ex As Exception
            AddMessage("MSG_9000_01", {"Insert Product Option" & ex.Message})
        End Try
    End Sub
    '--------add row end---------
    'ShowData method for Displaying Data in Gridview
    Private Sub showData()
        Dim strProductId As String
        strProductId = Trim(LBL_ID.Text)
        Try
            If strProductId <> "" Then
                GRD_OPT.DataSource = BL.GetProductOptChosen(strProductId)
                GRD_OPT.DataBind()
            Else
            End If
        Catch ex As Exception
            AddMessage("MSG_9000_01", {ex.Message})
        End Try
    End Sub
    '=========Option Grid End

    Private Function ChkEmptyValidateInput() As Boolean
        Dim lstError As New List(Of String)

        Dim numExp As New Regex("^[0-9-]*$")
        Dim name_vn_on = TXT_NM_VN_ON.Text
        Dim name_jp_on = TXT_NM_JP_ON.Text
        Dim name_en_on = TXT_NM_EN_ON.Text
        Dim name_off = TXT_NM_OFF.Text
        Dim price_show = TXT_PRICE_SHOW.Text
        Dim price_off = TXT_PRICE.Text
        Try
            If String.IsNullOrEmpty(name_vn_on) Then
                AddMessage("MSG_9000_03", {"Product Name VN On"})
                lstError.Add(TXT_NM_VN_ON.ClientID)
            End If
            If name_jp_on = "" Then
                AddMessage("MSG_9000_03", {"Product Name JP On"})
                lstError.Add(TXT_NM_JP_ON.ClientID)
            End If
            If name_en_on = "" Then
                AddMessage("MSG_9000_03", {"Product Name JP On"})
                lstError.Add(TXT_NM_JP_ON.ClientID)
            End If
            If name_vn_on = "" Then
                AddMessage("MSG_9000_03", {"Product Name EN On"})
                lstError.Add(TXT_NM_EN_ON.ClientID)
            End If
            If name_off = "" Then
                AddMessage("MSG_9000_03", {"Product Name Off"})
                lstError.Add(TXT_NM_OFF.ClientID)
            End If

            If price_off = "" Then
                AddMessage("MSG_9000_03", {"Product Price"})
                lstError.Add(TXT_PRICE.ClientID)
            End If

            If price_show = "" Then
                AddMessage("MSG_9000_03", {"Product Price"})
                lstError.Add(TXT_PRICE_SHOW.ClientID)
            End If

            'validate input check
            If Not numExp.Match(price_show).Success Then
                AddMessage("MSG_1000_04", {"Price show"})
                lstError.Add(TXT_PRICE_SHOW.ClientID)
            End If
            If Not numExp.Match(price_off).Success Then
                AddMessage("MSG_1000_04", {"Price off"})
                lstError.Add(TXT_PRICE.ClientID)
            End If

            If Not String.IsNullOrEmpty(price_off) AndAlso IsNumeric(price_off) AndAlso Convert.ToInt32(price_off) <= 0 Then
                AddMessage("MSG_1000_06", {"Price"})
                lstError.Add(TXT_PRICE.ClientID)
            End If
            If Not String.IsNullOrEmpty(price_show) AndAlso IsNumeric(price_show) AndAlso Convert.ToInt32(price_show) <= 0 Then
                AddMessage("MSG_1000_06", {"Price show"})
                lstError.Add(TXT_PRICE_SHOW.ClientID)
            End If
            If lstError.Count > 0 Then
                SetError(Me.form1, lstError)
                Return False
            End If
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
End Class
