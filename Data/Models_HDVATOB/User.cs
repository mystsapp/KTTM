using System;
using System.Collections.Generic;

#nullable disable

namespace Data.Models_HDVATOB
{
    public partial class User
    {
        public string Username { get; set; }
        public string Hoten { get; set; }
        public string Password { get; set; }
        public string Accounthddt { get; set; }
        public string Passwordhddt { get; set; }
        public string Maviettat { get; set; }
        public bool? IsAdmin { get; set; }
        public string Chinhanh { get; set; }
        public string Logfile { get; set; }
        public DateTime? Ngaytao { get; set; }
        public string Nguoitao { get; set; }
    }
}
