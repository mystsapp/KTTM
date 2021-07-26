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

        $('#btnTimPhieu').off('click').on('click', function () {

            $('#timPhieuModal').modal('show');
            $('#timPhieuModal').draggable();

        })

        // phieu click --> load kvctpct
        $('tr .tdVal').click(function () {
            $('#btnTT141').attr('disabled', true); // whien click onother row ==> off btnTT141
            $('#btnTamUng').attr('disabled', true); // whien click onother row ==> off btnTamUng

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
        $('#hidKVPCTId').val(soCT);

        // page
        var page = $('.active span').text();
        // $('#hidPage').val(page);

        // gang' loaiphieu
        $('#hidLoaiPhieu').val(loaiPhieu);
        //$('#hidLoaiPhieuFull').text(loaiPhieu);

        $('#KVCTPCT_Create_Partial').hide(500);
        $('#KVCTPCT_Edit_Partial').hide(500);

        indexController.Load_KVCTPCTPartial(soCT, page);

    },
    KhachHang_By_Code: function (code) {

        $.ajax({
            url: '/KVCTPCTs/GetKhachHangs_By_Code',
            type: 'GET',
            data: { code: code },
            dataType: 'json',
            success: function (response) {
                if (response.status) {
                    var data = response.data;
                    // console.log(data);

                    if ($('#btnKhSearch').data('name') === 'maKhNo') { // search of no
                        $('#txtMaKhNo').val(data.code);
                        $('#txtTenKhNo').val(data.name);
                    }
                    if ($('#btnKhSearch').data('name') === 'maKhCo') { // search of co
                        $('#txtMaKhCo').val(data.code);
                        $('#txtTenKhCo').val(data.name);
                    }

                    $('#txtKyHieu').val(data.taxSign);
                    $('#txtMauSoHD').val(data.taxForm);
                    $('#txtMsThue').val(data.taxCode);
                    $('#txtTenKH').val(data.name);
                    $('#txtDiaChi').val(data.address);

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