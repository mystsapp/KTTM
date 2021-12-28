using System;
using System.Collections.Generic;
using System.Text;

namespace Data.ViewModels
{
    public class ListViewModel
    {
        public int Id { get; set; }
        public string StringId { get; set; }
        public string Code { get; set; }
        public string TaxCode { get; set; }
        public string Name { get; set; }

        // GetLoaiCT_By_MaCp
        public string MaCP { get; set; }

        public string TenChiPhi { get; set; }
        public string HTTC { get; set; }
        public string TKNo { get; set; }
        public string TKCo { get; set; }
        public string DienGiai { get; set; }
        public string LoaiCTU { get; set; }
        public string BoPhan { get; set; }
    }
}