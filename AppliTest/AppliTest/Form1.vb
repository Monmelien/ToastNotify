
Public Class Form1
    Public WithEvents test As New ToastNotify.Toast

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        Dim test2 As New List(Of String)

        For Each line In TextBox1.Lines
            test2.Add(line)
        Next
        test.text = test2

        Select Case ComboBox1.SelectedIndex
            Case 0
                test.Type = ToastNotify.Toast.ToastType.ImageAndText01
                test.image = TextBox2.Text
            Case 1
                test.Type = ToastNotify.Toast.ToastType.ImageAndText02
                test.image = TextBox2.Text
            Case 2
                test.Type = ToastNotify.Toast.ToastType.ImageAndText03
                test.image = TextBox2.Text
            Case 3
                test.Type = ToastNotify.Toast.ToastType.ImageAndText04
                test.image = TextBox2.Text
            Case 4
                test.Type = ToastNotify.Toast.ToastType.Text01
                test.image = ""
            Case 5
                test.Type = ToastNotify.Toast.ToastType.Text02
                test.image = ""
            Case 6
                test.Type = ToastNotify.Toast.ToastType.Text03
                test.image = ""
            Case 7
                test.Type = ToastNotify.Toast.ToastType.Text04
                test.image = ""
        End Select


        test.Audio = ComboBox2.Text
        test.show()

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


    Private Sub Button2_Click(sender As Object, e As EventArgs)
        MsgBox(test.show2())
    End Sub

    Private Sub ComboBox2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox2.SelectedIndexChanged

    End Sub
End Class
