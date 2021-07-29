using Data.Models_KTTM;
using Data.Models_QLTaiKhoan;
using Data.Utilities;
using KTTM.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace KTTM.Controllers
{
    public class BaoCaosController : BaseController
    {
        private readonly IKVPCTService _kVPCTService;
        private readonly IKVCTPCTService _kVCTPCTService;

        public BaoCaosController(IKVPCTService kVPCTService, IKVCTPCTService kVCTPCTService)
        {
            _kVPCTService = kVPCTService;
            _kVCTPCTService = kVCTPCTService;
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> InPhieu(string soCT, int page)
        {
            KVPCT kVPCT = await _kVPCTService.GetBySoCT(soCT);
            string loaiphieu = kVPCT.MFieu == "T" ? "THU" : "CHI";

            // from session
            var user = HttpContext.Session.GetSingle<User>("loginUser");

            ExcelPackage ExcelApp = new ExcelPackage();
            ExcelWorksheet xlSheet = ExcelApp.Workbook.Worksheets.Add("Report");
            // Định dạng chiều dài cho cột
            xlSheet.Column(1).Width = 40;// DIỄN GIẢI
            xlSheet.Column(2).Width = 20;// TK ĐỐI ỨNG
            xlSheet.Column(3).Width = 20;// SỐ TIỀN NT
            xlSheet.Column(4).Width = 20;// SỐ TIỀN
            xlSheet.Column(5).Width = 30;// SGTCODE

            xlSheet.Cells[1, 1].Value = "CÔNG TY TNHH MTV DVLH SAIGONTOURIST";
            xlSheet.Cells[1, 1].Style.Font.SetFromFont(new Font("Times New Roman", 12, FontStyle.Bold));
            xlSheet.Cells[1, 1, 1, 2].Merge = true;

            xlSheet.Cells[2, 1].Value = "45 LÊ THÁNH TÔN, P.BẾN NGHÉ, Q.1, TP.HCM";
            xlSheet.Cells[2, 1].Style.Font.SetFromFont(new Font("Times New Roman", 12, FontStyle.Bold));
            xlSheet.Cells[2, 1, 2, 2].Merge = true;

            xlSheet.Cells[1, 3].Value = "CỘNG HOÀ XÃ HỘI CHỦ NGHĨA VIỆT NAM";
            xlSheet.Cells[1, 3].Style.Font.SetFromFont(new Font("Times New Roman", 12, FontStyle.Bold));
            xlSheet.Cells[1, 3, 1, 5].Merge = true;
            setCenterAligment(1, 3, 1, 5, xlSheet);

            xlSheet.Cells[2, 3].Value = "Độc Lập - Tự Do - Hạnh Phúc";
            xlSheet.Cells[2, 3].Style.Font.SetFromFont(new Font("Times New Roman", 12, FontStyle.Bold));
            xlSheet.Cells[2, 3, 2, 5].Merge = true;
            setCenterAligment(2, 3, 2, 5, xlSheet);

            xlSheet.Cells[3, 1].Value = "BẢNG KÊ CHI TIẾT PHIẾU " + loaiphieu + " " + kVPCT.SoCT;
            xlSheet.Cells[3, 1].Style.Font.SetFromFont(new Font("Times New Roman", 16, FontStyle.Bold));
            xlSheet.Cells[3, 1, 3, 5].Merge = true;
            setCenterAligment(3, 1, 3, 10, xlSheet);

            // Tạo header
            xlSheet.Cells[5, 1].Value = "DIỄN GIẢI";
            xlSheet.Cells[5, 2].Value = "TK ĐỐI ỨNG";
            xlSheet.Cells[5, 3].Value = "SỐ TIỀN NT";
            xlSheet.Cells[5, 4].Value = "SỐ TIỀN";
            xlSheet.Cells[5, 5].Value = "SGTCODE";

            xlSheet.Cells[5, 1, 5, 12].Style.Font.SetFromFont(new Font("Times New Roman", 12, FontStyle.Bold));
            setBorder(5, 1, 5, 5, xlSheet);
            setCenterAligment(5, 1, 5, 5, xlSheet);

            // do du lieu tu table
            int dong = 6;

            //// moi load vao
            var kVCTPCTs = await _kVCTPCTService.List_KVCTPCT_By_SoCT(soCT);

            //du lieu
            //int iRowIndex = 6;

            Color colFromHex = System.Drawing.ColorTranslator.FromHtml("#D3D3D3");// ColorTranslator.FromHtml("#D3D3D3");
            Color colorTotalRow = ColorTranslator.FromHtml("#66ccff");
            Color colorThanhLy = ColorTranslator.FromHtml("#7FFF00");
            Color colorChuaThanhLy = ColorTranslator.FromHtml("#FFDEAD");

            int idem = 1;

            if (kVCTPCTs.Count() > 0)
            {

                foreach (var item in kVCTPCTs)
                {
                    xlSheet.Cells[dong, 1].Value = item.DienGiai;
                    TrSetCellBorder(xlSheet, dong, 1, ExcelBorderStyle.Thin, ExcelHorizontalAlignment.Justify, Color.Silver, "Times New Roman", 12, FontStyle.Regular);
                    //xlSheet.Cells[dong, 1].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                    if (kVPCT.MFieu == "T")
                    {
                        xlSheet.Cells[dong, 2].Value = item.TKCo;
                    }
                    else
                    {
                        xlSheet.Cells[dong, 2].Value = item.TKNo;
                    }
                    TrSetCellBorder(xlSheet, dong, 2, ExcelBorderStyle.Thin, ExcelHorizontalAlignment.Left, Color.Silver, "Times New Roman", 12, FontStyle.Regular);
                    // xlSheet.Cells[dong, 3].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                    xlSheet.Cells[dong, 3].Value = item.SoTienNT;
                    TrSetCellBorder(xlSheet, dong, 3, ExcelBorderStyle.Thin, ExcelHorizontalAlignment.Center, Color.Silver, "Times New Roman", 12, FontStyle.Regular);
                    //xlSheet.Cells[dong, 4].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                    xlSheet.Cells[dong, 4].Value = item.SoTien;
                    TrSetCellBorder(xlSheet, dong, 4, ExcelBorderStyle.Thin, ExcelHorizontalAlignment.Center, Color.Silver, "Times New Roman", 12, FontStyle.Regular);
                    //xlSheet.Cells[dong, 5].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                    xlSheet.Cells[dong, 5].Value = item.Sgtcode;
                    TrSetCellBorder(xlSheet, dong, 5, ExcelBorderStyle.Thin, ExcelHorizontalAlignment.Left, Color.Silver, "Times New Roman", 12, FontStyle.Regular);
                    // xlSheet.Cells[dong, 6].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                    //setBorder(5, 1, dong, 10, xlSheet);
                    NumberFormat(dong, 3, dong, 4, xlSheet);

                    dong++;
                    idem++;
                }

                xlSheet.Cells[dong, 1].Value = "TỔNG CỘNG:";
                xlSheet.Cells[dong, 3].Formula = "SUM(C6:C" + (dong - 1) + ")";
                xlSheet.Cells[dong, 4].Formula = "SUM(D6:D" + (dong - 1) + ")";

                NumberFormat(dong, 3, dong, 4, xlSheet);
                setFontBold(dong, 1, dong, 5, 12, xlSheet);
                setBorder(dong, 1, dong, 5, xlSheet);

                xlSheet.Cells[dong + 2, 1].Value = "Người lập bảng kê";
                xlSheet.Cells[dong + 2, 1].Style.Font.SetFromFont(new Font("Times New Roman", 12, FontStyle.Regular));
                xlSheet.Cells[dong + 2, 4].Value = "Kế toán trưởng";
                xlSheet.Cells[dong + 2, 4].Style.Font.SetFromFont(new Font("Times New Roman", 12, FontStyle.Regular));

                setCenterAligment(dong + 2, 1, dong + 2, 4, xlSheet);
            }
            else
            {
                SetAlert("Phiếu này không có chi tiết nào.", "warning");
                return RedirectToAction(nameof(Index), "Home", new { soCT, page });
            }

            //dong++;
            //// Merger cot 4,5 ghi tổng tiền
            //setRightAligment(dong, 3, dong, 3, xlSheet);
            //xlSheet.Cells[dong, 1, dong, 2].Merge = true;
            //xlSheet.Cells[dong, 1].Value = "Tổng tiền: ";

            // Sum tổng tiền
            // xlSheet.Cells[dong, 5].Value = "TC:";
            //DateTimeFormat(6, 4, 6 + d.Count(), 4, xlSheet);
            // DateTimeFormat(6, 4, 9, 4, xlSheet);
            // setCenterAligment(6, 4, 9, 4, xlSheet);
            // xlSheet.Cells[dong, 6].Formula = "SUM(F6:F" + (6 + d.Count() - 1) + ")";

            //setBorder(5, 1, 5 + d.Count() + 2, 10, xlSheet);

            //setFontBold(5, 1, 5, 8, 11, xlSheet);
            //setFontSize(6, 1, 6 + d.Count() + 2, 8, 11, xlSheet);
            // canh giua cot stt
            //setCenterAligment(6, 1, 6 + dong + 2, 1, xlSheet);
            // canh giua code chinhanh
            //setCenterAligment(6, 3, 6 + dong + 2, 3, xlSheet);
            // NumberFormat(6, 6, 6 + d.Count(), 6, xlSheet);
            // định dạng số cot, đơn giá, thành tiền tong cong
            // NumberFormat(6, 8, dong, 9, xlSheet);

            // setBorder(dong, 5, dong, 6, xlSheet);
            // setFontBold(dong, 5, dong, 6, 12, xlSheet);

            //xlSheet.View.FreezePanes(6, 20);

            //end du lieu

            byte[] fileContents;
            try
            {
                fileContents = ExcelApp.GetAsByteArray();
                return File(
                fileContents: fileContents,
                contentType: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                fileDownloadName: "BangKe_" + kVPCT.SoCT + "_" + System.DateTime.Now.ToString("dd/MM/yyyy HH:mm") + ".xlsx");
            }
            catch (Exception)
            {
                throw;
            }
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        private static void NumberFormat(int fromRow, int fromColumn, int toRow, int toColumn, ExcelWorksheet sheet)
        {
            using (var range = sheet.Cells[fromRow, fromColumn, toRow, toColumn])
            {
                range.Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                range.Style.Numberformat.Format = "#,#0";
            }
        }

        private static void DateFormat(int fromRow, int fromColumn, int toRow, int toColumn, ExcelWorksheet sheet)
        {
            using (var range = sheet.Cells[fromRow, fromColumn, toRow, toColumn])
            {
                range.Style.Numberformat.Format = "dd/MM/yyyy";
                range.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            }
        }

        private static void DateTimeFormat(int fromRow, int fromColumn, int toRow, int toColumn, ExcelWorksheet sheet)
        {
            using (var range = sheet.Cells[fromRow, fromColumn, toRow, toColumn])
            {
                range.Style.Numberformat.Format = "dd/MM/yyyy HH:mm";
                range.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            }
        }

        private static void setRightAligment(int fromRow, int fromColumn, int toRow, int toColumn, ExcelWorksheet sheet)
        {
            using (var range = sheet.Cells[fromRow, fromColumn, toRow, toColumn])
            {
                range.Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
            }
        }

        private static void setCenterAligment(int fromRow, int fromColumn, int toRow, int toColumn, ExcelWorksheet sheet)
        {
            using (var range = sheet.Cells[fromRow, fromColumn, toRow, toColumn])
            {
                range.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            }
        }

        private static void setFontSize(int fromRow, int fromColumn, int toRow, int toColumn, int fontSize, ExcelWorksheet sheet)
        {
            using (var range = sheet.Cells[fromRow, fromColumn, toRow, toColumn])
            {
                range.Style.Font.SetFromFont(new Font("Times New Roman", fontSize, FontStyle.Regular));
            }
        }

        private static void setFontBold(int fromRow, int fromColumn, int toRow, int toColumn, int fontSize, ExcelWorksheet sheet)
        {
            using (var range = sheet.Cells[fromRow, fromColumn, toRow, toColumn])
            {
                range.Style.Font.SetFromFont(new Font("Times New Roman", fontSize, FontStyle.Bold));
            }
        }

        private static void setBorder(int fromRow, int fromColumn, int toRow, int toColumn, ExcelWorksheet sheet)
        {
            using (var range = sheet.Cells[fromRow, fromColumn, toRow, toColumn])
            {
                range.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                range.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                range.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                range.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
            }
        }

        private static void setBorderAround(int fromRow, int fromColumn, int toRow, int toColumn, ExcelWorksheet sheet)
        {
            using (var range = sheet.Cells[fromRow, fromColumn, toRow, toColumn])
            {
                range.Style.Border.BorderAround(ExcelBorderStyle.Thin);
            }
        }

        private static void PhantramFormat(int fromRow, int fromColumn, int toRow, int toColumn, ExcelWorksheet sheet)
        {
            using (var range = sheet.Cells[fromRow, fromColumn, toRow, toColumn])
            {
                range.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                range.Style.Numberformat.Format = "0 %";
            }
        }

        private static void mergercell(int fromRow, int fromColumn, int toRow, int toColumn, ExcelWorksheet sheet)
        {
            using (var range = sheet.Cells[fromRow, fromColumn, toRow, toColumn])
            {
                range.Merge = true;
                range.Style.VerticalAlignment = ExcelVerticalAlignment.Top;
                range.Style.WrapText = true;
            }
        }

        private static void numberMergercell(int fromRow, int fromColumn, int toRow, int toColumn, ExcelWorksheet sheet)
        {
            using (var range = sheet.Cells[fromRow, fromColumn, toRow, toColumn])
            {
                var a = sheet.Cells[fromRow, fromColumn].Value;
                range.Merge = true;
                range.Style.VerticalAlignment = ExcelVerticalAlignment.Top;
                range.Clear();
                sheet.Cells[fromRow, fromColumn].Value = a;
            }
        }

        private static void wrapText(int fromRow, int fromColumn, int toRow, int toColumn, ExcelWorksheet sheet)
        {
            using (var range = sheet.Cells[fromRow, fromColumn, toRow, toColumn])
            {
                range.Style.WrapText = true;
            }
        }

        public static string convertToUnSign3(string s)
        {
            Regex regex = new Regex("\\p{IsCombiningDiacriticalMarks}+");
            string temp = s.Normalize(NormalizationForm.FormD);
            return regex.Replace(temp, String.Empty).Replace('\u0111', 'd').Replace('\u0110', 'D');
        }

        ///////////////// new ///////////////////
        ///
        public void TrSetCellBorder(ExcelWorksheet xlSheet, int iRowIndex, int colIndex, ExcelBorderStyle excelBorderStyle, ExcelHorizontalAlignment excelHorizontalAlignment, Color borderColor, string fontName, int fontSize, FontStyle fontStyle)
        {
            xlSheet.Cells[iRowIndex, colIndex].Style.HorizontalAlignment = excelHorizontalAlignment;
            // Set Border
            xlSheet.Cells[iRowIndex, colIndex].Style.Border.Left.Style = excelBorderStyle;
            xlSheet.Cells[iRowIndex, colIndex].Style.Border.Top.Style = excelBorderStyle;
            xlSheet.Cells[iRowIndex, colIndex].Style.Border.Right.Style = excelBorderStyle;
            xlSheet.Cells[iRowIndex, colIndex].Style.Border.Bottom.Style = excelBorderStyle;
            // Set màu ch Border
            //xlSheet.Cells[iRowIndex, colIndex].Style.Border.Left.Color.SetColor(borderColor);
            //xlSheet.Cells[iRowIndex, colIndex].Style.Border.Top.Color.SetColor(borderColor);
            //xlSheet.Cells[iRowIndex, colIndex].Style.Border.Right.Color.SetColor(borderColor);
            //xlSheet.Cells[iRowIndex, colIndex].Style.Border.Bottom.Color.SetColor(borderColor);

            // Set Font cho text  trong Range hiện tại
            xlSheet.Cells[iRowIndex, colIndex].Style.Font.SetFromFont(new Font(fontName, fontSize, fontStyle));
        }

    }
}
