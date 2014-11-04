
Public Class Form1
    Public WithEvents test As New ToastNotify.Toast(ToastNotify.Toast.ToastType.ImageAndText04)

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        Dim test2 As New List(Of String)

        For Each line In TextBox1.Lines
            test2.Add(line)
        Next

        test.text1 = "test"
        test.text2 = "Test 2"
        test.text3 = "Test 3"

        MessageBox.Show(test.getXml())
        TextBox2.Text = test.text1
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
