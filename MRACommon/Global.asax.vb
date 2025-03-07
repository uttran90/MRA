﻿Imports System.Web.SessionState
Imports System.IO

Public Class Global_asax
    Inherits System.Web.HttpApplication

    Sub Application_Start(ByVal sender As Object, ByVal e As EventArgs)
        ' Fires when the application is started
        ' メッセージファイルを読み込み
        Dim msgFile As String = ConfigurationManager.AppSettings("MSG_FILE")
        Dim document As New System.Xml.XmlDocument
        document.Load(msgFile)

        Dim messages As System.Xml.XmlNodeList = document.SelectNodes("//en/msg")

        Dim dic As New Dictionary(Of String, String())

        For Each msg As System.Xml.XmlElement In messages
            dic.Add(msg.GetAttribute("id"), {msg.GetAttribute("type"), msg.InnerText})
        Next

        Application("G_MESSAGES") = dic
    End Sub

    Sub Session_Start(ByVal sender As Object, ByVal e As EventArgs)
        ' Fires when the session is started
    End Sub

    Sub Application_BeginRequest(ByVal sender As Object, ByVal e As EventArgs)
        ' Fires at the beginning of each request
    End Sub

    Sub Application_AuthenticateRequest(ByVal sender As Object, ByVal e As EventArgs)
        ' Fires upon attempting to authenticate the use
    End Sub

    Sub Application_Error(ByVal sender As Object, ByVal e As EventArgs)
        ' Fires when an error occurs
    End Sub

    Sub Session_End(ByVal sender As Object, ByVal e As EventArgs)
        ' Fires when the session ends
    End Sub

    Sub Application_End(ByVal sender As Object, ByVal e As EventArgs)
        ' Fires when the application ends
    End Sub

End Class