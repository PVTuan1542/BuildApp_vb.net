Public Class Main
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        TabControl1.SelectTab(0)
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        TabControl1.SelectTab(1)
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        TabControl1.SelectTab(2)
    End Sub

    Private Sub btSP_Click(sender As Object, e As EventArgs) Handles btSP.Click
        QLSanPham.Show()
    End Sub

    Private Sub btKh_Click(sender As Object, e As EventArgs) Handles btKh.Click
        QLKhachHang.Show()
    End Sub

    Private Sub btHD_Click(sender As Object, e As EventArgs) Handles btHD.Click
        QLHoaDon.Show()
    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        Me.Close()
    End Sub
End Class
