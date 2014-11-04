
Public Class Form1
    Public WithEvents test As ToastNotify.Toast

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim TypeToast As ToastNotify.Toast.ToastType

        Select Case ComboBox1.SelectedIndex
            Case 0
                TypeToast = ToastNotify.Toast.ToastType.ImageAndText01
            Case 1
                TypeToast = ToastNotify.Toast.ToastType.ImageAndText02
            Case 2
                TypeToast = ToastNotify.Toast.ToastType.ImageAndText03
            Case 3
                TypeToast = ToastNotify.Toast.ToastType.ImageAndText04
            Case 4
                TypeToast = ToastNotify.Toast.ToastType.Text01
            Case 5
                TypeToast = ToastNotify.Toast.ToastType.Text02
            Case 6
                TypeToast = ToastNotify.Toast.ToastType.Text03
            Case 7
                TypeToast = ToastNotify.Toast.ToastType.Text04
        End Select
        test = New ToastNotify.Toast(TypeToast)

        Dim test2 As New List(Of String)

        For Each line In TextBox1.Lines
            test2.Add(line)
        Next



        test.text = test2

        Dim test3 As List(Of String)
        test3 = test.text
        For Each line In test3
            MsgBox(line)
        Next


        MessageBox.Show("|" & test3.Count & "|" & vbNewLine & test.GetXml())
        'TextBox2.Text = test.text1
        'test.show()

    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged

    End Sub

    Private Sub toastActivated(sender As Object, e As ToastNotify.ToastEventArgs) Handles test.toastActivated
        'MsgBox("Notification activé")
    End Sub

    Private Sub toastDismissed(sender As Object, e As ToastNotify.ToastDismissEventArgs) Handles test.toastDismissed
        'MsgBox("Notification annulé")
    End Sub

    Private Sub toastFailed(sender As Object, e As ToastNotify.ToastFailEventArgs) Handles test.toastFailed
        'MsgBox("Notification echoué")
    End Sub

    Private Sub ComboBox2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox2.SelectedIndexChanged

    End Sub
End Class
