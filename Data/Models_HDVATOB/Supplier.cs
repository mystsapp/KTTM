using System;
using System.Collections.Generic;

#nullable disable

namespace Data.Models_HDVATOB
{
    public partial class Supplier
    {
        public string Code { get; set; }
        public string Chinhanh { get; set; }
        public string Name { get; set; }
        public string Realname { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string Telephone { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public string Contact { get; set; }
        public DateTime? Date { get; set; }
        public string Field { get; set; }
        public string Suppliercode { get; set; }
        public string Paymentcode { get; set; }
        public int? Room { get; set; }
        public int? Level { get; set; }
        public string Website { get; set; }
        public string Nation { get; set; }
        public string Taxcode { get; set; }
        public string Taxsign { get; set; }
        public string Taxform { get; set; }
        public string Httt { get; set; }
        public bool? Muave { get; set; }
        public bool? Daily { get; set; }
        public bool? Active { get; set; }
        public string Logfile { get; set; }
    }
}
