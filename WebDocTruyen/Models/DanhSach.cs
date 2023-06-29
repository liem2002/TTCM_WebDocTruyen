using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebDocTruyen.Context;

namespace WebDocTruyen.Models
{
    public class DanhSach
    {

        public List<TheLoai> theLoais {  get; set; }
        public Truyen truyen { get; set; }
        public DanhGia DanhGia { get; set; }
        public List<Chuong> chuongs { get; set; }
        public List<DanhGia> danhGia { get;set; }
    }
}