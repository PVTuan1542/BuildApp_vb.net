Imports System.Data.OleDb
Public Class QLHoaDon
    Private Con As OleDbConnection
    Private WithEvents danh_sach As BindingManagerBase
    Public lenh As String

    Private Sub QLHoaDon_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim constring As String = "Provider=SQLNCLI11;Data Source=PHAN_VAN_TUAN\SQLEXPRESS;
                        Integrated Security=SSPI;Initial Catalog=DataQLBanHang"
        Con = New OleDbConnection(constring)
        Xuat_ThongTin()
        danh_sach_moi(sender, e)
    End Sub
    Private Sub Xuat_ThongTin()
        Dim lenh As String
        'Khai báo câu lệnh truy vấn dùng để đọc bảng HoaDon
        lenh = "select * from tblHoaDon"
        'Khai báo đối tượng Command dùng để thực hiện câu lệnh truy vấn
        Dim cmd As New OleDbCommand(lenh, Con)
        'Trước khi đọc cần mở kết nối ra
        Con.Open()
        'Sử dụng phương thức ExecuteReader để đọc và trả về cho đối tượng DataReader
        Dim bang_doc As OleDbDataReader = cmd.ExecuteReader
        'Khai báo DataTable để nhận kết quả là các dòng đọc được
        Dim dttable As New DataTable("tblHoaDon")
        'Sử dụng phương thức Load và gởi vào DataReader để bắt đầu đọc các dòng ra DataTable
        dttable.Load(bang_doc, LoadOption.OverwriteChanges)
        'Sau khi đọc cần đóng kết nối lại
        Con.Close()
        DataGridView1.DataSource = dttable
        With Me.DataGridView1
            .Columns(0).Width = 150
            .Columns(0).HeaderText = "Mã hóa đơn"
            .Columns(1).Width = 100
            .Columns(1).HeaderText = "Mã khách hàng"
            .Columns(2).Width = 200
            .Columns(2).HeaderText = "Tên khách hàng"
            .Columns(3).Width = 100
            .Columns(3).HeaderText = "Mã sản phẩm"
            .Columns(4).Width = 300
            .Columns(4).HeaderText = "Tên sản phẩm"
            .Columns(5).Width = 100
            .Columns(5).HeaderText = "Số lượng mua"
            .Columns(6).Width = 100
            .Columns(6).HeaderText = "Thanh tiền"
        End With

        'khoi tao lien ket voi datatable.
        danh_sach = Me.BindingContext(dttable)
    End Sub

    Private Sub xuat()
        tbHD.Text = danh_sach.Current("MHD")
        tbMKH.Text = danh_sach.Current("MKH")
        tbTKH.Text = danh_sach.Current("TenKH")
        tbMSP.Text = danh_sach.Current("MSP")
        tbTSP.Text = danh_sach.Current("TenSP")
        tbSLM.Text = danh_sach.Current("SLMua")
        tbTT.Text = danh_sach.Current("ThanhTien")
    End Sub

    Private Sub danh_sach_moi(ByVal sender As Object, ByVal e As System.EventArgs) Handles danh_sach.PositionChanged
        xuat()
    End Sub
    Private Sub bt_Vedau_Click(sender As Object, e As EventArgs) Handles bt_Vedau.Click
        If danh_sach.Position = 0 Then
            MsgBox("Đã về Đầu")
        Else
            danh_sach.Position = 0
        End If
    End Sub

    Private Sub bt_Vetruoc_Click(sender As Object, e As EventArgs) Handles bt_Vetruoc.Click

        If danh_sach.Position = 0 Then
            MsgBox("Đã về đầu")
        Else
            danh_sach.Position -= 1
        End If
    End Sub

    Private Sub bt_Vesau_Click(sender As Object, e As EventArgs) Handles bt_Vesau.Click
        If danh_sach.Position = danh_sach.Count - 1 Then
            MsgBox("Đã về cuối")
        Else
            danh_sach.Position += 1
        End If
    End Sub

    Private Sub bt_Vecuoi_Click(sender As Object, e As EventArgs) Handles bt_Vecuoi.Click
        If danh_sach.Position = danh_sach.Count - 1 Then
            MsgBox(" Đã về cuối")
        Else
            danh_sach.Position = danh_sach.Count - 1
        End If
    End Sub
    Private Sub btThem_Click(sender As Object, e As EventArgs) Handles btThem.Click
        tbHD.Text = ""
        tbMKH.Text = ""
        tbTKH.Text = ""
        tbMSP.Text = ""
        tbTSP.Text = ""
        tbSLM.Text = ""
        tbTT.Text = ""
    End Sub

    Private Sub btLuu_Click(sender As Object, e As EventArgs) Handles btLuu.Click
        If MsgBox("Bạn có muốn lưu không?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, "Lưu") = MsgBoxResult.Yes Then
            If tbHD.Text = "" Or tbMKH.Text = "" Or tbTKH.Text = "" Or tbMSP.Text = "" Or
                 tbTSP.Text = "" Or tbSLM.Text = "" Or tbTT.Text = "" Then
                MsgBox("Chưa nhập giá trị!!!")
            Else
                lenh = "insert into tblHoaDon(MHD,MKH,TenKH,MSP,TenSP,SLMua,ThanhTien) values('" &
               tbHD.Text & "', '" & tbMKH.Text & "', N'" & tbTKH.Text & "', '" & tbMSP.Text & "', N'" & tbTSP.Text & "',
                '" & tbSLM.Text & "', '" & tbTT.Text & "')"
                Dim cmd As New OleDbCommand(lenh, Con)
                Con.Open()
                cmd.ExecuteNonQuery()
                Con.Close()
                Xuat_ThongTin()
                MsgBox(" Đã thêm!")
            End If
        End If
    End Sub
    Private Sub btSua_Click(sender As Object, e As EventArgs) Handles btSua.Click
        If MsgBox("Bạn có muốn sửa không ? ", MsgBoxStyle.Question + MsgBoxStyle.YesNo, "Sửa ") = MsgBoxResult.Yes Then
            If tbHD.Text = "" Or tbMKH.Text = "" Or tbTKH.Text = "" Or tbMSP.Text = "" Or
                 tbTSP.Text = "" Or tbSLM.Text = "" Or tbTT.Text = "" Then
                MsgBox("Nhập Giá Trị Cần Sửa !!! ")
            Else
                lenh = "Update tblHoaDon set MHD = '" & tbHD.Text & "',
                                         MKH = '" & tbMKH.Text & "',
                                         TenKH    = N'" & tbTKH.Text & "',
                                         MSP     = '" & tbMSP.Text & "',
                                         TenSP      = N'" & tbTSP.Text & "',
                                         SLMua = '" & tbSLM.Text & "',
                                         ThanhTien = '" & tbTT.Text & "' where MHD = '" & Trim(tbHD.Text) & "'"
                Dim cmd As New OleDbCommand(lenh, Con)
                Con.Open()
                cmd.ExecuteNonQuery()
                Con.Close()
                Xuat_ThongTin()
                MsgBox("Sửa thành công")
            End If
        End If
    End Sub
    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Me.Close()
    End Sub

    Private Sub btXoa_Click(sender As Object, e As EventArgs) Handles btXoa.Click
        If MsgBox("Bạn có muốn xóa không ?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, "Xóa") = MsgBoxResult.Yes Then
            If tbMKH.Text = "" Then
                MsgBox(" Phải Chọn Giá Trị Cần Xóa !!! ")
            Else
                lenh = " Delete from tblHoaDon where MHD = '" & tbHD.Text & "' and MHD = '" & Trim(tbHD.Text) & "'"
                Dim cmd As New OleDbCommand(lenh, Con)
                Con.Open()
                cmd.ExecuteNonQuery()
                Con.Close()
                Xuat_ThongTin()
                MsgBox("Xóa thành công")
            End If
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        ThongKe.Show()
    End Sub
End Class