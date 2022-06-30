Imports System
Imports System.Reflection
Public Class BasePL
    Inherits System.Web.UI.Page

    Public Enum State
        ''' <summary>
        ''' 初期表示状態
        ''' ヘッダ項目がある場合はヘッダ入力可、ボディ入力不可
        ''' </summary>
        Init = 0

        ''' <summary>
        ''' ボディ入力可能状態
        ''' </summary>
        ''' 
        Enter = 1

        ''' <summary>
        ''' 実行確認状態
        ''' </summary>
        Check = 2
    End Enum
    ''' <summary>
    ''' 画面状態判定
    ''' </summary>
    Public PageState As State = State.Init
    Public PageData As New PageData
    Private Const _MRA_PAGE_DATA As String = "MRA_PAGE_DATA"

    Public Event InitLoad As EventHandler
    Private _isInitialLoad As Boolean
    Protected Overrides Sub OnLoad(e As EventArgs)
        If _isInitialLoad Then
            RaiseEvent InitLoad(Me, e)
            Exit Sub
        End If
        MyBase.OnLoad(e)
    End Sub

    Private Sub BasePL_Load(sender As Object, e As EventArgs) Handles Me.Load
    End Sub

    Private Sub BasePL_PreLoad(sender As Object, e As EventArgs) Handles Me.PreLoad
        If Page.IsPostBack Then
            _isInitialLoad = True
        Else
            _isInitialLoad = True
        End If
        PageState = ViewState("PageState")
    End Sub

    Private Sub BasePL_PreRenderComplete(sender As Object, e As EventArgs) Handles Me.PreRenderComplete

    End Sub

    Protected Overrides Sub OnPreRender(e As EventArgs)

        MyBase.OnPreRender(e)
    End Sub

    ''' <summary>
    ''' フォルムのエラーアイテムをクリアする。
    ''' </summary>
    Public Sub ClearError(ByRef form1 As HtmlForm)
        Dim typeName As String
        For Each item As Object In form1.Controls
            typeName = item.GetType.Name
            Select Case typeName
                Case "TextBox"
                    With DirectCast(item, System.Web.UI.WebControls.TextBox)
                        .Style("border-color") = ""
                    End With
                Case "HtmlInputText"
                    With DirectCast(item, System.Web.UI.HtmlControls.HtmlInputText)
                        .Style("border-color") = ""
                    End With
                Case "HtmlInputGenericControl"
                    With DirectCast(item, System.Web.UI.HtmlControls.HtmlInputGenericControl)
                        .Style("border-color") = ""
                    End With
            End Select
        Next
    End Sub

    ''' <summary>
    ''' フォルムのエラーアイテムをセットする。
    ''' </summary>
    ''' <param name="lstName">アイテムのIDリスト</param>
    Public Sub SetError(ByRef form1 As HtmlForm, ByVal lstName As List(Of String))
        Dim itemIndex As Integer = 0
        Dim itemNm As String
        Dim item As Object
        Dim typeName As String

        For Each itemNm In lstName
            For Each item In form1.Controls
                typeName = item.GetType.Name
                Select Case typeName
                    Case "TextBox"
                        With DirectCast(item, System.Web.UI.WebControls.TextBox)
                            If .ID = itemNm Then
                                .Style("border-color") = "red"
                                If itemIndex = 0 Then
                                    .Focus()
                                End If
                                itemIndex += 1
                            End If
                        End With
                    Case "HtmlInputText"
                        With DirectCast(item, System.Web.UI.HtmlControls.HtmlInputText)
                            If .ID = itemNm Then
                                .Style("border-color") = "red"
                                If itemIndex = 0 Then
                                    .Focus()
                                End If
                                itemIndex += 1
                            End If
                        End With
                    Case "HtmlInputGenericControl"
                        With DirectCast(item, System.Web.UI.HtmlControls.HtmlInputGenericControl)
                            If .ID = itemNm Then
                                .Style("border-color") = "red"
                                If itemIndex = 0 Then
                                    .Focus()
                                End If
                                itemIndex += 1
                            End If
                        End With
                End Select
            Next
        Next
    End Sub
    Public Sub CleaAllTextBox(ByRef form1 As HtmlForm)
        Dim typeName As String
        For Each item As Object In form1.Controls
            typeName = item.GetType.Name
            Select Case typeName
                Case "TextBox"
                    With DirectCast(item, System.Web.UI.WebControls.TextBox)
                        .Text = ""
                    End With
            End Select
        Next
    End Sub
End Class
' カスタム属性クラス
<AttributeUsage(AttributeTargets.Class, AllowMultiple:=False)>
Public Class PageAttribute
    Inherits Attribute

    Public Enum types
        parent = 0
        child = 1
    End Enum

    Private _name As String
    Public pageType As types

    Public Property Name As String
        Get
            Return _name
        End Get
        Set(ByVal value As String)
            _name = value
        End Set
    End Property

    Public Sub New(ByVal name As String)
        Me._name = name
    End Sub

End Class

