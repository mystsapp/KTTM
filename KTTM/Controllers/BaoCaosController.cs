﻿using Data.Models_DanhMucKT;
using Data.Models_KTTM;
using Data.Models_QLTaiKhoan;
using Data.Utilities;
using KTTM.Models;
using KTTM.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
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
        private readonly IKVPTCService _kVPTCService;
        private readonly IKVCTPTCService _kVCTPTCService;
        private readonly ITT621Service _tT621Service;
        private readonly ITamUngService _tamUngService;
        private readonly IBaoCaoService _baoCaoService;
        private readonly ITonQuyService _tonQuyService;

        [BindProperty]
        public BaoCaoViewModel BaoCaoVM { get; set; }

        public BaoCaosController(IKVPTCService kVPTCService, IKVCTPTCService kVCTPTCService,
                                 ITT621Service tT621Service, ITamUngService tamUngService,
                                 IBaoCaoService baoCaoService, ITonQuyService tonQuyService)
        {
            _kVPTCService = kVPTCService;
            _kVCTPTCService = kVCTPTCService;
            _tT621Service = tT621Service;
            _tamUngService = tamUngService;
            _baoCaoService = baoCaoService;
            _tonQuyService = tonQuyService;

            BaoCaoVM = new BaoCaoViewModel();
        }

        [HttpPost]
        public async Task<IActionResult> InCTPhieu(Guid kVPTCId, int page)
        {
            KVPTC kVPTC = await _kVPTCService.GetByGuidIdAsync(kVPTCId);
            string loaiphieu = kVPTC.MFieu == "T" ? "THU" : "CHI";

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

            xlSheet.Cells[3, 1].Value = "BẢNG KÊ CHI TIẾT PHIẾU " + loaiphieu + " " + kVPTC.SoCT;
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
            var kVCTPTCs = await _kVCTPTCService.List_KVCTPCT_By_KVPTCid(kVPTCId);

            //du lieu
            //int iRowIndex = 6;

            Color colFromHex = System.Drawing.ColorTranslator.FromHtml("#D3D3D3");// ColorTranslator.FromHtml("#D3D3D3");
            Color colorTotalRow = ColorTranslator.FromHtml("#66ccff");
            Color colorThanhLy = ColorTranslator.FromHtml("#7FFF00");
            Color colorChuaThanhLy = ColorTranslator.FromHtml("#FFDEAD");

            int idem = 1;

            if (kVCTPTCs.Count() > 0)
            {
                foreach (var item in kVCTPTCs)
                {
                    xlSheet.Cells[dong, 1].Value = item.DienGiai;
                    TrSetCellBorder(xlSheet, dong, 1, ExcelBorderStyle.Thin, ExcelHorizontalAlignment.Justify, Color.Silver, "Times New Roman", 12, FontStyle.Regular);
                    //xlSheet.Cells[dong, 1].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                    if (kVPTC.MFieu == "T")
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
                return RedirectToAction(nameof(Index), "Home", new { id = kVPTCId, page });
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
                fileDownloadName: "BangKe_" + kVPTC.SoCT + "_" + System.DateTime.Now.ToString("dd/MM/yyyy HH:mm") + ".xlsx");
            }
            catch (Exception)
            {
                throw;
            }
        }

        // BaoCaoQuyTienVND
        [HttpPost]
        public async Task<IActionResult> BaoCaoQuyTienVND(string searchFromDate, string searchToDate)
        {
            // from session
            var user = HttpContext.Session.GetSingle<User>("loginUser");

            //string toDate = DateTime.Parse(searchToDate).AddDays(-1).ToString("dd/MM/yyyy");
            string fromDate = DateTime.Parse(searchFromDate).AddDays(-1).ToString("dd/MM/yyyy");
            //var tonQuies = _tonQuyService.FindTonQuy_By_Date("02/01/2020", toDate);
            var tonQuies = _tonQuyService.FindTonQuy_By_Date("02/01/2020", fromDate, user.Macn);
            var tonQuy = tonQuies.OrderByDescending(x => x.NgayCT).FirstOrDefault();
            //IEnumerable<KVCTPTC> kVCTPTCs = await _kVCTPTCService.FinByDate(searchFromDate, searchToDate, user.Macn); // loaitien == "VND"
            IEnumerable<KVCTPTC> kVCTPTCs = await _kVCTPTCService.FinBy_TonQuy_Date(tonQuy.NgayCT.Value.ToShortDateString(), searchToDate, user.Macn); // loaitien == "VND"

            List<KVCTPCT_Model_GroupBy_SoCT> kVCTPCT_Model_GroupBy_SoCTs = new List<KVCTPCT_Model_GroupBy_SoCT>();
            if (kVCTPTCs.Count() > 0)
            {
                kVCTPCT_Model_GroupBy_SoCTs = _kVCTPTCService.KVCTPTC_Model_GroupBy_SoCTs(kVCTPTCs); // groupby name (makh)
            }
            else
            {
                kVCTPCT_Model_GroupBy_SoCTs.Add(new KVCTPCT_Model_GroupBy_SoCT()
                {
                    CongPhatSinh_Thu = 0,
                    CongPhatSinh_Chi = 0
                });
            }

            ExcelPackage ExcelApp = new ExcelPackage();
            ExcelWorksheet xlSheet = ExcelApp.Workbook.Worksheets.Add("Report");
            // Định dạng chiều dài cho cột
            xlSheet.Column(1).Width = 15;
            xlSheet.Column(2).Width = 15;
            xlSheet.Column(3).Width = 40;
            xlSheet.Column(4).Width = 30;
            xlSheet.Column(5).Width = 20;
            xlSheet.Column(6).Width = 15;
            xlSheet.Column(7).Width = 15;

            xlSheet.Cells[1, 1].Value = "CÔNG TY TNHH MỘT THÀNH VIÊN DỊCH VỤ LỮ HÀNH SAIGONTOURIST";
            xlSheet.Cells[1, 1].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
            xlSheet.Cells[1, 1].Style.Font.SetFromFont(new Font("Times New Roman", 12, FontStyle.Bold));
            xlSheet.Cells[1, 1, 2, 3].Merge = true;

            xlSheet.Cells[1, 5].Value = "CỘNG HOÀ XÃ HỘI CHỦ NGHĨA VIỆT NAM";
            xlSheet.Cells[1, 5].Style.Font.SetFromFont(new Font("Times New Roman", 12, FontStyle.Bold));
            xlSheet.Cells[1, 5, 1, 7].Merge = true;

            xlSheet.Cells[2, 5].Value = "Độc Lập - Tự Do - Hạnh Phúc";
            xlSheet.Cells[2, 5].Style.Font.SetFromFont(new Font("Times New Roman", 12, FontStyle.Bold));
            xlSheet.Cells[2, 5, 2, 7].Merge = true;

            string stringSoQuy = "SỔ QUỸ KIÊM BÁO CÁO QUỸ TIỀN MẶT VNĐ ";
            string stringTuNgay;
            if (searchFromDate != searchToDate)
            {
                stringTuNgay = "Từ ngày " + searchFromDate + " đến ngày " + searchToDate;
            }
            else
            {
                stringTuNgay = "Ngày " + searchFromDate;
            }
            xlSheet.Cells[3, 1].Value = stringSoQuy;
            xlSheet.Cells[3, 1].Style.Font.SetFromFont(new Font("Times New Roman", 16, FontStyle.Bold));
            xlSheet.Cells[3, 1, 3, 7].Merge = true;
            xlSheet.Cells[4, 1].Value = stringTuNgay;
            xlSheet.Cells[4, 1].Style.Font.SetFromFont(new Font("Times New Roman", 12, FontStyle.Bold | FontStyle.Italic));
            //xlSheet.Cells[4, 1].Style.Font.Bold = true;
            //xlSheet.Cells[4, 1].Style.Font.Italic = true;
            xlSheet.Cells[4, 1, 4, 7].Merge = true;

            // Tạo header
            xlSheet.Cells[6, 1].Value = "SỐ CT";
            xlSheet.Cells[6, 1, 6, 2].Merge = true;
            xlSheet.Cells[7, 1].Value = "THU";
            xlSheet.Cells[7, 2].Value = "CHI";
            xlSheet.Cells[6, 3].Value = "DIỄN GIẢI";
            //xlSheet.Cells[6, 3].Style.WrapText = true;
            xlSheet.Cells[6, 3, 7, 3].Merge = true;
            xlSheet.Cells[6, 4].Value = "HỌ TÊN";
            //xlSheet.Cells[6, 4].Style.WrapText = true;
            xlSheet.Cells[6, 4, 7, 4].Merge = true;
            xlSheet.Cells[6, 5].Value = "TÀI KHOẢN ĐỐI ỨNG";
            //xlSheet.Cells[6, 5].Style.WrapText = true;
            xlSheet.Cells[6, 5, 7, 5].Merge = true;
            xlSheet.Cells[6, 6].Value = "SỐ TIỀN";
            xlSheet.Cells[6, 6, 6, 7].Merge = true;
            xlSheet.Cells[7, 6].Value = "THU";
            xlSheet.Cells[7, 7].Value = "CHI";

            xlSheet.Cells[6, 1, 7, 7].Style.Font.SetFromFont(new Font("Times New Roman", 12, FontStyle.Bold));
            xlSheet.Cells[6, 1, 7, 7].Style.WrapText = true;
            xlSheet.Cells[6, 1, 7, 7].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center; // Alignment is center
            xlSheet.Cells[6, 1, 7, 7].Style.VerticalAlignment = ExcelVerticalAlignment.Center; // Alignment is center
            //worksheet.Column(8).Style.VerticalAlignment = ExcelVerticalAlignment.Top;

            setBorder(6, 1, 7, 7, xlSheet);
            setCenterAligment(1, 1, 7, 7, xlSheet);

            // do du lieu tu table
            int dong = 8;

            //du lieu
            //int iRowIndex = 6;

            Color colFromHex = System.Drawing.ColorTranslator.FromHtml("#D3D3D3");// ColorTranslator.FromHtml("#D3D3D3");
            Color colorTotalRow = ColorTranslator.FromHtml("#66ccff");
            Color colorThanhLy = ColorTranslator.FromHtml("#7FFF00");
            Color colorChuaThanhLy = ColorTranslator.FromHtml("#FFDEAD");

            int idem = 1;

            xlSheet.Cells[dong, 3].Value = "TỒN ĐẦU";
            TrSetCellBorder(xlSheet, dong, 3, ExcelBorderStyle.None, ExcelHorizontalAlignment.Center, Color.Silver, "Times New Roman", 11, FontStyle.Bold);

            xlSheet.Cells[dong, 6].Value = tonQuy.SoTien;
            TrSetCellBorder(xlSheet, dong, 6, ExcelBorderStyle.None, ExcelHorizontalAlignment.Center, Color.Silver, "Times New Roman", 11, FontStyle.Bold);
            //NumberFormat(dong, 6, dong, 6, xlSheet);
            dong++;

            if (kVCTPTCs.Count() > 0)
            {
                foreach (var item in kVCTPCT_Model_GroupBy_SoCTs)
                {
                    foreach (var kvctpct in item.KVCTPTCs)
                    {
                        if (item.SoCT.Contains("QT") || item.SoCT.Contains("NC")) // 0146NC2022: đổi từ NT sang tiền việt
                        {
                            xlSheet.Cells[dong, 1].Value = kvctpct.SoCT;
                            TrSetCellBorder(xlSheet, dong, 1, ExcelBorderStyle.None, ExcelHorizontalAlignment.Left, Color.Silver, "Times New Roman", 11, FontStyle.Regular);
                        }
                        else
                        {
                            xlSheet.Cells[dong, 2].Value = kvctpct.SoCT;
                            TrSetCellBorder(xlSheet, dong, 2, ExcelBorderStyle.None, ExcelHorizontalAlignment.Left, Color.Silver, "Times New Roman", 11, FontStyle.Regular);
                        }
                        if (item.KVCTPTCs.Count() == 1)
                        {
                            xlSheet.Cells[dong, 3].Value = kvctpct.DienGiai;
                            TrSetCellBorder(xlSheet, dong, 3, ExcelBorderStyle.None, ExcelHorizontalAlignment.Left, Color.Silver, "Times New Roman", 11, FontStyle.Bold);
                            // xlSheet.Cells[dong, 3].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        }
                        else
                        {
                            xlSheet.Cells[dong, 3].Value = kvctpct.DienGiai;
                            TrSetCellBorder(xlSheet, dong, 3, ExcelBorderStyle.None, ExcelHorizontalAlignment.Left, Color.Silver, "Times New Roman", 11, FontStyle.Regular);
                            // xlSheet.Cells[dong, 3].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        }

                        xlSheet.Cells[dong, 4].Value = kvctpct.KVPTC.HoTen;//.TenKH;
                        TrSetCellBorder(xlSheet, dong, 4, ExcelBorderStyle.None, ExcelHorizontalAlignment.Left, Color.Silver, "Times New Roman", 11, FontStyle.Regular);
                        //xlSheet.Cells[dong, 4].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                        xlSheet.Cells[dong, 5].Value = item.SoCT.Contains("QT") ? kvctpct.TKCo : kvctpct.TKNo;
                        TrSetCellBorder(xlSheet, dong, 5, ExcelBorderStyle.None, ExcelHorizontalAlignment.Left, Color.Silver, "Times New Roman", 11, FontStyle.Regular);
                        //xlSheet.Cells[dong, 5].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                        if (item.KVCTPTCs.Count() == 1)
                        {
                            if (item.SoCT.Contains("QT") || item.SoCT.Contains("NC")) // 0146NC2022: đổi từ NT sang tiền việt
                            {
                                xlSheet.Cells[dong, 6].Value = kvctpct.SoTien;
                                TrSetCellBorder(xlSheet, dong, 6, ExcelBorderStyle.None, ExcelHorizontalAlignment.Right, Color.Silver, "Times New Roman", 11, FontStyle.Bold);
                            }
                            else
                            {
                                xlSheet.Cells[dong, 7].Value = kvctpct.SoTien;
                                TrSetCellBorder(xlSheet, dong, 7, ExcelBorderStyle.None, ExcelHorizontalAlignment.Right, Color.Silver, "Times New Roman", 11, FontStyle.Bold);
                            }

                            //xlSheet.Cells[dong, 8].Value = kvctpct.KVPTC.NgayCT.Value.ToString("dd/MM/yyyy");
                            //TrSetCellBorder(xlSheet, dong, 8, ExcelBorderStyle.None, ExcelHorizontalAlignment.Right, Color.Silver, "Times New Roman", 11, FontStyle.Regular);
                        }
                        else
                        {
                            if (item.SoCT.Contains("QT") || item.SoCT.Contains("NC")) // 0146NC2022: đổi từ NT sang tiền việt
                            {
                                xlSheet.Cells[dong, 6].Value = kvctpct.SoTien;
                                TrSetCellBorder(xlSheet, dong, 6, ExcelBorderStyle.None, ExcelHorizontalAlignment.Right, Color.Silver, "Times New Roman", 11, FontStyle.Regular);
                            }
                            else
                            {
                                xlSheet.Cells[dong, 7].Value = kvctpct.SoTien;
                                TrSetCellBorder(xlSheet, dong, 7, ExcelBorderStyle.None, ExcelHorizontalAlignment.Right, Color.Silver, "Times New Roman", 11, FontStyle.Regular);
                            }
                            //xlSheet.Cells[dong, 8].Value = kvctpct.KVPTC.NgayCT.Value.ToString("dd/MM/yyyy");
                            //TrSetCellBorder(xlSheet, dong, 7, ExcelBorderStyle.None, ExcelHorizontalAlignment.Right, Color.Silver, "Times New Roman", 11, FontStyle.Regular);

                            dong++;
                        }
                        //setBorder(5, 1, dong, 10, xlSheet);
                        //NumberFormat(dong, 6, dong + 1, 7, xlSheet);

                        //dong++;
                        idem++;
                    }
                    if (item.KVCTPTCs.Count() > 1)
                    {
                        xlSheet.Cells[dong, 3].Value = "Tổng cộng phiếu: " + item.SoCT;
                        TrSetCellBorder(xlSheet, dong, 3, ExcelBorderStyle.None, ExcelHorizontalAlignment.Right, Color.Silver, "Times New Roman", 11, FontStyle.Bold);

                        if (item.SoCT.Contains("QT"))
                        {
                            xlSheet.Cells[dong, 6].Value = item.TongCong;
                            TrSetCellBorder(xlSheet, dong, 6, ExcelBorderStyle.None, ExcelHorizontalAlignment.Right, Color.Silver, "Times New Roman", 11, FontStyle.Bold);
                        }
                        else
                        {
                            xlSheet.Cells[dong, 7].Value = item.TongCong;
                            TrSetCellBorder(xlSheet, dong, 7, ExcelBorderStyle.None, ExcelHorizontalAlignment.Right, Color.Silver, "Times New Roman", 11, FontStyle.Bold);
                        }
                    }

                    dong++;
                }
            }

            xlSheet.Cells[dong, 3].Value = "CỘNG PHÁT SINH:";
            xlSheet.Cells[dong, 3].Style.Font.SetFromFont(new Font("Times New Roman", 11, FontStyle.Bold));
            xlSheet.Cells[dong, 6].Value = kVCTPCT_Model_GroupBy_SoCTs.FirstOrDefault().CongPhatSinh_Thu;
            xlSheet.Cells[dong, 6].Style.Font.SetFromFont(new Font("Times New Roman", 11, FontStyle.Bold));
            xlSheet.Cells[dong, 7].Value = kVCTPCT_Model_GroupBy_SoCTs.FirstOrDefault().CongPhatSinh_Chi;
            xlSheet.Cells[dong, 7].Style.Font.SetFromFont(new Font("Times New Roman", 11, FontStyle.Bold));
            dong++;
            xlSheet.Cells[dong, 3].Value = "TỒN CUỐI:";
            xlSheet.Cells[dong, 3].Style.Font.SetFromFont(new Font("Times New Roman", 11, FontStyle.Bold));
            decimal tonCuoi = tonQuy.SoTien + kVCTPCT_Model_GroupBy_SoCTs.FirstOrDefault().CongPhatSinh_Thu - kVCTPCT_Model_GroupBy_SoCTs.FirstOrDefault().CongPhatSinh_Chi;
            xlSheet.Cells[dong, 6].Value = tonCuoi;
            xlSheet.Cells[dong, 6].Style.Font.SetFromFont(new Font("Times New Roman", 11, FontStyle.Bold));

            NumberFormat(8, 6, dong, 7, xlSheet);
            //setFontBold(dong, 1, dong, 10, 12, xlSheet);
            setBorder(6, 1, dong, 7, xlSheet);
            dong++;

            xlSheet.Cells[dong, 3].Value = "Kế toán";
            xlSheet.Cells[dong, 3].Style.Font.SetFromFont(new Font("Times New Roman", 11));
            xlSheet.Cells[dong, 4].Value = "Thủ quỹ";
            xlSheet.Cells[dong, 4].Style.Font.SetFromFont(new Font("Times New Roman", 11));
            xlSheet.Cells[dong, 5].Value = "Kế toán trưởng";
            xlSheet.Cells[dong, 5].Style.Font.SetFromFont(new Font("Times New Roman", 11));
            xlSheet.Cells[dong, 5, dong, 7].Merge = true;

            setCenterAligment(dong, 1, dong, 7, xlSheet);

            // ghi log va save tonquy tbl
            TonQuy tonQuy1 = new TonQuy()
            {
                LoaiTien = "VND",
                TyGia = 1,
                LogFile = "-User tạo: " + user.Username + " vào lúc: " + System.DateTime.Now.ToString(), // user.Username
                NgayCT = DateTime.Parse(searchToDate), //
                NgayTao = DateTime.Now,
                NguoiTao = user.Hoten,
                SoTien = tonCuoi,
                SoTienNT = tonCuoi,
                MaCn = user.Macn
            };

            var tonQuies1 = _tonQuyService.Find_Equal_By_Date(tonQuy1.NgayCT.Value, user.Macn, "VND");
            if (tonQuies1.Count > 0) // co ton tai
            {
                var tonQuy2 = _tonQuyService.GetById(tonQuies1.FirstOrDefault().Id);
                tonQuy2.LogFile += "==== người chạy lại " + user.Username + " lúc: " + DateTime.Now;
                tonQuy2.SoTien = tonCuoi;
                tonQuy2.SoTienNT = tonCuoi;

                await _tonQuyService.UpdateAsync(tonQuy2);
            }
            else
            {
                await _tonQuyService.CreateAsync(tonQuy1);
            }

            //}

            byte[] fileContents;
            try
            {
                fileContents = ExcelApp.GetAsByteArray();
                return File(
                fileContents: fileContents,
                contentType: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                fileDownloadName: "QuyTienVND_" + System.DateTime.Now.ToString("dd/MM/yyyy HH:mm") + ".xlsx");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // BaoCaoQuyTienNT_1Sheet
        [HttpPost]
        public async Task<IActionResult> BaoCaoQuyTienNT_1Sheet(string searchFromDate, string searchToDate)
        {
            // from session
            var user = HttpContext.Session.GetSingle<User>("loginUser");

            // check ngay ton quy
            var result = CheckNgayTonQuy(searchFromDate, searchToDate);
            var value = JsonConvert.SerializeObject(result.Result.Value);
            var viewModel = JsonConvert.DeserializeObject<ViewModel>(value);
            if (!viewModel.Status)
            {
                SetAlert(viewModel.Message, "warning");
                return LocalRedirect("/");
            }
            // check ngay ton quy

            //string toDate = DateTime.Parse(searchToDate).AddDays(-1).ToString("dd/MM/yyyy");
            string fromDate = DateTime.Parse(searchFromDate).AddDays(-1).ToString("dd/MM/yyyy");
            //var tonQuies = _tonQuyService.FindTonQuy_By_Date("02/01/2020", toDate);
            IEnumerable<NgoaiTe> ngoaiTes = _baoCaoService.GetAllNgoaiTe().Where(x => x.MaNt != "VND");
            List<TonQuy_LoaiTien_KVCTPTC> tonQuy_LoaiTien_KVCTPTC = new List<TonQuy_LoaiTien_KVCTPTC>();

            foreach (var ngoaiTe in ngoaiTes)
            {
                // tonquy theo loaitien
                var tonQuies = _tonQuyService.FindTonQuy_By_Date("02/01/2020", fromDate, user.Macn, ngoaiTe.MaNt);
                var tonQuy = tonQuies.OrderByDescending(x => x.NgayCT).FirstOrDefault();
                if (tonQuy != null)
                {
                    IEnumerable<KVCTPTC> kVCTPTCs1 = await _kVCTPTCService.FinBy_TonQuy_Date(
                    tonQuy.NgayCT.Value.ToShortDateString(), searchToDate, user.Macn, ngoaiTe.MaNt); // loaitien != "VND"

                    tonQuy_LoaiTien_KVCTPTC.Add(new TonQuy_LoaiTien_KVCTPTC()
                    {
                        NgoaiTe = ngoaiTe,
                        TonQuy = tonQuy,
                        KVCTPTCs = kVCTPTCs1,
                        KVCTPTC_NT_GroupBy_SoCTs = _kVCTPTCService.KVCTPTC_NT_GroupBy_SoCTs(kVCTPTCs1)
                    });
                }
                else
                {
                    tonQuy_LoaiTien_KVCTPTC.Add(new TonQuy_LoaiTien_KVCTPTC()
                    {
                        NgoaiTe = ngoaiTe,
                        TonQuy = new TonQuy() /*{ SoTienNT = 0, SoTien = 0 }*/,
                        KVCTPTC_NT_GroupBy_SoCTs = new List<KVCTPTC_NT_GroupBy_SoCTs>()
                    });
                }
            } // group by theo ngoaite

            tonQuy_LoaiTien_KVCTPTC = tonQuy_LoaiTien_KVCTPTC.OrderBy(x => x.NgoaiTe.MaNt).ToList();

            foreach (var item in tonQuy_LoaiTien_KVCTPTC)
            {
                try
                {
                    item.CongPhatSinh_Thu_NT = item.KVCTPTCs == null ? 0 : item.KVCTPTCs.Where(x => x.SoCT.Contains("NT")).Sum(x => x.SoTienNT).Value;
                    item.CongPhatSinh_Thu = item.KVCTPTCs == null ? 0 : item.KVCTPTCs.Where(x => x.SoCT.Contains("NT")).Sum(x => x.SoTien).Value;

                    item.CongPhatSinh_Chi_NC = item.KVCTPTCs == null ? 0 : item.KVCTPTCs.Where(x => x.SoCT.Contains("NC")).Sum(x => x.SoTienNT).Value;
                    item.CongPhatSinh_Chi = item.KVCTPTCs == null ? 0 : item.KVCTPTCs.Where(x => x.SoCT.Contains("NC")).Sum(x => x.SoTien).Value;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            ExcelPackage ExcelApp = new ExcelPackage();
            ExcelWorksheet xlSheet = ExcelApp.Workbook.Worksheets.Add("Report");
            // Định dạng chiều dài cho cột
            xlSheet.Column(1).Width = 20;
            xlSheet.Column(2).Width = 20;
            xlSheet.Column(3).Width = 40;
            xlSheet.Column(4).Width = 30;
            xlSheet.Column(5).Width = 20;
            xlSheet.Column(6).Width = 15;
            xlSheet.Column(7).Width = 15;
            xlSheet.Column(8).Width = 15;
            xlSheet.Column(9).Width = 15;

            xlSheet.Cells[1, 1].Value = "CÔNG TY TNHH MỘT THÀNH VIÊN DỊCH VỤ LỮ HÀNH SAIGONTOURIST";
            xlSheet.Cells[1, 1].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
            xlSheet.Cells[1, 1].Style.Font.SetFromFont(new Font("Times New Roman", 12, FontStyle.Bold));
            xlSheet.Cells[1, 1, 2, 3].Merge = true;

            xlSheet.Cells[1, 5].Value = "CỘNG HOÀ XÃ HỘI CHỦ NGHĨA VIỆT NAM";
            xlSheet.Cells[1, 5].Style.Font.SetFromFont(new Font("Times New Roman", 12, FontStyle.Bold));
            xlSheet.Cells[1, 5, 1, 9].Merge = true;

            xlSheet.Cells[2, 5].Value = "Độc Lập - Tự Do - Hạnh Phúc";
            xlSheet.Cells[2, 5].Style.Font.SetFromFont(new Font("Times New Roman", 12, FontStyle.Bold));
            xlSheet.Cells[2, 5, 2, 9].Merge = true;

            string stringSoQuy = "SỔ QUỸ KIÊM BÁO CÁO QUỸ NGOẠI TỆ ";
            string stringTuNgay;
            if (searchFromDate != searchToDate)
            {
                stringTuNgay = "Từ ngày " + searchFromDate + " đến ngày " + searchToDate;
            }
            else
            {
                stringTuNgay = "Ngày " + searchFromDate;
            }
            xlSheet.Cells[3, 1].Value = stringSoQuy;
            xlSheet.Cells[3, 1].Style.Font.SetFromFont(new Font("Times New Roman", 16, FontStyle.Bold));
            xlSheet.Cells[3, 1, 3, 7].Merge = true;
            xlSheet.Cells[4, 1].Value = stringTuNgay;
            xlSheet.Cells[4, 1].Style.Font.SetFromFont(new Font("Times New Roman", 12, FontStyle.Bold | FontStyle.Italic));
            //xlSheet.Cells[4, 1].Style.Font.Bold = true;
            //xlSheet.Cells[4, 1].Style.Font.Italic = true;
            xlSheet.Cells[4, 1, 4, 9].Merge = true;

            // Tạo header
            xlSheet.Cells[6, 1].Value = "SỐ CT";
            xlSheet.Cells[6, 1, 6, 2].Merge = true;
            xlSheet.Cells[7, 1].Value = "THU";
            xlSheet.Cells[7, 2].Value = "CHI";
            xlSheet.Cells[6, 3].Value = "DIỄN GIẢI";
            //xlSheet.Cells[6, 3].Style.WrapText = true;
            xlSheet.Cells[6, 3, 7, 3].Merge = true;
            xlSheet.Cells[6, 4].Value = "HỌ TÊN";
            //xlSheet.Cells[6, 4].Style.WrapText = true;
            xlSheet.Cells[6, 4, 7, 4].Merge = true;
            xlSheet.Cells[6, 5].Value = "TK ĐỐI ỨNG";
            //xlSheet.Cells[6, 5].Style.WrapText = true;
            xlSheet.Cells[6, 5, 7, 5].Merge = true;
            xlSheet.Cells[6, 6].Value = "SỐ TIỀN NT";
            xlSheet.Cells[6, 6, 6, 7].Merge = true;
            xlSheet.Cells[7, 6].Value = "THU";
            xlSheet.Cells[7, 7].Value = "CHI";

            xlSheet.Cells[6, 8].Value = "SỐ TIỀN";
            xlSheet.Cells[6, 8, 6, 9].Merge = true;
            xlSheet.Cells[7, 8].Value = "THU";
            xlSheet.Cells[7, 9].Value = "CHI";

            xlSheet.Cells[6, 1, 7, 9].Style.Font.SetFromFont(new Font("Times New Roman", 12, FontStyle.Bold));
            xlSheet.Cells[6, 1, 7, 9].Style.WrapText = true;
            xlSheet.Cells[6, 1, 7, 9].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center; // Alignment is center
            xlSheet.Cells[6, 1, 7, 9].Style.VerticalAlignment = ExcelVerticalAlignment.Center; // Alignment is center
            //worksheet.Column(8).Style.VerticalAlignment = ExcelVerticalAlignment.Top;

            setBorder(6, 1, 7, 9, xlSheet);
            setCenterAligment(1, 1, 7, 9, xlSheet);

            // do du lieu tu table
            int dong = 8;

            //du lieu
            //int iRowIndex = 6;

            Color colFromHex = System.Drawing.ColorTranslator.FromHtml("#D3D3D3");// ColorTranslator.FromHtml("#D3D3D3");
            Color colorTotalRow = ColorTranslator.FromHtml("#66ccff");
            Color colorThanhLy = ColorTranslator.FromHtml("#7FFF00");
            Color colorChuaThanhLy = ColorTranslator.FromHtml("#FFDEAD");

            int idem = 1;

            //xlSheet.Cells[dong, 3].Value = "TỒN ĐẦU";
            //TrSetCellBorder(xlSheet, dong, 3, ExcelBorderStyle.None, ExcelHorizontalAlignment.Center, Color.Silver, "Times New Roman", 11, FontStyle.Bold);

            //// xlSheet.Cells[dong, 6].Value = tonQuy.SoTien;
            //TrSetCellBorder(xlSheet, dong, 6, ExcelBorderStyle.None, ExcelHorizontalAlignment.Center, Color.Silver, "Times New Roman", 11, FontStyle.Bold);
            ////NumberFormat(dong, 6, dong, 6, xlSheet);
            //dong++;

            if (tonQuy_LoaiTien_KVCTPTC.Count() > 0)
            {
                foreach (var item in tonQuy_LoaiTien_KVCTPTC)
                {
                    xlSheet.Cells[dong, 3].Value = "TỒN ĐẦU " + item.NgoaiTe.TenNt;
                    TrSetCellBorder(xlSheet, dong, 3, ExcelBorderStyle.None, ExcelHorizontalAlignment.Left, Color.Silver, "Times New Roman", 11, FontStyle.Bold);

                    if (item.TonQuy.SoTienNT != 0)
                    {
                        xlSheet.Cells[dong, 6].Value = item.TonQuy.SoTienNT;
                    }
                    else
                    {
                        xlSheet.Cells[dong, 6].Value = "";
                    }
                    TrSetCellBorder(xlSheet, dong, 6, ExcelBorderStyle.None, ExcelHorizontalAlignment.Center, Color.Silver, "Times New Roman", 11, FontStyle.Bold);

                    if (item.TonQuy.SoTien != 0)
                    {
                        xlSheet.Cells[dong, 8].Value = item.TonQuy.SoTien;
                    }
                    else
                    {
                        xlSheet.Cells[dong, 8].Value = "";
                    }
                    TrSetCellBorder(xlSheet, dong, 8, ExcelBorderStyle.None, ExcelHorizontalAlignment.Center, Color.Silver, "Times New Roman", 11, FontStyle.Bold);

                    //foreach (var kvctpct in item.KVCTPTCs)
                    foreach (var kvctpctG in item.KVCTPTC_NT_GroupBy_SoCTs)
                    {

                        dong++;
                        if (kvctpctG.SoCT.Contains("NT"))
                        {
                            xlSheet.Cells[dong, 1].Value = kvctpctG.SoCT;
                            TrSetCellBorder(xlSheet, dong, 1, ExcelBorderStyle.None, ExcelHorizontalAlignment.Left, Color.Silver, "Times New Roman", 11, FontStyle.Bold);

                            xlSheet.Cells[dong, 3].Value = kvctpctG.KVCTPTCs.FirstOrDefault().KVPTC.NgayCT;
                            DateFormat(dong, 3, dong, 3, xlSheet);
                            TrSetCellBorder(xlSheet, dong, 3, ExcelBorderStyle.None, ExcelHorizontalAlignment.Left, Color.Silver, "Times New Roman", 11, FontStyle.Bold);

                            xlSheet.Cells[dong, 4].Value = kvctpctG.KVCTPTCs.FirstOrDefault().KVPTC.HoTen;//.TenKH;
                            TrSetCellBorder(xlSheet, dong, 4, ExcelBorderStyle.None, ExcelHorizontalAlignment.Left, Color.Silver, "Times New Roman", 11, FontStyle.Bold);

                            xlSheet.Cells[dong, 6].Value = kvctpctG.KVCTPTCs.Sum(x => x.SoTienNT);// kvctpct.SoTienNT;//.SoTienNT;
                            TrSetCellBorder(xlSheet, dong, 6, ExcelBorderStyle.None, ExcelHorizontalAlignment.Left, Color.Silver, "Times New Roman", 11, FontStyle.Bold);

                            xlSheet.Cells[dong, 8].Value = kvctpctG.KVCTPTCs.Sum(x => x.SoTien);// kvctpct.SoTien;//.SoTien;
                            TrSetCellBorder(xlSheet, dong, 8, ExcelBorderStyle.None, ExcelHorizontalAlignment.Left, Color.Silver, "Times New Roman", 11, FontStyle.Bold);

                            dong++;
                            foreach (var kvctptc in kvctpctG.KVCTPTCs)
                            {
                                xlSheet.Cells[dong, 3].Value = kvctptc.DienGiai;
                                TrSetCellBorder(xlSheet, dong, 3, ExcelBorderStyle.None, ExcelHorizontalAlignment.Left, Color.Silver, "Times New Roman", 11, FontStyle.Regular);
                                xlSheet.Cells[dong, 5].Value = kvctptc.TKNo; // TK doi ung
                                TrSetCellBorder(xlSheet, dong, 5, ExcelBorderStyle.None, ExcelHorizontalAlignment.Left, Color.Silver, "Times New Roman", 11, FontStyle.Regular);
                                xlSheet.Cells[dong, 6].Value = kvctptc.SoTienNT;//.SoTienNT;
                                TrSetCellBorder(xlSheet, dong, 6, ExcelBorderStyle.None, ExcelHorizontalAlignment.Left, Color.Silver, "Times New Roman", 11, FontStyle.Regular);
                                xlSheet.Cells[dong, 8].Value = kvctptc.SoTien;//.SoTien;
                                TrSetCellBorder(xlSheet, dong, 8, ExcelBorderStyle.None, ExcelHorizontalAlignment.Left, Color.Silver, "Times New Roman", 11, FontStyle.Regular);
                                dong++;
                            }


                        }
                        else
                        {

                            xlSheet.Cells[dong, 2].Value = kvctpctG.SoCT;
                            TrSetCellBorder(xlSheet, dong, 1, ExcelBorderStyle.None, ExcelHorizontalAlignment.Left, Color.Silver, "Times New Roman", 11, FontStyle.Bold);

                            xlSheet.Cells[dong, 3].Value = kvctpctG.KVCTPTCs.FirstOrDefault().KVPTC.NgayCT; // kvctpctG.NgayCT;
                            DateFormat(dong, 3, dong, 3, xlSheet);
                            TrSetCellBorder(xlSheet, dong, 3, ExcelBorderStyle.None, ExcelHorizontalAlignment.Left, Color.Silver, "Times New Roman", 11, FontStyle.Bold);

                            xlSheet.Cells[dong, 4].Value = kvctpctG.KVCTPTCs.FirstOrDefault().KVPTC.HoTen; // kvctpctG.HoTen;//.TenKH;
                            TrSetCellBorder(xlSheet, dong, 4, ExcelBorderStyle.None, ExcelHorizontalAlignment.Left, Color.Silver, "Times New Roman", 11, FontStyle.Bold);

                            xlSheet.Cells[dong, 7].Value = kvctpctG.KVCTPTCs.Sum(x => x.SoTienNT);// kvctpct.SoTienNT;//.SoTienNT;
                            TrSetCellBorder(xlSheet, dong, 7, ExcelBorderStyle.None, ExcelHorizontalAlignment.Left, Color.Silver, "Times New Roman", 11, FontStyle.Bold);

                            xlSheet.Cells[dong, 9].Value = kvctpctG.KVCTPTCs.Sum(x => x.SoTien);// kvctpct.SoTien;//.SoTien;
                            TrSetCellBorder(xlSheet, dong, 9, ExcelBorderStyle.None, ExcelHorizontalAlignment.Left, Color.Silver, "Times New Roman", 11, FontStyle.Regular);

                            dong++;
                            foreach (var kvctptc in kvctpctG.KVCTPTCs)
                            {
                                xlSheet.Cells[dong, 3].Value = kvctptc.DienGiai;
                                TrSetCellBorder(xlSheet, dong, 3, ExcelBorderStyle.None, ExcelHorizontalAlignment.Left, Color.Silver, "Times New Roman", 11, FontStyle.Regular);
                                xlSheet.Cells[dong, 5].Value = kvctptc.TKCo;
                                TrSetCellBorder(xlSheet, dong, 5, ExcelBorderStyle.None, ExcelHorizontalAlignment.Left, Color.Silver, "Times New Roman", 11, FontStyle.Regular);
                                xlSheet.Cells[dong, 7].Value = kvctptc.SoTienNT;//.SoTienNT;
                                TrSetCellBorder(xlSheet, dong, 7, ExcelBorderStyle.None, ExcelHorizontalAlignment.Left, Color.Silver, "Times New Roman", 11, FontStyle.Regular);
                                xlSheet.Cells[dong, 9].Value = kvctptc.SoTien;//.SoTien;
                                TrSetCellBorder(xlSheet, dong, 9, ExcelBorderStyle.None, ExcelHorizontalAlignment.Left, Color.Silver, "Times New Roman", 11, FontStyle.Regular);
                                dong++;
                            }
                            
                        }

                        //dong++;
                    }
                    dong++;

                    xlSheet.Cells[dong, 3].Value = "CỘNG PHÁT SINH";
                    xlSheet.Cells[dong, 3].Style.Font.SetFromFont(new Font("Times New Roman", 11, FontStyle.Bold | FontStyle.Italic));
                    if (item.CongPhatSinh_Thu_NT != 0)
                    {
                        xlSheet.Cells[dong, 6].Value = item.CongPhatSinh_Thu_NT;
                    }
                    else
                    {
                        xlSheet.Cells[dong, 6].Value = "";
                    }
                    xlSheet.Cells[dong, 6].Style.Font.SetFromFont(new Font("Times New Roman", 11, FontStyle.Bold));
                    if (item.CongPhatSinh_Chi_NC != 0)
                    {
                        xlSheet.Cells[dong, 7].Value = item.CongPhatSinh_Chi_NC;
                    }
                    else
                    {
                        xlSheet.Cells[dong, 7].Value = "";
                    }
                    xlSheet.Cells[dong, 7].Style.Font.SetFromFont(new Font("Times New Roman", 11, FontStyle.Bold));
                    if (item.CongPhatSinh_Thu != 0)
                    {
                        xlSheet.Cells[dong, 8].Value = item.CongPhatSinh_Thu;
                    }
                    else
                    {
                        xlSheet.Cells[dong, 8].Value = "";
                    }
                    xlSheet.Cells[dong, 8].Style.Font.SetFromFont(new Font("Times New Roman", 11, FontStyle.Bold));
                    if (item.CongPhatSinh_Chi != 0)
                    {
                        xlSheet.Cells[dong, 9].Value = item.CongPhatSinh_Chi;
                    }
                    else
                    {
                        xlSheet.Cells[dong, 9].Value = "";
                    }
                    xlSheet.Cells[dong, 9].Style.Font.SetFromFont(new Font("Times New Roman", 11, FontStyle.Bold));

                    dong++;

                    xlSheet.Cells[dong, 3].Value = "TỒN CUỐI";
                    xlSheet.Cells[dong, 3].Style.Font.SetFromFont(new Font("Times New Roman", 11, FontStyle.Bold));

                    decimal tonCuoiNT = item.TonQuy.SoTienNT + item.CongPhatSinh_Thu_NT - item.CongPhatSinh_Chi_NC;
                    if (tonCuoiNT != 0)
                    {
                        xlSheet.Cells[dong, 6].Value = tonCuoiNT;
                    }
                    else
                    {
                        xlSheet.Cells[dong, 6].Value = "";
                    }
                    xlSheet.Cells[dong, 6].Style.Font.SetFromFont(new Font("Times New Roman", 11, FontStyle.Bold));

                    decimal tonCuoi = item.TonQuy.SoTien + item.CongPhatSinh_Thu - item.CongPhatSinh_Chi;
                    if (tonCuoi != 0)
                    {
                        xlSheet.Cells[dong, 8].Value = tonCuoi;
                    }
                    else
                    {
                        xlSheet.Cells[dong, 8].Value = "";
                    }
                    xlSheet.Cells[dong, 8].Style.Font.SetFromFont(new Font("Times New Roman", 11, FontStyle.Regular));

                    dong++;
                    dong++;

                    // ghi log va save tonquy tbl
                    TonQuy tonQuy1 = new TonQuy()
                    {
                        LoaiTien = item.TonQuy.LoaiTien,
                        TyGia = item.TonQuy.TyGia,
                        LogFile = "-User tạo: " + user.Username + " vào lúc: " + System.DateTime.Now.ToString(), // user.Username
                        NgayCT = DateTime.Parse(searchToDate), //
                        NgayTao = DateTime.Now,
                        NguoiTao = user.Hoten,
                        SoTien = tonCuoi,
                        SoTienNT = tonCuoiNT,
                        MaCn = user.Macn
                    };

                    var tonQuies1 = _tonQuyService.Find_Equal_By_Date(tonQuy1.NgayCT.Value, user.Macn, item.TonQuy.LoaiTien);
                    if (tonQuies1.Count > 0) // co ton tai
                    {
                        var tonQuy2 = _tonQuyService.GetById(tonQuies1.FirstOrDefault().Id);
                        tonQuy2.LogFile += "==== người chạy lại " + user.Username + " lúc: " + DateTime.Now;
                        tonQuy2.SoTien = tonCuoi;
                        tonQuy2.SoTienNT = tonCuoiNT;

                        await _tonQuyService.UpdateAsync(tonQuy2);
                    }
                    else
                    {
                        await _tonQuyService.CreateAsync(tonQuy1);
                    }
                }
            }
            dong++;

            NumberFormat(8, 6, dong, 9, xlSheet);
            //setFontBold(dong, 1, dong, 10, 12, xlSheet);
            setBorder(6, 1, dong - 2, 9, xlSheet);
            //dong++;

            xlSheet.Cells[dong, 2].Value = "Người báo cáo";
            xlSheet.Cells[dong, 2].Style.Font.SetFromFont(new Font("Times New Roman", 11));
            xlSheet.Cells[dong, 4].Value = "Thủ quỹ";
            xlSheet.Cells[dong, 4].Style.Font.SetFromFont(new Font("Times New Roman", 11));
            xlSheet.Cells[dong, 6].Value = "Kế toán trưởng";
            xlSheet.Cells[dong, 6].Style.Font.SetFromFont(new Font("Times New Roman", 11));
            xlSheet.Cells[dong, 6, dong, 8].Merge = true;

            setCenterAligment(dong, 1, dong, 9, xlSheet);

            //// ghi log va save tonquy tbl
            //TonQuy tonQuy1 = new TonQuy()
            //{
            //    LoaiTien = "VND",
            //    TyGia = 1,
            //    LogFile = "-User tạo: " + user.Username + " vào lúc: " + System.DateTime.Now.ToString(), // user.Username
            //    NgayCT = DateTime.Parse(searchToDate), //
            //    NgayTao = DateTime.Now,
            //    NguoiTao = user.Hoten,
            //    //SoTien = tonCuoi,
            //    //SoTienNT = tonCuoi,
            //    MaCn = user.Macn
            //};

            //var tonQuies1 = _tonQuyService.Find_Equal_By_Date(tonQuy1.NgayCT.Value);
            //if (tonQuies1.Count > 0) // co ton tai
            //{
            //    var tonQuy2 = _tonQuyService.GetById(tonQuies1.FirstOrDefault().Id);
            //    tonQuy2.LogFile += "==== người chạy lại " + user.Username + " lúc: " + DateTime.Now;
            //    //tonQuy2.SoTien = tonCuoi;
            //    //tonQuy2.SoTienNT = tonCuoi;

            //    await _tonQuyService.UpdateAsync(tonQuy2);
            //}
            //else
            //{
            //    await _tonQuyService.CreateAsync(tonQuy1);
            //}

            ////}

            byte[] fileContents;
            try
            {
                fileContents = ExcelApp.GetAsByteArray();
                return File(
                fileContents: fileContents,
                contentType: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                fileDownloadName: "QuyTienNT_" + System.DateTime.Now.ToString("dd/MM/yyyy HH:mm") + ".xlsx");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // BaoCaoQuyTienNT
        [HttpPost]
        public async Task<IActionResult> BaoCaoQuyTienNT(string searchFromDate, string searchToDate)
        {
            // from session
            var user = HttpContext.Session.GetSingle<User>("loginUser");

            // check ngay ton quy
            var result = CheckNgayTonQuy(searchFromDate, searchToDate);
            var value = JsonConvert.SerializeObject(result.Result.Value);
            var viewModel = JsonConvert.DeserializeObject<ViewModel>(value);
            if (!viewModel.Status)
            {
                SetAlert(viewModel.Message, "warning");
                return LocalRedirect("/");
            }
            // check ngay ton quy

            //string toDate = DateTime.Parse(searchToDate).AddDays(-1).ToString("dd/MM/yyyy");
            string fromDate = DateTime.Parse(searchFromDate).AddDays(-1).ToString("dd/MM/yyyy");
            //var tonQuies = _tonQuyService.FindTonQuy_By_Date("02/01/2020", toDate);
            IEnumerable<NgoaiTe> ngoaiTes = _baoCaoService.GetAllNgoaiTe().Where(x => x.MaNt != "VND");

            ExcelPackage ExcelApp = new ExcelPackage();

            foreach (var ngoaiTe in ngoaiTes.OrderByDescending(x => x.MaNt))
            {
                TonQuy_LoaiTien_KVCTPCT_GroupBy_SoCTs tonQuy_LoaiTien_KVCTPCT_GroupBy_SoCT = new TonQuy_LoaiTien_KVCTPCT_GroupBy_SoCTs();

                // tonquy theo loaitien
                var tonQuies = _tonQuyService.FindTonQuy_By_Date("02/01/2020", fromDate, user.Macn, ngoaiTe.MaNt);
                var tonQuy = tonQuies.OrderByDescending(x => x.NgayCT).FirstOrDefault();
                if (tonQuy != null)
                {
                    IEnumerable<KVCTPTC> kVCTPTCs1 = await _kVCTPTCService.FinBy_TonQuy_Date(
                    tonQuy.NgayCT.Value.ToShortDateString(), searchToDate, user.Macn, ngoaiTe.MaNt); // loaitien != "VND"

                    tonQuy_LoaiTien_KVCTPCT_GroupBy_SoCT = new TonQuy_LoaiTien_KVCTPCT_GroupBy_SoCTs()
                    {
                        NgoaiTe = ngoaiTe,
                        TonQuy = tonQuy,
                        KVCTPTC_NT_GroupBy_SoCTs = _kVCTPTCService.KVCTPTC_NT_GroupBy_SoCTs(kVCTPTCs1).OrderBy(x => x.SoCT)
                    };
                }
                else
                {
                    tonQuy_LoaiTien_KVCTPCT_GroupBy_SoCT = new TonQuy_LoaiTien_KVCTPCT_GroupBy_SoCTs()
                    {
                        NgoaiTe = ngoaiTe,
                        TonQuy = new TonQuy() /*{ SoTienNT = 0, SoTien = 0 }*/,
                        KVCTPTC_NT_GroupBy_SoCTs = new List<KVCTPTC_NT_GroupBy_SoCTs>()
                    };
                }

                // export
                ExcelWorksheet xlSheet = ExcelApp.Workbook.Worksheets.Add(ngoaiTe.MaNt);
                // Định dạng chiều dài cho cột

                xlSheet.Column(1).Width = 40; // DIỄN GIẢI
                xlSheet.Column(2).Width = 15; // TK ĐỐI ỨNG
                xlSheet.Column(3).Width = 15; // SỐ TIỀN NT THU
                xlSheet.Column(4).Width = 15; // SỐ TIỀN NT CHI
                xlSheet.Column(5).Width = 15; // SỐ TIỀN THU
                xlSheet.Column(6).Width = 15; // SỐ TIỀN THU

                xlSheet.Cells[1, 1].Value = "CÔNG TY TNHH MỘT THÀNH VIÊN DỊCH VỤ LỮ HÀNH SAIGONTOURIST";
                xlSheet.Cells[1, 1].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                xlSheet.Cells[1, 1].Style.WrapText = true;
                xlSheet.Cells[1, 1].Style.Font.SetFromFont(new Font("Times New Roman", 12, FontStyle.Regular));
                xlSheet.Cells[1, 1, 2, 2].Merge = true;

                xlSheet.Cells[1, 3].Value = "CỘNG HOÀ XÃ HỘI CHỦ NGHĨA VIỆT NAM";
                xlSheet.Cells[1, 3].Style.Font.SetFromFont(new Font("Times New Roman", 12, FontStyle.Regular));
                xlSheet.Cells[1, 3, 1, 6].Merge = true;

                xlSheet.Cells[2, 3].Value = "Độc Lập - Tự Do - Hạnh Phúc";
                xlSheet.Cells[2, 3].Style.Font.SetFromFont(new Font("Times New Roman", 12, FontStyle.Regular));
                xlSheet.Cells[2, 3, 2, 6].Merge = true;

                string stringSoQuy = "SỔ QUỸ KIÊM BÁO CÁO QUỸ NGOẠI TỆ " + ngoaiTe.MaNt;
                string stringTuNgay;
                if (searchFromDate != searchToDate)
                {
                    stringTuNgay = "Từ ngày " + searchFromDate + " đến ngày " + searchToDate;
                }
                else
                {
                    stringTuNgay = "Ngày " + searchFromDate;
                }
                xlSheet.Cells[3, 1].Value = stringSoQuy;
                xlSheet.Cells[3, 1].Style.Font.SetFromFont(new Font("Times New Roman", 16, FontStyle.Bold));
                xlSheet.Cells[3, 1, 3, 6].Merge = true;
                xlSheet.Cells[4, 1].Value = stringTuNgay;
                xlSheet.Cells[4, 1].Style.Font.SetFromFont(new Font("Times New Roman", 12, FontStyle.Regular | FontStyle.Italic));
                //xlSheet.Cells[4, 1].Style.Font.Bold = true;
                //xlSheet.Cells[4, 1].Style.Font.Italic = true;
                xlSheet.Cells[4, 1, 4, 6].Merge = true;

                // Tạo header
                xlSheet.Cells[6, 1].Value = "DIỄN GIẢI";
                xlSheet.Cells[6, 1, 7, 1].Merge = true;
                xlSheet.Cells[6, 2].Value = "TK ĐỐI ỨNG";
                xlSheet.Cells[6, 2, 7, 2].Merge = true;
                xlSheet.Cells[6, 3].Value = "SỐ TIỀN NT";
                //xlSheet.Cells[6, 5].Style.WrapText = true;
                xlSheet.Cells[6, 3, 6, 4].Merge = true;
                xlSheet.Cells[7, 3].Value = "THU";
                xlSheet.Cells[7, 4].Value = "CHI";
                xlSheet.Cells[6, 5].Value = "SỐ TIỀN";
                //xlSheet.Cells[6, 5].Style.WrapText = true;
                xlSheet.Cells[6, 5, 6, 6].Merge = true;
                xlSheet.Cells[7, 5].Value = "THU";
                xlSheet.Cells[7, 6].Value = "CHI";

                xlSheet.Cells[6, 1, 7, 6].Style.Font.SetFromFont(new Font("Times New Roman", 12, FontStyle.Regular));
                xlSheet.Cells[6, 1, 7, 6].Style.WrapText = true;
                xlSheet.Cells[6, 1, 7, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center; // Alignment is center
                xlSheet.Cells[6, 1, 7, 6].Style.VerticalAlignment = ExcelVerticalAlignment.Center; // Alignment is center
                                                                                                   //worksheet.Column(8).Style.VerticalAlignment = ExcelVerticalAlignment.Top;

                setBorder(6, 1, 7, 6, xlSheet);
                setCenterAligment(1, 1, 7, 6, xlSheet);

                // do du lieu tu table
                int dong = 8;

                //du lieu
                //int iRowIndex = 6;

                Color colFromHex = System.Drawing.ColorTranslator.FromHtml("#D3D3D3");// ColorTranslator.FromHtml("#D3D3D3");
                Color colorTotalRow = ColorTranslator.FromHtml("#66ccff");
                Color colorThanhLy = ColorTranslator.FromHtml("#7FFF00");
                Color colorChuaThanhLy = ColorTranslator.FromHtml("#FFDEAD");

                int idem = 1;

                //xlSheet.Cells[dong, 3].Value = "TỒN ĐẦU";
                //TrSetCellBorder(xlSheet, dong, 3, ExcelBorderStyle.None, ExcelHorizontalAlignment.Center, Color.Silver, "Times New Roman", 11, FontStyle.Bold);

                //// xlSheet.Cells[dong, 6].Value = tonQuy.SoTien;
                //TrSetCellBorder(xlSheet, dong, 6, ExcelBorderStyle.None, ExcelHorizontalAlignment.Center, Color.Silver, "Times New Roman", 11, FontStyle.Bold);
                ////NumberFormat(dong, 6, dong, 6, xlSheet);
                //dong++;

                //if (tonQuy_LoaiTien_KVCTPCT_GroupBy_SoCT.KVCTPTC_NT_GroupBy_SoCTs.Count() > 0)
                if (tonQuy_LoaiTien_KVCTPCT_GroupBy_SoCT != null)
                {
                    xlSheet.Cells[dong, 1].Value = "TỒN ĐẦU " + tonQuy_LoaiTien_KVCTPCT_GroupBy_SoCT.NgoaiTe.TenNt;
                    TrSetCellBorder(xlSheet, dong, 1, ExcelBorderStyle.None, ExcelHorizontalAlignment.Left, Color.Silver, "Times New Roman", 11, FontStyle.Bold);

                    if (tonQuy_LoaiTien_KVCTPCT_GroupBy_SoCT.TonQuy.SoTienNT != 0)
                    {
                        xlSheet.Cells[dong, 3].Value = tonQuy_LoaiTien_KVCTPCT_GroupBy_SoCT.TonQuy.SoTienNT;
                    }
                    else
                    {
                        xlSheet.Cells[dong, 3].Value = "";
                    }
                    TrSetCellBorder(xlSheet, dong, 3, ExcelBorderStyle.None, ExcelHorizontalAlignment.Center, Color.Silver, "Times New Roman", 11, FontStyle.Bold);

                    if (tonQuy_LoaiTien_KVCTPCT_GroupBy_SoCT.TonQuy.SoTien != 0)
                    {
                        xlSheet.Cells[dong, 5].Value = tonQuy_LoaiTien_KVCTPCT_GroupBy_SoCT.TonQuy.SoTien;
                    }
                    else
                    {
                        xlSheet.Cells[dong, 5].Value = "";
                    }
                    TrSetCellBorder(xlSheet, dong, 5, ExcelBorderStyle.None, ExcelHorizontalAlignment.Center, Color.Silver, "Times New Roman", 11, FontStyle.Bold);
                    dong++;
                    foreach (var item in tonQuy_LoaiTien_KVCTPCT_GroupBy_SoCT.KVCTPTC_NT_GroupBy_SoCTs)
                    {
                        //dong++;

                        xlSheet.Cells[dong, 1].Value = item.SoCT + " " +
                            item.KVCTPTCs.FirstOrDefault().KVPTC.NgayCT.Value.ToString("dd/MM/yyyy") + " " +
                            item.KVCTPTCs.FirstOrDefault().KVPTC.HoTen;
                        TrSetCellBorder(xlSheet, dong, 1, ExcelBorderStyle.None, ExcelHorizontalAlignment.Left, Color.Silver, "Times New Roman", 11, FontStyle.Bold);
                        xlSheet.Cells[dong, 1].Style.WrapText = true;

                        if (item.SoCT.Contains("NT"))
                        {
                            xlSheet.Cells[dong, 3].Value = item.TongCong_Thu_NT;
                            TrSetCellBorder(xlSheet, dong, 3, ExcelBorderStyle.None, ExcelHorizontalAlignment.Left, Color.Silver, "Times New Roman", 11, FontStyle.Bold);
                            xlSheet.Cells[dong, 5].Value = item.TongCong_Thu;
                            TrSetCellBorder(xlSheet, dong, 5, ExcelBorderStyle.None, ExcelHorizontalAlignment.Left, Color.Silver, "Times New Roman", 11, FontStyle.Bold);
                        }
                        else
                        {
                            xlSheet.Cells[dong, 4].Value = item.TongCong_Chi_NT;
                            TrSetCellBorder(xlSheet, dong, 4, ExcelBorderStyle.None, ExcelHorizontalAlignment.Left, Color.Silver, "Times New Roman", 11, FontStyle.Bold);
                            xlSheet.Cells[dong, 6].Value = item.TongCong_Chi;
                            TrSetCellBorder(xlSheet, dong, 6, ExcelBorderStyle.None, ExcelHorizontalAlignment.Left, Color.Silver, "Times New Roman", 11, FontStyle.Bold);
                        }

                        dong++;

                        foreach (var item1 in item.KVCTPTCs)
                        {
                            if (item1.KVPTC.SoCT.Contains("NT"))
                            {
                                xlSheet.Cells[dong, 1].Value = item1.DienGiai;
                                TrSetCellBorder(xlSheet, dong, 1, ExcelBorderStyle.None, ExcelHorizontalAlignment.Left, Color.Silver, "Times New Roman", 11, FontStyle.Regular);

                                xlSheet.Cells[dong, 2].Value = item1.TKCo;
                                TrSetCellBorder(xlSheet, dong, 2, ExcelBorderStyle.None, ExcelHorizontalAlignment.Left, Color.Silver, "Times New Roman", 11, FontStyle.Regular);

                                xlSheet.Cells[dong, 3].Value = item1.SoTienNT;//.SoTienNT;
                                TrSetCellBorder(xlSheet, dong, 3, ExcelBorderStyle.None, ExcelHorizontalAlignment.Left, Color.Silver, "Times New Roman", 11, FontStyle.Regular);
                                xlSheet.Cells[dong, 5].Value = item1.SoTien;//.SoTien;
                                TrSetCellBorder(xlSheet, dong, 5, ExcelBorderStyle.None, ExcelHorizontalAlignment.Left, Color.Silver, "Times New Roman", 11, FontStyle.Regular);
                            }
                            else
                            {
                                xlSheet.Cells[dong, 1].Value = item1.DienGiai;
                                TrSetCellBorder(xlSheet, dong, 1, ExcelBorderStyle.None, ExcelHorizontalAlignment.Left, Color.Silver, "Times New Roman", 11, FontStyle.Regular);

                                xlSheet.Cells[dong, 2].Value = item1.TKNo;
                                TrSetCellBorder(xlSheet, dong, 2, ExcelBorderStyle.None, ExcelHorizontalAlignment.Left, Color.Silver, "Times New Roman", 11, FontStyle.Regular);

                                xlSheet.Cells[dong, 4].Value = item1.SoTienNT;//.SoTienNT;
                                TrSetCellBorder(xlSheet, dong, 4, ExcelBorderStyle.None, ExcelHorizontalAlignment.Left, Color.Silver, "Times New Roman", 11, FontStyle.Regular);
                                xlSheet.Cells[dong, 6].Value = item1.SoTien;//.SoTien;
                                TrSetCellBorder(xlSheet, dong, 6, ExcelBorderStyle.None, ExcelHorizontalAlignment.Left, Color.Silver, "Times New Roman", 11, FontStyle.Regular);
                            }
                            dong++;
                        }

                        dong++;
                    }
                    xlSheet.Cells[dong, 1].Value = "CỘNG PHÁT SINH";
                    xlSheet.Cells[dong, 1].Style.Font.SetFromFont(new Font("Times New Roman", 11, FontStyle.Bold | FontStyle.Italic));
                    var congPhatSinhThu_NT = tonQuy_LoaiTien_KVCTPCT_GroupBy_SoCT.KVCTPTC_NT_GroupBy_SoCTs.Sum(x => x.TongCong_Thu_NT);
                    if (congPhatSinhThu_NT != 0)
                    {
                        xlSheet.Cells[dong, 3].Value = congPhatSinhThu_NT;
                    }
                    else
                    {
                        xlSheet.Cells[dong, 3].Value = "";
                    }
                    xlSheet.Cells[dong, 3].Style.Font.SetFromFont(new Font("Times New Roman", 11, FontStyle.Bold));
                    var congPhatSinhChi_NT = tonQuy_LoaiTien_KVCTPCT_GroupBy_SoCT.KVCTPTC_NT_GroupBy_SoCTs.Sum(x => x.TongCong_Chi_NT);
                    if (congPhatSinhChi_NT != 0)
                    {
                        xlSheet.Cells[dong, 4].Value = congPhatSinhChi_NT;
                    }
                    else
                    {
                        xlSheet.Cells[dong, 4].Value = "";
                    }
                    xlSheet.Cells[dong, 4].Style.Font.SetFromFont(new Font("Times New Roman", 11, FontStyle.Bold));
                    var congPhatSinhThu = tonQuy_LoaiTien_KVCTPCT_GroupBy_SoCT.KVCTPTC_NT_GroupBy_SoCTs.Sum(x => x.TongCong_Thu);
                    if (congPhatSinhThu != 0)
                    {
                        xlSheet.Cells[dong, 5].Value = congPhatSinhThu;
                    }
                    else
                    {
                        xlSheet.Cells[dong, 5].Value = "";
                    }
                    xlSheet.Cells[dong, 5].Style.Font.SetFromFont(new Font("Times New Roman", 11, FontStyle.Bold));
                    var congPhatSinhChi = tonQuy_LoaiTien_KVCTPCT_GroupBy_SoCT.KVCTPTC_NT_GroupBy_SoCTs.Sum(x => x.TongCong_Chi);
                    if (congPhatSinhChi != 0)
                    {
                        xlSheet.Cells[dong, 6].Value = congPhatSinhChi;
                    }
                    else
                    {
                        xlSheet.Cells[dong, 6].Value = "";
                    }
                    xlSheet.Cells[dong, 6].Style.Font.SetFromFont(new Font("Times New Roman", 11, FontStyle.Bold));

                    dong++;

                    xlSheet.Cells[dong, 1].Value = "TỒN CUỐI";
                    xlSheet.Cells[dong, 1].Style.Font.SetFromFont(new Font("Times New Roman", 11, FontStyle.Bold));

                    decimal tonCuoiNT = tonQuy_LoaiTien_KVCTPCT_GroupBy_SoCT.TonQuy.SoTienNT +
                                        congPhatSinhThu_NT -
                                        congPhatSinhChi_NT;
                    if (tonCuoiNT != 0)
                    {
                        xlSheet.Cells[dong, 3].Value = tonCuoiNT;
                    }
                    else
                    {
                        xlSheet.Cells[dong, 3].Value = "";
                    }
                    xlSheet.Cells[dong, 3].Style.Font.SetFromFont(new Font("Times New Roman", 11, FontStyle.Bold));

                    decimal tonCuoi = tonQuy_LoaiTien_KVCTPCT_GroupBy_SoCT.TonQuy.SoTien +
                                      congPhatSinhThu -
                                      congPhatSinhChi;
                    if (tonCuoi != 0)
                    {
                        xlSheet.Cells[dong, 5].Value = tonCuoi;
                    }
                    else
                    {
                        xlSheet.Cells[dong, 5].Value = "";
                    }
                    xlSheet.Cells[dong, 5].Style.Font.SetFromFont(new Font("Times New Roman", 11, FontStyle.Bold));

                    dong++;

                    // ghi log va save tonquy tbl
                    TonQuy tonQuy1 = new TonQuy()
                    {
                        LoaiTien = tonQuy_LoaiTien_KVCTPCT_GroupBy_SoCT.TonQuy.LoaiTien,
                        TyGia = tonQuy_LoaiTien_KVCTPCT_GroupBy_SoCT.TonQuy.TyGia,
                        LogFile = "-User tạo: " + user.Username + " vào lúc: " + System.DateTime.Now.ToString(), // user.Username
                        NgayCT = DateTime.Parse(searchToDate), //
                        NgayTao = DateTime.Now,
                        NguoiTao = user.Hoten,
                        SoTien = tonCuoi,
                        SoTienNT = tonCuoiNT,
                        MaCn = user.Macn
                    };

                    var tonQuies1 = _tonQuyService.Find_Equal_By_Date(
                        tonQuy1.NgayCT.Value, user.Macn, tonQuy_LoaiTien_KVCTPCT_GroupBy_SoCT.TonQuy.LoaiTien);
                    if (tonQuies1.Count > 0) // co ton tai
                    {
                        var tonQuy2 = _tonQuyService.GetById(tonQuies1.FirstOrDefault().Id);
                        tonQuy2.LogFile += "==== người chạy lại " + user.Username + " lúc: " + DateTime.Now;
                        tonQuy2.SoTien = tonCuoi;
                        tonQuy2.SoTienNT = tonCuoiNT;

                        await _tonQuyService.UpdateAsync(tonQuy2);
                    }
                    else
                    {
                        await _tonQuyService.CreateAsync(tonQuy1);
                    }
                    // ghi log va save tonquy tbl
                }
                dong++;

                NumberFormat(8, 3, dong, 6, xlSheet);
                //setFontBold(dong, 1, dong, 10, 12, xlSheet);
                setBorder(6, 1, dong - 2, 6, xlSheet);
                //dong++;

                xlSheet.Cells[dong, 1].Value = "Người báo cáo";
                xlSheet.Cells[dong, 1].Style.Font.SetFromFont(new Font("Times New Roman", 11));
                xlSheet.Cells[dong, 3].Value = "Thủ quỹ";
                xlSheet.Cells[dong, 3].Style.Font.SetFromFont(new Font("Times New Roman", 11));
                xlSheet.Cells[dong, 5].Value = "Kế toán trưởng";
                xlSheet.Cells[dong, 5].Style.Font.SetFromFont(new Font("Times New Roman", 11));
                xlSheet.Cells[dong, 5, dong, 6].Merge = true;

                setCenterAligment(dong, 1, dong, 6, xlSheet);

                //// ghi log va save tonquy tbl
                //TonQuy tonQuy1 = new TonQuy()
                //{
                //    LoaiTien = "VND",
                //    TyGia = 1,
                //    LogFile = "-User tạo: " + user.Username + " vào lúc: " + System.DateTime.Now.ToString(), // user.Username
                //    NgayCT = DateTime.Parse(searchToDate), //
                //    NgayTao = DateTime.Now,
                //    NguoiTao = user.Hoten,
                //    //SoTien = tonCuoi,
                //    //SoTienNT = tonCuoi,
                //    MaCn = user.Macn
                //};

                //var tonQuies1 = _tonQuyService.Find_Equal_By_Date(tonQuy1.NgayCT.Value);
                //if (tonQuies1.Count > 0) // co ton tai
                //{
                //    var tonQuy2 = _tonQuyService.GetById(tonQuies1.FirstOrDefault().Id);
                //    tonQuy2.LogFile += "==== người chạy lại " + user.Username + " lúc: " + DateTime.Now;
                //    //tonQuy2.SoTien = tonCuoi;
                //    //tonQuy2.SoTienNT = tonCuoi;

                //    await _tonQuyService.UpdateAsync(tonQuy2);
                //}
                //else
                //{
                //    await _tonQuyService.CreateAsync(tonQuy1);
                //}

                ////}

                // export
            } // group by theo ngoaite

            byte[] fileContents;
            try
            {
                fileContents = ExcelApp.GetAsByteArray();
                return File(
                fileContents: fileContents,
                contentType: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                fileDownloadName: "QuyTienNT_" + System.DateTime.Now.ToString("dd/MM/yyyy HH:mm") + ".xlsx");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // InChungTuGhiSo_Partial
        public IActionResult TheoDoiTUNoiBoTk141_Partial(string searchFromDate, string searchToDate, string errorMessage)
        {
            if (!string.IsNullOrEmpty(errorMessage))
            {
                ModelState.AddModelError("", errorMessage);
            }
            searchFromDate = DateTime.Now.ToString("dd/MM/yyyy");
            searchToDate = DateTime.Now.AddDays(10).ToString("dd/MM/yyyy");
            BaoCaoVM.PhongBans = GetPhongBans_Where();

            ViewBag.searchFromDate = searchFromDate;
            ViewBag.searchToDate = searchToDate;
            if (string.IsNullOrEmpty(searchFromDate) && string.IsNullOrEmpty(searchToDate)) // moi load vao
            {
                BaoCaoVM.TT621s = null;
                return PartialView(BaoCaoVM);
            }

            // dao ngay thang
            DateTime fromDate = DateTime.Parse(searchFromDate); // NgayCT
            DateTime toDate = DateTime.Parse(searchToDate); // NgayCT

            if (fromDate > toDate) // dao nguoc lai
            {
                string tmp = searchFromDate;
                searchFromDate = searchToDate;
                searchToDate = tmp;
                ViewBag.searchFromDate = searchFromDate;
                ViewBag.searchToDate = searchToDate;
            }

            return PartialView(BaoCaoVM);
        }

        // In
        [HttpPost]
        public IActionResult TheoDoiTUNoiBoTk141_Partial_Excel_In(string tuNgay, string denNgay, int id_BoPhan)
        {
            // from session
            var user = HttpContext.Session.GetSingle<User>("loginUser");

            PhongBan phongBan = new PhongBan();
            if (id_BoPhan == 0)
            {
                phongBan = _baoCaoService.GetPhongBanById(1); // IB
            }
            else
            {
                phongBan = _baoCaoService.GetPhongBanById(id_BoPhan);
            }
            IEnumerable<TamUng> tamUngs = _tamUngService.FindTamUngs_IncludeTwice_By_Phong(phongBan.BoPhan, user.Macn);
            List<TamUngModel_GroupBy_Name> tamUngModel_GroupBy_Names = new List<TamUngModel_GroupBy_Name>();
            if (tamUngs.Count() > 0)
            {
                var abc = _tamUngService.TamUngModels_GroupBy_Name(tamUngs.OrderBy(x => x.NgayCT), user.Macn);
                if (abc == null)
                {
                    ViewBag.errorMessage = "Không tìm thấy MaKh";
                    return View("~/Views/Shared/Error.cshtml");
                }
                tamUngModel_GroupBy_Names = abc.ToList();// _tamUngService.TamUngModels_GroupBy_Name(tamUngs.OrderBy(x => x.NgayCT), user.Macn).ToList();
                if (tamUngModel_GroupBy_Names.Count() == 0 || tamUngModel_GroupBy_Names == null)
                {
                    SetAlert("Không tìm thấy MaKh", "warning");
                    return NoContent();
                }
            }

            ExcelPackage ExcelApp = new ExcelPackage();
            ExcelWorksheet xlSheet = ExcelApp.Workbook.Worksheets.Add("Report");
            // Định dạng chiều dài cho cột
            xlSheet.Column(1).Width = 15;// Ngày CT
            xlSheet.Column(2).Width = 15;// Số CT
            xlSheet.Column(3).Width = 40;// Diễn giải
            xlSheet.Column(4).Width = 15;// Số tiền NT
            xlSheet.Column(5).Width = 10;// LT
            xlSheet.Column(6).Width = 10;// Tỷ giá
            xlSheet.Column(7).Width = 15;// VNĐ

            xlSheet.Cells[1, 1].Value = "CÔNG TY TNHH MỘT THÀNH VIÊN";
            xlSheet.Cells[1, 1].Style.Font.SetFromFont(new Font("Times New Roman", 12));
            xlSheet.Cells[1, 1, 1, 3].Merge = true;

            xlSheet.Cells[2, 1].Value = "DVLH SAIGONTOURIST";
            xlSheet.Cells[2, 1].Style.Font.SetFromFont(new Font("Times New Roman", 12));
            xlSheet.Cells[2, 1, 2, 3].Merge = true;

            xlSheet.Cells[1, 4].Value = "CỘNG HOÀ XÃ HỘI CHỦ NGHĨA VIỆT NAM ";
            xlSheet.Cells[1, 4].Style.Font.SetFromFont(new Font("Times New Roman", 12));
            xlSheet.Cells[1, 4, 1, 7].Merge = true;
            setCenterAligment(1, 4, 1, 7, xlSheet);

            xlSheet.Cells[2, 7].Value = "Độc lập - Tự Do - Hạnh Phúc";
            xlSheet.Cells[2, 7].Style.Font.SetFromFont(new Font("Times New Roman", 12));
            xlSheet.Cells[2, 4, 2, 7].Merge = true;
            setCenterAligment(2, 4, 2, 7, xlSheet);

            xlSheet.Cells[3, 1].Value = "BÁO CÁO CHI TIẾT SỐ DƯ TÀI KHOÃN 141";
            xlSheet.Cells[3, 1].Style.Font.SetFromFont(new Font("Times New Roman", 16, FontStyle.Bold));
            xlSheet.Cells[3, 1, 3, 7].Merge = true;
            setCenterAligment(3, 1, 3, 7, xlSheet);

            string stringKinhGoi = "Kính gởi: Trưởng phòng " + phongBan.TenBoPhan;
            string stringXinGui = "Xin gửi phòng nhắc nhỡ các nhân viên sau đây về phòng KT để thanh toán các phiếu tạm ứng trước ngày " + tuNgay;
            xlSheet.Cells[5, 1].Value = stringKinhGoi;
            xlSheet.Cells[5, 1].Style.Font.SetFromFont(new Font("Times New Roman", 12));
            //xlSheet.Cells[4, 1].Style.Font.Bold = true;
            //xlSheet.Cells[4, 1].Style.Font.Italic = true;

            xlSheet.Cells[6, 1].Value = stringXinGui;
            xlSheet.Cells[6, 1].Style.Font.SetFromFont(new Font("Times New Roman", 12));

            // Tạo header
            xlSheet.Cells[7, 1].Value = "Ngày CT";
            xlSheet.Cells[7, 2].Value = "Số CT";
            xlSheet.Cells[7, 3].Value = "Diễn giải";
            xlSheet.Cells[7, 4].Value = "Số tiền NT";
            xlSheet.Cells[7, 5].Value = "LT";
            xlSheet.Cells[7, 6].Value = "Tỷ giá";
            xlSheet.Cells[7, 7].Value = "VNĐ";

            xlSheet.Cells[7, 1, 7, 7].Style.Font.SetFromFont(new Font("Times New Roman", 12, FontStyle.Bold));
            setBorder(7, 1, 7, 7, xlSheet);
            setCenterAligment(7, 1, 7, 7, xlSheet);

            // do du lieu tu table
            int dong = 8;

            //du lieu
            //int iRowIndex = 6;

            Color colFromHex = System.Drawing.ColorTranslator.FromHtml("#D3D3D3");// ColorTranslator.FromHtml("#D3D3D3");
            Color colorTotalRow = ColorTranslator.FromHtml("#66ccff");
            Color colorThanhLy = ColorTranslator.FromHtml("#7FFF00");
            Color colorChuaThanhLy = ColorTranslator.FromHtml("#FFDEAD");

            if (tamUngModel_GroupBy_Names.Count > 0)
            {
                foreach (var tamUngModel_GroupBy_Name in tamUngModel_GroupBy_Names)
                {
                    xlSheet.Cells[dong, 3].Value = tamUngModel_GroupBy_Name.Name;
                    setBorder(dong, 1, dong, 7, xlSheet);
                    xlSheet.Cells[dong, 3].Style.Font.SetFromFont(new Font("Times New Roman", 12, FontStyle.Bold));

                    dong++;

                    foreach (var item in tamUngModel_GroupBy_Name.TamUngModels)
                    {
                        xlSheet.Cells[dong, 1].Value = item.NgayCT;
                        TrSetCellBorder(xlSheet, dong, 1, ExcelBorderStyle.Thin, ExcelHorizontalAlignment.Justify, Color.Silver, "Times New Roman", 12, FontStyle.Regular);
                        //xlSheet.Cells[dong, 1].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                        xlSheet.Cells[dong, 2].Value = item.SoCT;
                        TrSetCellBorder(xlSheet, dong, 2, ExcelBorderStyle.Thin, ExcelHorizontalAlignment.Left, Color.Silver, "Times New Roman", 12, FontStyle.Regular);
                        // xlSheet.Cells[dong, 3].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                        xlSheet.Cells[dong, 3].Value = item.DienGiai;
                        TrSetCellBorder(xlSheet, dong, 3, ExcelBorderStyle.Thin, ExcelHorizontalAlignment.Center, Color.Silver, "Times New Roman", 12, FontStyle.Regular);
                        //xlSheet.Cells[dong, 4].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                        xlSheet.Cells[dong, 4].Value = item.SoTienNT;
                        TrSetCellBorder(xlSheet, dong, 4, ExcelBorderStyle.Thin, ExcelHorizontalAlignment.Center, Color.Silver, "Times New Roman", 12, FontStyle.Regular);
                        //xlSheet.Cells[dong, 5].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                        xlSheet.Cells[dong, 5].Value = item.LT;
                        TrSetCellBorder(xlSheet, dong, 5, ExcelBorderStyle.Thin, ExcelHorizontalAlignment.Left, Color.Silver, "Times New Roman", 12, FontStyle.Regular);
                        // xlSheet.Cells[dong, 6].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                        xlSheet.Cells[dong, 6].Value = item.TyGia;
                        TrSetCellBorder(xlSheet, dong, 6, ExcelBorderStyle.Thin, ExcelHorizontalAlignment.Left, Color.Silver, "Times New Roman", 12, FontStyle.Regular);
                        // xlSheet.Cells[dong, 6].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                        xlSheet.Cells[dong, 7].Value = item.VND;
                        TrSetCellBorder(xlSheet, dong, 7, ExcelBorderStyle.Thin, ExcelHorizontalAlignment.Left, Color.Silver, "Times New Roman", 12, FontStyle.Regular);
                        // xlSheet.Cells[dong, 6].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                        //setBorder(5, 1, dong, 10, xlSheet);
                        DateFormat(dong, 1, dong, 1, xlSheet);

                        dong++;
                    }

                    NumberFormat(9, 4, dong, 4, xlSheet);
                    NumberFormat(9, 7, dong + 1, 7, xlSheet);

                    xlSheet.Cells[dong, 3].Value = "Tổng cộng:";
                    xlSheet.Cells[dong, 3].Style.Font.SetFromFont(new Font("Times New Roman", 12, FontStyle.Bold | FontStyle.Italic));
                    xlSheet.Cells[dong, 5].Value = "VNĐ";
                    xlSheet.Cells[dong, 5].Style.Font.SetFromFont(new Font("Times New Roman", 12, FontStyle.Bold | FontStyle.Italic));
                    xlSheet.Cells[dong, 7].Value = tamUngModel_GroupBy_Name.TongCong; // VND
                    xlSheet.Cells[dong, 7].Style.Font.SetFromFont(new Font("Times New Roman", 12, FontStyle.Bold | FontStyle.Italic));
                    setBorder(dong, 1, dong, 7, xlSheet);

                    // NgoaiTe
                    var results = (from p in tamUngModel_GroupBy_Name.TamUngModels
                                   group p by p.LT into g
                                   select new TamUngModel_GroupByLoaiTien { LoaiTien = g.Key, TamUngModels = g.ToList() }).ToList();

                    //List<ViewModel> tongTienNT = new List<ViewModel>();
                    foreach (var item in results)
                    {
                        if (item.LoaiTien != "VND")
                        {
                            //tongTienNT.Add(new ViewModel() { LoaiTien = item.LoaiTien, TongTienNT = item.TamUngModels.Sum(x => x.SoTienNT).Value});
                            dong++;
                            xlSheet.Cells[dong, 4].Value = item.TamUngModels.Sum(x => x.SoTienNT).Value;
                            xlSheet.Cells[dong, 4].Style.Font.SetFromFont(new Font("Times New Roman", 12, FontStyle.Bold | FontStyle.Italic));
                            xlSheet.Cells[dong, 5].Value = item.LoaiTien;
                            xlSheet.Cells[dong, 5].Style.Font.SetFromFont(new Font("Times New Roman", 12, FontStyle.Bold | FontStyle.Italic));
                        }
                    }

                    setBorder(dong, 1, dong, 7, xlSheet);
                    dong++;
                    //setBorder(dong, 1, dong, 7, xlSheet);
                    //dong++;
                }

                xlSheet.Cells[dong, 1].Value = "Đề nghị các thành viên trên thanh toán chậm nhất là ngày: " + denNgay;
                xlSheet.Cells[dong, 1].Style.Font.SetFromFont(new Font("Times New Roman", 12));
                dong++;
                xlSheet.Cells[dong, 1].Value = "Quá thời hạn trên Phòng KT sẽ không chi tạm ứng tiếp tục và chuyển danh sách này cho BGD Công ty giải quyết.";
                xlSheet.Cells[dong, 1].Style.Font.SetFromFont(new Font("Times New Roman", 12));
                dong++;
                DateTime denNgayDate = DateTime.Parse(denNgay);
                xlSheet.Cells[dong, 5].Value = "Ngày " + denNgayDate.Day + " tháng " + denNgayDate.Month + " năm " + denNgayDate.Year;
                xlSheet.Cells[dong, 5].Style.Font.SetFromFont(new Font("Times New Roman", 12));
                xlSheet.Cells[dong, 5, dong, 7].Merge = true;
                dong++;
                xlSheet.Cells[dong, 2].Value = "Người lập";
                xlSheet.Cells[dong, 2].Style.Font.SetFromFont(new Font("Times New Roman", 12));
                xlSheet.Cells[dong, 5].Value = "Kế toán trưởng";
                xlSheet.Cells[dong, 5].Style.Font.SetFromFont(new Font("Times New Roman", 12));
                xlSheet.Cells[dong, 5, dong, 7].Merge = true;

                //NumberFormat(dong, 3, dong, 4, xlSheet);
                //setFontBold(dong, 1, dong, 10, 12, xlSheet);
                //setBorder(dong, 1, dong, 10, xlSheet);

                //xlSheet.Cells[dong + 2, 1].Value = "Người lập bảng kê";
                //xlSheet.Cells[dong + 2, 1].Style.Font.SetFromFont(new Font("Times New Roman", 12, FontStyle.Regular));
                //xlSheet.Cells[dong + 2, 4].Value = "Kế toán trưởng";
                //xlSheet.Cells[dong + 2, 4].Style.Font.SetFromFont(new Font("Times New Roman", 12, FontStyle.Regular));

                //setCenterAligment(dong + 2, 1, dong + 2, 4, xlSheet);
            }
            else
            {
                SetAlert("Phiếu này không có chi tiết nào.", "warning");
                return NoContent();
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
                fileDownloadName: "TheoDoiTUNoiBoPhong_" + phongBan.BoPhan + "_" + System.DateTime.Now.ToString("dd/MM/yyyy HH:mm") + ".xlsx");
            }
            catch (Exception)
            {
                throw;
            }
        }

        // InTatCA
        [HttpPost]
        public IActionResult TheoDoiTUNoiBoTk141_Partial_Excel_InTatCa(string tuNgay, string denNgay)
        {
            // from session
            var user = HttpContext.Session.GetSingle<User>("loginUser");

            ExcelPackage ExcelApp = new ExcelPackage();

            List<PhongBan> phongBans = GetPhongBans_Where().ToList();

            foreach (var phongBan in phongBans)
            {
                List<TamUng> tamUngs = _tamUngService.FindTamUngs_IncludeTwice_By_Phong(phongBan.BoPhan, user.Macn).ToList();

                List<TamUngModel_GroupBy_Name> tamUngModel_GroupBy_Names = new List<TamUngModel_GroupBy_Name>();
                if (tamUngs.Count() > 0)
                {
                    var abc = _tamUngService.TamUngModels_GroupBy_Name(tamUngs.OrderBy(x => x.NgayCT), user.Macn);
                    if (abc.FirstOrDefault().Status == false)
                    {
                        ViewBag.errorMessage = "Không tìm thấy MaKh: " + abc.FirstOrDefault().MaKh;
                        return View("~/Views/Shared/Error.cshtml");
                    }
                    tamUngModel_GroupBy_Names = abc.ToList();// _tamUngService.TamUngModels_GroupBy_Name(tamUngs.OrderBy(x => x.NgayCT), user.Macn).ToList();

                    ExcelWorksheet xlSheet = ExcelApp.Workbook.Worksheets.Add(phongBan.TenBoPhan.Trim());
                    // Định dạng chiều dài cho cột
                    xlSheet.Column(1).Width = 15;// Ngày CT
                    xlSheet.Column(2).Width = 15;// Số CT
                    xlSheet.Column(3).Width = 40;// Diễn giải
                    xlSheet.Column(4).Width = 15;// Số tiền NT
                    xlSheet.Column(5).Width = 10;// LT
                    xlSheet.Column(6).Width = 10;// Tỷ giá
                    xlSheet.Column(7).Width = 15;// VNĐ

                    xlSheet.Cells[1, 1].Value = "CÔNG TY TNHH MỘT THÀNH VIÊN";
                    xlSheet.Cells[1, 1].Style.Font.SetFromFont(new Font("Times New Roman", 12));
                    xlSheet.Cells[1, 1, 1, 3].Merge = true;

                    xlSheet.Cells[2, 1].Value = "DVLH SAIGONTOURIST";
                    xlSheet.Cells[2, 1].Style.Font.SetFromFont(new Font("Times New Roman", 12));
                    xlSheet.Cells[2, 1, 2, 3].Merge = true;

                    xlSheet.Cells[1, 4].Value = "CỘNG HOÀ XÃ HỘI CHỦ NGHĨA VIỆT NAM ";
                    xlSheet.Cells[1, 4].Style.Font.SetFromFont(new Font("Times New Roman", 12));
                    xlSheet.Cells[1, 4, 1, 7].Merge = true;
                    setCenterAligment(1, 4, 1, 7, xlSheet);

                    xlSheet.Cells[2, 7].Value = "Độc lập - Tự Do - Hạnh Phúc";
                    xlSheet.Cells[2, 7].Style.Font.SetFromFont(new Font("Times New Roman", 12));
                    xlSheet.Cells[2, 4, 2, 7].Merge = true;
                    setCenterAligment(2, 4, 2, 7, xlSheet);

                    xlSheet.Cells[3, 1].Value = "BÁO CÁO CHI TIẾT SỐ DƯ TÀI KHOÃN 141";
                    xlSheet.Cells[3, 1].Style.Font.SetFromFont(new Font("Times New Roman", 16, FontStyle.Bold));
                    xlSheet.Cells[3, 1, 3, 7].Merge = true;
                    setCenterAligment(3, 1, 3, 7, xlSheet);

                    string stringKinhGoi = "Kính gởi: Trưởng phòng " + phongBan.TenBoPhan;
                    string stringXinGui = "Xin gửi phòng nhắc nhỡ các nhân viên sau đây về phòng KT để thanh toán các phiếu tạm ứng trước ngày " + tuNgay;
                    xlSheet.Cells[5, 1].Value = stringKinhGoi;
                    xlSheet.Cells[5, 1].Style.Font.SetFromFont(new Font("Times New Roman", 12));
                    //xlSheet.Cells[4, 1].Style.Font.Bold = true;
                    //xlSheet.Cells[4, 1].Style.Font.Italic = true;

                    xlSheet.Cells[6, 1].Value = stringXinGui;
                    xlSheet.Cells[6, 1].Style.Font.SetFromFont(new Font("Times New Roman", 12));

                    // Tạo header
                    xlSheet.Cells[7, 1].Value = "Ngày CT";
                    xlSheet.Cells[7, 2].Value = "Số CT";
                    xlSheet.Cells[7, 3].Value = "Diễn giải";
                    xlSheet.Cells[7, 4].Value = "Số tiền NT";
                    xlSheet.Cells[7, 5].Value = "LT";
                    xlSheet.Cells[7, 6].Value = "Tỷ giá";
                    xlSheet.Cells[7, 7].Value = "VNĐ";

                    xlSheet.Cells[7, 1, 7, 7].Style.Font.SetFromFont(new Font("Times New Roman", 12, FontStyle.Bold));
                    setBorder(7, 1, 7, 7, xlSheet);
                    setCenterAligment(7, 1, 7, 7, xlSheet);

                    // do du lieu tu table
                    int dong = 8;

                    //du lieu
                    //int iRowIndex = 6;

                    Color colFromHex = System.Drawing.ColorTranslator.FromHtml("#D3D3D3");// ColorTranslator.FromHtml("#D3D3D3");
                    Color colorTotalRow = ColorTranslator.FromHtml("#66ccff");
                    Color colorThanhLy = ColorTranslator.FromHtml("#7FFF00");
                    Color colorChuaThanhLy = ColorTranslator.FromHtml("#FFDEAD");

                    foreach (var tamUngModel_GroupBy_Name in tamUngModel_GroupBy_Names)
                    {
                        xlSheet.Cells[dong, 3].Value = tamUngModel_GroupBy_Name.Name;
                        setBorder(dong, 1, dong, 7, xlSheet);
                        xlSheet.Cells[dong, 3].Style.Font.SetFromFont(new Font("Times New Roman", 12, FontStyle.Bold));

                        dong++;

                        foreach (var item in tamUngModel_GroupBy_Name.TamUngModels)
                        {
                            xlSheet.Cells[dong, 1].Value = item.NgayCT;
                            TrSetCellBorder(xlSheet, dong, 1, ExcelBorderStyle.Thin, ExcelHorizontalAlignment.Justify, Color.Silver, "Times New Roman", 12, FontStyle.Regular);
                            //xlSheet.Cells[dong, 1].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                            xlSheet.Cells[dong, 2].Value = item.SoCT;
                            TrSetCellBorder(xlSheet, dong, 2, ExcelBorderStyle.Thin, ExcelHorizontalAlignment.Left, Color.Silver, "Times New Roman", 12, FontStyle.Regular);
                            // xlSheet.Cells[dong, 3].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                            xlSheet.Cells[dong, 3].Value = item.DienGiai;
                            TrSetCellBorder(xlSheet, dong, 3, ExcelBorderStyle.Thin, ExcelHorizontalAlignment.Center, Color.Silver, "Times New Roman", 12, FontStyle.Regular);
                            //xlSheet.Cells[dong, 4].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                            xlSheet.Cells[dong, 4].Value = item.SoTienNT;
                            TrSetCellBorder(xlSheet, dong, 4, ExcelBorderStyle.Thin, ExcelHorizontalAlignment.Center, Color.Silver, "Times New Roman", 12, FontStyle.Regular);
                            //xlSheet.Cells[dong, 5].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                            xlSheet.Cells[dong, 5].Value = item.LT;
                            TrSetCellBorder(xlSheet, dong, 5, ExcelBorderStyle.Thin, ExcelHorizontalAlignment.Left, Color.Silver, "Times New Roman", 12, FontStyle.Regular);
                            // xlSheet.Cells[dong, 6].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                            xlSheet.Cells[dong, 6].Value = item.TyGia;
                            TrSetCellBorder(xlSheet, dong, 6, ExcelBorderStyle.Thin, ExcelHorizontalAlignment.Left, Color.Silver, "Times New Roman", 12, FontStyle.Regular);
                            // xlSheet.Cells[dong, 6].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                            xlSheet.Cells[dong, 7].Value = item.VND;
                            TrSetCellBorder(xlSheet, dong, 7, ExcelBorderStyle.Thin, ExcelHorizontalAlignment.Left, Color.Silver, "Times New Roman", 12, FontStyle.Regular);
                            // xlSheet.Cells[dong, 6].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                            //setBorder(5, 1, dong, 10, xlSheet);
                            DateFormat(dong, 1, dong, 1, xlSheet);

                            dong++;
                        }

                        NumberFormat(9, 4, dong, 4, xlSheet);
                        NumberFormat(9, 7, dong + 1, 7, xlSheet);

                        xlSheet.Cells[dong, 3].Value = "Tổng cộng:";
                        xlSheet.Cells[dong, 3].Style.Font.SetFromFont(new Font("Times New Roman", 12, FontStyle.Bold | FontStyle.Italic));
                        xlSheet.Cells[dong, 5].Value = "VNĐ";
                        xlSheet.Cells[dong, 5].Style.Font.SetFromFont(new Font("Times New Roman", 12, FontStyle.Bold | FontStyle.Italic));
                        xlSheet.Cells[dong, 7].Value = tamUngModel_GroupBy_Name.TongCong; // VND
                        xlSheet.Cells[dong, 7].Style.Font.SetFromFont(new Font("Times New Roman", 12, FontStyle.Bold | FontStyle.Italic));
                        setBorder(dong, 1, dong, 7, xlSheet);

                        // NgoaiTe
                        var results = (from p in tamUngModel_GroupBy_Name.TamUngModels
                                       group p by p.LT into g
                                       select new TamUngModel_GroupByLoaiTien { LoaiTien = g.Key, TamUngModels = g.ToList() }).ToList();

                        //List<ViewModel> tongTienNT = new List<ViewModel>();
                        foreach (var item in results)
                        {
                            if (item.LoaiTien != "VND")
                            {
                                //tongTienNT.Add(new ViewModel() { LoaiTien = item.LoaiTien, TongTienNT = item.TamUngModels.Sum(x => x.SoTienNT).Value});
                                dong++;
                                xlSheet.Cells[dong, 4].Value = item.TamUngModels.Sum(x => x.SoTienNT).Value;
                                xlSheet.Cells[dong, 4].Style.Font.SetFromFont(new Font("Times New Roman", 12, FontStyle.Bold | FontStyle.Italic));
                                xlSheet.Cells[dong, 5].Value = item.LoaiTien;
                                xlSheet.Cells[dong, 5].Style.Font.SetFromFont(new Font("Times New Roman", 12, FontStyle.Bold | FontStyle.Italic));
                            }
                        }

                        setBorder(dong, 1, dong, 7, xlSheet);
                        dong++;
                        //setBorder(dong, 1, dong, 7, xlSheet);
                        //dong++;
                    }

                    xlSheet.Cells[dong, 1].Value = "Đề nghị các thành viên trên thanh toán chậm nhất là ngày: " + denNgay;
                    xlSheet.Cells[dong, 1].Style.Font.SetFromFont(new Font("Times New Roman", 12));
                    dong++;
                    xlSheet.Cells[dong, 1].Value = "Quá thời hạn trên Phòng KT sẽ không chi tạm ứng tiếp tục và chuyển danh sách này cho BGD Công ty giải quyết.";
                    xlSheet.Cells[dong, 1].Style.Font.SetFromFont(new Font("Times New Roman", 12));
                    dong++;
                    DateTime denNgayDate = DateTime.Parse(denNgay);
                    xlSheet.Cells[dong, 5].Value = "Ngày: " + denNgayDate.Day + " tháng " + denNgayDate.Month + " năm " + denNgayDate.Year;
                    xlSheet.Cells[dong, 5].Style.Font.SetFromFont(new Font("Times New Roman", 12));
                    xlSheet.Cells[dong, 5, dong, 7].Merge = true;
                    dong++;
                    xlSheet.Cells[dong, 2].Value = "Người lập";
                    xlSheet.Cells[dong, 2].Style.Font.SetFromFont(new Font("Times New Roman", 12));
                    xlSheet.Cells[dong, 5].Value = "Kế toán trưởng";
                    xlSheet.Cells[dong, 5].Style.Font.SetFromFont(new Font("Times New Roman", 12));
                    xlSheet.Cells[dong, 5, dong, 7].Merge = true;

                    //else
                    //{
                    //    //SetAlert("Phiếu này không có chi tiết nào.", "warning");
                    //    return NoContent();
                    //}
                }
            }

            //end du lieu

            byte[] fileContents;
            try
            {
                fileContents = ExcelApp.GetAsByteArray();
                return File(
                fileContents: fileContents,
                contentType: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                fileDownloadName: "TheoDoiTUNoiBoPhong_" + "InTatCa" + "_" + System.DateTime.Now.ToString("dd/MM/yyyy HH:mm") + ".xlsx");
            }
            catch (Exception)
            {
                throw;
            }
        }

        // TatCa1Sheet
        [HttpPost]
        public async Task<IActionResult> TheoDoiTUNoiBoTk141_Partial_Excel_TatCa1Sheet(string tuNgay, string denNgay)
        {
            // from session
            var user = HttpContext.Session.GetSingle<User>("loginUser");

            PhongBan phongBan = new PhongBan();
            List<PhongBan> phongBans = GetPhongBans_Where().ToList();

            List<TamUng> tamUngs = _tamUngService.FindTamUngs_IncludeTwice_By_Phong("", user.Macn).ToList();
            List<TamUngModel_GroupBy_Name_Phong> tamUngModel_GroupBy_Name_Phongs = new List<TamUngModel_GroupBy_Name_Phong>();
            if (tamUngs.Count() > 0)
            {
                var abc = await _tamUngService.TamUngModels_GroupBy_Name_TwoKey_Phong(tamUngs.OrderBy(x => x.NgayCT), user.Macn);
                if (abc.FirstOrDefault().Status == false)
                {
                    ViewBag.errorMessage = "Không tìm thấy MaKh: " + abc.FirstOrDefault().MaKh;
                    return View("~/Views/Shared/Error.cshtml");
                }
                var tamUngModel_GroupBy_Name_Phongs1 = await _tamUngService.TamUngModels_GroupBy_Name_TwoKey_Phong(tamUngs.OrderBy(x => x.NgayCT), user.Macn); // groupby name (makh)
                tamUngModel_GroupBy_Name_Phongs = tamUngModel_GroupBy_Name_Phongs1.ToList();
            }

            ExcelPackage ExcelApp = new ExcelPackage();
            ExcelWorksheet xlSheet = ExcelApp.Workbook.Worksheets.Add("Report");
            // Định dạng chiều dài cho cột
            xlSheet.Column(1).Width = 15;// Ngày CT
            xlSheet.Column(2).Width = 15;// Số CT
            xlSheet.Column(3).Width = 40;// Diễn giải
            xlSheet.Column(4).Width = 15;// Số tiền NT
            xlSheet.Column(5).Width = 10;// LT
            xlSheet.Column(6).Width = 10;// Tỷ giá
            xlSheet.Column(7).Width = 15;// VNĐ

            xlSheet.Cells[1, 1].Value = "CÔNG TY TNHH MỘT THÀNH VIÊN";
            xlSheet.Cells[1, 1].Style.Font.SetFromFont(new Font("Times New Roman", 12));
            xlSheet.Cells[1, 1, 1, 3].Merge = true;

            xlSheet.Cells[2, 1].Value = "DVLH SAIGONTOURIST";
            xlSheet.Cells[2, 1].Style.Font.SetFromFont(new Font("Times New Roman", 12));
            xlSheet.Cells[2, 1, 2, 3].Merge = true;

            xlSheet.Cells[1, 4].Value = "CỘNG HOÀ XÃ HỘI CHỦ NGHĨA VIỆT NAM ";
            xlSheet.Cells[1, 4].Style.Font.SetFromFont(new Font("Times New Roman", 12));
            xlSheet.Cells[1, 4, 1, 7].Merge = true;
            setCenterAligment(1, 4, 1, 7, xlSheet);

            xlSheet.Cells[2, 7].Value = "Độc lập - Tự Do - Hạnh Phúc";
            xlSheet.Cells[2, 7].Style.Font.SetFromFont(new Font("Times New Roman", 12));
            xlSheet.Cells[2, 4, 2, 7].Merge = true;
            setCenterAligment(2, 4, 2, 7, xlSheet);

            xlSheet.Cells[3, 1].Value = "BÁO CÁO CHI TIẾT SỐ DƯ TÀI KHOÃN 141";
            xlSheet.Cells[3, 1].Style.Font.SetFromFont(new Font("Times New Roman", 16, FontStyle.Bold));
            xlSheet.Cells[3, 1, 3, 7].Merge = true;
            setCenterAligment(3, 1, 3, 7, xlSheet);

            string stringKinhGoi = "Kính gởi: Trưởng phòng " + phongBan.TenBoPhan;
            string stringXinGui = "Xin gửi phòng nhắc nhỡ các nhân viên sau đây về phòng KT để thanh toán các phiếu tạm ứng trước ngày " + tuNgay;
            xlSheet.Cells[5, 1].Value = stringKinhGoi;
            xlSheet.Cells[5, 1].Style.Font.SetFromFont(new Font("Times New Roman", 12));
            //xlSheet.Cells[4, 1].Style.Font.Bold = true;
            //xlSheet.Cells[4, 1].Style.Font.Italic = true;

            xlSheet.Cells[6, 1].Value = stringXinGui;
            xlSheet.Cells[6, 1].Style.Font.SetFromFont(new Font("Times New Roman", 12));

            // Tạo header
            xlSheet.Cells[7, 1].Value = "Ngày CT";
            xlSheet.Cells[7, 2].Value = "Số CT";
            xlSheet.Cells[7, 3].Value = "Diễn giải";
            xlSheet.Cells[7, 4].Value = "Số tiền NT";
            xlSheet.Cells[7, 5].Value = "LT";
            xlSheet.Cells[7, 6].Value = "Tỷ giá";
            xlSheet.Cells[7, 7].Value = "VNĐ";

            xlSheet.Cells[7, 1, 7, 7].Style.Font.SetFromFont(new Font("Times New Roman", 12, FontStyle.Bold));
            setBorder(7, 1, 7, 7, xlSheet);
            setCenterAligment(7, 1, 7, 7, xlSheet);

            // do du lieu tu table
            int dong = 8;

            //du lieu
            //int iRowIndex = 6;

            Color colFromHex = System.Drawing.ColorTranslator.FromHtml("#D3D3D3");// ColorTranslator.FromHtml("#D3D3D3");
            Color colorTotalRow = ColorTranslator.FromHtml("#66ccff");
            Color colorThanhLy = ColorTranslator.FromHtml("#7FFF00");
            Color colorChuaThanhLy = ColorTranslator.FromHtml("#FFDEAD");

            if (tamUngModel_GroupBy_Name_Phongs.Count > 0)
            {
                foreach (var tamUngModels_GroupBy_Name_Phong in tamUngModel_GroupBy_Name_Phongs)
                {
                    // Name_Phong
                    xlSheet.Cells[dong, 3].Value = tamUngModels_GroupBy_Name_Phong.Name_Phong;
                    setBorder(dong, 1, dong, 7, xlSheet);
                    xlSheet.Cells[dong, 3].Style.Font.SetFromFont(new Font("Times New Roman", 12, FontStyle.Bold));
                    dong++;

                    foreach (var tamUngModel_GroupBy_Name in tamUngModels_GroupBy_Name_Phong.TamUngModel_GroupBy_Names)
                    {
                        // Name
                        xlSheet.Cells[dong, 3].Value = tamUngModel_GroupBy_Name.Name;
                        setBorder(dong, 1, dong, 7, xlSheet);
                        xlSheet.Cells[dong, 3].Style.Font.SetFromFont(new Font("Times New Roman", 12, FontStyle.Bold | FontStyle.Italic));
                        dong++;
                        // list tamung
                        foreach (var item in tamUngModel_GroupBy_Name.TamUngModels)
                        {
                            xlSheet.Cells[dong, 1].Value = item.NgayCT;
                            TrSetCellBorder(xlSheet, dong, 1, ExcelBorderStyle.Thin, ExcelHorizontalAlignment.Justify, Color.Silver, "Times New Roman", 12, FontStyle.Regular);
                            //xlSheet.Cells[dong, 1].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                            xlSheet.Cells[dong, 2].Value = item.SoCT;
                            TrSetCellBorder(xlSheet, dong, 2, ExcelBorderStyle.Thin, ExcelHorizontalAlignment.Left, Color.Silver, "Times New Roman", 12, FontStyle.Regular);
                            // xlSheet.Cells[dong, 3].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                            xlSheet.Cells[dong, 3].Value = item.DienGiai;
                            TrSetCellBorder(xlSheet, dong, 3, ExcelBorderStyle.Thin, ExcelHorizontalAlignment.Center, Color.Silver, "Times New Roman", 12, FontStyle.Regular);
                            //xlSheet.Cells[dong, 4].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                            xlSheet.Cells[dong, 4].Value = item.SoTienNT;
                            TrSetCellBorder(xlSheet, dong, 4, ExcelBorderStyle.Thin, ExcelHorizontalAlignment.Center, Color.Silver, "Times New Roman", 12, FontStyle.Regular);
                            //xlSheet.Cells[dong, 5].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                            xlSheet.Cells[dong, 5].Value = item.LT;
                            TrSetCellBorder(xlSheet, dong, 5, ExcelBorderStyle.Thin, ExcelHorizontalAlignment.Left, Color.Silver, "Times New Roman", 12, FontStyle.Regular);
                            // xlSheet.Cells[dong, 6].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                            xlSheet.Cells[dong, 6].Value = item.TyGia;
                            TrSetCellBorder(xlSheet, dong, 6, ExcelBorderStyle.Thin, ExcelHorizontalAlignment.Left, Color.Silver, "Times New Roman", 12, FontStyle.Regular);
                            // xlSheet.Cells[dong, 6].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                            xlSheet.Cells[dong, 7].Value = item.VND;
                            TrSetCellBorder(xlSheet, dong, 7, ExcelBorderStyle.Thin, ExcelHorizontalAlignment.Left, Color.Silver, "Times New Roman", 12, FontStyle.Regular);
                            // xlSheet.Cells[dong, 6].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                            //setBorder(5, 1, dong, 10, xlSheet);
                            DateFormat(dong, 1, dong, 1, xlSheet);

                            dong++;
                        }

                        NumberFormat(9, 4, dong, 4, xlSheet);
                        NumberFormat(9, 7, dong + 1, 7, xlSheet);

                        xlSheet.Cells[dong, 3].Value = "Tổng cộng:";
                        xlSheet.Cells[dong, 3].Style.Font.SetFromFont(new Font("Times New Roman", 12, FontStyle.Bold | FontStyle.Italic));
                        xlSheet.Cells[dong, 5].Value = "VNĐ";
                        xlSheet.Cells[dong, 5].Style.Font.SetFromFont(new Font("Times New Roman", 12, FontStyle.Bold | FontStyle.Italic));
                        xlSheet.Cells[dong, 7].Value = tamUngModel_GroupBy_Name.TongCong;
                        xlSheet.Cells[dong, 7].Style.Font.SetFromFont(new Font("Times New Roman", 12, FontStyle.Bold | FontStyle.Italic));
                        setBorder(dong, 1, dong, 7, xlSheet);

                        // NgoaiTe
                        var results = (from p in tamUngModel_GroupBy_Name.TamUngModels
                                       group p by p.LT into g
                                       select new TamUngModel_GroupByLoaiTien { LoaiTien = g.Key, TamUngModels = g.ToList() }).ToList();

                        //List<ViewModel> tongTienNT = new List<ViewModel>();
                        foreach (var item in results)
                        {
                            if (item.LoaiTien != "VND")
                            {
                                //tongTienNT.Add(new ViewModel() { LoaiTien = item.LoaiTien, TongTienNT = item.TamUngModels.Sum(x => x.SoTienNT).Value});
                                dong++;
                                xlSheet.Cells[dong, 4].Value = item.TamUngModels.Sum(x => x.SoTienNT).Value;
                                xlSheet.Cells[dong, 4].Style.Font.SetFromFont(new Font("Times New Roman", 12, FontStyle.Bold | FontStyle.Italic));
                                xlSheet.Cells[dong, 5].Value = item.LoaiTien;
                                xlSheet.Cells[dong, 5].Style.Font.SetFromFont(new Font("Times New Roman", 12, FontStyle.Bold | FontStyle.Italic));
                            }
                        }

                        setBorder(dong, 1, dong, 7, xlSheet);
                        dong++;

                        //setBorder(dong, 1, dong, 7, xlSheet);
                        //dong++;
                    }
                }

                xlSheet.Cells[dong, 1].Value = "Đề nghị các thành viên trên thanh toán chậm nhất là ngày: " + denNgay;
                xlSheet.Cells[dong, 1].Style.Font.SetFromFont(new Font("Times New Roman", 12));
                dong++;
                xlSheet.Cells[dong, 1].Value = "Quá thời hạn trên Phòng KT sẽ không chi tạm ứng tiếp tục và chuyển danh sách này cho BGD Công ty giải quyết.";
                xlSheet.Cells[dong, 1].Style.Font.SetFromFont(new Font("Times New Roman", 12));
                dong++;
                DateTime denNgayDate = DateTime.Parse(denNgay);
                xlSheet.Cells[dong, 5].Value = "Ngày " + denNgayDate.Day + " tháng " + denNgayDate.Month + " năm " + denNgayDate.Year;
                xlSheet.Cells[dong, 5].Style.Font.SetFromFont(new Font("Times New Roman", 12));
                xlSheet.Cells[dong, 5, dong, 7].Merge = true;
                dong++;
                xlSheet.Cells[dong, 2].Value = "Người lập";
                xlSheet.Cells[dong, 2].Style.Font.SetFromFont(new Font("Times New Roman", 12));
                xlSheet.Cells[dong, 5].Value = "Kế toán trưởng";
                xlSheet.Cells[dong, 5].Style.Font.SetFromFont(new Font("Times New Roman", 12));
                xlSheet.Cells[dong, 5, dong, 7].Merge = true;

                //NumberFormat(dong, 3, dong, 4, xlSheet);
                //setFontBold(dong, 1, dong, 10, 12, xlSheet);
                //setBorder(dong, 1, dong, 10, xlSheet);

                //xlSheet.Cells[dong + 2, 1].Value = "Người lập bảng kê";
                //xlSheet.Cells[dong + 2, 1].Style.Font.SetFromFont(new Font("Times New Roman", 12, FontStyle.Regular));
                //xlSheet.Cells[dong + 2, 4].Value = "Kế toán trưởng";
                //xlSheet.Cells[dong + 2, 4].Style.Font.SetFromFont(new Font("Times New Roman", 12, FontStyle.Regular));

                //setCenterAligment(dong + 2, 1, dong + 2, 4, xlSheet);
            }
            else
            {
                //SetAlert("Phiếu này không có chi tiết nào.", "warning");
                return NoContent();
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
                fileDownloadName: "TheoDoiTUNoiBoPhong_" + phongBan.BoPhan + "_" + System.DateTime.Now.ToString("dd/MM/yyyy HH:mm") + ".xlsx");
            }
            catch (Exception)
            {
                throw;
            }
        }

        // InChungTuGhiSo_Partial
        public IActionResult InChungTuGhiSo_Partial(string searchFromDate, string searchToDate, string maKhCo)
        {
            // from session
            var user = HttpContext.Session.GetSingle<User>("loginUser");

            ViewBag.searchFromDate = searchFromDate;
            ViewBag.searchToDate = searchToDate;
            ViewBag.maKhCo = maKhCo;
            if (string.IsNullOrEmpty(searchFromDate) && string.IsNullOrEmpty(searchToDate)) // moi load vao
            {
                BaoCaoVM.TT621s = null;
                return PartialView(BaoCaoVM);
            }

            // dao ngay thang
            DateTime fromDate = DateTime.Parse(searchFromDate); // NgayCT
            DateTime toDate = DateTime.Parse(searchToDate); // NgayCT

            if (fromDate > toDate) // dao nguoc lai
            {
                string tmp = searchFromDate;
                searchFromDate = searchToDate;
                searchToDate = tmp;
                ViewBag.searchFromDate = searchFromDate;
                ViewBag.searchToDate = searchToDate;
            }

            BaoCaoVM.TT621s = _tT621Service.FindTT621s_IncludeTwice_By_Date(searchFromDate, searchToDate,
                user.Macn, maKhCo, user.Username).OrderBy(x => x.NgayCT);

            ViewBag.tT621GroupBy_PhieuTCs = _tT621Service.GroupBy_PhieuTC(BaoCaoVM.TT621s);

            if (BaoCaoVM.TT621s == null)
            {
                return Json(false);
            }

            return PartialView(BaoCaoVM);
        }

        // InChungTuGhiSoExcel
        [HttpPost]
        public async Task<IActionResult> InChungTuGhiSoExcel(string tuNgay, string denNgay,
                                                             long id_TT, string soCT_TT, string ngayCT_TT, string maKhCo_TT, string hoTen_TT, string phieuTC_TT)
        {
            // from login session
            var user = HttpContext.Session.GetSingle<User>("loginUser");

            IEnumerable<TT621> tT621s = _tT621Service.FindTT621s_IncludeTwice_By_Date(tuNgay, denNgay, user.Macn, "", user.Username);
            TT621 tT621 = tT621s.Where(x => x.Id == id_TT).FirstOrDefault();
            List<TT621> tT621s_By_TamUng = _tT621Service.FindTT621s_IncludeTwice(tT621.TamUngId).ToList();
            //List<KVCTPTC> kVCTPTCs_By_SoCT = await _kVCTPTCService.List_KVCTPCT_By_SoCT(phieuTC_TT, user.Macn);

            string loaiphieu = phieuTC_TT.Contains("T") ? "THU" : "CHI";

            ExcelPackage ExcelApp = new ExcelPackage();
            ExcelWorksheet xlSheet = ExcelApp.Workbook.Worksheets.Add("Report");
            // Định dạng chiều dài cho cột
            xlSheet.Column(1).Width = 40;// Diễn giải
            xlSheet.Column(2).Width = 40;// Diễn giải phụ
            xlSheet.Column(3).Width = 15;// Số tiền NT
            xlSheet.Column(4).Width = 10;// LT
            xlSheet.Column(5).Width = 10;// Tỷ giá
            xlSheet.Column(6).Width = 15;// Số tiền
            xlSheet.Column(7).Width = 20;// TK nợ
            xlSheet.Column(8).Width = 10;// Mã KH nợ
            xlSheet.Column(9).Width = 40;// Tên KH nợ
            xlSheet.Column(10).Width = 20;// Code đoàn
            xlSheet.Column(11).Width = 20;// CT Gốc

            xlSheet.Cells[1, 1].Value = "Đơn vị: CÔNG TY TNHH MTV DVLH SAIGONTOURIST";
            xlSheet.Cells[1, 1].Style.Font.SetFromFont(new Font("Times New Roman", 12, FontStyle.Bold));
            xlSheet.Cells[1, 1, 1, 11].Merge = true;

            xlSheet.Cells[2, 1].Value = "Địa chỉ: 45 LÊ THÁNH TÔN, P.BẾN NGHÉ, Q.1, TP.HCM";
            xlSheet.Cells[2, 1].Style.Font.SetFromFont(new Font("Times New Roman", 12, FontStyle.Bold));
            xlSheet.Cells[2, 1, 2, 11].Merge = true;

            xlSheet.Cells[3, 1].Value = "CHÚNG TỪ GHI SỔ TK 141 SỐ " + soCT_TT;
            xlSheet.Cells[3, 1].Style.Font.SetFromFont(new Font("Times New Roman", 16, FontStyle.Bold));
            xlSheet.Cells[3, 1, 3, 11].Merge = true;
            setCenterAligment(3, 1, 3, 11, xlSheet);

            string stringNgay = "Ngày " + ngayCT_TT + " phiếu " + loaiphieu.ToLower() + " số " + phieuTC_TT + " - " +
                "Nhân viên: " + maKhCo_TT + " " + hoTen_TT;
            xlSheet.Cells[4, 1].Value = stringNgay;
            xlSheet.Cells[4, 1].Style.Font.SetFromFont(new Font("Times New Roman", 12, FontStyle.Bold | FontStyle.Italic));
            //xlSheet.Cells[4, 1].Style.Font.Bold = true;
            //xlSheet.Cells[4, 1].Style.Font.Italic = true;
            xlSheet.Cells[4, 1, 4, 11].Merge = true;
            setCenterAligment(4, 1, 4, 11, xlSheet);

            // Tạo header
            xlSheet.Cells[5, 1].Value = "Diễn giải";
            xlSheet.Cells[5, 2].Value = "Diễn giải phụ";
            xlSheet.Cells[5, 3].Value = "Số tiền NT";
            xlSheet.Cells[5, 4].Value = "LT";
            xlSheet.Cells[5, 5].Value = "Tỷ giá";
            xlSheet.Cells[5, 6].Value = "Số tiền";
            xlSheet.Cells[5, 7].Value = "TK nợ";
            xlSheet.Cells[5, 8].Value = "Mã KH nợ";
            xlSheet.Cells[5, 9].Value = "Tên KH nợ";
            xlSheet.Cells[5, 10].Value = "Code đoàn";
            xlSheet.Cells[5, 11].Value = "CT Gốc";

            xlSheet.Cells[5, 1, 11, 12].Style.Font.SetFromFont(new Font("Times New Roman", 12, FontStyle.Bold));
            setBorder(5, 1, 5, 11, xlSheet);
            setCenterAligment(5, 1, 5, 11, xlSheet);

            // do du lieu tu table
            int dong = 6;

            //du lieu
            //int iRowIndex = 6;

            Color colFromHex = System.Drawing.ColorTranslator.FromHtml("#D3D3D3");// ColorTranslator.FromHtml("#D3D3D3");
            Color colorTotalRow = ColorTranslator.FromHtml("#66ccff");
            Color colorThanhLy = ColorTranslator.FromHtml("#7FFF00");
            Color colorChuaThanhLy = ColorTranslator.FromHtml("#FFDEAD");

            int idem = 1;

            if (tT621s_By_TamUng.Count > 0)
            {
                foreach (var item in tT621s_By_TamUng)
                {
                    xlSheet.Cells[dong, 1].Value = item.DienGiai;
                    TrSetCellBorder(xlSheet, dong, 1, ExcelBorderStyle.Thin, ExcelHorizontalAlignment.Justify, Color.Silver, "Times New Roman", 12, FontStyle.Regular);
                    //xlSheet.Cells[dong, 1].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                    xlSheet.Cells[dong, 2].Value = item.DienGiaiP;
                    TrSetCellBorder(xlSheet, dong, 2, ExcelBorderStyle.Thin, ExcelHorizontalAlignment.Justify, Color.Silver, "Times New Roman", 12, FontStyle.Regular);
                    //xlSheet.Cells[dong, 1].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                    xlSheet.Cells[dong, 3].Value = item.SoTienNT;
                    TrSetCellBorder(xlSheet, dong, 3, ExcelBorderStyle.Thin, ExcelHorizontalAlignment.Left, Color.Silver, "Times New Roman", 12, FontStyle.Regular);
                    // xlSheet.Cells[dong, 3].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                    xlSheet.Cells[dong, 4].Value = item.LoaiTien;
                    TrSetCellBorder(xlSheet, dong, 4, ExcelBorderStyle.Thin, ExcelHorizontalAlignment.Center, Color.Silver, "Times New Roman", 12, FontStyle.Regular);
                    //xlSheet.Cells[dong, 4].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                    xlSheet.Cells[dong, 5].Value = item.TyGia;
                    TrSetCellBorder(xlSheet, dong, 5, ExcelBorderStyle.Thin, ExcelHorizontalAlignment.Center, Color.Silver, "Times New Roman", 12, FontStyle.Regular);
                    //xlSheet.Cells[dong, 5].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                    xlSheet.Cells[dong, 6].Value = item.SoTien;
                    TrSetCellBorder(xlSheet, dong, 6, ExcelBorderStyle.Thin, ExcelHorizontalAlignment.Left, Color.Silver, "Times New Roman", 12, FontStyle.Regular);
                    // xlSheet.Cells[dong, 6].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                    xlSheet.Cells[dong, 7].Value = item.TKNo;
                    TrSetCellBorder(xlSheet, dong, 7, ExcelBorderStyle.Thin, ExcelHorizontalAlignment.Left, Color.Silver, "Times New Roman", 12, FontStyle.Regular);
                    // xlSheet.Cells[dong, 6].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                    xlSheet.Cells[dong, 8].Value = item.MaKhNo;
                    TrSetCellBorder(xlSheet, dong, 8, ExcelBorderStyle.Thin, ExcelHorizontalAlignment.Left, Color.Silver, "Times New Roman", 12, FontStyle.Regular);
                    // xlSheet.Cells[dong, 6].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                    xlSheet.Cells[dong, 9].Value = item.TenKH;
                    TrSetCellBorder(xlSheet, dong, 9, ExcelBorderStyle.Thin, ExcelHorizontalAlignment.Left, Color.Silver, "Times New Roman", 12, FontStyle.Regular);
                    // xlSheet.Cells[dong, 6].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                    xlSheet.Cells[dong, 10].Value = item.Sgtcode;
                    TrSetCellBorder(xlSheet, dong, 10, ExcelBorderStyle.Thin, ExcelHorizontalAlignment.Left, Color.Silver, "Times New Roman", 12, FontStyle.Regular);
                    // xlSheet.Cells[dong, 6].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                    xlSheet.Cells[dong, 11].Value = item.SoCTGoc;
                    TrSetCellBorder(xlSheet, dong, 11, ExcelBorderStyle.Thin, ExcelHorizontalAlignment.Left, Color.Silver, "Times New Roman", 12, FontStyle.Regular);
                    // xlSheet.Cells[dong, 6].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                    //setBorder(5, 1, dong, 10, xlSheet);
                    NumberFormat(dong, 3, dong + 1, 3, xlSheet);
                    NumberFormat(dong, 6, dong + 1, 6, xlSheet);

                    dong++;
                    idem++;
                }

                xlSheet.Cells[dong, 1].Value = "Tổng cộng:";
                xlSheet.Cells[dong, 1].Style.Font.SetFromFont(new Font("Times New Roman", 12, FontStyle.Bold | FontStyle.Italic));
                xlSheet.Cells[dong, 3].Formula = "SUM(C6:C" + (dong - 1) + ")";
                xlSheet.Cells[dong, 6].Formula = "SUM(F6:F" + (dong - 1) + ")";

                //NumberFormat(dong, 3, dong, 4, xlSheet);
                //setFontBold(dong, 1, dong, 10, 12, xlSheet);
                setBorder(dong, 1, dong, 11, xlSheet);

                //xlSheet.Cells[dong + 2, 1].Value = "Người lập bảng kê";
                //xlSheet.Cells[dong + 2, 1].Style.Font.SetFromFont(new Font("Times New Roman", 12, FontStyle.Regular));
                //xlSheet.Cells[dong + 2, 4].Value = "Kế toán trưởng";
                //xlSheet.Cells[dong + 2, 4].Style.Font.SetFromFont(new Font("Times New Roman", 12, FontStyle.Regular));

                //setCenterAligment(dong + 2, 1, dong + 2, 4, xlSheet);
            }
            else
            {
                //SetAlert("Phiếu này không có chi tiết nào.", "warning");
                return NoContent();
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
                fileDownloadName: "ChungTuGhiSoTK141_" + soCT_TT + "_" + System.DateTime.Now.ToString("dd/MM/yyyy HH:mm") + ".xlsx");
            }
            catch (Exception)
            {
                throw;
            }
        }

        private IEnumerable<Data.Models_DanhMucKT.PhongBan> GetPhongBans_Where()
        {
            //return _kVCTPTCService.GetAll_PhongBans().Where(x => x.BoPhan == "ND" ||
            //                                                                   x.BoPhan == "HK" ||
            //                                                                   x.BoPhan == "HC" ||
            //                                                                   x.BoPhan == "HD" ||
            //                                                                   x.BoPhan == "KT" ||
            //                                                                   x.BoPhan == "TB" ||
            //                                                                   x.BoPhan == "TH" ||
            //                                                                   x.BoPhan == "VT" ||
            //                                                                   x.BoPhan == "VS" ||
            //                                                                   x.BoPhan == "IB" ||
            //                                                                   x.BoPhan == "XE");

            return _kVCTPTCService.GetAll_PhongBans();
        }

        //public async Task<JsonResult> CheckNgayTonQuy_NT(string tuNgay, string denNgay, string loaiTien)
        //{
        //    // from login session
        //    var user = HttpContext.Session.GetSingle<User>("loginUser");

        //    DateTime fromDate = DateTime.Parse(tuNgay);
        //    DateTime compareDate = DateTime.Parse("01/06/2021");

        //    if (fromDate < compareDate)
        //    {
        //        return Json(new
        //        {
        //            status = false,
        //            message = "Không đồng ý tồn quỹ trước 01/06/2021"
        //        });
        //    }

        //    DateTime toDate = DateTime.Parse(denNgay);
        //    if (fromDate > toDate) // dao nguoc lai
        //    {
        //        return Json(new
        //        {
        //            status = false,
        //            message = "Từ ngày <b> không được lớn hơn </b> đến ngày"
        //        });
        //    }

        //    // tonquy truoc ngay fromdate => xem co ton dau` ko ( tranh truong hop chua tinh ton dau cho vai phieu )
        //    string kVCTPTCs1 = await _tonQuyService.CheckTonDauStatus_NT(DateTime.Parse(tuNgay), loaiTien, user.Macn);
        //    if (!string.IsNullOrEmpty(kVCTPTCs1))
        //    {
        //        return Json(new
        //        {
        //            status = false,
        //            message = "Ngày " + kVCTPTCs1 + " chưa tính tồn quỹ"
        //        });
        //    }

        //    //bool boolDate1 = _tonQuyService.Find_Equal_By_Date(toDate);
        //    //if (boolDate1) // co rồi
        //    //{
        //    //    return Json(new
        //    //    {
        //    //        status = false,
        //    //        message = "Ngày " + toDate.ToString("dd/MM/yyyy") + " đã tính tồn quỹ"
        //    //    });
        //    //}

        //    return Json(new
        //    {
        //        status = true,
        //        message = "Good job!"
        //    });
        //}

        public async Task<JsonResult> CheckNgayTonQuy(string tuNgay, string denNgay)
        {
            // from session
            var user = HttpContext.Session.GetSingle<User>("loginUser");

            DateTime fromDate = DateTime.Parse(tuNgay);
            DateTime compareDate = DateTime.Parse("03/01/2020");

            if (fromDate < compareDate)
            {
                return Json(new
                {
                    status = false,
                    message = "Không đồng ý tồn quỹ trước 03/01/2020"
                });
            }

            DateTime toDate = DateTime.Parse(denNgay);
            if (fromDate > toDate) // dao nguoc lai
            {
                return Json(new
                {
                    status = false,
                    message = "Từ ngày <b> không được lớn hơn </b> đến ngày"
                });
            }

            //// tonquy truoc ngay fromdate => xem co ton dau` ko ( tranh truong hop chua tinh ton dau cho vai phieu )
            //string kVCTPTCs1 = await _tonQuyService.CheckTonDauStatus(DateTime.Parse(tuNgay), user.Macn);
            //if (!string.IsNullOrEmpty(kVCTPTCs1))
            //{
            //    return Json(new
            //    {
            //        status = false,
            //        message = "Ngày " + kVCTPTCs1 + " chưa tính tồn quỹ"
            //    });
            //}

            //bool boolDate1 = _tonQuyService.Find_Equal_By_Date(toDate);
            //if (boolDate1) // co rồi
            //{
            //    return Json(new
            //    {
            //        status = false,
            //        message = "Ngày " + toDate.ToString("dd/MM/yyyy") + " đã tính tồn quỹ"
            //    });
            //}

            return Json(new
            {
                status = true,
                message = "Good job!"
            });
        }

        // ExportTkNo1311
        [HttpPost]
        public async Task<IActionResult> ExportTkNo1311(string tuNgay, string denNgay)
        {
            List<KVCTPTC> kVCTPTCs = HttpContext.Session.Gets<KVCTPTC>("kVCTPTCs_1311").ToList();

            // from login session
            var user = HttpContext.Session.GetSingle<User>("loginUser");

            ExcelPackage ExcelApp = new ExcelPackage();
            ExcelWorksheet xlSheet = ExcelApp.Workbook.Worksheets.Add("Report");
            // Định dạng chiều dài cho cột
            xlSheet.Column(1).Width = 20;// NGÀY PC
            xlSheet.Column(2).Width = 20;// SỐ PC
            xlSheet.Column(3).Width = 20;// SỐ THẺ (4 số cuối)
            xlSheet.Column(4).Width = 20;// APP CODE
            xlSheet.Column(5).Width = 20;// LOẠI THẺ
            xlSheet.Column(6).Width = 20;// VND
            xlSheet.Column(7).Width = 20;// VND
            xlSheet.Column(8).Width = 20;// SGTCODE

            xlSheet.Cells[1, 1].Value = "BẢNG THEO DÕI THẺ";
            xlSheet.Cells[1, 1].Style.Font.SetFromFont(new Font("Times New Roman", 16, FontStyle.Bold));
            xlSheet.Cells[1, 1, 1, 8].Merge = true;

            string stringNgay = "Ngày " + tuNgay + " đến " + denNgay;
            xlSheet.Cells[2, 1].Value = stringNgay;
            xlSheet.Cells[2, 1, 2, 8].Merge = true;
            xlSheet.Cells[2, 1].Style.Font.SetFromFont(new Font("Times New Roman", 12, FontStyle.Bold | FontStyle.Italic));

            setCenterAligment(1, 1, 2, 1, xlSheet);

            // Tạo header
            xlSheet.Cells[4, 1].Value = "NGÀY PC";
            xlSheet.Cells[4, 1, 5, 1].Merge = true;
            xlSheet.Cells[4, 2].Value = "SỐ PC";
            xlSheet.Cells[4, 2, 5, 2].Merge = true;
            xlSheet.Cells[4, 3].Value = "SỐ THẺ (4 số cuối)";
            xlSheet.Cells[4, 3, 5, 3].Merge = true;
            xlSheet.Cells[4, 4].Value = "APP CODE";
            xlSheet.Cells[4, 4, 5, 4].Merge = true;
            xlSheet.Cells[4, 5].Value = "LOẠI THẺ";
            xlSheet.Cells[4, 5, 5, 5].Merge = true;
            xlSheet.Cells[4, 6].Value = "VND";
            xlSheet.Cells[4, 6, 4, 7].Merge = true;
            xlSheet.Cells[5, 6].Value = "Số tiền";
            xlSheet.Cells[5, 7].Value = "CÓ 1131";
            xlSheet.Cells[4, 8].Value = "SGTCODE";
            xlSheet.Cells[4, 8, 5, 8].Merge = true;

            setBorder(4, 1, 5, 8, xlSheet);
            setCenterAligment(4, 1, 5, 8, xlSheet);
            xlSheet.Cells[4, 1, 5, 8].Style.Font.SetFromFont(new Font("Times New Roman", 12, FontStyle.Bold));
            xlSheet.Cells[4, 1, 5, 8].Style.VerticalAlignment = ExcelVerticalAlignment.Center; // canh giữa cột

            // do du lieu tu table
            int dong = 6;

            //du lieu
            //int iRowIndex = 6;

            Color colFromHex = System.Drawing.ColorTranslator.FromHtml("#D3D3D3");// ColorTranslator.FromHtml("#D3D3D3");
            Color colorTotalRow = ColorTranslator.FromHtml("#66ccff");
            Color colorThanhLy = ColorTranslator.FromHtml("#7FFF00");
            Color colorChuaThanhLy = ColorTranslator.FromHtml("#FFDEAD");

            int idem = 1;

            if (kVCTPTCs.Count > 0)
            {
                foreach (var item in kVCTPTCs)
                {
                    xlSheet.Cells[dong, 1].Value = item.KVPTC.NgayCT;
                    TrSetCellBorder(xlSheet, dong, 1, ExcelBorderStyle.Thin, ExcelHorizontalAlignment.Justify, Color.Silver, "Times New Roman", 12, FontStyle.Regular);
                    //xlSheet.Cells[dong, 1].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                    xlSheet.Cells[dong, 2].Value = item.KVPTC.SoCT;
                    TrSetCellBorder(xlSheet, dong, 2, ExcelBorderStyle.Thin, ExcelHorizontalAlignment.Justify, Color.Silver, "Times New Roman", 12, FontStyle.Regular);
                    //xlSheet.Cells[dong, 1].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                    if (!string.IsNullOrEmpty(item.CardNumber) && item.CardNumber.Length > 4)
                    {

                        var soThe = item.CardNumber.Substring(item.CardNumber.Length - 4, 4);
                        xlSheet.Cells[dong, 3].Value = soThe.ToString();

                    }
                    else
                    {
                        xlSheet.Cells[dong, 3].Value = string.IsNullOrEmpty(item.CardNumber) ? "" : item.CardNumber.ToString();
                        //xlSheet.Cells[dong, 3].Value = item.CardNumber.ToString();
                    }
                    //xlSheet.Cells[dong, 3].Value = string.IsNullOrEmpty(item.CardNumber) ? "" : item.CardNumber.ToString();

                    TrSetCellBorder(xlSheet, dong, 3, ExcelBorderStyle.Thin, ExcelHorizontalAlignment.Left, Color.Silver, "Times New Roman", 12, FontStyle.Regular);
                    // xlSheet.Cells[dong, 3].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                    xlSheet.Cells[dong, 4].Value = item.SalesSlip;
                    TrSetCellBorder(xlSheet, dong, 4, ExcelBorderStyle.Thin, ExcelHorizontalAlignment.Center, Color.Silver, "Times New Roman", 12, FontStyle.Regular);
                    //xlSheet.Cells[dong, 4].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                    xlSheet.Cells[dong, 5].Value = item.LoaiThe;
                    TrSetCellBorder(xlSheet, dong, 5, ExcelBorderStyle.Thin, ExcelHorizontalAlignment.Center, Color.Silver, "Times New Roman", 12, FontStyle.Regular);
                    //xlSheet.Cells[dong, 5].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                    xlSheet.Cells[dong, 6].Value = item.SoTien;
                    TrSetCellBorder(xlSheet, dong, 6, ExcelBorderStyle.Thin, ExcelHorizontalAlignment.Left, Color.Silver, "Times New Roman", 12, FontStyle.Regular);
                    // xlSheet.Cells[dong, 6].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                    xlSheet.Cells[dong, 7].Value = "";
                    TrSetCellBorder(xlSheet, dong, 7, ExcelBorderStyle.Thin, ExcelHorizontalAlignment.Left, Color.Silver, "Times New Roman", 12, FontStyle.Regular);
                    // xlSheet.Cells[dong, 6].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                    xlSheet.Cells[dong, 8].Value = item.Sgtcode ?? "";
                    TrSetCellBorder(xlSheet, dong, 8, ExcelBorderStyle.Thin, ExcelHorizontalAlignment.Center, Color.Silver, "Times New Roman", 12, FontStyle.Regular);
                    // xlSheet.Cells[dong, 6].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                    //setBorder(5, 1, dong, 10, xlSheet);
                    NumberFormat(dong, 6, dong + 1, 6, xlSheet);

                    dong++;
                    idem++;
                }

                xlSheet.Cells[dong, 5].Value = "Tổng cộng:";
                xlSheet.Cells[dong, 5].Style.Font.SetFromFont(new Font("Times New Roman", 12, FontStyle.Bold | FontStyle.Italic));
                xlSheet.Cells[dong, 6].Formula = "SUM(F6:F" + (dong - 1) + ")";

                //NumberFormat(dong, 3, dong, 4, xlSheet);
                //setFontBold(dong, 1, dong, 10, 12, xlSheet);
                setBorder(dong, 1, dong, 8, xlSheet);
                DateFormat(6, 1, dong, 1, xlSheet);

                //xlSheet.Cells[dong + 2, 1].Value = "Người lập bảng kê";
                //xlSheet.Cells[dong + 2, 1].Style.Font.SetFromFont(new Font("Times New Roman", 12, FontStyle.Regular));
                //xlSheet.Cells[dong + 2, 4].Value = "Kế toán trưởng";
                //xlSheet.Cells[dong + 2, 4].Style.Font.SetFromFont(new Font("Times New Roman", 12, FontStyle.Regular));

                //setCenterAligment(dong + 2, 1, dong + 2, 4, xlSheet);
            }
            else
            {
                //SetAlert("Phiếu này không có chi tiết nào.", "warning");
                return NoContent();
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
                SetAlert("Good job!", "success");
                return File(
                fileContents: fileContents,
                contentType: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                fileDownloadName: "BangTheoDoiThe_" + System.DateTime.Now.ToString("dd/MM/yyyy HH:mm") + ".xlsx");
            }
            catch (Exception ex)
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