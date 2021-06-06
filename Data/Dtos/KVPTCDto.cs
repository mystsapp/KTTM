using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Data.Dtos
{
    public class KVPTCDto
    {
        [DisplayName("Số phiếu")]
        public string SoCT { get; set; }

        [DisplayName("Ngày lập phiếu")]
        public DateTime NgayCT { get; set; }

        [DisplayName("Loại phiếu")]
        public string MFieu { get; set; }

        [DisplayName("Loại tiền")]
        public string NgoaiTe { get; set; }

        [DisplayName("Họ và tên")]
        public string HoTen { get; set; }

        [DisplayName("Đơn vị")]
        public string DonVi { get; set; }

        [DisplayName("Phòng")]
        public string Phong { get; set; }

        [DisplayName("Người lập phiếu")]
        public string LapPhieu { get; set; }

        public DateTime Create { get; set; }

        public string MayTinh { get; set; }

        public DateTime Lock { get; set; }

        public string Locker { get; set; }

    }
}
