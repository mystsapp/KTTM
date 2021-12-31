using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Data.Models_KTTM
{
    public class ErrorLog
    {
        public long Id { get; set; }

        [MaxLength(300, ErrorMessage = "Chiều dài tối đa 300 ký tự"), Column(TypeName = "nvarchar(300)")]
        public string Message { get; set; }

        [MaxLength(300, ErrorMessage = "Chiều dài tối đa 300 ký tự"), Column(TypeName = "nvarchar(300)")]
        public string InnerMessage { get; set; }

        [DisplayName("Chi nhánh")]
        [MaxLength(5, ErrorMessage = "Chiều dài tối đa 5 ký tự"), Column(TypeName = "varchar(5)")]
        public string MaCn { get; set; }

        public DateTime NgayTao { get; set; }

        [Column(TypeName = "nvarchar(MAX)")]
        public string LogFile { get; set; }
    }
}