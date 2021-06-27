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

        //$.each($('.cursor-pointer'), function (i, item) {

        //    var huy = $(item).data('huy');
        //    //console.log(huy);
        //    if (huy === 'True') {
        //        $(this).addClass('bg-secondary');
        //    }

        //});


        //$.when(indexController.checkHuy(id)).done(function (response) {
        //    if (response.status === true) { // check huy
        //        console.log(response);
        //        $('.cursor-pointer').addClass('bg-secondary');
        //    }
        //})
        //var invoicesCount = indexController.checkInvoices(id);
        //if (invoicesCount > 0) {
        //    $('#btnHuy').prop('disabled', true);
        //}
        //});

        //$('.btnKhoiPhucTour').off('click').on('click', function () {
        //    //return $.ajax({
        //    //    url: '/Tours/HuyTourPartial',
        //    //    data: {
        //    //        id: $(this).data('id')
        //    //    },
        //    //    dataType: 'json',
        //    //    type: 'GET',
        //    //    success: function (response) {
        //    //        console.log(response);
        //    //        //if (response.status) {
        //    //        //    console.log(response.toursCount);
        //    //        //    return response.toursCount;
        //    //        //}

        //    //        //else
        //    //        //    return 10;
        //    //    }
        //    //});
        //    id = $(this).data('id');
        //    bootbox.confirm({
        //        title: "Restore Confirm?",
        //        message: "Bạn có muốn <b> khôi phục </b> Tour này không?",
        //        buttons: {
        //            cancel: {
        //                label: '<i class="fa fa-times"></i> Cancel'
        //            },
        //            confirm: {
        //                label: '<i class="fa fa-check"></i> Confirm'
        //            }

        //        },
        //        callback: function (result) {
        //            if (result) {
        //                $('#hidTourId').val(id);
        //                $('#frmKhoiPhucTour').submit();
        //            }
        //        }

        //    });
        //});

        // phieu click --> load kvctpct
        $('tr .tdVal').click(function () {

            soCT = $(this).data('id');
            loaiPhieu = $(this).data('loaiphieu');
            
            indexController.TdVal_Click(soCT, loaiPhieu);

            //// gang' soCT cho btnCashier
            //$('.btnCashier').attr('disabled', false);
            ////$('.btnCashier').attr('data-id', soCT);
            //$('#hidCashier').val(soCT);

            //// gang' soCT cho btnThemDong
            //$('#btnThemDong').attr('disabled', false);
            ////$('#btnThemDong').attr('data-id', soCT);
            //$('#hidThemDong').val(soCT);

            //$('#KVCTPCT_Create_Partial').hide(500);
            //$('#KVCTPCT_Edit_Partial').hide(500);

            //indexController.Load_KVCTPCTPartial(soCT);

        });
        // phieu click --> load kvctpct

        // show cashier modal
        $('.btnCashier').off('click').on('click', function () {
            var a = 1;
            var page = $('.active span').text();

            soCT = $('#hidThemDong').val();
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

            var page = $('.active span').text();

            soCT = $('#hidThemDong').val();
            strUrl = $(this).data('url');
            $('#KVCTPCT_Tbl').hide(500);
            $('#KVCTPCT_Edit_Partial').hide(500);

            var url = '/KVCTPCTs/KVCTPCT_Modal_Full_Partial';
            $.get(url, { soCT: soCT, page: page }, function (data) {

                $('#ThemDongModal_Full').modal('show');
                $('.ThemDongModal_Full_Body').html(data);
                $('#ThemDongModal_Full').draggable();
            });

        });
        // contextmenu for themdong

        // themdong click
        $('#btnThemDong').click(function () {

            var page = $('.active span').text();

            soCT = $('#hidThemDong').val();
            strUrl = $(this).data('url');

            var url = '/KVCTPCTs/KVCTPCT_Modal_Partial';
            $.get(url, { soCT: soCT, strUrl: strUrl, page: page }, function (data) {

                $('#ThemDongModal').modal('show');
                $('.ThemDongModal_Body').html(data);
                $('#ThemDongModal').draggable();
            });

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

        //// invoice click --> CTInvoices & CTVAT
        //$('.tdInvoiceVal').click(function () {

        //    invoiceId = $(this).data('id');
        //    indexController.Load_CTInvoice_CTVAT_Partial(invoiceId);

        //    //var url = '/Invoices/CTInvoicesCTVATsInInvoicePartial';
        //    //$.get(url, { invoiceId: invoiceId }, function (response) {

        //    //    $('.cTInVoiceCTVAT').html(response);

        //    //});
        //});
        //// invoice click --> CTInvoices & CTVAT

        //////////////////////////////////////////////////////////////////////////////// CreateKhachPartial

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

        //// close create partial
        //$('#btnCloseKhachCreatePartial').off('click').on('click', function () {
        //    $('#sDSKhach').show(500);
        //});
        //$('#btnBackKhachHangCreatePartial').off('click').on('click', function () {
        //    $('#khachHangCreatePartial').hide(500);
        //    $('#sDSKhach').show(500);
        //});
        //// close create invoice partial

        //////////////////////////////////////////////////////////////////////////////// CreateKhachPartial

        //////////////////////////////////////////////////////////////////////////////// EditKhachPartial

        //$('.btnEditKhachHang').off('click').on('click', function (e) {
        //    e.preventDefault();

        //    idKhachTour = $(this).data('id');

        //    $('#sDSKhach').hide(500);
        //    $('#khachHangCreatePartial').hide(500);
        //    //$('#createInvoicePartial').hide(500);

        //    var url = '/DSKhachHangs/KhachHangEditPartial';
        //    $.get(url, { id: idKhachTour }, function (response) {

        //        $('#khachHangEditPartial').show(500);

        //        $('#khachHangEditPartial').html(response);

        //    });
        //});

        //// close create partial
        //$('#btnCloseKhachEditPartial').off('click').on('click', function () {
        //    $('#sDSKhach').show(500);
        //});
        //$('#btnBackKhachHangEditPartial').off('click').on('click', function () {
        //    $('#khachHangEditPartial').hide(500);
        //    $('#sDSKhach').show(500);
        //});
        //// close create invoice partial

        //////////////////////////////////////////////////////////////////////////////// EditKhachPartial

        //////////////////////////////////////////////////////////////////////////////// XoaKhachPartial

        //$('.btnXoaKhachHangPartial').off('click').on('click', function () {

        //    id = $(this).data('id');

        //    $.ajax({
        //        url: '/DSKhachHangs/Delete',
        //        data: {
        //            id: id
        //        },
        //        dataType: 'json',
        //        type: 'POST',
        //        success: function (response) {
        //            if (response.status) {

        //                toastr.success('Xóa thành công!'); // toastr in admin/tour/indexController.js

        //                tourid = response.tourid;

        //                indexController.Load_DSKhachHang(tourid);
        //            }
        //            else {
        //                toastr.error(response.message);

        //            }
        //        }
        //    });

        //});

        //////////////////////////////////////////////////////////////////////////////// Xoa1KhachPartial

        //////////////////////////////////////////////////////////////////////////////// CreateInvoicePartial finish post

        //// create new invoice
        //$('#btnNewInvoice').off('click').on('click', function () {

        //    tourid = $(this).data('tourid');

        //    $('#tabs_KeToan_TourInfo').hide(500);

        //    var url = '/Invoices/CreateInvoicePartial';
        //    $.get(url, { tourid: tourid }, function (response) {

        //        $('#createInvoicePartial').show(500);

        //        $('#createInvoicePartial').html(response);

        //    });
        //});
        //// create new invoice
        //// close crete invoice partial
        //$('#btnCloseCreateInvoicePartial').off('click').on('click', function () {
        //    $('#tabs_KeToan_TourInfo').show(500);
        //});
        //$('#btnBackCreateInvoicePartial').off('click').on('click', function () {
        //    $('#createInvoicePartial').hide(500);
        //    $('#tabs_KeToan_TourInfo').show(500);
        //});
        //// close crete invoice partial

        //$('#btnCreateInvoicePartial').off('click').on('click', function () {

        //    // if frm valid
        //    if ($('#frmInvoiceCreatePartial').valid()) {
        //        var invoice = $('#frmInvoiceCreatePartial').serialize();
        //        $.ajax({
        //            type: "POST",
        //            url: "/Invoices/CreateInvoicePartial",
        //            data: invoice,
        //            dataType: "json",
        //            success: function (response) {
        //                if (response.status) {

        //                    toastr.success('Thêm mới invoice thành công!');

        //                    $('#createInvoicePartial').hide();

        //                    $('#tabs_KeToan_TourInfo').show();
        //                    tourId = tourIdInCreateInvoicePartial; // receive it from CreateInvoicePartial
        //                    indexController.Load_KeToan_TourInfoByTourPartial(tourId);

        //                }
        //                else {
        //                    toastr.error('Thêm mới invoice không thành công!');
        //                    //  debugger
        //                    // $('#createInvoiceModal').show();
        //                    // $('.createInvoicePartial').html(response);
        //                    //$('#createInvoiceModal').draggable();

        //                    //tourid = $(this).data('tourid');
        //                    //var url = '/Invoices/CreateInvoicePartial';
        //                    //$.get(url, { tourid: tourid }, function (response) {

        //                    //    $('#createInvoiceModal').show();
        //                    //    $('.createInvoicePartial').html(response);
        //                    //    $('#createInvoiceModal').draggable();

        //                    //});

        //                }
        //            }
        //        });
        //    }
        //});

        ////////////////////////////////////////////////////////////////////////////////// CreateInvoicePartial finish post

        ////////////////////////////////////////////////////////////////////////////////// EditInvoicePartial finish post

        ////// edit invoice

        //$('.btnEditInvoice').on('click', function () {

        //    tourid = $(this).data('tourid');
        //    invoiceId = $(this).data('invoiceid');

        //    $('#tabs_KeToan_TourInfo').hide(500);
        //    //$('#createInvoicePartial').hide(500);

        //    var url = '/Invoices/EditInvoicePartial';
        //    $.get(url, { tourid: tourid, invoiceId: invoiceId }, function (response) {

        //        $('#editInvoicePartial').show(500);

        //        $('#editInvoicePartial').html(response);

        //    });
        //});

        ////// edit invoice

        // back editinvoicepartial

        // back editinvoicepartial
        // --> btn submit edit invoice in its partial

        //// del invoice ( huy invoice)

        //$('.btnHuyInvoice').off('click').on('click', function () {

        //    id = $(this).data('id');
        //    strUrl = $(this).data('url');

        //    $.get('/Invoices/HuyInvoicePartial', { id: id, strUrl: strUrl }, function (response) {


        //        $('#huyInvoiceModal').modal('show');
        //        $('.huyInvoicePartial').html(response);
        //        $('#huyInvoiceModal').draggable();
        //    });
        //});
        //// btnHuyInvoicePartialSubmit in its partial

        // del invoice ( huy invoice)

        //////////////////////////////////////////////////////////////////////////////// EditInvoicePartial finish post

        //// BackCreateCTInvoicePartial
        //$('#btnBackCreateCTInvoicePartial').off('click').on('click', function () {

        //    $('#createCTInvoicePartial').hide(500);

        //    $('#tabs_KeToan_TourInfo').show(500);
        //});
        //// BackCreateCTInvoicePartial
        //////////////////////////////////////////////////////////////////////////////// CTInvoicesCTVATsInInvoicePartial


        //////////////////////////////////////////////////////////////////////////////// editCTInvoicePartial

    },
    Load_KVCTPCTPartial: function (soCT, page) {

        var url = '/KVCTPCTs/KVCTPCTPartial';
        $.get(url, { soCT: soCT, page: page }, function (response) {

            $('#KVCTPCT_Tbl').html(response);
            $('#KVCTPCT_Tbl').show(500);

        });

    },
    TdVal_Click: function (soCT, loaiPhieu) {

        //soCT = $(this).data('id');

        // gang' soCT cho btnCashier
        $('.btnCashier').attr('disabled', false);
        $('#hidCashier').val(soCT);

        // gang' soCT cho btnThemDong
        $('#btnThemDong').attr('disabled', false);
        $('#hidThemDong').val(soCT);

        // page
        var page = $('.active span').text();
       // $('#hidPage').val(page);

        // gang' loaiphieu
        $('#hidLoaiPhieu').text(loaiPhieu);
        $('#hidLoaiPhieuFull').text(loaiPhieu);

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