
using DanhSachSinhVien.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DanhSachSinhVien.IServices
{
    public interface IServicesSinhVien
    {
        //phuong thuc them sinh vien
        string addSinhVien(SinhVien sinhVien);
        //sua thong tin sinh vien
        string updateSinhVien(SinhVien sinhVien);
        //xoa sinh vien (theo masv)
        string removeSinhVien(int MaSinhVien);
        //lay danh sach sinh vien
        List<SinhVien> getlstSinhVien();
        //tim kiem theo maSV
        SinhVien findSinhVienByMaSinhVien(int MaSinhVien);
        //fill data tu file vao list trong services
    }
}
