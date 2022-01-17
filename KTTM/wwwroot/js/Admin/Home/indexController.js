﻿function addCommas(x) {
    var parts = x.toString().split(".");
    parts[0] = parts[0].replace(/\D/g, "").replace(/\B(?=(\d{3})+(?!\d))/g, ",");
    return parts.join(".");
}

var indexController = {
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

        indexController.registerEvent();
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

        $('#btnTimPhieu').off('click').on('click', function () {
            $('#timPhieuModal').modal('show');
            $('#timPhieuModal').draggable();
        })

        // phieu click --> load kvctpct
        $('tr .tdVal').click(function () {
            $('#btnTT141').attr('disabled', true); // whien click onother row ==> off btnTT141
            $('#btnTamUng').attr('disabled', true); // whien click onother row ==> off btnTamUng

            var userTao = $(this).data('usertao');
            var userLogon = $('#hidUserLogon').val();

            id = $(this).data('id'); // KVPTC id
            loaiPhieu = $(this).data('loaiphieu');

            indexController.TdVal_Click(id, loaiPhieu, userTao, userLogon);
        });
        // phieu click --> load kvctpct

        // show cashier modal
        $('.btnCashier').off('click').on('click', function () {
            var page = $('.active span').text();

            kvptcId = $('#hidKVPCTId').val();
            strUrl = $('.layDataCashier').data('url');
            $.get('/KVCTPTCs/LayDataCashierPartial', { kVPTCId: kvptcId, strUrl: strUrl, page: page }, function (data) {
                $('#layDataCashier').modal('show');
                $('.layDataCashier_Body').html(data);
                $('#layDataCashier').draggable();
            });
        });

        // show qlxe modal
        $('.btnQlXe').off('click').on('click', function () {
            var page = $('.active span').text();

            kvptcId = $('#hidKVPCTId').val();
            strUrl = $('.layDataCashier').data('url');
            $.get('/KVCTPTCs/LayDataQLXePartial', { kVPTCId: kvptcId, strUrl: strUrl, page: page }, function (data) {
                $('#layDataQLXe').modal('show');
                $('.layDataQLXe_Body').html(data);
                $('#layDataQLXe').draggable();
            });
        });

        // contextmenu for themdong
        $('#btnThemDong').contextmenu(function (e) {
            e.preventDefault();
            var loaiPhieu = $('#hidLoaiPhieu').val();
            if (loaiPhieu === 'C')
                $('#frmThemDong_ContextMenu').submit();
            else
                return;
        });
        // contextmenu for themdong

        // themdong click
        $('#btnThemDong').click(function () {
            $('#frmThemDong').submit();
        });
        // themdong click

        // giu trang thai phieu click
        $('#phieuTbl .cursor-pointer').off('click').on('click', function () {
            if ($(this).hasClass("hoverClass"))
                $(this).removeClass("hoverClass");
            else {
                $('.cursor-pointer').removeClass("hoverClass");
                $(this).addClass("hoverClass");
            }
        });
        // giu trang thai phieu click

        // tdVal_KVCTPCT row click
        $('tr .tdVal_KVCTPCT').click(function () {
            kVCTPCTId = $(this).data('id');
            $('#hidKVCTPCTId').val(kVCTPCTId); // for TT141 and 1411KhongTC

            // for btnTamUng
            var promise = indexController.CheckTamUng(kVCTPCTId);
            promise.then(function (data) {
                if (data) {
                    $('#btnTamUng').attr('disabled', false);
                    $('#hidTamUng').val(kVCTPCTId);
                }
                else {
                    $('#btnTamUng').attr('disabled', true);
                }
            }, error => alert(error));

            // for TT141
            var promiseTT141 = indexController.CheckTT141(kVCTPCTId);
            promiseTT141.then(function (data) {
                if (data) {
                    $('#btnTT141').attr('disabled', false);
                }
                else {
                    $('#btnTT141').attr('disabled', true);
                }
            }, error => alert(error));

            // for => copy dong cu cho dong moi
            $('#hidIdCu').val(kVCTPCTId);
        });
        // tdVal_KVCTPCT row click

        // giu trang thai CT phieu click
        $('#cTPhieuTbl .ctphieu-cursor-pointer').off('click').on('click', function () {
            if ($(this).hasClass("hoverClass"))
                $(this).removeClass("hoverClass");
            else {
                $('.ctphieu-cursor-pointer').removeClass("hoverClass");
                $(this).addClass("hoverClass");
            }
        });
        // giu trang thai CT phieu click

        // create new KVCTPCT
        $('#btn_New_KVCTPCT').off('click').on('click', function () {
            kvpctid = $(this).data('kvpctid');

            $('#KVCTPCT_Tbl').hide(500);
            $('#KVCTPCT_Edit_Partial').hide(500);

            var url = '/KVCTPTCs/KVCTPCT_Create_Partial';
            $.get(url, { kvpctid: kvpctid }, function (response) {
                $('#KVCTPCT_Create_Partial').show(500);
                $('#KVCTPCT_Create_Partial').html(response);
            });
        });
        // create new KVCTPCT

        // btnInCTPhieu
        $('#btnInCTPhieu').off('click').on('click', function () {
            $('#frmInCTPhieu').submit();
        })

        // btnInPhieu
        $('#btnInPhieu').off('click').on('click', function () {
            kvptcId = $('#hidId_InPhieu').val();
            $.post('/Home/CheckSoLuongDong', { id: kvptcId }, function (response) {
                if (!response) {
                    bootbox.alert("Phiếu này không có dòng nào hết!");
                    return;
                }
                else {
                    $.post('/Home/CheckInPhieu', { id: kvptcId }, function (response) {
                        if (!response) {
                            bootbox.alert("Còn tồn tại 1 CT Phiếu chưa TU hoặc TT TU!");
                        }
                        else {
                            $('#frmInPhieu').submit();
                        }
                    })
                }
            })
        })
    },
    Load_KVCTPCTPartial: function (id, page) { // KVPTC id
        var url = '/KVCTPTCs/KVCTPTCPartial';
        $.get(url, { KVPTCid: id, page: page }, function (response) {
            $('#KVCTPCT_Tbl').html(response);
            $('#KVCTPCT_Tbl').show(500);
        });
    },
    TdVal_Click: function (id, loaiPhieu, userTao, userLogon) { // KVPTC id
        // gang' soCT cho btnCashier
        $('.btnCashier').attr('disabled', false);

        // gang' soCT cho btnThemDong
        $('#btnThemDong').attr('disabled', false);
        $('#hidThemDongId').val(id);
        $('#hidThemDongId_ContextMenu').val(id);
        $('#hidKVPCTId').val(id);

        // gang' soCT cho btnInphieu
        $('#btnInPhieu').attr('disabled', false);
        $('#btnInCTPhieu').attr('disabled', false);
        $('#hidId_InPhieu').val(id);
        $('#hidId_InCTPhieu').val(id);

        // attachExcel
        if (loaiPhieu === 'C') {
            $('#attachExcel').attr('disabled', false);
            $('.btnQlXe').attr('disabled', false);
        }
        else {
            $('#attachExcel').attr('disabled', true);
            $('.btnQlXe').attr('disabled', true);
        }

        // page
        var page = $('.active span').text();
        // $('#hidPage').val(page);

        // gang' loaiphieu
        $('#hidLoaiPhieu').val(loaiPhieu);
        //$('#hidLoaiPhieuFull').text(loaiPhieu);

        $('#KVCTPCT_Create_Partial').hide(500);
        $('#KVCTPCT_Edit_Partial').hide(500);

        ///// (usertao !== userLogon)
        if (userTao !== userLogon) {
            $('#btnThemDong').attr('disabled', true);
            $('.btnQlXe').attr('disabled', true);
            $('.btnCashier').attr('disabled', true);
            $('#btnTamUng').attr('disabled', true);
            $('#attachExcel').attr('disabled', true);
            $('#btnTT141').attr('disabled', true);
        }

        indexController.Load_KVCTPCTPartial(id, page); // KVPTC id
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
                    console.log(data);

                    if (txtMaKh === 'txtMaKhNo') { // search of no
                        $('#txtMaKhNo').val(data.code);
                        $('#txtTenKhNo').val(data.tenThuongMai);
                    }
                    if (txtMaKh === 'txtMaKhCo') { // search of co
                        $('#txtMaKhCo').val(data.code);
                        $('#txtTenKhCo').val(data.tenThuongMai);
                    }

                    $('#txtKyHieu').val(data.kyHieuHd);
                    $('#txtMauSoHD').val(data.mauSoHd);
                    $('#txtMsThue').val(data.maSoThue);
                    $('#txtTenKH').val(data.tenThuongMai);
                    $('#txtDiaChi').val(data.diaChi);
                }
                else {// search ko co KH nao het => ...
                    if ($('#btnKhSearch').data('name') === 'maKhNo') { // search of no
                        $('#txtMaKhNo').val('');
                        $('#txtTenKhNo').val('');
                    }
                    if ($('#btnKhSearch').data('name') === 'maKhCo') { // search of co
                        $('#txtMaKhCo').val('');
                        $('#txtTenKhCo').val('');
                    }

                    $('#txtKyHieu').val('');
                    $('#txtMauSoHD').val('');
                    $('#txtMsThue').val('');
                    $('#txtTenKH').val('');
                    $('#txtDiaChi').val('');
                }
            }
        });
    },
    CheckTamUng: function (kVCTPCTId) {
        return $.post('/TamUngs/CheckTamUng', { kVCTPCTId: kVCTPCTId }, function (response) {
            // console.log(response);
            return response;
        })
    },
    CheckTT141: function (kVCTPCTId) {
        return $.post('/TamUngs/CheckTT141', { kVCTPCTId: kVCTPCTId }, function (response) {
            //console.log(response);
            return response;
        })
    }
};
indexController.init();