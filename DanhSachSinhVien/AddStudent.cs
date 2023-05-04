using DanhSachSinhVien.Models;
using DevExpress.XtraEditors.DXErrorProvider;
using DevExpress.XtraEditors.Mask;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Principal;
using System.Windows.Forms;

namespace DanhSachSinhVien
{
    public partial class AddStudent : Form
    {
        private List<SinhVien> SinhVienList = new List<SinhVien>();
        private DXErrorProvider errorProvider = new DXErrorProvider();
        private RepositoryItemComboBox comboBoxGender = new RepositoryItemComboBox();
        private RepositoryItemDateEdit dateEditBirthday = new RepositoryItemDateEdit();
        private RepositoryItemComboBox comboBoxObjectType = new RepositoryItemComboBox();
        public AddStudent()
        {
            InitializeComponent();
            //khoi tao control
            InitControls();
        }
        void InitControls()
        {
            //khoi tao repo cho cot gender
            cbbGioiTinh.Items.AddRange(new string[] { "Nam", "Nữ", "Khác" });
            cbbDoiTuong.Items.AddRange(new string[] { "Hộ nghèo", "Thương binh", "Tàn tật", "Phá sản" });
            mme_GhiChu.Lines = new string[] { };
            //them cot vao grid control
            gridView2.Columns.Add(new DevExpress.XtraGrid.Columns.GridColumn() { Caption = "Mã sinh viên", FieldName = "MaSinhVien", Visible = true });
            gridView2.Columns.Add(new DevExpress.XtraGrid.Columns.GridColumn() { Caption = "Họ tên", FieldName = "HoTen", Visible = true });
            gridView2.Columns.Add(new DevExpress.XtraGrid.Columns.GridColumn() { Caption = "Giới tính", FieldName = "GioiTinh", Visible = true, ColumnEdit = comboBoxGender });
            gridView2.Columns.Add(new DevExpress.XtraGrid.Columns.GridColumn() { Caption = "Ngày sinh", FieldName = "NgaySinh", Visible = true, ColumnEdit = dateEditBirthday });
            gridView2.Columns.Add(new DevExpress.XtraGrid.Columns.GridColumn() { Caption = "Đối tượng", FieldName = "DoiTuong", Visible = true, ColumnEdit = comboBoxObjectType });
            gridView2.Columns.Add(new DevExpress.XtraGrid.Columns.GridColumn() { Caption = "Điểm toán", FieldName = "DiemToan", Visible = true });
            gridView2.Columns.Add(new DevExpress.XtraGrid.Columns.GridColumn() { Caption = "Điểm văn", FieldName = "DiemVan", Visible = true });
            gridView2.Columns.Add(new DevExpress.XtraGrid.Columns.GridColumn() { Caption = "Điểm anh", FieldName = "DiemAnh", Visible = true });
            gridView2.Columns.Add(new DevExpress.XtraGrid.Columns.GridColumn() { Caption = "Ghi chú", FieldName = "GhiChu", Visible = true });
        }
        private bool CheckDuplicate(string maSV)
        {
            foreach (SinhVien sv in SinhVienList)
            {
                if (sv.MaSinhVien == maSV)
                    return true;
            }
            return false;
        }

        private void btn_Them_Click(object sender, EventArgs e)
        {
            SinhVien sv = new SinhVien();
            sv.MaSinhVien = txtMaSV.Text;
            if (CheckDuplicate(txtMaSV.Text))
            {
                MessageBox.Show("Mã sinh viên đã tồn tại!");
                return;
            }
            sv.HoTen = txtHoTen.Text;
            sv.GioiTinh = cbbGioiTinh.SelectedItem.ToString();
            sv.NgaySinh = dateNgaySinh.DateTime;
            sv.DoiTuong = cbbDoiTuong.Text;
            sv.DiemToan = Convert.ToDouble(txt_DiemToan.Text);
            sv.DiemVan = Convert.ToDouble(txtDiemVan.Text);
            sv.DiemAnh = Convert.ToDouble(txtDiemAnh.Text);
            sv.GhiChu = mme_GhiChu.Text;
            SinhVienList.Add(sv);
            // load data into the grid control
            gdc_DanhSachSV.DataSource = SinhVienList;
            gdc_DanhSachSV.RefreshDataSource();
        }
        private void btn_Luu_Click(object sender, EventArgs e)
        {
            //save data grid control file txt
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            saveFileDialog.ShowDialog();

            if (saveFileDialog.FileName != "")
            {
                using (StreamWriter writer = new StreamWriter(saveFileDialog.FileName))
                {
                    foreach (SinhVien sv in SinhVienList)
                    {
                        writer.WriteLine($"{sv.MaSinhVien}\t{sv.HoTen}\t{sv.GioiTinh}\t{sv.NgaySinh}\t{sv.DoiTuong}\t{sv.DiemToan}\t{sv.DiemVan}\t{sv.DiemAnh}");
                    }
                    MessageBox.Show("Lưu dữ liệu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
        private void gdc_DanhSachSV_Load(object sender, EventArgs e)
        {
            gdc_DanhSachSV.DataSource = SinhVienList;
        }

        //khi click vào 1 dòng sẽ tự động fill dữ liệu vào controls
        private void gridView2_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            //lay sinh vien duoc chon tu controls
            SinhVien sv = gridView2.GetRow(e.FocusedRowHandle) as SinhVien;
            //neu khong co sinh vien duoc chon thi thoat khoi phuong thuc
            if (sv == null) return;
            //fill du lieu vao cac control
            txtMaSV.Text = sv.MaSinhVien;
            txtHoTen.Text = sv.HoTen;
            cbbGioiTinh.SelectedItem = sv.GioiTinh;
            dateNgaySinh.DateTime = sv.NgaySinh;
            cbbDoiTuong.Text = sv.DoiTuong;
            txt_DiemToan.Text = sv.DiemToan.ToString();
            txtDiemVan.Text = sv.DiemVan.ToString();
            txtDiemAnh.Text = sv.DiemAnh.ToString();
            mme_GhiChu.Text = sv.GhiChu;
        }

        private void btnSua_Click(object sender, EventArgs e)
        {          

        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            //xoa theo truong duoc chon
            int rowIndex = gridView2.FocusedRowHandle;
            SinhVienList.RemoveAt(rowIndex);
            gdc_DanhSachSV.DataSource = SinhVienList;
            gridView2.RefreshData();
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            //lam moi trang
            txtMaSV.Text = "";
            txtHoTen.Text = "";
            cbbGioiTinh.Text = "";
            cbbDoiTuong.Text = "";
            dateNgaySinh.Text = "";
            txt_DiemToan.Text = "";
            txtDiemVan.Text = "";
            txtDiemAnh.Text = "";
            mme_GhiChu.Text = "";
        }

        private void gridView2_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            // Lấy thông tin sinh viên đã được chỉnh sửa
            SinhVien sinhVien = gridView1.GetRow(e.RowHandle) as SinhVien;

            if (sinhVien != null)
            {
                // Cập nhật thông tin sinh viên đã được chỉnh sửa
                if (e.Column.FieldName == "MaSinhVien")
                {
                    sinhVien.MaSinhVien = e.Value.ToString();
                }
                else if (e.Column.FieldName == "HoTen")
                {
                    sinhVien.HoTen = e.Value.ToString();
                }
                else if (e.Column.FieldName == "GioiTinh")
                {
                    sinhVien.GioiTinh = e.Value.ToString();
                }
                else if (e.Column.FieldName == "NgaySinh")
                {
                    sinhVien.NgaySinh = (DateTime)e.Value;
                }
                else if (e.Column.FieldName == "DoiTuong")
                {
                    sinhVien.DoiTuong = e.Value.ToString();
                }
                else if (e.Column.FieldName == "DiemToan")
                {
                    sinhVien.DiemToan = Convert.ToDouble(e.Value);
                }
                else if (e.Column.FieldName == "DiemVan")
                {
                    sinhVien.DiemVan = Convert.ToDouble(e.Value);
                }
                else if (e.Column.FieldName == "DiemAnh")
                {
                    sinhVien.DiemAnh = Convert.ToDouble(e.Value);
                }
            }
        }

        private void btn_TimKiem_Click(object sender, EventArgs e)
        {
            //tim kiem sinh vien theo MaSinhVien
            string searchValue = txt_TimKiem.Text;
            var result = SinhVienList.Where(sv => sv.MaSinhVien.Contains(searchValue)).ToList();
            gdc_DanhSachSV.DataSource = result;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Tim sinh vien theo {DoiTuong}
            Dictionary<string, int> dict = new Dictionary<string, int>();
            foreach (SinhVien sv in SinhVienList)
            {
                if (dict.ContainsKey(sv.DoiTuong))
                {
                    dict[sv.DoiTuong]++;
                }
                else
                {
                    dict.Add(sv.DoiTuong, 1);
                }
            }

            string result = "";
            foreach (var pair in dict)
            {
                result += pair.Key + ": " + pair.Value + "\n";
            }
            MessageBox.Show(result, "Tổng số sinh viên theo từng đối tượng");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //tim tong so sinh vien trong gridcontrol
            int tongSoSV = SinhVienList.Count;
            MessageBox.Show("Tổng số sinh viên : " + tongSoSV);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Lọc danh sách sinh viên có điểm trung bình lớn hơn 8.0 và đếm số lượng sinh viên
            int count = SinhVienList.Count(sv => (sv.DiemToan + sv.DiemVan + sv.DiemAnh) / 3.0 > 8.0);

            // Hiển thị kết quả lên một Label
            lblTongSVThang8.Text = $"Tổng số sinh viên có thành tích trên 8 : {count}";
        }

        private void txt_DiemToan_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            double diem;
            if (double.TryParse(txt_DiemToan.Text, out diem))
            {
                if (diem > 10)
                {
                    e.Cancel = true;
                    txt_DiemToan.ErrorText = "Điểm không được lớn hơn 10";
                }
            }
        }

        private void txtDiemVan_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            double diem;
            if (double.TryParse(txtDiemVan.Text, out diem))
            {
                if (diem > 10)
                {
                    e.Cancel = true;
                    txtDiemVan.ErrorText = "Điểm không được lớn hơn 10";
                }
            }
        }

        private void txtDiemAnh_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            double diem;
            if (double.TryParse(txtDiemAnh.Text, out diem))
            {
                if (diem > 10)
                {
                    e.Cancel = true;
                    txtDiemAnh.ErrorText = "Điểm không được lớn hơn 10";
                }
            }
        }

        private void txtMaSV_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            string maSV = txtMaSV.Text.Trim();
            if (!maSV.StartsWith("SV"))
            {
                e.Cancel = true;
                txtMaSV.ErrorText = "Mã sinh viên phải bắt đầu bằng 'SV'";
            }
            else
            {
                txtMaSV.ErrorText = "";
            }
        }

        private void txtHoTen_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtHoTen.Text))
            {
                e.Cancel = true;
                errorProvider.SetError(txtHoTen, "Bạn phải nhập họ tên.");
            }
            else
            {
                e.Cancel = false;
                errorProvider.SetError(txtHoTen, "");
            }
        }

        private void cbbGioiTinh_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }
    }
}