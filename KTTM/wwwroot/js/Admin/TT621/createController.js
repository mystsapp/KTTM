////function addCommas(x) {
////    var parts = x.toString().split(".");
////    parts[0] = parts[0].replace(/\D/g, "").replace(/\B(?=(\d{3})+(?!\d))/g, ",");
////    return parts.join(".");
////}

var createController = {
    init: function () {
        toastr.options = { // toastr options
            "closeButton": false,
            "debug": false,
            "newestOnTop": false,
            "progressBar": true,
            "positionClass": "toast-top-right",
            "preventDuplicates": false,
            "onclick": null,
            "showDuration": "300",
            "hideDuration": "2000",
            "timeOut": "2000",
            "extendedTimeOut": "1000",
            "showEasing": "swing",
            "hideEasing": "linear",
            "showMethod": "fadeIn",
            "hideMethod": "fadeOut"
        };

        createController.registerEvent();
    },

    registerEvent: function () {
        // format .numbers
        $('input.numbers').keyup(function (event) {
            // Chỉ cho nhập số
            if (event.which >= 37 && event.which <= 40) return;

            $(this).val(function (index, value) {
                return addCommas(value);
            });
        });

        // giu trang thai CT TT va lay tamungid (GetTT621s_By_TamUng)
        $('.tamUngTr').off('click').on('click', function () {
            //debugger
            if ($(this).hasClass("hoverClass"))
                $(this).removeClass("hoverClass");
            else {
                $('.tamUngTr').removeClass("hoverClass");
                $(this).addClass("hoverClass");
            }

            //if ($(this).hasClass("hoverClass"))
            //    $(this).removeClass("hoverClass");
            //else {
            //    debugger
            //    var idList = [];
            //    $.each($('.tamUngTr'), function (i, item) {
            //        $(this).removeClass("hoverClass");
            //    });
            //    $(this).addClass("hoverClass");
            //}

            $('#hidIdCu').val(0); // moi lan click tro lai tamungtr -> reset hidIdCu(id dong tt621) = 0
            tamUngId = $(this).data('id');
            soTienNT = $('#txtSoTienNT_Create').val(); // TT621Create_View
            kVCTPCTId_PhieuTC = $('#hidKVCTPCTId').val();
            loaiPhieu = $('#hidLoaiPhieu').val();

            $('#hidTamUngId').val(tamUngId); // for TT141

            //// check btnThemMoiCTTT status
            //createController.Check_SoTienNTCanKetChuyen_For_BtnThemMoiCTTT_Status(tamUngId, soTienNT)//////////////////
            $('#btnThemMoiCT').attr('disabled', false); // ko can check lun
            $('#btnDeleteAll').attr('disabled', false);
            $('#btnImportExcell').attr('disabled', false);

            // gang' commentText khi lick tamung
            createController.GetCommentText_By_TamUng(tamUngId, soTienNT, loaiPhieu);

            // Check_KetChuyenBtnStatus
            createController.Check_KetChuyenBtnStatus(tamUngId, soTienNT, loaiPhieu);

            // gang' ds TT141 theo tamung
            createController.GetTT621s_By_TamUng(tamUngId);

            // an nut btnCapNhatCT
            $('#btnCapNhatCT').attr('disabled', true);

            // Check_ThuHoanUngBtnStatus
            createController.Check_ThuHoanUngBtnStatus(tamUngId);
        });
        // giu trang thai CT TT va lay tamungid (GetTT621s_By_TamUng)

        // btnThemMoiCT
        $('#btnThemMoiCT').off('click').on('click', function () {
            //tamUngId = $('#hidTamUngId').val();
            //if (tamUngId == null || tamUngId == '') {
            //    alert('chưa chọn tạm ứng nào để thanh toán');
            //}
            //id_Dong_Da_Click = $('#hidIdCu').val();
            //kVCTPCTId_PhieuTC = $('#hidKVCTPCTId').val(); // dung de: GetDummyTT621_By_KVCTPCT. TT621Create view
            //var url = '/TT621s/ThemMoiCT_TT_Partial';

            //$.get(url, { tamUngId: tamUngId, kVCTPCTId_PhieuTC: kVCTPCTId_PhieuTC, id_Dong_Da_Click: id_Dong_Da_Click }, function (data) {
            //    $('.ThemMoiCT_TT_Body').html(data);
            //    $('#ThemMoiCT_TT_Modal').modal('show');
            //    $('#ThemMoiCT_TT_Modal').draggable();
            //})

            createController.ThemMoiCT_TT_Partial();
        })
        // btnThemMoiCT

        // btnCapNhatCT
        $('#btnCapNhatCT').off('click').on('click', function () {
            kVCTPCTId_PhieuTC = $('#hidKVCTPCTId').val();
            tt621Id = $('#hidTT621Id').val();

            createController.CapNhatCT_TT_Partial(tt621Id, kVCTPCTId_PhieuTC);
        })
        // btnCapNhatCT
        //// btnDelete
        //$('#btnDelete').off('click').on('click', function () {
        //    $('#btnThemMoiCT').attr('disabled', true);
        //    $('#btnCapNhatCT').attr('disabled', true);

        //    kVCTPTCId_PhieuTC = $('#hidKVCTPCTId').val();
        //    tt621Id = $('#hidTT621Id').val();
        //    tamUngId = $('#hidTamUngId').val();
        //    soTienNT = $('#txtSoTienNT_Create').val(); // TT621Create_View
        //    loaiPhieu = $('#hidLoaiPhieu').val();

        //    bootbox.confirm("Bạn có muốn <b> xoá </b> không?", function (result) {
        //        if (result) {
        //            $.post('/TT621s/Delete', { tt621Id: tt621Id, kVCTPTCId_PhieuTC: kVCTPTCId_PhieuTC }, function (response) {
        //                //console.log(response);
        //                if (response.status) {
        //                    toastr.success('Xoá thành công', 'Xoá!');

        //                    createController.GetTT621s_By_TamUng(tamUngId);
        //                    createController.GetCommentText_By_TamUng(tamUngId, soTienNT, loaiPhieu);
        //                    $('#btnDelete').attr('disabled', true); // disabled btnDelete
        //                    $('#btnKetChuyen').attr('disabled', true) // disabled kechuyen

        //                    // Check_ThuHoanUngBtnStatus
        //                    createController.Check_ThuHoanUngBtnStatus(tamUngId);

        //                    if (response.tT621sCount === '')
        //                        createController.Enabled_TU_Khong_TT();
        //                }
        //                else {
        //                    toastr.error(response.message, 'Xoá thanh toán!')
        //                }
        //            });
        //        }
        //    });
        //})
        //// btnDelete
        // btnDeleteAll
        $('#btnDeleteAll').off('click').on('click', function () {
            $('#btnThemMoiCT').attr('disabled', true);
            $('#btnCapNhatCT').attr('disabled', true);

            kVCTPTCId_PhieuTC = $('#hidKVCTPCTId').val();
            tt621Id = $('#hidTT621Id').val();
            tamUngId = $('#hidTamUngId').val();
            if (tamUngId === '') {
                alert('Bạn chưa chọn TU!');
            }
            soTienNT = $('#txtSoTienNT_Create').val(); // TT621Create_View
            loaiPhieu = $('#hidLoaiPhieu').val();

            bootbox.confirm("Bạn có muốn <strong> xoá tất cả TT</strong> không?", function (result) {
                if (result) {
                    $.post('/TT621s/btnDeleteAll', { tamUngId: tamUngId, kVCTPTCId_PhieuTC: kVCTPTCId_PhieuTC }, function (response) {
                        //console.log(response);
                        if (response.status) {
                            toastr.success('Xoá thành công', 'Xoá!');

                            createController.GetTT621s_By_TamUng(tamUngId);
                            createController.GetCommentText_By_TamUng(tamUngId, soTienNT, loaiPhieu);
                            $('#btnDeleteAll').attr('disabled', true); // disabled btnDeleteAll
                            $('#btnKetChuyen').attr('disabled', true) // disabled kechuyen

                            // Check_ThuHoanUngBtnStatus
                            createController.Check_ThuHoanUngBtnStatus(tamUngId);

                            if (response.tT621sCount === '')
                                createController.Enabled_TU_Khong_TT();
                        }
                        else {
                            toastr.error(response.message, 'Xoá thanh toán!')
                        }
                    });
                }
            });
        })
        // btnDeleteAll

        // btnKetChuyen
        $('#btnKetChuyen').off('click').on('click', function () {
            tamUngId = $('#hidTamUngId').val();
            soTienNT = $('#txtSoTienNT_Create').val(); // TT621Create_View
            kVCTPCTId_PhieuTC = $('#hidKVCTPCTId').val(); // TT621Create_View

            bootbox.confirm("Bạn có muốn <b> kết chuyển </b> không?", function (result) {
                if (result) {
                    $.post('/TT621s/KetChuyen', { tamUngId: tamUngId, soTienNT_PhieuTC: soTienNT, kVCTPCTId_PhieuTC: kVCTPCTId_PhieuTC }, function (status) {
                        if (status) {
                            location.reload(); // reload lai trang
                            toastr.success('Kết chuyển thành công', 'Kết chuyển!');
                        }
                        else {
                            toastr.error('Kết chuyển thất bại', 'Kết chuyển!');
                        }
                    })
                }
            });
        })
    },

    ThemMoiCT_TT_Partial: function (dienGiaiP, hTTC, soTienNT) {
        tamUngId = $('#hidTamUngId').val();
        if (tamUngId == null || tamUngId == '') {
            alert('chưa chọn tạm ứng nào để thanh toán');
        }
        id_Dong_Da_Click = $('#hidIdCu').val();
        kVCTPCTId_PhieuTC = $('#hidKVCTPCTId').val(); // dung de: GetDummyTT621_By_KVCTPCT. TT621Create view
        var url = '/TT621s/ThemMoiCT_TT_Partial';

        $.get(url, { tamUngId: tamUngId, kVCTPCTId_PhieuTC: kVCTPCTId_PhieuTC, id_Dong_Da_Click: id_Dong_Da_Click, dienGiaiP: dienGiaiP, hTTC: hTTC, soTienNT: soTienNT }, function (data) {
            $('.ThemMoiCT_TT_Body').html(data);
            $('#ThemMoiCT_TT_Modal').modal('show');
            $('#ThemMoiCT_TT_Modal').draggable();
        })
    },

    //ThemMoiCT_TT_ContextMenu_Partial: function () {
    //    debugger
    //    var dienGiaiP = $('#txtDienGiaiP').val();
    //    var hTTC = $('#ddlHttc').val();
    //    var soTienNT = $('#txtSoTienNT').val();

    //    var loaiTien = '@Model.KVPTC.NgoaiTe';

    //    if (loaiTien === 'VN') {
    //        tamUngId = $('#hidTamUngId').val();
    //        if (tamUngId == null || tamUngId == '') {
    //            alert('chưa chọn tạm ứng nào để thanh toán');
    //        }
    //        id_Dong_Da_Click = $('#hidIdCu').val();
    //        kVCTPCTId_PhieuTC = $('#hidKVCTPCTId').val(); // dung de: GetDummyTT621_By_KVCTPCT. TT621Create view
    //        var url = '/TT621s/ThemMoiCT_TT_ContextMenu_Partial';

    //        $.get(url, { tamUngId: tamUngId, kVCTPCTId_PhieuTC: kVCTPCTId_PhieuTC, id_Dong_Da_Click: id_Dong_Da_Click, dienGiaiP: dienGiaiP, hTTC: hTTC, soTienNT: soTienNT }, function (data) {
    //            $('.ThemMoiCT_TT_ContextMenu_Body').html(data);
    //            $('#ThemMoiCT_TT_ContextMenu_Modal').modal('show');
    //            $('#ThemMoiCT_TT_ContextMenu_Modal').draggable();
    //        })
    //    }
    //    else {
    //        toastr.warning("Hãy chọn phiếu TT VN.");
    //        return;
    //    }

    //},

    GetTT621s_By_TamUng: function (tamUngId) {
        $.ajax({
            url: '/TT621s/GetTT621s_By_TamUng',
            type: 'GET',
            data: {
                tamUngId: tamUngId
            },
            dataType: 'json',
            success: function (response) {
                //console.log(response);
                var data = response.data;
                var html = '';
                var template = $('#data-template').html();

                if (response.status) {
                    $.each(data, function (i, item) {
                        html += Mustache.render(template, {
                            Id: item.id,
                            SoCT: item.soCT,
                            DienGiai: item.dienGiai,
                            DienGiaiP: item.dienGiaiP,
                            SoTienNT: numeral(item.soTienNT).format('0,0'),
                            LoaiTien: item.loaiTien,
                            TyGia: numeral(item.tyGia).format('0,0.00'),
                            SoTien: numeral(item.soTien).format('0,0.00'),
                            TKNo: item.tkNo,
                            MaKhNo: item.maKhNo,
                            TKCo: item.tkCo,
                            MaKhCo: item.maKhCo,
                            Sgtcode: item.sgtcode
                            //PhieuTC: item.phieuTC
                        });
                    });

                    $('#ctThanhToanBody').html(html);

                    // giu trang thai CT TT va gang' TT621 id
                    $('.trTT621').off('click').on('click', function () {
                        if ($(this).hasClass("hoverClass"))
                            $(this).removeClass("hoverClass");
                        else {
                            $('.trTT621').removeClass("hoverClass");
                            $(this).addClass("hoverClass");
                        }
                        tt621Id = $(this).data('id');
                        $('#hidTT621Id').val(tt621Id); // moi lan click tt621 tr se gang' id len hidTT621Id
                        $('#hidIdCu').val(tt621Id); // moi lan click tt621 tr se gang' id len hidIdCu

                        phieuTC = $(this).data('phieutc');                             // trong tt621 tbl
                        var soCT_TT621CreateView = $('#kVPCTId_TT621CreateView').val();// trong TT621CreateView

                        if (phieuTC.includes("T") && soCT_TT621CreateView.includes("T")) { // cung phieu T cho capnhat
                            $('#btnCapNhatCT').attr('disabled', false);
                        }
                        if (phieuTC.includes("C") && soCT_TT621CreateView.includes("C")) { // cung phieu C cho capnhat
                            $('#btnCapNhatCT').attr('disabled', false);
                        }
                        //if ((!phieuTC.includes("T") && soCT_TT621CreateView.includes("T")) ||
                        //    (phieuTC.includes("T") && !soCT_TT621CreateView.includes("T"))) { // khac phieu => ko cho capnhat
                        //    $('#btnCapNhatCT').attr('disabled', true);
                        //}
                        $('#btnCapNhatCT').attr('disabled', false);
                        $('#btnDelete').attr('disabled', false);
                    })
                }
                else {
                    $('#ctThanhToanBody').html('');
                }
            }
        });
    },

    Disabled_TU_Khong_TT: function (tamUngId) {
        var idList = [];
        $.each($('.tamUngTr'), function (i, item) {
            var id = $(item).data('id');

            if (parseFloat(tamUngId) !== parseFloat(id)) {
                //idList.push({
                //    Id: tamUngId_item
                //});
                //console.log(tamUngId_item);
                //console.log('#' + tamUngId_item);
                $('#' + id).css({
                    'pointer-events': 'none',
                    'background-color': 'grey'
                });
            }
            //else {
            //    $('#' + id).removeProp({
            //        'pointer-events': 'none',
            //        'background-color': 'grey'
            //    });
            //}
        });
    },
    Enabled_TU_Khong_TT: function () {
        //var idList = [];
        //$.each($('.tamUngTr'), function (i, item) {
        //    var id = $(item).data('id');

        //    $('#' + id).removeProp("pointer-events");
        //    $('#' + id).removeProp("background-color");

        //});

        //$(".tamUngTr").removeClass().removeAttr('style');
    },
    Check_KetChuyenBtnStatus: function (tamUngId, soTienNT, loaiPhieu) {
        $.post('/TT621s/Check_KetChuyenBtnStatus', { tamUngId: tamUngId, soTienNT_Tren_TT621Create: soTienNT, loaiPhieu: loaiPhieu }, function (status) {
            $('#btnKetChuyen').attr('disabled', status)
        })
    },

    GetCommentText_By_TamUng: function (tamUngId, soTienNT, loaiPhieu) {
        $.get('/TT621s/GetCommentText_By_TamUng', { tamUngId: tamUngId, soTienNT: soTienNT, loaiPhieu: loaiPhieu }, function (response) {
            $('#txtCommentText').val(response);
        })
    },

    Check_SoTienNTCanKetChuyen_For_BtnThemMoiCTTT_Status: function (tamUngId, soTienNT) {
        $.post('/TT621s/Check_BtnThemMoiCTTT_Status', { tamUngId: tamUngId, soTienNT_Tren_TT621Create: soTienNT }, function (status) {
            $('#btnThemMoiCT').attr('disabled', status);
        })
    },

    Gang_SoTienNT_CanKetChuyen: function (tamUngId, soTienNT, loaiPhieu) {
        $.get('/TT621s/Gang_SoTienNT_CanKetChuyen', { tamUngId: tamUngId, soTienNT_Tren_TT621Create: soTienNT, loaiPhieu: loaiPhieu }, function (soTien) {
            $('#hidSoTienNT_CanKetChuyen').val(soTien);
        })
    },

    CapNhatCT_TT_Partial: function (tt621Id, kVCTPCTId_PhieuTC) {
        var url = '/TT621s/CapNhatCT_TT_Partial';

        $.get(url, { tt621Id: tt621Id, kVCTPCTId_PhieuTC: kVCTPCTId_PhieuTC }, function (data) {
            $('.CapNhatCT_TT_Body').html(data);
            $('#CapNhatCT_TT_Modal').modal('show');
            $('#CapNhatCT_TT_Modal').draggable();
        })
    },
    KhachHang_By_Code: function (code, txtMaKh) {
        $.ajax({
            url: '/KVCTPTCs/GetKhachHangs_By_Code',
            type: 'GET',
            data: { code: code },
            dataType: 'json',
            success: function (response) {
                if (response.status) {
                    var data = response.data;
                    // console.log(data);

                    if (txtMaKh === 'txtMaKhNo') { // search of no
                        $('#txtMaKhNo_ThemMoi').val(data.code);
                        $('#txtTenKhNo_ThemMoi').val(data.tenThuongMai);
                    }
                    if (txtMaKh === 'txtMaKhCo') { // search of co
                        $('#txtMaKhCo_ThemMoi').val(data.code);
                        $('#txtTenKhCo_ThemMoi').val(data.tenThuongMai);
                    }

                    $('#txtKyHieu').val(data.kyHieuHd);
                    $('#txtMauSoHD').val(data.mauSoHd);
                    $('#txtMsThue').val(data.maSoThue);
                    $('#txtTenKH').val(data.tenThuongMai);
                    $('#txtDiaChi').val(data.diaChi);
                }
            }
        });
    },
    KhachHang_By_Code_CapNhat: function (code, txtMaKh) {
        $.ajax({
            url: '/KVCTPTCs/GetKhachHangs_By_Code',
            type: 'GET',
            data: { code: code },
            dataType: 'json',
            success: function (response) {
                if (response.status) {
                    var data = response.data;
                    // console.log(data);

                    if (txtMaKh === 'txtMaKhNo') { // search of no
                        $('#txtMaKhNo_CapNhat').val(data.code);
                        $('#txtTenKhNo').val(data.tenThuongMai);
                    }
                    if (txtMaKh === 'txtMaKhCo') { // search of co
                        $('#txtMaKhCo_CapNhat').val(data.code);
                        $('#txtTenKhCo_CapNhat').val(data.tenThuongMai);
                    }

                    $('#txtKyHieu').val(data.kyHieuHd);
                    $('#txtMauSoHD').val(data.mauSoHd);
                    $('#txtMsThue').val(data.maSoThue);
                    $('#txtTenKH').val(data.tenThuongMai);
                    $('#txtDiaChi').val(data.diaChi);
                }
            }
        });
    },

    Check_ThuHoanUngBtnStatus: function (tamUngId) {
        $.post('/TT621s/Check_ThuHoanUngBtnStatus', { tamUngId: tamUngId }, function (status) {
            $('#btnThuHoanUng').attr('disabled', status)
        })
    }
};
createController.init();