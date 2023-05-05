using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DanhSachSinhVien.Models
{
    [Serializable]
    public class SinhVien
    {
        public string MaSinhVien { get; set; }
        public string HoTen { get; set; }
        public string GioiTinh { get; set; }
        public DateTime NgaySinh { get; set; }
        public string DoiTuong { get; set; }
        public string DiemToan { get; set; }
        public string DiemVan { get; set; }
        public string DiemAnh { get; set; }
        public string GhiChu { get; set; }       
    }
}
