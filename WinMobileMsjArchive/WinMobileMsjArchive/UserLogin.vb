Public Class UserLogin

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        End
    End Sub

    Private Sub btnOk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOk.Click
        Dim Login As GrilToDatabase = New GrilToDatabase
        'UserID Public degi�kene yaz�l�r..
        Module1.UserID = Login.LoginPasswordControl(Me.txtUserName.Text, Me.txtPassword.Text)
        'UserID<>0 d�nerse girilen UserName ge�erli bir UserName'dir..
        If Not Module1.UserID = 0 Then
            'Kullan�c� ad� Publiv UserName adl� de�i�kene aktar�larak saklan�r..
            Module1.UserName = Me.txtUserName.Text
            'Di�er form'a ge�ilir..
            Dim frm As MsjWindow = New MsjWindow
            frm.Show()
            'UserName ve Password alanlar� temizlenerek kullan�c� ad� ve parola g�venli�i sa�lan�r..
            Me.txtPassword.Text = ""
            Me.txtUserName.Text = ""
            'Login formu g�r�nmez yap�l�r..
            Me.Visible = False
        Else
            Module1.UserName = ""
            MessageBox.Show("L�tfen Ge�erli Bir Kullan�c� Ad� ve �ifre Giriniz!!", "Ge�ersiz Giri�", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1)
        End If
    End Sub
End Class