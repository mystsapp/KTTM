﻿////function addCommas(x) {
////    var parts = x.toString().split(".");
////    parts[0] = parts[0].replace(/\D/g, "").replace(/\B(?=(\d{3})+(?!\d))/g, ",");
////    return parts.join(".");
////}

var khongTCController = {
    init: function () {

        //toastr.options = { // toastr options
        //    "closeButton": false,
        //    "debug": false,
        //    "newestOnTop": false,
        //    "progressBar": true,
        //    "positionClass": "toast-top-right",
        //    "preventDuplicates": false,
        //    "onclick": null,
        //    "showDuration": "300",
        //    "hideDuration": "2000",
        //    "timeOut": "2000",
        //    "extendedTimeOut": "1000",
        //    "showEasing": "swing",
        //    "hideEasing": "linear",
        //    "showMethod": "fadeIn",
        //    "hideMethod": "fadeOut"
        //};

        khongTCController.registerEvent();
    },

    registerEvent: function () {

        //// format .numbers
        //$('input.numbers').keyup(function (event) {

        //    // Chỉ cho nhập số
        //    if (event.which >= 37 && event.which <= 40) return;

        //    $(this).val(function (index, value) {
        //        return addCommas(value);
        //    });
        //});

        // giu trang thai CT TT va lay tamungid (GetTT621s_By_TamUng)
        $('.tamUngTr').off('click').on('click', function () {

            if ($(this).hasClass("hoverClass"))
                $(this).removeClass("hoverClass");
            else {
                $('.tamUngTr').removeClass("hoverClass");
                $(this).addClass("hoverClass");
            }

            tamUngId = $(this).data('id');
            //soTienNT = $('#txtSoTienNT_Create').val(); // TT621Create_View
            //kVCTPCTId_PhieuTC = $('#hidKVCTPCTId').val();

            $('#hidTamUngId').val(tamUngId); // for TT141

            // check btnThemMoiCTTT status
            khongTCController.Check_SoTienNTCanKetChuyen_For_BtnThemMoiCTTT_Status(tamUngId, 0)//////////////////

            // gang' commentText khi lick tamung            
            khongTCController.GetCommentText_By_TamUng(tamUngId, 0);

            // Check_KetChuyenBtnStatus
            khongTCController.Check_KetChuyenBtnStatus(tamUngId, 0);

            // gang' ds TT141 theo tamung
            khongTCController.GetTT621s_By_TamUng(tamUngId);

            // an nut btnCapNhatCT
            $('#btnCapNhatCT').attr('disabled', true);

        });
        // giu trang thai CT TT va lay tamungid (GetTT621s_By_TamUng)

        // btnThemMoiCT
        $('#btnThemMoiCT').off('click').on('click', function () {

            tamUngId = $('#hidTamUngId').val();
           // kVCTPCTId_PhieuTC = $('#hidKVCTPCTId').val(); // dung de: GetDummyTT621_By_KVCTPCT. TT621Create view
            var url = '/TT621s/ThemMoiCT_TT_KhongTC_Partial';

            $.get(url, { tamUngId: tamUngId}, function (data) {
                
                $('#ThemMoiCT_TT_KhongTC_Modal').modal('show');
                $('.ThemMoiCT_TT_KhongTC_Body').html(data);
                $('#ThemMoiCT_TT_KhongTC_Modal').draggable();
            })

        })
        // btnThemMoiCT

        // btnCapNhatCT
        $('#btnCapNhatCT').off('click').on('click', function () {

            // kVCTPCTId_PhieuTC = $('#hidKVCTPCTId').val();
            tt621Id = $('#hidTT621Id').val();

            khongTCController.CapNhatCT_TT_Partial(tt621Id);
        })
        
        // btnDelete
        $('#btnDelete').off('click').on('click', function () {

            tt621Id = $('#hidTT621Id').val();
            tamUngId = $('#hidTamUngId').val();
           // soTienNT = $('#txtSoTienNT_Create').val(); // TT621Create_View

            bootbox.confirm("Bạn có muốn <b> xoá </b> không?", function (result) {
                if (result) {

                    $.post('/TT621s/Delete', { tt621Id: tt621Id }, function (response) {
                        //console.log(response);
                        if (response.status) {
                            toastr.success('Xoá thành công', 'Xoá!');

                            khongTCController.GetTT621s_By_TamUng(tamUngId);
                            khongTCController.GetCommentText_By_TamUng(tamUngId, 0);
                        }
                        else {
                            toastr.error(response.message, 'Thêm thanh toán!')
                        }
                    });
                }
            });

        })
        
        // btnKetChuyen
        $('#btnKetChuyen').off('click').on('click', function () {

            tamUngId = $('#hidTamUngId').val();
            //soTienNT = $('#txtSoTienNT_Create').val(); // TT621Create_View

            bootbox.confirm("Bạn có muốn <b> kết chuyển </b> không?", function (result) {
                if (result) {

                    $.post('/TT621s/KetChuyen', { tamUngId: tamUngId, soTienNT_PhieuTC: 0 }, function (status) {
                        if (status) {
                            toastr.success('Kết chuyển thành công', 'Kết chuyển!');
                            location.reload(); // reload lai trang

                        }
                        else {
                            toastr.error('Kết chuyển thất bại', 'Kết chuyển!');
                        }
                    })
                }
            });

        })
    },

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
                            DienGiai: item.dienGiai,
                            DienGiaiP: item.dienGiaiP,
                            SoTienNT: numeral(item.soTienNT).format('0,0'),
                            LoaiTien: item.loaiTien,
                            TyGia: numeral(item.tyGia).format('0,0'),
                            SoTien: numeral(item.soTien).format('0,0'),
                            TKNo: item.tkNo,
                            MaKhNo: item.maKhNo,
                            TKCo: item.tkCo,
                            MaKhCo: item.maKhCo,
                            PhieuTC: item.phieuTC
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

                        $('#btnCapNhatCT').attr('disabled', false); // chac chan' la phieu chi --> vi chi danh cho tamung thoi
                        $('#btnDelete').attr('disabled', false);

                    })
                }
                else {

                    $('#ctThanhToanBody').html('');

                }
            }
        });
    },

    Check_KetChuyenBtnStatus: function (tamUngId, soTienNT) {
        $.post('/TT621s/Check_KetChuyenBtnStatus', { tamUngId: tamUngId, soTienNT_Tren_TT621Create: soTienNT }, function (status) {
            
            $('#btnKetChuyen').attr('disabled', status)

        })
    },

    GetCommentText_By_TamUng: function (tamUngId, soTienNT) {
        $.get('/TT621s/GetCommentText_By_TamUng', { tamUngId: tamUngId, soTienNT: soTienNT }, function (response) {
            $('#txtCommentText').val(response);
        })

    },

    Check_SoTienNTCanKetChuyen_For_BtnThemMoiCTTT_Status: function (tamUngId, soTienNT) {
        $.post('/TT621s/Check_BtnThemMoiCTTT_Status', { tamUngId: tamUngId, soTienNT_Tren_TT621Create: soTienNT }, function (status) {

            $('#btnThemMoiCT').attr('disabled', status);

        })
    },

    //Gang_SoTienNT_CanKetChuyen: function (tamUngId, soTienNT) {
    //    $.get('/TT621s/Gang_SoTienNT_CanKetChuyen', { tamUngId: tamUngId, soTienNT_Tren_TT621Create: soTienNT }, function (soTien) {
    //        $('#hidSoTienNT_CanKetChuyen').val(soTien);
    //    })
    //},

    CapNhatCT_TT_Partial: function (tt621Id) {

        var url = '/TT621s/CapNhatCT_TT_KhongTC_Partial';

        $.get(url, { tt621Id: tt621Id }, function (data) {

            $('.CapNhatCT_TT_Body').html(data);
            $('#CapNhatCT_TT_Modal').modal('show');
            $('#CapNhatCT_TT_Modal').draggable();
        })
    },
    //KhachHang_By_Code: function (code, txtMaKh) {

    //    $.ajax({
    //        url: '/KVCTPCTs/GetKhachHangs_By_Code',
    //        type: 'GET',
    //        data: { code: code },
    //        dataType: 'json',
    //        success: function (response) {
    //            if (response.status) {
    //                var data = response.data;
    //                // console.log(data);

    //                if (txtMaKh === 'txtMaKhNo') { // search of no
    //                    $('#txtMaKhNo_ThemMoi').val(data.code);
    //                    $('#txtTenKhNo_ThemMoi').val(data.name);
    //                }
    //                if (txtMaKh === 'txtMaKhCo') { // search of co
    //                    $('#txtMaKhCo_ThemMoi').val(data.code);
    //                    $('#txtTenKhCo_ThemMoi').val(data.name);
    //                }

    //                $('#txtKyHieu').val(data.taxSign);
    //                $('#txtMauSoHD').val(data.taxForm);
    //                $('#txtMsThue').val(data.taxCode);
    //                $('#txtTenKH').val(data.name);
    //                $('#txtDiaChi').val(data.address);

    //            }

    //        }
    //    });

    //}

};
khongTCController.init();