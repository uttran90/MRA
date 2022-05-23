Public Class PageData
    Public _PageData As Dictionary(Of String, Object)
    Public Sub New()
        _PageData = New Dictionary(Of String, Object)
    End Sub

    Public Sub SetItem(ByVal id As String, ByVal val As Object)
        If _PageData.ContainsKey(id) Then
            _PageData(id) = val
        Else
            _PageData.Add(id, val)
        End If
    End Sub

    Public Function GetItem(ByVal id As String) As Object
        If _PageData.ContainsKey(id) Then
            Return _PageData(id)
        End If
    End Function

End Class