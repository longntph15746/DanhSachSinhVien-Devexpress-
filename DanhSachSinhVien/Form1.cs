using DanhSachSinhVien.Models;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.DXErrorProvider;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraVerticalGrid;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DanhSachSinhVien
{
    public partial class Form1 : Form
    {
        private List<SinhVien> lstSinhVien = new List<SinhVien>();
        private DXErrorProvider errorProvider = new DXErrorProvider();
            
        public Form1()
        {
            InitializeComponent();

        }
        static int countSV = 0;

        private string GenerateMaSV()
        {
            countSV++; // Tăng số lượng sinh viên lên 1
            return "SV" + countSV.ToString("000"); // Sinh mã sinh viên tự động
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            SinhVien sv = new SinhVien();
            sv.MaSinhVien = GenerateMaSV();
            sv.HoTen = txtHoTen.Text;
            sv.GioiTinh = cboGioiTinh.SelectedItem.ToString();
            sv.DoiTuong = cboDoiTuong.SelectedItem.ToString();
            sv.DiemToan = (txtDiemToan.Text);
            sv.DiemVan = (txtDiemVan.Text);
            sv.DiemAnh = (txtDiemAnh.Text);
            sv.GhiChu = mmGhiChu.Text;
            lstSinhVien.Add(sv);
            gdcSinhVien.DataSource = lstSinhVien;
            gdcSinhVien.RefreshDataSource();
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            //lay sinh vien duoc chon tu controls
            SinhVien sv = gridView1.GetRow(e.FocusedRowHandle) as SinhVien;
            //neu khong co sinh vien duoc chon thi thoat khoi phuong thuc
            if (sv == null) return;
            //fill du lieu vao cac control
            txtHoTen.Text = sv.HoTen;
            cboGioiTinh.SelectedItem = sv.GioiTinh;
            dateNgaySinh.DateTime = sv.NgaySinh;
            cboDoiTuong.SelectedItem = sv.DoiTuong;
            txtDiemToan.Text = sv.DiemToan.ToString();
            txtDiemVan.Text = sv.DiemVan.ToString();
            txtDiemAnh.Text = sv.DiemAnh.ToString();
            mmGhiChu.Text = sv.GhiChu;
        }

        private void gridView1_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            // Lấy thông tin sinh viên đã được chỉnh sửa
            SinhVien sinhVien = gridView1.GetRow(e.RowHandle) as SinhVien;
            if (sinhVien != null)
            {
                // Cập nhật thông tin sinh viên đã được chỉnh sửa
                if (e.Column.FieldName == "HoTen")
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
                    sinhVien.DiemToan = e.Value.ToString();
                }
                else if (e.Column.FieldName == "DiemVan")
                {
                    sinhVien.DiemVan = e.Value.ToString();
                }
                else if (e.Column.FieldName == "DiemAnh")
                {
                    sinhVien.DiemAnh = e.Value.ToString();
                }
                else if (e.Column.FieldName == "GhiChu")
                {
                    sinhVien.GhiChu = e.Value.ToString();
                }
            }

        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            //xoa theo truong duoc chon
            int rowIndex = gridView1.FocusedRowHandle;
            lstSinhVien.RemoveAt(rowIndex);
            gdcSinhVien.DataSource = lstSinhVien;
            gridView1.RefreshData();
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            //tim kiem sinh vien theo MaSinhVien
            string searchValue = txtTimkiem.Text;
            var result = lstSinhVien.Where(sv => sv.MaSinhVien.Contains(searchValue)).ToList();
            gdcSinhVien.DataSource = result;
        }

        private void bbiThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SinhVien sv = new SinhVien();
            sv.MaSinhVien = GenerateMaSV();
            sv.HoTen = txtHoTen.Text;
            sv.GioiTinh = cboGioiTinh.Text;
            sv.DoiTuong = cboDoiTuong.Text;
            sv.DiemToan = (txtDiemToan.Text);
            sv.DiemVan = (txtDiemVan.Text);
            sv.DiemAnh = (txtDiemAnh.Text);
            sv.GhiChu = mmGhiChu.Text;
            lstSinhVien.Add(sv);
            gdcSinhVien.DataSource = lstSinhVien;
            gdcSinhVien.RefreshDataSource();
        }

        private void bbiXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //xoa theo truong duoc chon
            int rowIndex = gridView1.FocusedRowHandle;
            lstSinhVien.RemoveAt(rowIndex);
            gdcSinhVien.DataSource = lstSinhVien;
            gridView1.RefreshData();
        }

        private void barButtonItem5_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //tim tong so sinh vien trong gridcontrol
            int tongSoSV = lstSinhVien.Count;
            MessageBox.Show("Tổng số sinh viên : " + tongSoSV);
        }

        private void txtHoTen_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtHoTen.Text))
            {
                e.Cancel = true;
                errorProvider.SetError(txtHoTen, "Bạn phải nhập họ tên trước.");
            }
            else
            {
                e.Cancel = false;
                errorProvider.SetError(txtHoTen, "");
            }
        }

        private void barButtonItem6_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //Tim sinh vien theo {DoiTuong}
            Dictionary<string, int> dict = new Dictionary<string, int>();
            foreach (SinhVien sv in lstSinhVien)
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

        private void cboGioiTinh_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtDiemToan_Validating(object sender, CancelEventArgs e)
        {
            double diem;
            if (double.TryParse(txtDiemToan.Text, out diem))
            {
                if (diem > 10)
                {
                    e.Cancel = true;
                    txtDiemToan.ErrorText = "Điểm không được lớn hơn 10";
                }
            }
        }

        private void txtDiemVan_Validating(object sender, CancelEventArgs e)
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

        private void txtDiemAnh_Validating(object sender, CancelEventArgs e)
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

        private void cboGioiTinh_EditValueChanged(object sender, EventArgs e)
        {
            string gioiTinh = cboGioiTinh.EditValue as string;
        }

        private void barButtonItem7_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int count = 0;
            for (int i = 0; i < gridView1.RowCount; i++)
            {
                float diemToan = Convert.ToSingle(gridView1.GetRowCellValue(i, "DiemToan"));
                float diemVan = Convert.ToSingle(gridView1.GetRowCellValue(i, "DiemVan"));
                float diemAnh = Convert.ToSingle(gridView1.GetRowCellValue(i, "DiemAnh"));

                if ((diemToan + diemVan + diemAnh) / 3 > 8)
                    count++;
            }
            MessageBox.Show("Tong so sinh vien co diem trung binh 3 mon toan van anh tren 8: " + count.ToString());
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            // Lấy đường dẫn tương đối của file
            string filePath = @"D:\Vietsens\Học việc\TEST\Bai 1\DanhSachSinhVien\DanhSachSinhVien\TextFile1.txt";
            // Mở file để ghi dữ liệu
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                // Duyệt qua từng dòng trong gridview
                for (int i = 0; i < gridView1.RowCount; i++)
                {
                    // Lấy giá trị từng ô và ghi vào file
                    string row = "";
                    for (int j = 0; j < gridView1.Columns.Count; j++)
                    {
                        row += gridView1.GetRowCellValue(i, gridView1.Columns[j]).ToString() + "\t";
                    }
                    writer.WriteLine(row.TrimEnd('\t'));
                }
            }
            MessageBox.Show("Lưu dữ liệu thành công!");
        }

        private void LoadData()
        {
            // Đọc dữ liệu từ file
            string[] lines = System.IO.File.ReadAllLines(@"D:\Vietsens\Học việc\TEST\Bai 1\DanhSachSinhVien\DanhSachSinhVien\TextFile1.txt");

            // Tạo DataTable để lưu dữ liệu
            DataTable dt = new DataTable();
            // Thêm các cột vào DataTable
            for (int i = 0; i < lines[0].Length; i++)
            {
                dt.Columns.Add("Column " + (i + 1));
            }

            // Thêm dữ liệu từ file vào DataTable
            foreach (string line in lines)
            {
                DataRow row = dt.NewRow();
                string[] values = line.Split(' ');
                for (int i = 0; i < values.Length; i++) 
                {
                    row[i] = values[i];
                }
                dt.Rows.Add(row);
            }

            // Load dữ liệu lên GridView
            gdcSinhVien.DataSource = dt;
            gdcSinhVien.RefreshDataSource();
        }

        private void gdcSinhVien_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void gridView1_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {

        }

        private void btnSua_Click(object sender, EventArgs e)
        {

        }
    }
}

