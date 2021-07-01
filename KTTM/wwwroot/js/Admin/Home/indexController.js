function addCommas(x) {
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

        // phieu click --> load kvctpct
        $('tr .tdVal').click(function () {

            soCT = $(this).data('id');
            loaiPhieu = $(this).data('loaiphieu');

            indexController.TdVal_Click(soCT, loaiPhieu);

        });
        // phieu click --> load kvctpct

        // show cashier modal
        $('.btnCashier').off('click').on('click', function () {

            var page = $('.active span').text();

            soCT = $('#hidCashier').val();
            strUrl = $('.layDataCashier').data('url');
            $.get('/KVCTPCTs/LayDataCashierPartial', { id: soCT, strUrl: strUrl, page: page }, function (data) {

                $('#layDataCashier').modal('show');
                $('.layDataCashier_Body').html(data);
                $('#layDataCashier').draggable();
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

            var url = '/KVCTPCTs/KVCTPCT_Create_Partial';
            $.get(url, { kvpctid: kvpctid }, function (response) {

                $('#KVCTPCT_Create_Partial').show(500);
                $('#KVCTPCT_Create_Partial').html(response);

            });
        });
        // create new KVCTPCT


    },
    Load_KVCTPCTPartial: function (soCT, page) {

        var url = '/KVCTPCTs/KVCTPCTPartial';
        $.get(url, { soCT: soCT, page: page }, function (response) {

            $('#KVCTPCT_Tbl').html(response);
            $('#KVCTPCT_Tbl').show(500);

        });

    },
    TdVal_Click: function (soCT, loaiPhieu) {

        // gang' soCT cho btnCashier
        $('.btnCashier').attr('disabled', false);
        $('#hidCashier').val(soCT);

        // gang' soCT cho btnThemDong
        $('#btnThemDong').attr('disabled', false);
        $('#hidThemDongSoCT').val(soCT);
        $('#hidThemDongSoCT_ContextMenu').val(soCT);

        // page
        var page = $('.active span').text();
        // $('#hidPage').val(page);

        // gang' loaiphieu
        $('#hidLoaiPhieu').val(loaiPhieu);
        //$('#hidLoaiPhieuFull').text(loaiPhieu);

        $('#KVCTPCT_Create_Partial').hide(500);
        $('#KVCTPCT_Edit_Partial').hide(500);

        indexController.Load_KVCTPCTPartial(soCT, page);

    }
    //Load_CTInvoice_CTVAT_Partial: function (invoiceId) {

    //    var url = '/Invoices/CTInvoicesCTVATsInInvoicePartial';
    //    $.get(url, { invoiceId: invoiceId }, function (response) {

    //        $('.cTInVoiceCTVAT').html(response);
    //        $('.cTInVoiceCTVAT').show(5000);

    //    });
    //}
    //,
    //Load_BienNhan_CTBN_Partial: function (tourId) {

    //    var url = '/Tours/BienNhanAndCTBNPartial';
    //    $.get(url, { tourId: tourId }, function (response) {

    //        $('#BienNhanAndCTBNPartial').html(response);
    //        $('#BienNhanAndCTBNPartial').show(500);

    //    });

    //},
    //Load_CTBienNhanInBienNhanPartial: function (bienNhanId) {

    //    var url = '/BienNhans/CTBienNhanInBienNhanPartial';
    //    $.get(url, { bienNhanId: bienNhanId }, function (response) {

    //        $('#CTBienNhanInBienNhanPartial').html(response);
    //        $('#CTBienNhanInBienNhanPartial').show(500);
    //    });
    //},
    //Load_DSKhachHang: function (tourid) {

    //    $('#khachHangCreatePartial').hide(500);
    //    $('#khachHangEditPartial').hide(500);

    //    var url = '/DSKhachHangs/DSKhachHangPartial';
    //    $.get(url, { tourid: tourid }, function (response) {

    //        $('#sDSKhach').html(response);

    //        $('#sDSKhach').show(500);

    //    });
    //}

};
indexController.init();