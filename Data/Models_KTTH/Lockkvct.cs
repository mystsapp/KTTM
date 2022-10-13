using System;
using System.Collections.Generic;

#nullable disable

namespace Data.Models_KTTH
{
    public partial class Lockkvct
    {
        public Guid Id { get; set; }
        public string Chinhanh { get; set; }
        public bool Lockdata { get; set; }
        public DateTime? Thangnam { get; set; }
        public DateTime? Ngaycapnhat { get; set; }
        public string Userkhoa { get; set; }
        public string Logfile { get; set; }
    }
}
