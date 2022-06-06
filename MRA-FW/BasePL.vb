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
    Private Const _PAGE_DATA As String = "PAGE_DATA"

    Public Event InitLoad As EventHandler
    Private _isInitialLoad As Boolean
    Protected Overrides Sub OnLoad(e As EventArgs)
        If _isInitialLoad Then
            RaiseEvent InitLoad(Me, e)
            Exit Sub
        End If
        MyBase.OnLoad(e)
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

