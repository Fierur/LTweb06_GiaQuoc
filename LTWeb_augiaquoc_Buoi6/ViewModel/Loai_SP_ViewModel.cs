using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LTWeb_augiaquoc_Buoi6.Models;

namespace LTWeb_augiaquoc_Buoi6.ViewModel
{
    public class Loai_SP_ViewModel
    {
        public List<Loai> loais { get; set; }
        public List<SanPham> sanphams { get; set; }
    }
}