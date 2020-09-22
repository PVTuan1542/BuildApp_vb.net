Imports System.Data.OleDb
Public Class ThongKe

    Private Con As OleDbConnection
    Private WithEvents danh_sach As BindingManagerBase
    Public lenh As String

    Private Sub ThongKe_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim constring As String = "Provider=SQLNCLI11;Data Source=PHAN_VAN_TUAN\SQLEXPRESS;
                        Integrated Security=SSPI;Initial Catalog=DataQLBanHang"
        Con = New OleDbConnection(constring)

        'danh_sach_moi(sender, e)
       ' Xuat_ThongTin()
        ComboBox1.Items.Add("Thống kê hóa đơn thanh tiền lớn hơn 10000")
        ComboBox1.Items.Add("Thống kê thông tin khách hàng có tổng thanh tiền lớn hơn 10000")
        ComboBox1.Items.Add("Thống kê thông tin khách hàng, tổng số lần mua và tổng thanh tiền của khách")
    End Sub
    Private Sub Xuat_ThongTin()
        Dim lenh As String
        'Khai báo câu lệnh truy vấn 

        lenh = " "
        If ComboBox1.Text = "Thống kê hóa đơn thanh tiền lớn hơn 10000" Then
            lenh = "select hd.MHD,hd.MKH,hd.TenKH,hd.MSP ,hd.TenSP,hd.SLMua,hd.ThanhTien as N'TThanhTien'
                    from tblHoaDon as hd
                     where hd.ThanhTien > 10000"
        End If

        If ComboBox1.Text = "Thống kê thông tin khách hàng có tổng thanh tiền lớn hơn 10000" Then
            lenh = "select kh.MKH,kh.TenKH,kh.NgaySinh ,kh.GioiTinh,kh.SDT,sum(hd.ThanhTien) as N'TThanhTien'
                    from tblHoaDon as hd, tblKhachHang as kh
                     where hd.MKH=kh.MKH
                    group by kh.MKH,kh.TenKH,kh.NgaySinh ,kh.GioiTinh,kh.SDT 
                     having sum(hd.ThanhTien) > 10000 "
        End If
        If ComboBox1.Text = "Thống kê thông tin khách hàng, tổng số lần mua và tổng thanh tiền của khách" Then

            lenh = "select kh.MKH,kh.TenKH,kh.NgaySinh ,kh.GioiTinh,kh.SDT,count(hd.MKH),sum(hd.ThanhTien) as N'TThanhTien'
                    from tblHoaDon as hd, tblKhachHang as kh
                     where hd.MKH=kh.MKH
                      group by  kh.MKH, kh.TenKH, kh.NgaySinh, kh.GioiTinh, kh.SDT
                       
                    "
        End If

        'Khai báo đối tượng Command dùng để thực hiện câu lệnh truy vấn 
        Dim cmd As New OleDbCommand(lenh, Con)
        'Trước khi đọc cần mở kết nối ra
        Con.Open()
        'Sử dụng phương thức ExecuteReader để đọc và trả về cho đối tượng DataReader
        Dim bang_doc As OleDbDataReader = cmd.ExecuteReader
        'Khai báo DataTable để nhận kết quả là các dòng đọc được
        Dim dttable As New DataTable("tblTK")
        'Sử dụng phương thức Load và gởi vào DataReader để bắt đầu đọc các dòng ra DataTable
        dttable.Load(bang_doc, LoadOption.OverwriteChanges)
        'Sau khi đọc cần đóng kết nối lại
        Con.Close()
        DataGridView1.DataSource = dttable

        If ComboBox1.Text = "Thống kê hóa đơn thanh tiền lớn hơn 10000" Then
            With Me.DataGridView1
                .Columns(0).Width = 150
                .Columns(0).HeaderText = "Mã hóa đơn"
                .Columns(1).Width = 100
                .Columns(1).HeaderText = "Mã khách hàng"
                .Columns(2).Width = 250
                .Columns(2).HeaderText = "Tên khách hàng"
                .Columns(3).Width = 100
                .Columns(3).HeaderText = "Mã sản phẩm"
                .Columns(4).Width = 250
                .Columns(4).HeaderText = "Tên sản phẩm"
                .Columns(5).Width = 100
                .Columns(5).HeaderText = "Số lượng mua"
                .Columns(6).Width = 100
                .Columns(6).HeaderText = "Thanh tiền"

            End With
        End If
        If ComboBox1.Text = "Thống kê thông tin khách hàng có tổng thanh tiền lớn hơn 10000" Then
            With Me.DataGridView1
                .Columns(0).Width = 150
                .Columns(0).HeaderText = "Mã khách hàng"
                .Columns(1).Width = 300
                .Columns(1).HeaderText = "Tên khách hàng"
                .Columns(2).Width = 150
                .Columns(2).HeaderText = "Ngày sinh"
                .Columns(3).Width = 150
                .Columns(3).HeaderText = "Giới tính"
                .Columns(4).Width = 150
                .Columns(4).HeaderText = "SĐT"
                .Columns(5).Width = 150
                .Columns(5).HeaderText = "Tổng thanh tiền"
            End With
        End If

        If ComboBox1.Text = "Thống kê thông tin khách hàng, tổng số lần mua và tổng thanh tiền của khách" Then
            With Me.DataGridView1
                .Columns(0).Width = 150
                .Columns(0).HeaderText = "Mã khách hàng"
                .Columns(1).Width = 300
                .Columns(1).HeaderText = "Tên khách hàng"
                .Columns(2).Width = 150
                .Columns(2).HeaderText = "Ngày sinh"
                .Columns(3).Width = 100
                .Columns(3).HeaderText = "Giới tính"
                .Columns(4).Width = 150
                .Columns(4).HeaderText = "SĐT"
                .Columns(5).Width = 100
                .Columns(5).HeaderText = "Tổng số lần mua"
                .Columns(6).Width = 100
                .Columns(6).HeaderText = "Tổng thanh tiền"
            End With
        End If

        'khoi tao lien ket voi datatable.
        danh_sach = Me.BindingContext(dttable)
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Xuat_ThongTin()
        Dim i%, s&
        s = 0
        For i = 0 To danh_sach.Count - 1
            danh_sach.Position = i
            s += danh_sach.Current("TThanhTien")
        Next
        TextBox1.Text = s.ToString()

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Close()

    End Sub
End Class