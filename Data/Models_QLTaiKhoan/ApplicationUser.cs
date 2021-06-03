using System;
using System.Collections.Generic;

#nullable disable

namespace Data.Models_QLTaiKhoan
{
    public partial class ApplicationUser
    {
        public string Username { get; set; }
        public string Mact { get; set; }

        public virtual Application MactNavigation { get; set; }
    }
}
